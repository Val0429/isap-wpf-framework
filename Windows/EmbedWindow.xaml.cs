using Library.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.Windows {
    /// <summary>
    /// Interaction logic for EmbedWindow.xaml
    /// </summary>
    public partial class EmbedWindow : UserControl {
        private Window _window = null;
        public EmbedWindow() {
            InitializeComponent();

            this.Loaded += (object sender, RoutedEventArgs e) => {
                if (DesignerProperties.GetIsInDesignMode(this)) return;

                this._window = new Window();
                this._window.Content = this.Content;
                {
                    Binding binding = new Binding("LeftRatio") {
                        Source = this,
                        Mode = BindingMode.OneWay,
                        Converter = new RatioToActualScreenWidthConverter()
                    };
                    this._window.SetBinding(Window.LeftProperty, binding);
                }
                {
                    Binding binding = new Binding("TopRatio") {
                        Source = this,
                        Mode = BindingMode.OneWay,
                        Converter = new RatioToActualScreenHeightConverter()
                    };
                    this._window.SetBinding(Window.TopProperty, binding);
                }
                {
                    Binding binding = new Binding("WidthRatio") {
                        Source = this,
                        Mode = BindingMode.OneWay,
                        Converter = new RatioToActualScreenWidthConverter()
                    };
                    this._window.SetBinding(WidthProperty, binding);
                }
                {
                    Binding binding = new Binding("HeightRatio") {
                        Source = this,
                        Mode = BindingMode.OneWay,
                        Converter = new RatioToActualScreenHeightConverter()
                    };
                    this._window.SetBinding(HeightProperty, binding);
                }
                {
                    Binding binding = new Binding("MinWidth") {
                        Source = this,
                        Mode = BindingMode.OneWay
                    };
                    this._window.SetBinding(MinWidthProperty, binding);
                }
                {
                    Binding binding = new Binding("MinHeight") {
                        Source = this,
                        Mode = BindingMode.OneWay
                    };
                    this._window.SetBinding(MinHeightProperty, binding);
                }
                {
                    Binding binding = new Binding("Icon") {
                        Source = this,
                        Mode = BindingMode.OneWay
                    };
                    this._window.SetBinding(Window.IconProperty, binding);
                }

                this._window.Unloaded += (object s2, RoutedEventArgs e2) => {
                    (this.Parent as Panel)?.Children.Remove(this);
                    this._window = null;
                };

                this._window.Show();
                this._window.Focus();
            };

            this.Unloaded += (object sender, RoutedEventArgs e) => {
                this._window?.Close();
            };
        }


        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(int hWnd, int nCmdShow);
        private const int SW_MAXIMIZE = 3;

        public void BringToFront() {
            var hwnd = new WindowInteropHelper(_window).Handle;
            SetForegroundWindow(hwnd.ToInt32());
            ShowWindow(hwnd.ToInt32(), SW_MAXIMIZE);
        }

        #region "Dependency Property"
        public double LeftRatio {
            get { return (double)GetValue(LeftRatioProperty); }
            set { SetValue(LeftRatioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftRatioProperty =
            DependencyProperty.Register("LeftRatio", typeof(double), typeof(EmbedWindow), new PropertyMetadata(0.25));

        public double TopRatio {
            get { return (double)GetValue(TopRatioProperty); }
            set { SetValue(TopRatioProperty, value); }
        }
        // Using a DependencyProperty as the backing store for TopRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopRatioProperty =
            DependencyProperty.Register("TopRatio", typeof(double), typeof(EmbedWindow), new PropertyMetadata(0.25));

        public double WidthRatio {
            get { return (double)GetValue(WidthRatioProperty); }
            set { SetValue(WidthRatioProperty, value); }
        }
        // Using a DependencyProperty as the backing store for WidthRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthRatioProperty =
            DependencyProperty.Register("WidthRatio", typeof(double), typeof(EmbedWindow), new PropertyMetadata(0.5));

        public double HeightRatio {
            get { return (double)GetValue(HeightRatioProperty); }
            set { SetValue(HeightRatioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeightRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightRatioProperty =
            DependencyProperty.Register("HeightRatio", typeof(double), typeof(EmbedWindow), new PropertyMetadata(0.5));

        public ImageSource Icon {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(EmbedWindow), new PropertyMetadata(null));

        #endregion "Dependency Property"
    }
}
