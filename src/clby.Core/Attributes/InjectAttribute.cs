using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace clby.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => BindingSource.Services;
    }
}
