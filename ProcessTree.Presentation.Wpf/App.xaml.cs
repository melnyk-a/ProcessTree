﻿using Ninject;
using Ninject.Extensions.Conventions;
using ProcessTree.Domain;
using ProcessTree.Presentation.Wpf.Views;
using System.Windows;

namespace ProcessTree.Presentation.Wpf
{
    public partial class App : Application
    {
        private StandardKernel CreateContainer()
        {
            var container = new StandardKernel();

            container.Bind(
                configurator => configurator
                .From("ProcessTree.Presentation.Wpf", "ProcessTree.Domain")
                .SelectAllClasses()
                .BindAllInterfaces()
                );

            return container;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = CreateContainer();
            MainView mainView = container.Get<MainView>();
            mainView.Show();
        }
    }
}