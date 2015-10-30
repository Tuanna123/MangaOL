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
    public partial class TemplateHomeVez : UserControl
    {
        UcViewModels.TemplateHomeHozVM modelTemplateHomeHozVM = new UcViewModels.TemplateHomeHozVM();
        public TemplateHomeVez()
        {
            InitializeComponent();
            this.Loaded += TemplateHomeVez_Loaded;
        }

        public delegate void TapEvent(object sender, EventArgs e);
        public event TapEvent TapGirdItem;

        void TemplateHomeVez_Loaded(object sender, RoutedEventArgs e)
        {
            Models.Manga item = (sender as TemplateHomeVez).DataContext as Models.Manga;
            if (item != null)
            {
                var oj = App.MangaFavorite.FirstOrDefault(t => t.UriManga == item.UriManga);
                item.IsFavorite = IsFavorite = oj == null ? false : true;
            }
        }

        public bool IsFavorite
        {
            get { return (bool)GetValue(IsFavoriteProperty); }
            set { SetValue(IsFavoriteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFavorite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFavoriteProperty =
            DependencyProperty.Register("IsFavorite", typeof(bool), typeof(TemplateHomeVez), new PropertyMetadata(false, IsFavoriteChanged));
        private static void IsFavoriteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TemplateHomeVez th = d as TemplateHomeVez;
            th.SetFavorite((bool)e.NewValue);
        }

        private void SetFavorite(bool p)
        {
            btnFavorite.Icon = (!(bool)p) ? (System.Windows.Application.Current.Resources["HeartOutline"] as string) : (System.Windows.Application.Current.Resources["Heart"] as string);
        }

        private async void btnFavorite_Tap(object sender, EventArgs e)
        {
            var item = (sender as Mdl2).DataContext as Models.Manga;
            await this.modelTemplateHomeHozVM.InsertDeleteFavorite(item, IsFavorite);
        }

        private void Container_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (this.TapGirdItem !=null)
            {
                this.TapGirdItem(this, EventArgs.Empty);
            }
        }
    }
}
