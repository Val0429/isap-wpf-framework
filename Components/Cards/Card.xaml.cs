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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.Components.Cards {
    [ContentProperty("Content")]
    public partial class Card : UserControl {
        public Card() {
            InitializeComponent();
        }

        #region Dependency Properties

        public string Label {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(Card), new PropertyMetadata(null));



        public bool Visible {
            get { return (bool)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibleProperty =
            DependencyProperty.Register("Visible", typeof(bool), typeof(Card), new PropertyMetadata(true));


        #endregion Dependency Properties

        private void Button_Click(object sender, RoutedEventArgs e) {
            Visible = !Visible;
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e) {
            var expander = Template.FindName("Expander", this) as Expander;
            var viewer = expander.Template.FindName("Viewer", expander) as FrameworkElement;
            viewer.Tag = 3.0;
        }
    }
}
