using Library.Helpers;
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

namespace Library.Windows {
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : UserControl {
        private UIElement embedContent = null;
        private DependencyPropertyListener listener;
        public ModalWindow() {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            listener = new DependencyPropertyListener(this, ModalWindow.IsShowProperty, (e) => {
                var show = (bool)e.NewValue;
                var window = UIHelper.FindVisualParent<Window>(this);
                var rootGrid = window?.Content;
                if (rootGrid == null) return;
                if (rootGrid.GetType() != typeof(Grid)) {
                    throw new Exception("Root of ModalWindow object should be a <Grid>.");
                }
                Grid grid = rootGrid as Grid;
                UIElement element = this.Main as UIElement;

                if (show) {
                    var keep = this.Content;
                    this.Container.Children.Remove(element);
                    this.embedContent = element;
                    grid.Children.Add(element);
                    Panel.SetZIndex(element, 10000);
                } else {
                    grid.Children.Remove(element);
                    this.Container.Children.Add(this.embedContent);
                    this.embedContent = null;
                }
            });
        }

        #region Dependency Properties

        public bool IsShow {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(ModalWindow), new PropertyMetadata(false));

        #endregion Dependency Properties
    }

    public static class UIHelper {
        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the queried item.</param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, a null reference is being returned.</returns>
        public static T FindVisualParent<T>(DependencyObject child)
          where T : DependencyObject {
            // get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // we’ve reached the end of the tree
            if (parentObject == null) return null;

            // check if the parent matches the type we’re looking for
            T parent = parentObject as T;
            if (parent != null) {
                return parent;
            } else {
                // use recursion to proceed with next level
                return FindVisualParent<T>(parentObject);
            }
        }
    }
}
