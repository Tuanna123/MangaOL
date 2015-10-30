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
    public partial class TemplateHomeHoz : UserControl
    {
        UcViewModels.TemplateHomeHozVM model = new UcViewModels.TemplateHomeHozVM();

        
        public TemplateHomeHoz()
        {
            InitializeComponent();
            this.Loaded += TemplateHomeHoz_Loaded;
        }

        void TemplateHomeHoz_Loaded(object sender, RoutedEventArgs e)
        {
           Models.Manga item = this.DataContext as Models.Manga;
            if (item != null)
            {
                var oj = App.MangaFavorite.FirstOrDefault(t => t.UriManga == item.UriManga);
                item.IsFavorite = IsFavorite = oj == null ? false : true;
            }
            this.CheckPinOrUnPin();

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

        public delegate void TapFavoriteEvent(object sender, EventArgs e);
        public event TapFavoriteEvent TapFarivote;


        public delegate void TapHomeHozEvent(object sender, EventArgs e);
        public event TapHomeHozEvent TapGirdItem;



        public delegate void TapBlockEvent(object sender, EventArgs e);
        public event TapBlockEvent TapBlock;
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



        public bool IsFavorite
        {
            get { return (bool)GetValue(IsFavoriteProperty); }
            set { SetValue(IsFavoriteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFavorite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFavoriteProperty =
            DependencyProperty.Register("IsFavorite", typeof(bool), typeof(TemplateHomeHoz), new PropertyMetadata(false, IsFavoriteChanged));
        private static void IsFavoriteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TemplateHomeHoz th = d as TemplateHomeHoz;
            th.SetFavorite((bool)e.NewValue);
        }

        private void SetFavorite(bool p)
        {
            btnFavorite.Icon = (!(bool)p) ? (System.Windows.Application.Current.Resources["HeartOutline"] as string) : (System.Windows.Application.Current.Resources["Heart"] as string);
            btnFavorite.Text = (!(bool)p) ? "Yêu thích" : "Bỏ yêu thích";
        }

        public bool IsLock
        {
            get { return (bool)GetValue(IsLockProperty); }
            set { SetValue(IsLockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLockProperty =
            DependencyProperty.Register("IsLock", typeof(bool), typeof(TemplateHomeHoz), new PropertyMetadata(false, IsLockChanged));

        private static void IsLockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TemplateHomeHoz th = d as TemplateHomeHoz;
            th.SetLock((bool)e.NewValue);
        }

        private void SetLock(bool p)
        {
            btnBlock.Icon = ((bool)p) ? (System.Windows.Application.Current.Resources["HeartOutline"] as string) : (System.Windows.Application.Current.Resources["Heart"] as string);
            btnBlock.Text = (!(bool)p) ? "Block truyện" : "Unblock truyện";
        }



        public bool IsPinOrUnPin
        {
            get { return (bool)GetValue(IsPinOrUnPinProperty); }
            set { SetValue(IsPinOrUnPinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPinOrUnPin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPinOrUnPinProperty =
            DependencyProperty.Register("IsPinOrUnPin", typeof(bool), typeof(TemplateHomeHoz), new PropertyMetadata(false, IsPinOrUnPinChanged));
        private static void IsPinOrUnPinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TemplateHomeHoz th = d as TemplateHomeHoz;
            th.SetPinOrUnPin((bool)e.NewValue);
        }

        private void SetPinOrUnPin(bool p)
        {
            btnPinOrUnPin.Icon = ((bool)p) ? (System.Windows.Application.Current.Resources["UnPin"] as string) : (System.Windows.Application.Current.Resources["Pin"] as string);
            btnPinOrUnPin.Text = ((bool)p) ? "Un pin to start" : "Pin to start";
        }


        private void GridItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            borderOption.Visibility = System.Windows.Visibility.Collapsed;
            if (this.TapGirdItem != null)
            {
                this.TapGirdItem(this, EventArgs.Empty);
            }
        }

        private async void btnFavorite_Tap(object sender, EventArgs e)
        {
           var item = (sender as Mdl2).DataContext as Models.Manga;
            await this.model.InsertDeleteFavorite(item, IsFavorite);
        }

        private void btnTapBlock_Tap(object sender, EventArgs e)
        {
            borderOption.Visibility = System.Windows.Visibility.Collapsed;
            if (this.TapBlock != null)
            {
                this.TapBlock(this, EventArgs.Empty);
            }
        }

        private void btnPinOrUnPin_Tap(object sender, EventArgs e)
        {
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
    }
}
