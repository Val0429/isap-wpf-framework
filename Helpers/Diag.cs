using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Helpers {

    public static class Diag {
        public static void SetApply(object element, bool apply) {
            PresentationTraceSources.SetTraceLevel(element, PresentationTraceLevel.High);
        }
    }
}
