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
    public partial class TemplateNewRead : UserControl
    {
        UcViewModels.TemplateHomeHozVM modelTemplateHomeHozVM = new UcViewModels.TemplateHomeHozVM();
        
        public TemplateNewRead()
        {
            InitializeComponent();
            this.Loaded += TemplateNewRead_Loaded;
        }

        void TemplateNewRead_Loaded(object sender, RoutedEventArgs e)
        {
           Models.Manga item = this.DataContext as Models.Manga;
            if (item != null)
            {
                var oj = App.MangaFavorite.FirstOrDefault(t => t.UriManga == item.UriManga);
                item.IsFavorite = IsFavorite = oj == null ? false : true;
            }
            CheckPinOrUnPin();
        }
        private void CheckPinOrUnPin()
        {
            Models.Manga item = this.DataContext as Models.Manga;
            System.Windows.Data.Binding b = new System.Windows.Data.Binding("IsPin");
            b.Source = item;
            b.Mode = System.Windows.Data.BindingMode.TwoWay;
            this.SetBinding(TemplateNewRead.IsPinOrUnPinProperty, b);
        }
        public delegate void TapEvent(object sender, EventArgs e);
        public event TapEvent TapGirdItem;

        public delegate void TapFarivoteEvent(object sender, EventArgs e);
        public event TapFarivoteEvent TapFarivote;

        public delegate void TapDelelteEvent(object sender, EventArgs e);
        public event TapDelelteEvent TapDelete;
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
            DependencyProperty.Register("IsFavorite", typeof(bool), typeof(TemplateNewRead), new PropertyMetadata(false, IsFavoriteChanged));
        private static void IsFavoriteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TemplateNewRead th = d as TemplateNewRead;
            th.SetFavorite((bool)e.NewValue);
        }

        private void SetFavorite(bool p)
        {
            btnFavorite.Icon = (!(bool)p) ? (System.Windows.Application.Current.Resources["HeartOutline"] as string) : (System.Windows.Application.Current.Resources["Heart"] as string);
            btnFavorite.Text = (!(bool)p) ? "Yêu thích" : "Bỏ yêu thích";
        }
        public bool IsPinOrUnPin
        {
            get { return (bool)GetValue(IsPinOrUnPinProperty); }
            set { SetValue(IsPinOrUnPinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPinOrUnPin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPinOrUnPinProperty =
            DependencyProperty.Register("IsPinOrUnPin", typeof(bool), typeof(TemplateNewRead), new PropertyMetadata(false, IsPinOrUnPinChanged));
        private static void IsPinOrUnPinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TemplateNewRead th = d as TemplateNewRead;
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
            await this.modelTemplateHomeHozVM.InsertDeleteFavorite(item, IsFavorite);
            

            if (this.TapFarivote != null)
            {
                this.TapFarivote(this, EventArgs.Empty);
            }
        }

        private void btnDelete_Tap(object sender, EventArgs e)
        {
            borderOption.Visibility = System.Windows.Visibility.Collapsed;
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
            this.modelTemplateHomeHozVM.UpdatePinOrUpPin(item);
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
            IsPinOrUnPin = !IsPinOrUnPin;
        }
    }
}
