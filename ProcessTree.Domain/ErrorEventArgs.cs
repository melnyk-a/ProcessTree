using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTree.Domain
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