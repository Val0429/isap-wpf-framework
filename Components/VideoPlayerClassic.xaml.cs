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
    interface IVideoPlayerClassic {
        /// public dependency properties - code snippet: propdp ///////////
        /**
         * DP binding CMS source, and then feed into ActiveX Control.
         */
        string source { get; set; }

        /// public methods ///////////////////////////
        /**
         * Maybe enum speed? ex: 1 2 4 8 -1 -2
         */
        void Play(int speed);
        void Pause();
        void Stop();

        /// routed events - code snippet: valre //////////////////////////
        /**
         * Fired when play time changed.
         */
        event RoutedEventHandler OnTimeChanged;
    }
    /// <summary>
    /// Interaction logic for VideoPlayerClassic.xaml
    /// </summary>
    public partial class VideoPlayerClassic : UserControl {
        public VideoPlayerClassic() {
            InitializeComponent();
        }
    }
}
