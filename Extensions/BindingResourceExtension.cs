using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Library.Extensions {
    public class BindingResourceExtension : StaticResourceExtension {
        public BindingResourceExtension() : base() { }

        public BindingResourceExtension(object resourceKey) : base(resourceKey) { }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            var binding = base.ProvideValue(serviceProvider) as BindingBase;
            if (binding != null)
                return binding.ProvideValue(serviceProvider);
            else
                return null; //or throw an exception
        }
    }
}
