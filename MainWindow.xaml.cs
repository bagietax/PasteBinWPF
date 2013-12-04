using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace PasteBin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Do zmiany poźniej
        public string urlToLastPast;

        public MainWindow()
        {
            InitializeComponent();
            BindCombBoxs();

        }

        private void BindCombBoxs()
        {
            cmbExpire.ItemsSource = Enum.GetValues(typeof(ExpireData)).Cast<ExpireData>();
            cmbExpire.SelectedIndex = 0;
            cmbVisible.ItemsSource = Enum.GetValues(typeof(EVisibility)).Cast<EVisibility>();
            cmbVisible.SelectedIndex = 1;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            btnSend.IsEnabled = false;
            WebApiComunicator.SendPostAsyn(txtTitle.Text,
                new TextRange(txtMessage.Document.ContentStart, txtMessage.Document.ContentEnd).Text,
                (EVisibility)cmbVisible.SelectedItem, (ExpireData)cmbExpire.SelectedItem, "csharp", this);
        }
        public void UpdateMe(IAsyncResult iar)
        {

            this.Dispatcher.Invoke((Action)(() =>
            {
                btnSend.IsEnabled = true;
                SolidColorBrush myBrush = new SolidColorBrush();
                myBrush.Color = Colors.White;

                ColorAnimation myColorAnimation = new ColorAnimation();
                myColorAnimation.From = Colors.White;
                myColorAnimation.To = (WebApiComunicator.isError ? Colors.MediumVioletRed : Colors.LightGreen);

                myColorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                myColorAnimation.AutoReverse = true;

                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                MainGrid.Background = myBrush;

                urlToLastPast = WebApiComunicator.lastNoteURl;

                /*DoubleAnimation anim = new DoubleAnimation();

                anim.From = txtMessage.Height;
                anim.Duration = new Duration(TimeSpan.FromSeconds(2.0));
                anim.AccelerationRatio=0.2;
                anim.To = 0;

                txtMessage.BeginAnimation(HeightProperty, anim);*/

            }));

            /*
            this.Dispatcher.Invoke((Action)(() =>
            {
                //TODO na 2 taba, i wczytaj to co poszło;
                frame1.Source = new Uri(urlToLastPast, UriKind.RelativeOrAbsolute);
               
            }));*/
           
        }

    }
}
