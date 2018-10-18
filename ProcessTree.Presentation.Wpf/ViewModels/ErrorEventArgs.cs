using System;

namespace ProcessTree.Presentation.Wpf.ViewModels
{
    public class ErrorEventArgs : EventArgs
    {
        private readonly string errorMessage;

        public ErrorEventArgs(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        public string ErrorMessage => errorMessage;
    }
}