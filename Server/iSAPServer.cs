using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace Library.Server {
    public class OutputLogin {
        public string sessionId { get; set; }
    }

    public class iSAPServer : DependencyObject {
        #region Dependency Properties
        public string IP {
            get { return (string)GetValue(IPProperty); }
            set { SetValue(IPProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IP.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IPProperty =
            DependencyProperty.Register("IP", typeof(string), typeof(iSAPServer), new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnImpValueChanged)
                ));


        public string Port {
            get { return (string)GetValue(PortProperty); }
            set { SetValue(PortProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Port.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PortProperty =
            DependencyProperty.Register("Port", typeof(string), typeof(iSAPServer), new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnImpValueChanged)
                ));


        public string Account {
            get { return (string)GetValue(AccountProperty); }
            set { SetValue(AccountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Account.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AccountProperty =
            DependencyProperty.Register("Account", typeof(string), typeof(iSAPServer), new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnImpValueChanged)
                ));


        public string Password {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(iSAPServer), new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnImpValueChanged)
                ));

        #endregion Dependency Properties

        private static void OnImpValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            var server = sender as iSAPServer;
            if (!DesignerProperties.GetIsInDesignMode(server)) server.Login();
            //if (server.IP != null && server.Port != null && server.Account != null && server.Password != null) {
            //}
        }

        private string sessionId { get; set; }
        private Boolean logginIn { get; set; }
        private BehaviorSubject<Boolean> sjLogined = new BehaviorSubject<bool>(false);
        public async void Login() {
            if (this.IP == null || this.Port == null || this.Account == null || this.Password == null) return;
            this.logginIn = true;
            var host = string.Format("http://{0}:{1}", IP, Port);
            /// do login
            var uri = string.Format("{0}/users/login", host);
            using (var client = new HttpClient()) {
                var byteContent = new StringContent(string.Format("{{ \"username\": \"{0}\", \"password\": \"{1}\" }}", Account, Password));
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync(uri, byteContent);
                var resultStr = await result.Content.ReadAsStringAsync();
                if (result.StatusCode != System.Net.HttpStatusCode.OK) {
                    this.logginIn = false;
                    MessageBox.Show("Login Failed");
                    return;
                }
                var jsonSerializerx = new JavaScriptSerializer();
                var user = jsonSerializerx.Deserialize<OutputLogin>(resultStr);
                var sessionId = user.sessionId;
                this.sessionId = sessionId;
                this.logginIn = false;
                this.sjLogined.OnNext(true);
            }
        }

        public async Task<List<ExpandoObject>> R(string path) {
            if (this.logginIn == false) this.Login();
            await (this.sjLogined as IObservable<Boolean>).Where( value => value == true ).FirstOrDefaultAsync();
            var host = string.Format("http://{0}:{1}", IP, Port);
            /// do login
            var uri = string.Format("{0}{1}?paging.all=true&sessionId={2}", host, path, this.sessionId);
            using (var client = new HttpClient()) {
                var result = await client.GetAsync(uri);
                var resultStr = await result.Content.ReadAsStringAsync();
                if (result.StatusCode != System.Net.HttpStatusCode.OK) {
                    MessageBox.Show(string.Format("{0} Failed", path));
                    return null;
                }
                var jsonSerializerx = new JavaScriptSerializer();
                var rtn = jsonSerializerx.DeserializeObject(resultStr);
                /// generate result
                var list = new List<ExpandoObject>();
                //for (var o in (rtn as IEnumerator<object>)[1]) {
                var results = ((Dictionary<string, object>)rtn)["results"];
                foreach (var o in (Object[])results) {
                    var obj = new ExpandoObject();
                    var objc = (ICollection<KeyValuePair<string, object>>)obj;
                    foreach (var kvp in (Dictionary<string, object>)o) {
                        objc.Add(kvp);
                    }
                    list.Add(obj);
                }
                return list;
            }
        }
    }
}
