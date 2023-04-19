using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

namespace AudioAndVideoPlayer
{

    public partial class MainWindow : Window
    {

        DispatcherTimer? timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        public void timer_Tick(object sender, EventArgs e)
        {
            if (myMediaElement.NaturalDuration.HasTimeSpan)
            {                
                slider.Value = myMediaElement.Position.TotalSeconds;
            }
        }
        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (myMediaElement.Source != null)
            {
                myMediaElement.Play();
            }
            else
            {
                MessageBox.Show("Firstly open a media file", "File not chosen", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }
        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (myMediaElement.Source != null)
            {
                myMediaElement.Pause();
            }
            else
            {
                MessageBox.Show("Firstly open a media file", "File not chosen", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }
        private void FindFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mp4;*.wmv)|*.mp3;*.mp4;*.wmv";
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                myMediaElement.Source = new Uri(filePath);

                NameOfTheMedia.Content = System.IO.Path.GetFileName(filePath);
            }
        }

        private void myMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (myMediaElement.Source != null)
                slider.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void slider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            myMediaElement.Position = TimeSpan.FromSeconds(slider.Value);
        }
    }
}
