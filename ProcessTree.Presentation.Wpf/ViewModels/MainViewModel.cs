﻿using ProcessTree.Domain;
using ProcessTree.DomainModels;
using ProcessTree.Utilities.Wpf.Attributes;
using ProcessTree.Utilities.Wpf.Commands;
using ProcessTree.Utilities.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProcessTree.Presentation.Wpf.ViewModels
{
    internal sealed class MainViewModel : ViewModel
    {
        private readonly ICollection<ProcessViewModel> processes = new ObservableCollection<ProcessViewModel>();
        private readonly IProcessManager processManager;
        private readonly Command refreshCommand;
        private readonly Command stopCommand;
        private readonly Command startCommand;

        private string newProcessName = string.Empty;
        private ProcessViewModel selectedItem;

        public MainViewModel(IProcessManager processManager)
        {
            this.processManager = processManager;

            processManager.ErrorErised += (sender, e) =>
                {
                    OnErrorErised(new ErrorEventArgs(e.ErrorMessage));
                };

            stopCommand = new DelegateCommand(StopProcess, () => CanCloseProcess);
            refreshCommand = new DelegateCommand(Refresh);
            startCommand = new DelegateCommand(StartProcess, () => CanStartProcess);

            Refresh();
        }

        [DependsUponProperty(nameof(SelectedItem))]
        public bool CanCloseProcess => SelectedItem != null;

        [DependsUponProperty(nameof(NewProcessName))]
        public bool CanStartProcess => newProcessName != string.Empty;

        public string NewProcessName
        {
            get => newProcessName;
            set => SetProperty(ref newProcessName, value);
        }

        public IEnumerable<ProcessViewModel> Processes => processes;

        public Command RefreshCommand => refreshCommand;

        [RaiseCanExecuteDependsUpon(nameof(CanStartProcess))]
        public Command StartCommand => startCommand;

        [RaiseCanExecuteDependsUpon(nameof(CanCloseProcess))]
        public Command StopCommand => stopCommand;

        public ProcessViewModel SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public event EventHandler<ErrorEventArgs> ErrorErised;

        private ProcessViewModel FindParentInChildProcess(ProcessViewModel process)
        {
            ProcessViewModel parent = null;
            foreach (ProcessViewModel child in process.InnerProcesses)
            {
                if (child.Equals(selectedItem))
                {
                    parent = process;
                    break;
                }
                if (child.InnerProcesses.Contains(selectedItem))
                {
                    parent = child;
                    break;
                }
                else if (child.InnerProcesses.Count != 0)
                {
                    parent = FindParentInChildProcess(child);
                    if (parent != null)
                    {
                        break;
                    }
                }
            }
            return parent;
        }

        private ProcessViewModel FindSelectedParent()
        {
            ProcessViewModel parent = null;
            foreach (ProcessViewModel process in processes)
            {
                if (process.Equals(selectedItem))
                {
                    parent = null;
                    break;
                }
                else if (process.InnerProcesses.Count != 0)
                {
                    parent = FindParentInChildProcess(process);
                    if (parent != null)
                    {
                        break;
                    }
                }
            }
            return parent;
        }

        private void OnErrorErised(ErrorEventArgs e)
        {
            ErrorErised?.Invoke(this, e);
        }

        private void Refresh()
        {
            processes.Clear();

            foreach (ExtendProcess process in processManager.GetProcessTree())
            {
                processes.Add(new ProcessViewModel(process));
            }
        }

        private void RemoveSelectedProcessViewModel()
        {
            ProcessViewModel parent = FindSelectedParent();
            if (parent == null)
            {
                processes.Remove(selectedItem);
            }
            else
            {
                parent.InnerProcesses.Remove(selectedItem);
            }
        }

        private void StartProcess()
        {
            processManager.StartProcess(newProcessName);
        }

        private void StopProcess()
        {
            processManager.CloseProcess(selectedItem.ProcessId);

            RemoveSelectedProcessViewModel();
        }
    }
}