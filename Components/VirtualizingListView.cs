using Library.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.Components {

    public class VirtualizingListView : ListView {
        static VirtualizingListView() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VirtualizingListView), new FrameworkPropertyMetadata(typeof(VirtualizingListView)));
        }

        #region Dependency Properties

        public iSAPServer Server {
            get { return (iSAPServer)GetValue(ServerProperty); }
            set { SetValue(ServerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Server.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServerProperty =
            DependencyProperty.Register("Server", typeof(iSAPServer), typeof(VirtualizingListView), new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnImpValueChanged)
                ));


        public string Path {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(VirtualizingListView), new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnImpValueChanged)
                ));




        public IValueConverter Converter {
            get { return (IValueConverter)GetValue(ConverterProperty); }
            set { SetValue(ConverterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Converter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConverterProperty =
            DependencyProperty.Register("Converter", typeof(IValueConverter), typeof(VirtualizingListView));

        #endregion Dependency Properties

        private static void OnImpValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            var obj = sender as VirtualizingListView;
            if (obj.Server != null && obj.Path != null && !DesignerProperties.GetIsInDesignMode(obj)) {
                obj.Fetch();
            }
        }

        private async void Fetch() {
            var result = await this.Server.R(this.Path);
            dynamic obj = result;
            if (this.Converter != null) {
                //this.ItemsSource = result.Select(v => this.Converter.Convert(v, null, null, null));
                //this.ItemsSource = obj.results;
                this.ItemsSource = new List<object>(obj.results).Select(v => this.Converter.Convert(v, null, null, null)).ToArray();

            } else
                this.ItemsSource = obj.results;
        }

    }
}
