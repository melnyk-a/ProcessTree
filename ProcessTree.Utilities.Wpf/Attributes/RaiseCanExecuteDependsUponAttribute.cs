using System;

namespace ProcessTree.Utilities.Wpf.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RaiseCanExecuteDependsUponAttribute : Attribute
    {
        private readonly string propertyName;

        public RaiseCanExecuteDependsUponAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public string PropertyName => propertyName;
    }
}