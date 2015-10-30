using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MangaOL.Resources;
using MangaOL.ViewModels;
//using GoogleAds;
using System.Windows.Media;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using MangaOL.Models;
using System.Windows.Media.Animation;
using GoogleAds;
using Microsoft.Phone.Net.NetworkInformation;
using MangaCore;
using Microsoft.Phone.Notification;
using System.Text;

namespace MangaOL
{
    public partial class MainPage : PhoneApplicationPage
    {

        private MainPageVM models = new MainPageVM();
        private GoogleAds.InterstitialAd adsInter;
        private bool isShowSearch;
        private bool isSearch = false;
        bool isShowAppBar;
        private string keyWord;
        private bool isSearchOnline = true;

        #region GridHeaderWidthProperty

        public static readonly DependencyProperty GridHeaderWidthProperty = DependencyProperty.Register("GridHeaderWidth", typeof(double), typeof(MainPage), new PropertyMetadata((double)0));

        // public InterstitialAd adsInter;
        public double GridHeaderWidth
        {
            get
            {
                return (double)base.GetValue(MainPage.GridHeaderWidthProperty);
            }
            set
            {
                base.SetValue(MainPage.GridHeaderWidthProperty, value);
            }
        }
        #endregion

        public bool IsSidePanelOpen
        {
            get
            {
                CompositeTransform compositeTransform = this.gridSide.RenderTransform as CompositeTransform;
                return compositeTransform.TranslateX == 0.0;
            }
        }
        public MainPage()
        {
            HttpNotificationChannel pushChannel;
            string channelName = "ToastSampleChannel";
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.DataContext = models;

            pushChannel = HttpNotificationChannel.Find(channelName);

            // If the channel was not found, then create a new connection to the push service.
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);

                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                pushChannel.Open();

                // Bind this new channel for toast events.
                pushChannel.BindToShellToast();

            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
                // MessageBox.Show(String.Format("Channel Uri is {0}",
                //  pushChannel.ChannelUri.ToString()));

            }
            Windows.Networking.Connectivity.NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;

        }
        #region Push Notication
        void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {

            Dispatcher.BeginInvoke(() =>
            {
                // Display the new URI for testing purposes.   Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                //    MessageBox.Show(String.Format("Channel Uri is {0}",
                //      e.ChannelUri.ToString()));

            });
        }
        void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // Error handling logic for your particular application would be here.
            Dispatcher.BeginInvoke(() =>
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                    e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData))
                    );
        }
        void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Received Toast {0}:\n", DateTime.Now.ToShortTimeString());

            // Parse out the information that was part of the message.
            foreach (string key in e.Collection.Keys)
            {
                message.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

                if (string.Compare(
                    key,
                    "wp:Param",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.CompareOptions.IgnoreCase) == 0)
                {
                    relativeUri = e.Collection[key];
                }
            }

            // Display a dialog of all the fields in the toast.
            Dispatcher.BeginInvoke(() => MessageBox.Show(message.ToString()));

        }

        #endregion
        void NetworkInformation_NetworkStatusChanged(object sender)
        {
            PrintNetworkStatus();
        }
        private void PrintNetworkStatus()
        {
            //Dispatcher.BeginInvoke(() =>
            //    Utils.PrintNetworkStatus(NetworkInterface.GetIsNetworkAvailable())
            //);
        }
        private void RateApp()
        {
            string AppView = "CountViewApp";
            string count = MangaCore.Utils.SaveAppSeting(AppView, "1", true);
            if (string.IsNullOrEmpty(count))
            {
                MangaCore.Utils.SaveAppSeting(AppView, "1");
            }
            else
            {
                if (int.Parse(count) == 5)
                {
                    int num = Utils.Show("Thông báo", "Bạn có thể dành chút thời gian để ủng hộ và đánh giá ứng dụng tôi trên cửa hàng không?", new string[] { "Đồng ý", "Lần sau" }, 0);
                    if (num == 0)
                    {
                        MangaCore.Utils.SaveAppSeting(AppView, "6");
                        Microsoft.Phone.Tasks.MarketplaceReviewTask marketplaceReviewTask = new Microsoft.Phone.Tasks.MarketplaceReviewTask();
                        marketplaceReviewTask.Show();
                    }
                }
                else if (int.Parse(count) < 5)
                {
                    int c = int.Parse(count) + 1;
                    MangaCore.Utils.SaveAppSeting(AppView, c.ToString());
                }
                else
                    return;
            }
        }
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            this.GridHeaderWidth = this.ActualWidth / this.pivotMain.Items.Count;
            this.models.LoadPivot(1);
            this.models.LoadPivot(2);
            this.models.LoadPivot(3);
            this.models.LoadPivot(4);


        }
        #region Sự kiện
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (this.IsSidePanelOpen)
            {
                this.closeSidePanel.Begin();
                e.Cancel = true;
            }
            else if (stackSearch.Visibility == System.Windows.Visibility.Visible)
            {
                Hide_SettingSearch.Begin();
                e.Cancel = true;
            }
            else if (this.isShowSearch)
            {
                this.HideGridSearch.Begin();
                SystemTray.IsVisible = true;
                this.txtKeyWord.Text = this.keyWord = "";
                this.isShowSearch = !this.isShowSearch;

                e.Cancel = true;
            }
            else
            {
                int result = Utils.Show("Thông báo", "Bạn muốn thoát hay tiếp tục sử dụng ứng dụng?", new string[]
                    {
                       "Sử dụng", "Thoát"
                    }, 0);
                if (result == 0)
                {
                    e.Cancel = true;
                    return;
                }

            }

        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            await Utils.CheckPubic();
            if (!App.settings.Contains("sever"))
            {
                App.settings.Add("sever", MangaCore.Utils.GetEnumDescription(MangaCore.Comon.TitleSever.truyentranh8));
                App.settings.Save();
            }
            if (IsolatedStorageSettings.ApplicationSettings.Contains("sever") && string.IsNullOrEmpty(App.NewSever))
            {
                App.NewSever = App.Oldsever = (IsolatedStorageSettings.ApplicationSettings["sever"] as string);
            }
            this.txtSever.Text = App.NewSever;
            if (e.NavigationMode == NavigationMode.Back && App.Oldsever != App.NewSever)
            {
                App.Oldsever = App.NewSever;
                this.models.Mangas.Clear();
                this.models.IsLoadManga = LoadState.None;
                await this.models.LoadMenuManga();
                await this.models.LoadManga(true, "");
            }
            else if (e.NavigationMode != NavigationMode.Back || !(App.Oldsever == App.NewSever))
            {

                App.ForderDownload = await Utils.CreateFolder(App.NameFolder, App.Local);
                await this.models.LoadMenuManga();
                await this.models.LoadManga(true, "");
                RateApp();
            }
        }

        private void Info_Tap(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/SetingPage.xaml?index=1", UriKind.RelativeOrAbsolute));
        }

        private async void MenuItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MangaOL.Models.MenuItem menuItem = ((FrameworkElement)sender).DataContext as MangaOL.Models.MenuItem;
            this.models.IsLoadManga = LoadState.None;
            await this.models.LoadManga(true, menuItem.Action);
            this.closeSidePanel.Begin();
        }

       private void GridItemManga_Tap(object sender, EventArgs e)
        {
            var g = sender as UserControls.TemplateHomeHoz;
            if (g == null) return;
            //StoryboardLongList(g);
            Manga manga = g.DataContext as Manga;
            this.ReadManga(manga);
        }

       
       
        private void ReadManga(Manga manga)
        {
            App.IsTpyeNavigated = TypeNavigaed.Manga;
            PhoneApplicationService.Current.State["param"] = manga;
            this.NavigationService.Navigate(new Uri("/Views/DetailPage.xaml", UriKind.RelativeOrAbsolute));
        }


        private async void txtKeyWord_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && pivotMain.SelectedIndex == 0 && isSearchOnline)
            {
                this.isSearch = true;
                this.keyWord = this.txtKeyWord.Text.Trim();
                await this.models.SearchManga(this.keyWord, true);
            }
        }

        private void Remove_Tap(object sender, EventArgs e)
        {
            this.txtKeyWord.Text = "";
            this.txtKeyWord.Focus();
        }

        private async void LvMain_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            Manga manga = e.Container.DataContext as Manga;
            int index = this.models.Mangas.IndexOf(manga);
            if (this.models.Mangas.IndexOf(manga) == this.models.Mangas.Count - 1 && manga != null && isSearchOnline)
            {
                if (this.isShowSearch)
                {
                    await this.models.SearchManga(this.keyWord, false);
                }
                else
                {
                    await this.models.LoadManga(false, App.UrlMenu);
                }
            }
        }
        private void pivotMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.models.LoadPivot(this.pivotMain.SelectedIndex);
            if (this.ActualWidth != 0)
            {
                this.GridHeaderWidth = this.ActualWidth / this.pivotMain.Items.Count;
                ((DoubleAnimation)(this.HeaderPivotMove.Children[0])).To = this.GridHeaderWidth * this.pivotMain.SelectedIndex;
                this.HeaderPivotMove.Begin();
            }
        }
        private void GridHeaderPivot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.pivotMain.SelectedIndex = Grid.GetColumn(sender as Grid);
        }

        private async void btnDeleteAll_Tap(object sender, EventArgs e)
        {
            await this.models.DeleteAll(this.pivotMain.SelectedIndex);
        }

        private async void btnReset_Tap(object sender, EventArgs e)
        {
            await this.models.LoadManga(true, App.UrlMenu);
        }
        private void Menu_Tap(object sender, EventArgs e)
        {
            if (this.IsSidePanelOpen)
            {
                this.closeSidePanel.Begin();
            }
            else
            {
                this.openSidePanel.Begin();
            }
        }

        private void Seting_Tap(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/SetingPage.xaml?index=2", UriKind.RelativeOrAbsolute));
        }
        private void Sever_Tap(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/SetingPage.xaml?index=0", UriKind.RelativeOrAbsolute));
        }
        private void Mail_Tap(object sender, EventArgs e)
        {
            Utils.SendMail();
        }

        private void Grid_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            CompositeTransform compositeTransform = this.gridSide.RenderTransform as CompositeTransform;
            double num = compositeTransform.TranslateX + e.DeltaManipulation.Translation.X;
            if (num < 0.0 && num > -this.gridSide.Width)
            {
                compositeTransform.TranslateX = num;
            }
        }

        private void Grid_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (e.FinalVelocities.LinearVelocity.X > 0.0)
            {
                this.openSidePanel.Begin();
            }
            else
            {
                this.closeSidePanel.Begin();
            }
        }

        private void BtnAppBar_Tap(object sender, EventArgs e)
        {
            if (!isShowAppBar)
                ShowAppBar.Begin();
            else
                HideAppBar.Begin();
            isShowAppBar = !isShowAppBar;
        }

        private void Search_Tap(object sender, EventArgs e)
        {
            if (!this.isShowSearch)
            {
                this.ShowGridSearch.Begin();
                SystemTray.IsVisible = false;
                this.txtKeyWord.Focus();
            }
            this.isShowSearch = !this.isShowSearch;
        }

        private void RateApp_Tap(object sender, EventArgs e)
        {
            Microsoft.Phone.Tasks.MarketplaceReviewTask marketplaceReviewTask = new Microsoft.Phone.Tasks.MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void UcDownload_GridOffTap(object sender, EventArgs e)
        {
            var item = sender as Models.Download;
            App.IsTpyeNavigated = TypeNavigaed.Download;
            PhoneApplicationService.Current.State["param"] = item;
            NavigationService.Navigate(new Uri("/Views/DetailPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private async void UcDownload_GridDeleteTap(object sender, EventArgs e)
        {
            var item = sender as Models.Download;
            await this.models.DeleteDownLoad(item);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.isSearchOnline = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.isSearchOnline = false;
        }

        private void SettingSearch_Tap(object sender, EventArgs e)
        {
            if (stackSearch.Visibility == System.Windows.Visibility.Collapsed)
                Show_SettingSearch.Begin();
            else
                Hide_SettingSearch.Begin();
        }
        #endregion
        private void txtKeyWord_Changed(object sender, TextChangedEventArgs e)
        {
            switch (pivotMain.SelectedIndex)
            {
                case 0:
                    if (!this.isSearchOnline)
                    {
                        if (txtKeyWord.Text == "")
                        {
                            //LvMain.ItemsSource.Clear();
                            LvMain.ItemsSource = this.models.Mangas;
                           
                            if (gridSearch.Visibility == System.Windows.Visibility.Collapsed)
                            {
                                this.isSearchOnline = true;
                                radSearchOnline.IsChecked = true;
                                radSearchOffline.IsChecked = false;
                            }
                        }
                        else
                            LvMain.ItemsSource = this.models.Mangas.Where(t => t.NameManga.ToUpper().Contains(txtKeyWord.Text.ToUpper())).ToList();
                    }

                    break;
                case 1:
                    this.lb.ItemsSource = this.models.MangasH.Where(t => t.NameManga.ToUpper().Contains(txtKeyWord.Text.ToUpper()));
                    break;
                case 2:
                    this.lbFavoriteManga.ItemsSource = this.models.MangaFavorite.Where(t => t.NameManga.ToUpper().Contains(txtKeyWord.Text.ToUpper()));
                    break;
                case 3:
                    this.lbChaperBookmask.ItemsSource = this.models.ChaperBookmask.Where(t => t.NameChaper.ToUpper().Contains(txtKeyWord.Text.ToUpper()));
                    break;
                case 4:
                    this.lbDownload.ItemsSource = this.models.Downloads.Where(t => t.NameChaper.ToUpper().Contains(txtKeyWord.Text.ToUpper()));
                    break;
                default:
                    break;
            }
            //filter items with help of lambda expression 

          
        }

        private void StoryboardLongList(Grid ui)
        {
            Storyboard sb = ui.Resources["itemSb"] as Storyboard;
            sb.Begin();
        }
        private bool ItemTemplateLongListMainHoz;
        private void btnTemplateLongListMian_Tap(object sender, EventArgs e)
        {
            if (ItemTemplateLongListMainHoz)
            {
                (sender as UserControls.Mdl2).Icon = Application.Current.Resources["List"] as string;
                LvMain.GridCellSize = new Size(155, 290);
                LvMain.LayoutMode = LongListSelectorLayoutMode.Grid;
                LvMain.ItemTemplate = this.Resources["myCell3"] as DataTemplate;
            }
            else
            {
                (sender as UserControls.Mdl2).Icon = Application.Current.Resources["Grid"] as string;
                LvMain.GridCellSize = new Size();
                LvMain.LayoutMode = LongListSelectorLayoutMode.List;
                LvMain.ItemTemplate = this.Resources["myCell3Hoz"] as DataTemplate;
            }
            ItemTemplateLongListMainHoz = !ItemTemplateLongListMainHoz;
            
        }

        private void GridItemMangaNewRead_Tap(object sender, EventArgs e)
        {
            var g = sender as UserControls.TemplateNewRead;
            if (g == null) return;
            Manga manga = g.DataContext as Manga;
            this.ReadManga(manga);
        }

        private void btnDeleteNEwRead_Tap(object sender, EventArgs e)
        {
            Manga p = (sender as MangaOL.UserControls.TemplateNewRead).DataContext as Manga;
            App.MangasH.Remove(p);
            App.dbHelper.Delete<MangaCore.Sqlite.Models.SqlMangaHistory>(App.dbHelper.Select<MangaCore.Sqlite.Models.SqlMangaHistory>(t => t.Url == p.UriManga));
        }

        private void TempalteHomeVez_Tap(object sender, EventArgs e)
        {
            var b = sender as UserControls.TemplateHomeVez;
            if (b == null) return;
            Manga manga = b.DataContext as Manga;
            this.ReadManga(manga);
        }

        private void TemplateHomeHoz_Tap(object sender, EventArgs e)
        {
            var b = sender as UserControls.TemplateHomeHoz;
            if (b == null) return;
            Manga manga = b.DataContext as Manga;
            this.ReadManga(manga);
        }

        private void TemplateFavorite_Tap(object sender, EventArgs e)
        {
            var g = sender as UserControls.TemplateFavorite;
            if (g == null) return;
            Manga manga = g.DataContext as Manga;
            this.ReadManga(manga);
        }

        private void btnNotication_Tap(object s, EventArgs e)
        {
            var item = (s as MangaOL.UserControls.TemplateFavorite).DataContext as Manga;
            item.IsNotication = !item.IsNotication;
            var itemSql = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlMangaFavorite>(t => t.Url == item.UriManga);
            itemSql.IsNotication = !itemSql.IsNotication;
            App.dbHelper.Update<MangaCore.Sqlite.Models.SqlMangaFavorite>(itemSql);
        }

        private void TemplateChappBookMask_Tap(object s, EventArgs e)
        {
            var g = s as UserControls.TemplateChapBookMask;
            if (g == null) return;
            ChaperBookmask p = g.DataContext as ChaperBookmask;
            App.IsTpyeNavigated = TypeNavigaed.Chaper;
            PhoneApplicationService.Current.State["param"] = p;
            NavigationService.Navigate(new Uri("/Views/DetailPage.xaml", UriKind.RelativeOrAbsolute));
        }



    }
}