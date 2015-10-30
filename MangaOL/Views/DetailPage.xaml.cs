using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MangaCore;
using System.Text.RegularExpressions;
using Windows.Phone.System.UserProfile;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.Windows.Resources;

namespace MangaOL.Views
{
    public partial class DetailPage : PhoneApplicationPage
    {

        public void DelegateValueProBar(double value)
        {
            this.ValueProBarTotalImageOneped = value;
        }
        //private double m_ScaleFactor = 1.0;
        MangaOL.ViewModels.DetailPageVM models = new ViewModels.DetailPageVM();
        MangaOL.ViewModels.MainPageVM modelsMainPageVM = new ViewModels.MainPageVM();
        private int indexPivotImage = 0;
        private bool isPin;
        private bool isOrder;
        //private bool isReading = false;
        private int indexChaper = 0;
        private Models.Chaper itemChaper;
        private Models.ChaperBookmask itemChaperBookmask;
        private bool isCheckLR;
        private bool isCheckMoveChaperToMoveItem = false;
        private bool mutiSelect;
        private bool isSelectionChangedSilent;
        private string settingIndexPivotImage = "indexPivotImage";
        private string uriChaper = "Urichaper";
        public DetailPage()
        {
            InitializeComponent();
            this.DataContext = models;
            this.Loaded += DetailPage_Loaded;
            this.models.valueCallBack = new Delegates.ValueProbar(this.DelegateValueProBar);
        }
        void DetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUISettingInView();
            this.GridHeaderWidth = this.ActualWidth / pivotDetail.Items.Count;
        }

        #region Property Light 
        public double LightBooth
        {
            get { return (double)GetValue(LightBoothProperty); }
            set { SetValue(LightBoothProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LightBooth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightBoothProperty =
            DependencyProperty.Register("LightBooth", typeof(double), typeof(DetailPage), new PropertyMetadata((double)0.1));
        #endregion

        #region Property IconChaperBookmask
        public bool IsChaperBookmask
        {
            get { return (bool)GetValue(IsChaperBookmaskProperty); }
            set { SetValue(IsChaperBookmaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChaperBookmask.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChaperBookmaskProperty =
            DependencyProperty.Register("IsChaperBookmask", typeof(bool), typeof(DetailPage), new PropertyMetadata(default(bool)));


        #endregion

        #region IconDownloadInView

        public bool IsDownloadInView
        {
            get { return (bool)GetValue(IsDownloadInViewProperty); }
            set { SetValue(IsDownloadInViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDownloadInView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDownloadInViewProperty =
            DependencyProperty.Register("IsDownloadInView", typeof(bool), typeof(DetailPage), new PropertyMetadata(default(bool)));


        #endregion

        #region GridHeaderWidthProperty
        public static readonly DependencyProperty GridHeaderWidthProperty = DependencyProperty.Register("GridHeaderWidth", typeof(double), typeof(DetailPage), new PropertyMetadata((double)0));

        public double GridHeaderWidth
        {
            get
            {
                return (double)base.GetValue(GridHeaderWidthProperty);
            }
            set
            {
                base.SetValue(GridHeaderWidthProperty, value);
            }
        }
        #endregion

        #region IsFlipViewOrSldeProperty
        public bool IsFlipViewOrSlde
        {
            get { return (bool)GetValue(IsFlipViewOrSldeProperty); }
            set { SetValue(IsFlipViewOrSldeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFlipViewOrSlde.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFlipViewOrSldeProperty =
            DependencyProperty.Register("IsFlipViewOrSlde", typeof(bool), typeof(DetailPage), new PropertyMetadata((bool)false, IsFlipViewOrSldeChanged));


        private static void IsFlipViewOrSldeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var t = obj as DetailPage;
            t.SetValueTransitionMode((bool)e.NewValue);
        }
        private void SetValueTransitionMode(bool value)
        {
            pivotImage.TransitionMode = value ? Telerik.Windows.Controls.SlideViewTransitionMode.Flip : Telerik.Windows.Controls.SlideViewTransitionMode.Slide;
            Check_FlipOrSlide.IsChecked = value;
        }
        #endregion

        #region ValueProBarTotalImageOnped


        public double ValueProBarTotalImageOneped
        {
            get { return (double)GetValue(ValueProBarTotalimageOnpedProperty); }
            set { SetValue(ValueProBarTotalimageOnpedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueProBarTotalimageOnped.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProBarTotalimageOnpedProperty =
            DependencyProperty.Register("ValueProBarTotalImageOneped", typeof(double), typeof(DetailPage), new PropertyMetadata((double)0, ValueProBarTotalimageOnpedChanged));

        private static void ValueProBarTotalimageOnpedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DetailPage t = d as DetailPage;
            t.SetValueProBarTotalImageOneped((double)e.NewValue);
        }

        private void SetValueProBarTotalImageOneped(double p)
        {

            proBarTotalImageOneped.Visibility = p == this.models.Images.Count - 1 ? Visibility.Collapsed : Visibility.Visible;
        }
        #endregion

        #region Sự kiện
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }
            if (e.Uri.ToString().Contains("Toast"))
            {
                gridImage.Visibility = System.Windows.Visibility.Collapsed;
                string uriManga = NavigationContext.QueryString["Toast"];
                Utils.SecondaryTileUriSource = "/Views/DetailPage.xaml?UriManga=" + uriManga + "";

                await this.models.LoadDetail(uriManga);
                gridDetail.DataContext = this.models.ItemDetail;

                if (this.models.ItemDetail.IsFavorite)
                {
                    var itemMangaFavorite = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlMangaFavorite>(t => t.Url == uriManga);
                    itemMangaFavorite.ChaperCount = this.models.Chapers.Count;
                    App.dbHelper.Update<MangaCore.Sqlite.Models.SqlMangaFavorite>(itemMangaFavorite);
                }

                AdsAdmob.LoadInterstitialAd();
                return;
            }
            if (e.Uri.ToString().Contains("UriManga"))
            {
                // Update UI
                gridImage.Visibility = System.Windows.Visibility.Collapsed;
                isPin = true;
                btnPin.Icon = Application.Current.Resources["UnPin"] as string;
                btnPin.Text = "Un pin";
                // End Update UI
                // QueryString
                string uriManga = NavigationContext.QueryString["UriManga"];
                Utils.SecondaryTileUriSource = "/Views/DetailPage.xaml?UriManga=" + uriManga + "";
                // End QueryString
                await this.models.LoadDetail(uriManga);
                gridDetail.DataContext = this.models.ItemDetail;
                // this.IsReading = this.models.ItemDetail.IsRead;
                //this.IsFavorite = this.models.ItemDetail.IsFavorite;
                AdsAdmob.LoadInterstitialAd();
                return;

            }
            if (App.IsTpyeNavigated == TypeNavigaed.Manga)
            {
                // Update UI
                gridImage.Visibility = System.Windows.Visibility.Collapsed;
                // End Update UI
                // Xu ly dữ liệu
                MangaOL.Models.Manga p = (MangaOL.Models.Manga)PhoneApplicationService.Current.State["param"];
                await this.models.LoadDetail(p.UriManga);
                gridDetail.DataContext = this.models.ItemDetail;

                // this.IsReading = this.models.ItemDetail.IsRead;
                // this.IsFavorite = this.models.ItemDetail.IsFavorite;
                this.models.InsertUpdateManga(new Models.Manga(p.UriManga, p.NameManga, p.UriCover, "", p.Sever, DateTime.Now.ToString(MangaCore.Comon.FormatDateTime)));
                // End xử lý dữ liệu
                // Kiểm tra Title được pin chưa
                Utils.SecondaryTileUriSource = "/Views/DetailPage.xaml?UriManga=" + p.UriManga + "";
                var shellTitle = Utils.FindTile(Utils.SecondaryTileUriSource);
                if (shellTitle != null)
                {
                    isPin = true;
                    btnPin.Icon = Application.Current.Resources["UnPin"] as string;
                    btnPin.Text = "Un pin";
                }
                if (this.models.ItemDetail.IsFavorite)
                {
                    var itemMangaFavorite = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlMangaFavorite>(t => t.Url == p.UriManga);
                    itemMangaFavorite.ChaperCount = this.models.Chapers.Count;
                    App.dbHelper.Update<MangaCore.Sqlite.Models.SqlMangaFavorite>(itemMangaFavorite);
                }
                // Kết thúc
                AdsAdmob.LoadInterstitialAd();
            }
            else if (App.IsTpyeNavigated == TypeNavigaed.Chaper)
            {
                MangaOL.Models.ChaperBookmask p = (MangaOL.Models.ChaperBookmask)PhoneApplicationService.Current.State["param"];
                itemChaperBookmask = App.ChaperBookmask.FirstOrDefault(t => t.Url == p.Url);
                var isDownloadInView = App.Downloads.FirstOrDefault(t => t.Url == p.Url) != null;
                itemChaper = new Models.Chaper(p.NameChaper, p.Url, p.DateTimeCreate, true, true, isDownloadInView, true, p.Index.ToString(), false);
                await this.models.LoadImages(p.Url);
                this.UpdateUI(itemChaper);
                AdsAdmob.LoadInterstitialAd();
            }
            else if (App.IsTpyeNavigated == TypeNavigaed.Download)
            {
                MangaOL.Models.Download p = (MangaOL.Models.Download)PhoneApplicationService.Current.State["param"];
                itemChaperBookmask = App.ChaperBookmask.FirstOrDefault(t => t.Url == p.Url);
                var isChaperBookmask = itemChaperBookmask != null;
                itemChaper = new Models.Chaper(p.NameChaper, p.Url, p.DateTimeCreate, true, true, true, isChaperBookmask, "0", false);
                await this.models.LoadImagesOff(p);
                this.UpdateUI(itemChaper);

            }
        }

        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (gridImage.Visibility == System.Windows.Visibility.Visible && App.IsTpyeNavigated == TypeNavigaed.Manga)
                {
                    if (stopDownload != null)
                    {
                        stopDownload.Cancel();
                        System.Diagnostics.Debug.WriteLine("cancel download");
                    }
                    Utils.DeleteAppSeting(settingIndexPivotImage);
                    Utils.DeleteAppSeting(uriChaper);
                    this.models.InsertUpdateChaper(new MangaCore.Sqlite.Models.SqlHistoryRead(App.NewSever, itemChaper.NameChaper, indexPivotImage, itemChaper.UrlChaper, this.models.ItemDetail.UrlManga));
                    gridImage.Visibility = System.Windows.Visibility.Collapsed;
                    if (detail.Orientation.ToString().Contains("Landscape"))
                    {
                        SystemTray.IsVisible = false;
                    }
                    else
                    {
                        SystemTray.IsVisible = true;
                    }
                    e.Cancel = true;
                    return;
                }
                if (this.mutiSelect)
                {
                    this.Select();
                }


            }

        }
        private void txtChapSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            lbChaper.ItemsSource = this.models.Chapers.Where(t => t.NameChaper.Contains(txtKeyWord.Text));
        }

        private void ClearSearchChaper_Tap(object sender, EventArgs e)
        {
            txtKeyWord.Text = "";
        }
        private void Back_Tap(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));

        }

        private void FavoriteManga_Tap(object sender, EventArgs e)
        {
            this.models.InsertDeleteFavorite(this.models.ItemDetail);
        }

        private void Share_Tap(object sender, EventArgs e)
        {
            Microsoft.Phone.Tasks.ShareLinkTask shareLinkTask = new Microsoft.Phone.Tasks.ShareLinkTask();

            shareLinkTask.Title = this.models.ItemDetail.NameManga;
            shareLinkTask.LinkUri = new Uri(this.models.ItemDetail.UrlManga, UriKind.Absolute);
            shareLinkTask.Message = "Hãy đọc truyện cùng MangaOL - Windows phone";

            shareLinkTask.Show();
        }

        private void Reading_Tap(object sender, EventArgs e)
        {
            this.Reading();
        }

        private void btnPin_Tap(object sender, EventArgs e)
        {
            this.PinOrUnPin();
        }
        private void GridHeaderPivot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            pivotDetail.SelectedIndex = Grid.GetColumn(sender as Grid);
        }

        private void pivotDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (this.ActualWidth != 0)
            {
                this.GridHeaderWidth = this.ActualWidth / this.pivotDetail.Items.Count;
                ((System.Windows.Media.Animation.DoubleAnimation)(this.HeaderPivotMove.Children[0])).To = this.GridHeaderWidth * this.pivotDetail.SelectedIndex;
                this.HeaderPivotMove.Begin();
            }
        }
        private void imageManga_Failed(object sender, ExceptionRoutedEventArgs e)
        {
            System.Windows.Media.Imaging.BitmapImage bm = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Assets/Images/NoImage.png", UriKind.RelativeOrAbsolute));
            (sender as Image).Source = bm;
        }
        private async void GridDown_Tap(object sender, EventArgs e)
        {

            MangaOL.Models.Chaper itemChaper = (sender as MangaOL.UserControls.Mdl2).DataContext as MangaOL.Models.Chaper;
            if (this.mutiSelect)
            {
                this.GridChapTapMutiSelect(itemChaper);
            }
            else
            {
                Models.ChaperBookmask itemChaperBookmask = App.ChaperBookmask.FirstOrDefault(t => t.Url == itemChaper.UrlChaper);
                await this.models.Download(itemChaper, itemChaperBookmask);
            }
        }
        private async void GridItemChaper_Tap(object sender, EventArgs e)
        {
            MangaOL.Models.Chaper item = (sender as MangaOL.UserControls.Mdl2).DataContext as MangaOL.Models.Chaper;
            if (this.mutiSelect)
            {
                this.GridChapTapMutiSelect(item);
            }
            else
            {
                if (item != null)
                    await this.models.LoadImages(item.UrlChaper);
                this.UpdateUI(item);
                // Hide_Image.Begin();
                gridImage.Visibility = System.Windows.Visibility.Visible;
            }

        }
        #region Option
        private void btnWindowsption_Tap(object sender, EventArgs e)
        {
            gridJump.Visibility = System.Windows.Visibility.Collapsed;
            gridLight.Visibility = System.Windows.Visibility.Collapsed;
            gridOption.Visibility = System.Windows.Visibility.Visible;
        }
        private void Light_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            gridOption.Visibility = System.Windows.Visibility.Collapsed;
            var tag = (sender as UserControls.Mdl2).Tag.ToString();
            if (tag.ToLower() == "close")
            {
                gridLight.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (tag.ToLower() == "light")
            {
                gridSettingInView.Visibility = System.Windows.Visibility.Collapsed;
                gridLight.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void JumpImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            gridOption.Visibility = System.Windows.Visibility.Collapsed;
            var tag = (sender as UserControls.Mdl2).Tag.ToString();
            switch (tag.ToLower())
            {
                case "jump":
                    gridSettingInView.Visibility = System.Windows.Visibility.Collapsed;
                    gridJump.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "close":
                    gridJump.Visibility = System.Windows.Visibility.Collapsed;
                    txtSttJump.Text = "";
                    break;
                case "check_jump":
                    Regex reg = new Regex("\\d+");
                    if (reg.IsMatch(txtSttJump.Text.Trim()))
                    {
                        int value = int.Parse(txtSttJump.Text.Trim());
                        if (value <= 0 || value > this.models.Images.Count)
                        {
                            txtSttJump.Text = "";
                            txtStatusJumpImage.Text = "Lỗi số thứ tự ảnh";
                        }
                        else
                        {
                            txtStatusJumpImage.Text = "";
                            this.isSelectionChangedSilent = true;
                            pivotImage.SelectedItem = this.models.Images[value - 1];
                        }
                    }
                    else
                    {
                        txtStatusJumpImage.Text = "Lỗi số thứ tự ảnh";
                    }
                    break;
                default:
                    break;
            }
        }
        private void SliderJumpImage_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.isSelectionChangedSilent = true;
            int i = (int)e.NewValue;
            pivotImage.SelectedItem = this.models.Images[i];

        }
        private async void SetLockScreen_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                if (!LockScreenManager.IsProvidedByCurrentApplication)
                {
                    await LockScreenManager.RequestAccessAsync();
                }

                if (LockScreenManager.IsProvidedByCurrentApplication)
                {
                    var uriImage = this.models.Images[indexPivotImage].Tag;
                    if (string.IsNullOrEmpty(uriImage))
                    {
                        uriImage = this.models.Images[indexPivotImage].URLImage;
                    }
                    LockScreenHeples.StartDownloadImagefromServer(uriImage);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có một số lỗi, vui lòng thử lại!");
            }
        }
        private void DownloadImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.DownloadImageToLib();
        }
        #endregion
      
        private void btnList_Tap(object sender, EventArgs e)
        {
            gridChapInView.Visibility = gridChapInView.Visibility == System.Windows.Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            lbChaperInView.UpdateLayout();
            lbChaperInView.SelectedIndex = indexChaper;

        }
        private void GridLesftInView_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            gridChapInView.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void btnLeft_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            if (isCheckLR)
                Nextitem();
            else
                BackItem();
        }
        private void btnRight_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!isCheckLR)
                Nextitem();
            else
                BackItem();
        }
        private void BackChaper_Tap(object sender, EventArgs e)
        {
            if (isOrder)
                this.BackChaper();
            else
                this.NextChaper();
            //   LoadAds();
        }
        private void NextChap_Tap(object sender, EventArgs e)
        {
            if (!isOrder)
                this.BackChaper();
            else
                this.NextChaper();
            //   LoadAds();
        }
        private void Bookmask_Tap(object sender, EventArgs e)
        {
            this.BookMask();
        }
        private void DownloadInView_Tap(object sender, EventArgs e)
        {
            this.DownloadInView();
        }
        private async void GridListChapInView_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MangaOL.Models.Chaper item = (sender as Border).DataContext as MangaOL.Models.Chaper;
            if (item != null)
                await this.models.LoadImages(item.UrlChaper);
            this.UpdateUI(item);
            gridChapInView.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void OrderChapers_Tap(object sender, EventArgs e)
        {
            for (int i = 0; i < this.models.Chapers.Count / 2; i++)
            {
                var item1 = this.models.Chapers[i];
                this.models.Chapers[i] = this.models.Chapers[this.models.Chapers.Count - i - 1];
                this.models.Chapers[this.models.Chapers.Count - i - 1] = item1;
            }
            this.isOrder = !this.isOrder;
        }
        private void pivotImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (this.ucImageInZoom)
            {
                return;
            }
            if (gridTop.Visibility == System.Windows.Visibility.Visible)
                Hide_Image.Begin();
            else if (gridTop.Visibility == System.Windows.Visibility.Collapsed)
                Show_Image.Begin();
        }
        private void ucImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var th = sender as UserControls.UcImage;
            if (th.InZoom)
            {
                return;
            }
            if (gridTop.Visibility == System.Windows.Visibility.Visible)
                Hide_Image.Begin();
            else if (gridTop.Visibility == System.Windows.Visibility.Collapsed)
                Show_Image.Begin();
        }
        private void pivotImage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectIndexRadSliderView(e);
            Models.Image item = pivotImage.SelectedItem as Models.Image;
            if (item == null) return;
            indexPivotImage = this.models.Images.IndexOf(item);
            //Set Value Slider Jump Image
            SliderJumpImage.Value = indexPivotImage;
            //End
            txtIndexPivotImage.Text = item.Index;
            if (!string.IsNullOrEmpty(item.Tag))
            {
                item.URLImage = item.Tag;
                //   item.Tag = "";
            }
            this.models.LoadImage(item);
        }
        private void UcImage_UcImageOpened(object s, EventArgs e)
        {
            MangaOL.Models.Image item = s as MangaOL.Models.Image;
            item.Visibility = System.Windows.Visibility.Collapsed;
            
        }
        private void CheckLR_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            isCheckLR = true;
            UpdateSettingInView((bool)checkBox.IsChecked, checkBox.Tag.ToString());
        }
        private void CheckLR_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            isCheckLR = false;
            UpdateSettingInView((bool)checkBox.IsChecked, checkBox.Tag.ToString());
        }
        private void CheckLipOrSlide_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            IsFlipViewOrSlde = true;
            UpdateSettingInView((bool)checkBox.IsChecked, checkBox.Tag.ToString());
        }
        private void CheckLipOrSlide_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            IsFlipViewOrSlde = false;
            UpdateSettingInView((bool)checkBox.IsChecked, checkBox.Tag.ToString());
        }
        private void CheckMoveChapToMoveItem_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            isCheckMoveChaperToMoveItem = true;
            UpdateSettingInView((bool)checkBox.IsChecked, checkBox.Tag.ToString());
        }
        private void CheckMoveChapToMoveItem_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            isCheckMoveChaperToMoveItem = false;
            UpdateSettingInView((bool)checkBox.IsChecked, checkBox.Tag.ToString());
        }
        private void CloseSettinginVIew_Tap(object sender, EventArgs e)
        {
            var tag = (sender as UserControls.Mdl2).Tag.ToString();
            switch (tag.ToLower())
            {
                case "option":
                    txtStatusJumpImage.Text = "";
                    gridOption.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                default:
                    gridSettingInView.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }
        }

        private void btnSetting_Tap(object sender, EventArgs e)
        {
            gridSettingInView.Visibility = System.Windows.Visibility.Visible;
        }
        private void detail_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if (gridImage.Visibility == System.Windows.Visibility.Visible)
            {
                return;
            }
            if (e.Orientation.ToString().Contains("Landscape"))
            {
                SystemTray.IsVisible = false;
            }
            else
            {
                SystemTray.IsVisible = true;
            }
        }
        private void MutiSelect_Tap(object sender, EventArgs e)
        {

            var list = this.models.Chapers.Where(t => t.IsSelect == false);
            if (list.Count() > 0)
            {
                (sender as MangaOL.UserControls.Mdl2).Text = "Hủy";
                foreach (var item in list)
                {
                    item.IsSelect = true;
                }
            }
            else
            {
                (sender as MangaOL.UserControls.Mdl2).Text = "Tất cả";
                foreach (var item in this.models.Chapers)
                {
                    item.IsSelect = false;
                }
            }

        }
        private void Select_Tap(object sender, EventArgs e)
        {
            this.Select();
            (sender as MangaOL.UserControls.Mdl2).Text = this.mutiSelect ? "Hủy" : "Chọn";
        }
        private async void DownloadMutiSelect_Tap(object sender, EventArgs e)
        {
            var list = this.models.Chapers.Where(t => t.IsSelect == true);
            foreach (var item in list)
            {
                MangaOL.Models.Chaper itemChaper = item as MangaOL.Models.Chaper;
                Models.ChaperBookmask itemChaperBookmask = App.ChaperBookmask.FirstOrDefault(t => t.Url == itemChaper.UrlChaper);
                await this.models.Download(itemChaper, itemChaperBookmask);
            }
        }
        #endregion
        #region Funcs
        private async void DownloadImageToLib()
        {
            var uriImage = this.models.Images[indexPivotImage].Tag;
            if (string.IsNullOrEmpty(uriImage))
            {
                uriImage = this.models.Images[indexPivotImage].URLImage;
            }
            var content = await MangaCore.Utils.DownloadByteToUriImage(uriImage);
            Microsoft.Xna.Framework.Media.MediaLibrary lib = new Microsoft.Xna.Framework.Media.MediaLibrary();
            lib.SavePictureToCameraRoll("HubComics", content);
            MessageBox.Show("Đã lưu!!!");
        }

        private void SelectIndexRadSliderView(SelectionChangedEventArgs e)
        {
            if (isSelectionChangedSilent)
            {
                Models.Image fromItem = e.RemovedItems[0] as Models.Image;
                Models.Image toItem = e.AddedItems[0] as Models.Image;

                var fromIndex = this.models.Images.IndexOf(fromItem);
                var toIndex = this.models.Images.IndexOf(toItem);


                if (Math.Abs(fromIndex - toIndex) > 1)
                {
                    isSelectionChangedSilent = false;
                    if (toIndex > 0)
                    {
                        pivotImage.SelectedItem = this.models.Images[toIndex - 1];
                        pivotImage.MoveToNextItem();
                    }
                    else
                    {
                        pivotImage.SelectedItem = this.models.Images[toIndex + 1];
                        pivotImage.MoveToPreviousItem();
                    }
                }
            }
        }
        private void Select()
        {
            if (this.mutiSelect)
            {
                var list = this.models.Chapers.Where(t => t.IsSelect == true);
                foreach (var item in list)
                {
                    item.IsSelect = false;
                }
            }
            this.btnSelectAll.Text = "Tất cả";
            this.btnDownloadAll.IsEnabledMdl2 = !this.btnDownloadAll.IsEnabledMdl2;
            this.btnSelectAll.IsEnabledMdl2 = !this.btnSelectAll.IsEnabledMdl2;
            mutiSelect = !mutiSelect;
        }

        private void GridChapTapMutiSelect(Models.Chaper item)
        {
            if (item != null)
                item.IsSelect = !item.IsSelect;
            var list = this.models.Chapers.Where(t => t.IsSelect == false);
            if (list.Count() != this.models.Chapers.Count)
            {
                btnSelectAll.Text = list.Count() != this.models.Chapers.Count ? "Tất cả" : "Hủy";
            }
        }
        private async void Reading()
        {
            if (this.models.ItemDetail.IsRead)
            {
                gridImage.Visibility = System.Windows.Visibility.Visible;
                MangaCore.Sqlite.Models.SqlHistoryRead item = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlHistoryRead>().Where(t => t.UrlManga == this.models.ItemDetail.UrlManga).ToList().OrderByDescending(t => t.DateTimeCreate).ToList()[0];
                await this.models.LoadImages(item.UrlChaper);
                bool isDownload = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlDownload>(t => t.Url == item.UrlChaper) != null;
                bool isBookmask = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlChaperBookmask>(t => t.Url == item.UrlChaper) != null;
                this.UpdateUI(this.models.Chapers.FirstOrDefault(t => t.UrlChaper == item.UrlChaper));
                for (int i = 0; i < item.Index; i++)
                {
                    pivotImage.MoveToNextItem(false);
                }
            }
            else
            {
                Utils.ShowMessage("Chức năng ĐỌC TIẾP chỉ có tác dụng khi bạn đã đọc truyện này rồi.", "Thông báo");
            }
        }
        private void PinOrUnPin()
        {
            if (!isPin)
            {
                Utils.Pin(this.models.ItemDetail.NameManga, this.models.ItemDetail.UrlCover);
                btnPin.Icon = Application.Current.Resources["UnPin"] as string;
                btnPin.Text = "Un Pin";
            }
            else
            {
                Utils.UnPin();
                btnPin.Icon = Application.Current.Resources["Pin"] as string;
                btnPin.Text = "Pin";
            }
            isPin = !isPin;
        }

        System.Threading.CancellationTokenSource stopDownload;
        System.Threading.Tasks.Task task;
        private void UpdateUI(Models.Chaper item)
        {
            //Set Maxmuin Slider Jump
            if (this.models.Images != null)
            {
                SliderJumpImage.Maximum = this.models.Images.Count - 1;
            }
            //End
            SystemTray.IsVisible = false;
            if (itemChaper != null)
                itemChaper.IsReading = false;
            itemChaper = item;
            item.IsRead = true;
            item.IsReading = true;
            IsChaperBookmask = item.IsFavorite;
            IsDownloadInView = item.IsDownload;

            if (App.IsTpyeNavigated == TypeNavigaed.Manga)
            {
                txtHeaderChap.Text = item.NameChaper.Contains(this.models.ItemDetail.NameManga) ? item.NameChaper : this.models.ItemDetail.NameManga + " " + item.NameChaper;
                var nameChaper = item.NameChaper.Contains(this.models.ItemDetail.NameManga) ? item.NameChaper : this.models.ItemDetail.NameManga + " " + item.NameChaper;
                itemChaperBookmask = new Models.ChaperBookmask(App.NewSever, nameChaper, this.models.ItemDetail.UrlCover, item.UrlChaper, DateTime.Now.ToString(MangaCore.Comon.FormatDateTime), 0);
                indexChaper = this.models.Chapers.IndexOf(item);
                btnNextChaper.IsEnabledMdl2 = indexChaper == 0;
                btnBackChaper.IsEnabledMdl2 = indexChaper == this.models.Chapers.Count - 1;
                //Set Value Slider Jump
                if (this.models.Images != null)
                {
                    SliderJumpImage.Value = 0;
                }
                //End
                LoadImageTask();

            }
            else if (App.IsTpyeNavigated == TypeNavigaed.Chaper)
            {
                //  if (this.models.Images == null) return;

                btnList.IsEnabledMdl2 = true;
                btnNextChaper.IsEnabledMdl2 = true;
                btnBackChaper.IsEnabledMdl2 = true;
                txtHeaderChap.Text = item.NameChaper;

                for (int i = 0; i < int.Parse(item.Index); i++)
                {
                    pivotImage.MoveToNextItem(false);
                }
                //Set Value Slider Jump
                if (this.models.Images != null)
                {
                    SliderJumpImage.Value = double.Parse(item.Index);
                }
                //End
                LoadImageTask();
            }
            else if (App.IsTpyeNavigated == TypeNavigaed.Download)
            {
                btnList.IsEnabledMdl2 = true;
                btnBookmaskChaper.IsEnabledMdl2 = true;
                btnNextChaper.IsEnabledMdl2 = true;
                btnBackChaper.IsEnabledMdl2 = true;
                txtHeaderChap.Text = item.NameChaper;
            }
            //Insert Or Update Chaper History

            this.models.InsertUpdateChaper(new MangaCore.Sqlite.Models.SqlHistoryRead(App.NewSever, item.NameChaper, 0, item.UrlChaper, this.models.ItemDetail.UrlManga));
        }
        private void LoadImageTask()
        {

            if (stopDownload != null)
            {
                stopDownload.Cancel();
            }
            stopDownload = new System.Threading.CancellationTokenSource();
            task = new System.Threading.Tasks.Task(delegate
            {
                this.models.StartDownloadImageToByte(stopDownload.Token);
            });
            task.Start();
        }
        void BackItem(bool moveChaper = false)
        {
            if (indexPivotImage > 0 && !moveChaper)
            {
                pivotImage.MoveToPreviousItem();
            }
            else
            {
                if (App.IsTpyeNavigated == TypeNavigaed.Chaper && App.IsTpyeNavigated == TypeNavigaed.Download)
                    return;
                int num = 1;
                if (isCheckMoveChaperToMoveItem)
                {
                    num = Utils.Show("Thông báo", "Bạn có muốn chuyển về chaper trước không?", new string[] { "Không", "Có" }, 1);
                }
                if (num == 1)
                {
                    //    isNotication = true;
                    if (isOrder)
                        this.BackChaper();
                    else
                        this.NextChaper();
                    // LoadAds();
                }
            }

        }
        void Nextitem(bool moveChaper = false)
        {
            if (indexPivotImage < this.models.Images.Count - 1 && !moveChaper)
            {
                pivotImage.MoveToNextItem();
            }//
            else
            {
                if (App.IsTpyeNavigated == TypeNavigaed.Chaper && App.IsTpyeNavigated == TypeNavigaed.Download)
                    return;
                int num = 1;
                if (isCheckMoveChaperToMoveItem)
                {
                    num = Utils.Show("Thông báo", "Bạn có muốn chuyển đến chaper sau không?", new string[] { "Không", "Có" }, 1);
                }
                if (num == 1)
                {
                    //  isNotication = true;
                    if (!isOrder)
                        this.BackChaper();
                    else
                        this.NextChaper();
                    // LoadAds();
                }
            }
        }
        private async void NextChaper()
        {
            if (indexChaper < this.models.Chapers.Count - 1)
            {
                var item = this.models.Chapers[indexChaper + 1];
                if (item != null)
                    await this.models.LoadImages(item.UrlChaper);
                this.UpdateUI(item);
                //    if (isNotication)
                //  {
                txtNotication.Text = isOrder ? "Next chaper" : "Back chaper";
                Show_Notication.Begin();

                LoadAds();
                //  }
            }
            else
                Utils.ShowMessage("Bạn đang đọc ở chaper đầu tiền rồi.", "Thông báo");
        }
        private async void BackChaper()
        {
            if (indexChaper > 0)
            {
                var item = this.models.Chapers[indexChaper - 1];
                if (item != null)
                    await this.models.LoadImages(item.UrlChaper);
                this.UpdateUI(item);
                //    if (isNotication)
                //  {
                txtNotication.Text = !isOrder ? "Next chaper" : "Back chaper";
                Show_Notication.Begin();
                // isNotication = false;
                LoadAds();
                // }
            }
            else
                Utils.ShowMessage("Bạn đang đọc ở chaper cuối cùng rồi.", "Thông báo");
        }
        private void BookMask()
        {
            if (this.IsChaperBookmask)
            {
                App.ChaperBookmask.Remove(App.ChaperBookmask.FirstOrDefault(t => t.Url == itemChaper.UrlChaper));
                App.dbHelper.Delete<MangaCore.Sqlite.Models.SqlChaperBookmask>(App.dbHelper.Select<MangaCore.Sqlite.Models.SqlChaperBookmask>(t => t.Url == itemChaper.UrlChaper));
            }
            else
            {
                App.ChaperBookmask.Insert(0, new Models.ChaperBookmask(itemChaperBookmask.Sever, itemChaperBookmask.NameChaper, this.models.ItemDetail.UrlCover, itemChaperBookmask.Url, DateTime.Now.ToString(MangaCore.Comon.FormatDateTime), indexPivotImage));
                App.dbHelper.Insert<MangaCore.Sqlite.Models.SqlChaperBookmask>(new MangaCore.Sqlite.Models.SqlChaperBookmask(itemChaperBookmask.Sever, itemChaperBookmask.NameChaper, this.models.ItemDetail.UrlCover, itemChaperBookmask.Url, DateTime.Now.ToString(MangaCore.Comon.FormatDateTime), indexPivotImage));
            }
            this.IsChaperBookmask = !this.IsChaperBookmask;
        }
        private async void DownloadInView()
        {
            if (this.IsDownloadInView)
            {
                await this.modelsMainPageVM.DeleteDownLoad(App.Downloads.FirstOrDefault(t => t.Url == itemChaper.UrlChaper));
                if (App.IsTpyeNavigated == TypeNavigaed.Download)
                    btnDownloadInView.IsEnabledMdl2 = true;
            }
            else
            {
                await this.models.Download(itemChaper, itemChaperBookmask);
            }
            this.IsDownloadInView = !this.IsDownloadInView;
        }
        void UpdateSettingInView(bool value, string name)
        {
            System.IO.IsolatedStorage.IsolatedStorageSettings applicationSettings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
            if (!applicationSettings.Contains(name))
            {
                applicationSettings.Add(name, value);
            }
            else
            {
                applicationSettings[name] = value;
            }
            applicationSettings.Save();
        }
        void UpdateUISettingInView()
        {
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains(Check_Left_Right.Tag.ToString()))
            {
                this.isCheckLR = (bool)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings[Check_Left_Right.Tag.ToString()];
                Check_Left_Right.IsChecked = this.isCheckLR;
            }
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains(Check_FlipOrSlide.Tag.ToString()))
            {
                this.IsFlipViewOrSlde = (bool)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings[Check_FlipOrSlide.Tag.ToString()];

            }
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains(Check_MoveChapToMoveItem.Tag.ToString()))
            {
                this.isCheckMoveChaperToMoveItem = (bool)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings[Check_MoveChapToMoveItem.Tag.ToString()];
                Check_MoveChapToMoveItem.IsChecked = this.isCheckMoveChaperToMoveItem;

            }
        }
        #endregion
        #region Ads
        private int countLoadAdsFull = 1;
        void LoadAds()
        {
            // this.isReading = true;
            // AdsAdmob.LoadInterstitialAd();
            if (this.countLoadAdsFull == 2)
            {
                this.countLoadAdsFull = 1;
                AdsAdmob.LoadInterstitialAd();
            }
            else
                this.countLoadAdsFull++;
        }
        #endregion

        private bool ucImageInZoom;
        private void ucImage_ZoomOut(object s, EventArgs e)
        {
            this.ucImageInZoom = (s as UserControls.UcImage).InZoom;
        }

        private void ucImage_ZoomIn(object s, EventArgs e)
        {
            this.ucImageInZoom = (s as UserControls.UcImage).InZoom;
        }

    }
}