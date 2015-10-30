using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Input;
using System.Threading;
using Telerik.Windows.Controls.SlideView;

namespace MangaOL.UserControls
{
    public partial class UcImage : UserControl
    {
        public UcImage()
        {
            InitializeComponent();
            this.Loaded += UcImage_Loaded;
        }
        void UcImage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        void obj_Zoom_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height > e.PreviousSize.Height)
            {
                this.InZoom = true;
                if (this.ZoomIn != null)
                {
                    this.ZoomIn(this, EventArgs.Empty);
                }
            }
            else
            {
                this.InZoom = (sender as PanAndZoomImage).Zoom == 1 ? false : true;
                if (obj_Zoom.Zoom == 1)
                {
                    this.InZoom = false;
                }
            }
        }

        void obj_Zoom_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            PanAndZoomImage panAndZoomImage = (PanAndZoomImage)sender;
            if (Math.Abs(panAndZoomImage.Zoom - 1.0) < 0.05)
            {
                panAndZoomImage.Zoom = 1.0;
            }
        }
        public delegate void DeImageFailed(object s, EventArgs e);
        public delegate void DeImageOpened(object s, EventArgs e);
        public event DeImageFailed UcImageFailed;
        public event DeImageOpened UcImageOpened;
        public delegate void ZoomInEvent(object s, EventArgs e);
        public event ZoomInEvent ZoomIn;
        public delegate void ZoomOutEvent(object s, EventArgs e);
        public event ZoomOutEvent ZoomOut;
        private void btnZoomOut_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //var t = obj_Zoom.Zoom;
            //this.InZoom = t == 1 ? false : true;
            //obj_Zoom.Zoom = t == 1 ? 1 : t - 1;
            if (this.ZoomOut != null)
            {
                this.ZoomOut(this, EventArgs.Empty);
            }
        }

        private void btnZoomIn_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //var t = obj_Zoom.Zoom;
            //this.InZoom = true;
            //obj_Zoom.Zoom = t == 10 ? t : t + 1;
            if (this.ZoomIn != null)
            {
                this.ZoomIn(this, EventArgs.Empty);
            }
        }

        public Stretch StretchImage
        {
            get { return (Stretch)GetValue(StretchImageProperty); }
            set { SetValue(StretchImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StretchImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchImageProperty =
            DependencyProperty.Register("StretchImage", typeof(Stretch), typeof(UcImage), new PropertyMetadata((Stretch)Stretch.Uniform));




        public bool InZoom
        {
            get { return (bool)GetValue(InZoomProperty); }
            set { SetValue(InZoomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InZoom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InZoomProperty =
            DependencyProperty.Register("InZoom", typeof(bool), typeof(UcImage), new PropertyMetadata(false,InZoomChanged));

        private static void InZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UcImage th = d as UcImage;
            th.SetVisibilyStackPageZoom((bool)e.NewValue);
        }

        private void SetVisibilyStackPageZoom(bool p)
        {
            stackZoom.Visibility = p ? Visibility.Visible : System.Windows.Visibility.Collapsed;
        }


        public string UriSourceImage
        {
            get { return (string)GetValue(UriSourceImageProperty); }
            set { SetValue(UriSourceImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UriSourceImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UriSourceImageProperty =
            DependencyProperty.Register("UriSourceImage", typeof(string), typeof(UcImage), new PropertyMetadata((string)"", UriSourceChanged));
        private static void UriSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            UcImage imageHelper = o as UcImage;
            imageHelper.SetUriSourceImage((string)e.NewValue);
        }

        private async void SetUriSourceImage(string p)
        {
            try
            {
                if (p.StartsWith("http"))
                {
                    var item = (Models.Image)this.DataContext;
                    if (item.Bytes == null)
                    {
                        obj_Zoom.Source = await DownloadStreamToUriImage(p);
                    }
                    else
                    {
                        obj_Zoom.Source = BytesToImage(item.Bytes);
                    }

                }
                else
                {
                    Stream fs = File.OpenRead(p);
                    var arr_Byte = CopyToArray(fs);
                    obj_Zoom.Source = BytesToImage(arr_Byte);
                }
            }
            catch
            {
                obj_Zoom.Source = BytesToImage(null);
            }

            GC.Collect();
        }
        private byte[] CopyToArray(Stream input)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        private async System.Threading.Tasks.Task<BitmapImage> DownloadStreamToUriImage(string url)
        {
            try
            {
                var bytearr = await MangaCore.Utils.DownloadByteToUriImage(url);
                return BytesToImage(bytearr);
            }
            catch
            {
                stackLoading.Visibility = System.Windows.Visibility.Visible;
                return new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Assets/Images/NoImage.png", UriKind.Relative));
            }

        }
        private BitmapImage BytesToImage(byte[] bytes)
        {
            BitmapImage bitmapImage = new BitmapImage();
            if (bytes.Length == 0) return new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Assets/Images/NoImage.png", UriKind.Relative));
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    bitmapImage.SetSource(ms);
                    return bitmapImage;
                }
            }
            finally { bitmapImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Assets/Images/NoImage.png", UriKind.Relative)); }
        }
        void obj_Zoom_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            stackLoading.Visibility = System.Windows.Visibility.Collapsed;
            System.Windows.Media.Imaging.BitmapImage bm = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Assets/Images/NoImage.png", UriKind.Relative));
            obj_Zoom.Source = bm;
            if (this.UcImageFailed != null)
            {
                this.UcImageFailed(this.DataContext, EventArgs.Empty);
            }
        }

        void obj_Zoom_ImageOpened(object sender, RoutedEventArgs e)
        {
            stackLoading.Visibility = System.Windows.Visibility.Collapsed;
            if (this.UcImageOpened != null)
            {
                this.UcImageOpened(this.DataContext, EventArgs.Empty);
            }
        }

        private void obj_Zoom_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
           // var ư = obj_Zoom.Zoom;
        }


    }
}
