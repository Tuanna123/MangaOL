using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Reflection;
using MangaCore;

namespace MangaOL.Views
{
    public partial class SetingPage : PhoneApplicationPage
    {
        public SetingPage()
        {
            InitializeComponent();
            txtAppName.Text = MangaCore.Comon.AppName;
        }


        public string VersionMyApp
        {
            get { return (string)GetValue(VersionMyAppProperty); }
            set { SetValue(VersionMyAppProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VersionMyApp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VersionMyAppProperty =
            DependencyProperty.Register("VersionMyApp", typeof(string), typeof(SetingPage), new PropertyMetadata((string)""));

       // bool _fullSever;
        bool _checkUIColor;
        static bool isFirst = true;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            FullSever(true);
            _checkUIColor = Boolean.Parse(MangaCore.Utils.SaveAppSeting("UIColor", "false", true, false).ToString());
            check_UIColor.IsChecked = _checkUIColor;
           //hiển thị thông tin version
            this.VersionMyApp = " " + Utils.GetVersionApp();
            //End
            int selectedIndex = int.Parse(NavigationContext.QueryString["index"].ToString());
            this.pivot.SelectedIndex = selectedIndex;
            //18+
            this.Check_18_cong.IsChecked = App._18_cong;
            //Notication
            bool _notication = Boolean.Parse(MangaCore.Utils.SaveAppSeting(MangaCore.Comon.NoticationChaper, "true", true,false).ToString());
            bool _onlyWifi = Boolean.Parse(MangaCore.Utils.SaveAppSeting(MangaCore.Comon.OnlyWifi, "false", true,false).ToString());
            
            CheckNoticationOnlyWifi.Visibility = _notication ? Visibility.Visible : System.Windows.Visibility.Collapsed;
            CheckNoticationMangaFavorite.IsChecked = _notication;
            CheckNoticationOnlyWifi.IsChecked = _onlyWifi;
            //End notication
        }

      
        private void RadioButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            System.IO.IsolatedStorage.IsolatedStorageSettings applicationSettings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
            if (!applicationSettings.Contains("sever"))
            {
                applicationSettings.Add("sever", radioButton.Content);
            }
            else
            {
                applicationSettings["sever"] =  radioButton.Content;
            }
            App.NewSever = radioButton.Content.ToString();
            applicationSettings.Save();
        }
        private void Back_Tap(object sender, EventArgs e)
        {
            base.NavigationService.GoBack();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            this.MuoiTamCongChanged((bool)check.IsChecked);
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            this.MuoiTamCongChanged((bool)check.IsChecked);
        }
        private void HyperlinkButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Utils.SendMail();
        }
        private void btnDelete_Tap(object sender, EventArgs e)
        {
            var result = Utils.ShowMessage("Bạn có chắc chắn không?", "Thông báo", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var list = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlHistoryRead>();
                if (list.Count > 0)
                {
                    App.dbHelper.Delete<MangaCore.Sqlite.Models.SqlHistoryRead>();
                    Utils.ShowMessage("Đã xóa thành công.", "Success");
                }
                else
                {
                    Utils.ShowMessage("Không có dữ liệu để xóa.", "Thông báo");
                }
            }
        }
        private void CheckBoxNotication_Checked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            MangaCore.Utils.SaveAppSeting(MangaCore.Comon.NoticationChaper, check.IsChecked.Value.ToString());
            CheckNoticationOnlyWifi.Visibility = System.Windows.Visibility.Visible;
        }
        private void CheckBoxNotication_Unchecked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            MangaCore.Utils.SaveAppSeting(MangaCore.Comon.NoticationChaper, check.IsChecked.Value.ToString());
            CheckNoticationOnlyWifi.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void CheckBoxOnlyWifi_Checked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            MangaCore.Utils.SaveAppSeting(MangaCore.Comon.OnlyWifi, check.IsChecked.Value.ToString());
        }

        private void CheckBoxOnlyWifi_Unchecked(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            MangaCore.Utils.SaveAppSeting(MangaCore.Comon.OnlyWifi, check.IsChecked.Value.ToString());
        }

        private void CheckBoxShowSever_Checked(object sender, RoutedEventArgs e)
        {
            //var check = sender as CheckBox;
            //MangaCore.Utils.SaveAppSeting("CheckShowFullSever", check.IsChecked.Value.ToString());
            //_fullSever = (bool)check.IsChecked;
            //Check_18_cong.Visibility = System.Windows.Visibility.Visible;
            //this.FunSever(_fullSever);
        }
        private void CheckBoxShowSever_Unchecked(object sender, RoutedEventArgs e)
        {
           // var check = sender as CheckBox;
          //  MangaCore.Utils.SaveAppSeting("CheckShowFullSever", check.IsChecked.Value.ToString());
           // _fullSever = (bool)check.IsChecked;
           // Check_18_cong.Visibility = System.Windows.Visibility.Collapsed;
            //this.FunSever(_fullSever);
        }
        private void MuoiTamCongChanged(bool isChecked)
        {
            System.IO.IsolatedStorage.IsolatedStorageSettings applicationSettings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
            App._18_cong = isChecked;
            applicationSettings["18"] = App._18_cong.ToString();
            applicationSettings.Save();
            FullSever(true);
        }
        private void FullSever(bool value)
        {
            var list = MangaCore.Comon.ListSever;
            if (value)
            {
                if (!App._18_cong)
                {
                    list = list.Where(t => t.Tag != 18).ToList();
                }
            }
            else
            {
                list = list.Where(t => t.Key == MangaCore.Utils.GetEnumDescription(MangaCore.Comon.TitleSever.truyentranhtuan)).ToList();
            }

            this.lbSever.ItemsSource = list;
        }

        private void CheckBoxUIColor_Checked(object sender, RoutedEventArgs e)
        {
            Utils.SaveAppSeting("UIColor", "true");
            bool t = Boolean.Parse( Utils.SaveAppSeting("UIColor", "true",true));
            if (_checkUIColor != t)
            {
                txtMgsUiChange.Text = "Mở lại ứng dụng đề cập nhật giao diện";
            }
            
            Utils.SetColorUI();
            
        }

        private void CheckUIColor_UnCheck(object sender, RoutedEventArgs e)
        {
            Utils.SaveAppSeting("UIColor", "false");
            Utils.SetColorUI();
            txtMgsUiChange.Text = "Mở lại ứng dụng đề cập nhật giao diện";
        }
       
    }
}