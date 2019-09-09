﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Library.Helpers {
    public class MultiBinding : System.Windows.Data.MultiBinding {
        public MultiBinding(BindingBase b1) {
            Bindings.Add(b1);
        }
        public MultiBinding(BindingBase b1, BindingBase b2) {
            Bindings.Add(b1);
            Bindings.Add(b2);
        }
        public MultiBinding(BindingBase b1, BindingBase b2, BindingBase b3) {
            Bindings.Add(b1);
            Bindings.Add(b2);
            Bindings.Add(b3);
        }
    }
}
