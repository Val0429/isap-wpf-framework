using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace Library.Server {
    public partial class iSAPServer : DependencyObject {

        public async Task<object> R(string path) {
            if (this.sjLogined.Value == false) this.Login();
            await (this.sjLogined as IObservable<Boolean>).Where(value => value == true).FirstOrDefaultAsync();
            var host = string.Format("http://{0}:{1}", IP, Port);
            /// do login
            var uri = string.Format("{0}{1}?paging.all=true&sessionId={2}", host, path, this.sessionId);
            using (var client = new HttpClient()) {
                var result = await client.GetAsync(uri);
                var resultStr = await result.Content.ReadAsStringAsync();
                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                    this.Login();
                    return await this.R(path);

                } else if (result.StatusCode != System.Net.HttpStatusCode.OK) {
                    MessageBox.Show(string.Format("{0} Failed, Status Code: {1}, Message: {2}", path, result.StatusCode, resultStr));
                    return null;
                }
                var jsonSerializerx = new JavaScriptSerializer();
                var rtn = jsonSerializerx.DeserializeObject(resultStr);
                /// generate result
                return convertServerResponse(rtn);
            }
        }

    }
}
