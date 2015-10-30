using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace MangaOL.UserControls
{
    public partial class UcImageData : UserControl
    {
        public UcImageData()
        {
            InitializeComponent();
        }


        public Uri UriImage
        {
            get { return (Uri)GetValue(UriImageProperty); }
            set { SetValue(UriImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UriImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UriImageProperty =
            DependencyProperty.Register("UriImage", typeof(Uri), typeof(UcImageData), new PropertyMetadata((Uri)null, UriImageChanged));

        private static void UriImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            flag = false;
        }



        public Stretch StretchImage
        {
            get { return (Stretch)GetValue(StretchImageProperty); }
            set { SetValue(StretchImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StretchImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchImageProperty =
            DependencyProperty.Register("StretchImage", typeof(Stretch), typeof(UcImageData), new PropertyMetadata(Stretch.None));

       static bool flag = false;
        private void imageContent_Onpened(object sender, RoutedEventArgs e)
        {
           // if (flag) return;
            //imageContent.Opacity = 0;
            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.2)
            };

            Storyboard.SetTarget(opacityAnimation, (DependencyObject)sender);
            Storyboard.SetTargetProperty(opacityAnimation,
                                         new PropertyPath(System.Windows.Controls.Image.OpacityProperty));

            var storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnimation);
            storyboard.Completed+= (s,ed) => {
                flag = true;
            };
            storyboard.Begin();

        }

        private void imageContent_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var img = sender as Image;
            img.Opacity = 1;
            System.Windows.Media.Imaging.BitmapImage bm = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Assets/Images/NoImage.png", UriKind.RelativeOrAbsolute));
            imageContent.Source = bm;
        }

        private void imageContent_Loaded(object sender, RoutedEventArgs e)
        {
           // imageContent.Opacity = 0;
        }


        
        
    }
}
