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
    public partial class iSAPServer : DependencyObject {
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
            //if (!DesignerProperties.GetIsInDesignMode(server)) server.Login();
        }

    }
}
