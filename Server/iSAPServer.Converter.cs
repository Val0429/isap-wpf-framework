using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Server {
    public partial class iSAPServer : DependencyObject {
        private object convertDictionary(object json) {
            var obj = new ExpandoObject();
            var objc = (ICollection<KeyValuePair<string, object>>)obj;
            foreach (var kvp in (Dictionary<string, object>)json) {
                var key = kvp.Key;
                var value = kvp.Value;
                objc.Add(new KeyValuePair<string, object>(key, convertAny(value)));
            }
            return obj;
        }

        private object convertAny(object json) {
            var type = json.GetType();
            if (type.IsArray) {
                var list = new List<object>();
                foreach (var o in (object[])json) {
                    list.Add(convertAny(o));
                }
                return list.ToArray();

            } else if (type == typeof(System.Collections.Generic.Dictionary<string, object>)) {
                return convertDictionary(json);

            } else {
                return json;
            }
        }

        private object convertServerResponse(object json) {
            return convertAny(json);
        }
    }
}
