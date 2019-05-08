using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace Library.Server {
    public partial class iSAPServer : DependencyObject {
        public class OutputLogin {
            public string sessionId { get; set; }
        }

        private string sessionId { get; set; }
        private Boolean logginIn { get; set; }
        private BehaviorSubject<Boolean> sjLogined = new BehaviorSubject<bool>(false);
        public async void Login() {
            if (this.logginIn == true) return;
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
    }
}
