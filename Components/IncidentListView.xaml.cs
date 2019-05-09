using Library.Server;
using System;
using System.Collections.Generic;
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
    public class IIncidentListView {
        public string source { get; set; }
        public string time { get; set; }
    }
    /// <summary>
    /// Interaction logic for IncidentListView.xaml
    /// </summary>
    public partial class IncidentListView : UserControl {
        public IncidentListView() {
            InitializeComponent();
        }

        public iSAPServer Server {
            get { return (iSAPServer)GetValue(ServerProperty); }
            set { SetValue(ServerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Server.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServerProperty =
            DependencyProperty.Register("Server", typeof(iSAPServer), typeof(IncidentListView));

        public string Path {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(IncidentListView));
        


        public IValueConverter Converter {
            get { return (IValueConverter)GetValue(ConverterProperty); }
            set { SetValue(ConverterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Converter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConverterProperty =
            DependencyProperty.Register("Converter", typeof(IValueConverter), typeof(IncidentListView));
    }
}
