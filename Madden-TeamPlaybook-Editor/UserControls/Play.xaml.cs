using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class Play : UserControl
    {
        public Play()
        {
            InitializeComponent();
            //if (showAudibles)
            //{
            //    pnlAudibles.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    pnlAudibles.Visibility = Visibility.Collapsed;
            //}
        }

        public static DependencyProperty PlayProperty = DependencyProperty.Register("play", typeof(PlayVM), typeof(Play));
        [Bindable(true)] public PlayVM play
        {
            get { return GetValue(PlayProperty) as PlayVM; }
            set { SetValue(PlayProperty, value); }
        }

        public static DependencyProperty ShowAudiblesProperty = DependencyProperty.Register("showAudibles", typeof(bool), typeof(Play));
        [Bindable(true)] public bool showAudibles
        {
            get { return (bool)GetValue(ShowAudiblesProperty); }
            set { SetValue(ShowAudiblesProperty, value); }
        }

        private void saveArtview(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = play.PLYL.plyl.ToString(); // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG File (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                SaveCanvasToFile(play.ToCanvas(1, false), 96, filename);
            }
        }

        public static void SaveCanvasToFile(Canvas canvas, int dpi, string filename)
        {
            var rtb = new RenderTargetBitmap(
                (int)canvas.Width, //width
                (int)canvas.Height, //height
                dpi, //dpi x
                dpi, //dpi y
                PixelFormats.Pbgra32 // pixelformat
                );
            rtb.Render(canvas);

            var enc = new PngBitmapEncoder();
            BitmapFrame btf = BitmapFrame.Create(rtb);
            enc.Frames.Add(btf);
            using (var stm = System.IO.File.Create(filename))
            {
                enc.Save(stm);
            }
        }
    }
}
