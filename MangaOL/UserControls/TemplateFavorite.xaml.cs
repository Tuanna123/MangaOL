using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MangaOL.UserControls
{
    public partial class TemplateFavorite : UserControl
    {
        UserControls.UcViewModels.TemplateHomeHozVM model = new UcViewModels.TemplateHomeHozVM();
        
        public TemplateFavorite()
        {
            InitializeComponent();
            this.Loaded += TemplateFavorite_Loaded;
        }

        void TemplateFavorite_Loaded(object sender, RoutedEventArgs e)
        {
            CheckPinOrUnPin();
        }
        private void CheckPinOrUnPin()
        {
            // Kiểm tra Title được pin chưa
            Models.Manga item = this.DataContext as Models.Manga;
            System.Windows.Data.Binding b = new System.Windows.Data.Binding("IsPin");
            b.Source = item;
            b.Mode = System.Windows.Data.BindingMode.TwoWay;
            this.SetBinding(IsPinOrUnPinProperty, b);
        }
        public delegate void TapEvent(object sender, EventArgs e);
        public event TapEvent TapGirdItem;

        public delegate void TapDelelteEvent(object sender, EventArgs e);
        public event TapDelelteEvent TapDelete;

        public delegate void TapNoticationEvent(object s, EventArgs e);
        public event TapNoticationEvent TapNotication;
        private void btnOption_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            switch (borderOption.Visibility)
            {
                case Visibility.Collapsed:
                    borderOption.Visibility = System.Windows.Visibility.Visible;
                    break;
                default:
                    borderOption.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }
        }
        public bool IsPinOrUnPin
        {
            get { return (bool)GetValue(IsPinOrUnPinProperty); }
            set { SetValue(IsPinOrUnPinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPinOrUnPin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPinOrUnPinProperty =
            DependencyProperty.Register("IsPinOrUnPin", typeof(bool), typeof(TemplateFavorite), new PropertyMetadata(false, IsPinOrUnPinChanged));
        private static void IsPinOrUnPinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TemplateFavorite th = d as TemplateFavorite;
            th.SetPinOrUnPin((bool)e.NewValue);
        }

        private void SetPinOrUnPin(bool p)
        {
            btnPinOrUnPin.Icon = ((bool)p) ? (System.Windows.Application.Current.Resources["UnPin"] as string) : (System.Windows.Application.Current.Resources["Pin"] as string);
            btnPinOrUnPin.Text = ((bool)p) ? "Un pin to start" : "Pin to start";
        }


        public bool IsNoticaiton
        {
            get { return (bool)GetValue(IsNoticaitonProperty); }
            set { SetValue(IsNoticaitonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsNoticaiton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsNoticaitonProperty =
            DependencyProperty.Register("IsNoticaiton", typeof(bool), typeof(TemplateFavorite), new PropertyMetadata(false,IsNoticationChanged));

        public static void IsNoticationChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            var th = d as TemplateFavorite;
            th.SetNotication((bool)e.NewValue);

        }
        private void SetNotication(bool p)
        {
            btnNotication.Icon = ((bool)p) ? (System.Windows.Application.Current.Resources["BellOutline"] as string) : (System.Windows.Application.Current.Resources["BellOffOutline"] as string);
            btnNotication.Text = ((bool)p) ? "Tắt thông báo chap" : "Bật thông báo chap";
        }
         private void GridItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            borderOption.Visibility = System.Windows.Visibility.Collapsed;
            if (this.TapGirdItem != null)
            {
                this.TapGirdItem(this, EventArgs.Empty);
            }
        }

        private async void btnDelete_Tap(object sender, EventArgs e)
        {
            MangaOL.Models.Manga item = (sender as MangaOL.UserControls.Mdl2).DataContext as MangaOL.Models.Manga;
            await this.model.InsertDeleteFavorite(item, true);

            if (this.TapDelete != null)
            {
                this.TapDelete(this, EventArgs.Empty);
            }
        }
        private void btnPinOrUnPin_Tap(object sender, EventArgs e)
        {
            borderOption.Visibility = System.Windows.Visibility.Collapsed;
            var item = (sender as UserControls.Mdl2).DataContext as Models.Manga;
            PinOrUnPin();
            this.model.UpdatePinOrUpPin(item);
        }
        private void PinOrUnPin()
        {

            var item = this.DataContext as Models.Manga;
            Utils.SecondaryTileUriSource = "/Views/DetailPage.xaml?UriManga=" + item.UriManga + "";
            if (!IsPinOrUnPin)
            {
                Utils.Pin(item.NameManga, item.UriCover);
                btnPinOrUnPin.Icon = Application.Current.Resources["UnPin"] as string;
                btnPinOrUnPin.Text = "Un Pin";
            }
            else
            {
                Utils.UnPin();
                btnPinOrUnPin.Icon = Application.Current.Resources["Pin"] as string;
                btnPinOrUnPin.Text = "Pin";
            }
           // IsPinOrUnPin = !IsPinOrUnPin;
        }

        private void btnNoticaiton_Tap(object sender, EventArgs e)
        {
            borderOption.Visibility = System.Windows.Visibility.Collapsed;
            if(this.TapNotication != null)
            {
                this.TapNotication(this, EventArgs.Empty);
            }
        }

       

      
    }
}
