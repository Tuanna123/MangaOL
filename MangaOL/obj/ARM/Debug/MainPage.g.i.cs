﻿#pragma checksum "E:\WIPHONE\MangaOL\MangaOL\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7C3C4E7CCCB6A63EA2062612AC0DA094"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MangaOL.UserControls;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MangaOL {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Controls.PhoneApplicationPage local;
        
        internal System.Windows.Media.Animation.Storyboard HeaderPivotMove;
        
        internal System.Windows.Media.Animation.Storyboard closeSidePanel;
        
        internal System.Windows.Media.Animation.Storyboard openSidePanel;
        
        internal System.Windows.DataTemplate myCell3;
        
        internal System.Windows.DataTemplate myCell3Hoz;
        
        internal System.Windows.DataTemplate NewReadTemplate;
        
        internal System.Windows.DataTemplate FavoriteMangaTemplate;
        
        internal System.Windows.DataTemplate ChaperBookmaskTemplate;
        
        internal System.Windows.DataTemplate DownloadTemplate;
        
        internal System.Windows.Media.Animation.Storyboard ShowGridSearch;
        
        internal System.Windows.Media.Animation.Storyboard HideGridSearch;
        
        internal System.Windows.Media.Animation.Storyboard ShowAppBar;
        
        internal System.Windows.Media.Animation.Storyboard HideAppBar;
        
        internal System.Windows.Media.Animation.Storyboard Hide_SettingSearch;
        
        internal System.Windows.Media.Animation.Storyboard Show_SettingSearch;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid gridHeader;
        
        internal System.Windows.Controls.TextBlock textBlock;
        
        internal MangaOL.UserControls.Mdl2 mdl2;
        
        internal System.Windows.Controls.Grid gridSearch;
        
        internal System.Windows.Controls.TextBox txtKeyWord;
        
        internal System.Windows.Controls.Grid gridMain;
        
        internal Microsoft.Phone.Controls.Pivot pivotMain;
        
        internal Microsoft.Phone.Controls.LongListSelector LvMain;
        
        internal System.Windows.Controls.StackPanel StackPanelThongBaoMain;
        
        internal System.Windows.Controls.TextBlock txtTieuDe;
        
        internal System.Windows.Controls.TextBlock txtContent;
        
        internal System.Windows.Controls.ListBox lb;
        
        internal System.Windows.Controls.Grid ads4;
        
        internal System.Windows.Controls.ListBox lbFavoriteManga;
        
        internal System.Windows.Controls.Grid ads1;
        
        internal System.Windows.Controls.ListBox lbChaperBookmask;
        
        internal System.Windows.Controls.Grid ads2;
        
        internal System.Windows.Controls.ListBox lbDownload;
        
        internal System.Windows.Controls.Grid ads3;
        
        internal System.Windows.Controls.Grid gridHeaderPivot;
        
        internal System.Windows.Controls.Border border;
        
        internal System.Windows.Controls.StackPanel stackSearch;
        
        internal System.Windows.Controls.RadioButton radSearchOnline;
        
        internal System.Windows.Controls.RadioButton radSearchOffline;
        
        internal System.Windows.Controls.Grid gridAppBar;
        
        internal MangaOL.UserControls.Mdl2 btnDeleteAll;
        
        internal MangaOL.UserControls.Mdl2 btnTemplateLongListMain;
        
        internal MangaOL.UserControls.Mdl2 btnRefresh;
        
        internal MangaOL.UserControls.Mdl2 btnMail;
        
        internal MangaOL.UserControls.Mdl2 btnHozt;
        
        internal System.Windows.Controls.Grid grid;
        
        internal System.Windows.Controls.Grid gridSide;
        
        internal MangaOL.UserControls.Mdl2 txtSever;
        
        internal System.Windows.Controls.ListBox lbItem;
        
        internal MangaOL.UserControls.Mdl2 btnMenu;
        
        internal System.Windows.Controls.ProgressBar prLoading;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MangaOL;component/MainPage.xaml", System.UriKind.Relative));
            this.local = ((Microsoft.Phone.Controls.PhoneApplicationPage)(this.FindName("local")));
            this.HeaderPivotMove = ((System.Windows.Media.Animation.Storyboard)(this.FindName("HeaderPivotMove")));
            this.closeSidePanel = ((System.Windows.Media.Animation.Storyboard)(this.FindName("closeSidePanel")));
            this.openSidePanel = ((System.Windows.Media.Animation.Storyboard)(this.FindName("openSidePanel")));
            this.myCell3 = ((System.Windows.DataTemplate)(this.FindName("myCell3")));
            this.myCell3Hoz = ((System.Windows.DataTemplate)(this.FindName("myCell3Hoz")));
            this.NewReadTemplate = ((System.Windows.DataTemplate)(this.FindName("NewReadTemplate")));
            this.FavoriteMangaTemplate = ((System.Windows.DataTemplate)(this.FindName("FavoriteMangaTemplate")));
            this.ChaperBookmaskTemplate = ((System.Windows.DataTemplate)(this.FindName("ChaperBookmaskTemplate")));
            this.DownloadTemplate = ((System.Windows.DataTemplate)(this.FindName("DownloadTemplate")));
            this.ShowGridSearch = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ShowGridSearch")));
            this.HideGridSearch = ((System.Windows.Media.Animation.Storyboard)(this.FindName("HideGridSearch")));
            this.ShowAppBar = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ShowAppBar")));
            this.HideAppBar = ((System.Windows.Media.Animation.Storyboard)(this.FindName("HideAppBar")));
            this.Hide_SettingSearch = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Hide_SettingSearch")));
            this.Show_SettingSearch = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Show_SettingSearch")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.gridHeader = ((System.Windows.Controls.Grid)(this.FindName("gridHeader")));
            this.textBlock = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock")));
            this.mdl2 = ((MangaOL.UserControls.Mdl2)(this.FindName("mdl2")));
            this.gridSearch = ((System.Windows.Controls.Grid)(this.FindName("gridSearch")));
            this.txtKeyWord = ((System.Windows.Controls.TextBox)(this.FindName("txtKeyWord")));
            this.gridMain = ((System.Windows.Controls.Grid)(this.FindName("gridMain")));
            this.pivotMain = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivotMain")));
            this.LvMain = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("LvMain")));
            this.StackPanelThongBaoMain = ((System.Windows.Controls.StackPanel)(this.FindName("StackPanelThongBaoMain")));
            this.txtTieuDe = ((System.Windows.Controls.TextBlock)(this.FindName("txtTieuDe")));
            this.txtContent = ((System.Windows.Controls.TextBlock)(this.FindName("txtContent")));
            this.lb = ((System.Windows.Controls.ListBox)(this.FindName("lb")));
            this.ads4 = ((System.Windows.Controls.Grid)(this.FindName("ads4")));
            this.lbFavoriteManga = ((System.Windows.Controls.ListBox)(this.FindName("lbFavoriteManga")));
            this.ads1 = ((System.Windows.Controls.Grid)(this.FindName("ads1")));
            this.lbChaperBookmask = ((System.Windows.Controls.ListBox)(this.FindName("lbChaperBookmask")));
            this.ads2 = ((System.Windows.Controls.Grid)(this.FindName("ads2")));
            this.lbDownload = ((System.Windows.Controls.ListBox)(this.FindName("lbDownload")));
            this.ads3 = ((System.Windows.Controls.Grid)(this.FindName("ads3")));
            this.gridHeaderPivot = ((System.Windows.Controls.Grid)(this.FindName("gridHeaderPivot")));
            this.border = ((System.Windows.Controls.Border)(this.FindName("border")));
            this.stackSearch = ((System.Windows.Controls.StackPanel)(this.FindName("stackSearch")));
            this.radSearchOnline = ((System.Windows.Controls.RadioButton)(this.FindName("radSearchOnline")));
            this.radSearchOffline = ((System.Windows.Controls.RadioButton)(this.FindName("radSearchOffline")));
            this.gridAppBar = ((System.Windows.Controls.Grid)(this.FindName("gridAppBar")));
            this.btnDeleteAll = ((MangaOL.UserControls.Mdl2)(this.FindName("btnDeleteAll")));
            this.btnTemplateLongListMain = ((MangaOL.UserControls.Mdl2)(this.FindName("btnTemplateLongListMain")));
            this.btnRefresh = ((MangaOL.UserControls.Mdl2)(this.FindName("btnRefresh")));
            this.btnMail = ((MangaOL.UserControls.Mdl2)(this.FindName("btnMail")));
            this.btnHozt = ((MangaOL.UserControls.Mdl2)(this.FindName("btnHozt")));
            this.grid = ((System.Windows.Controls.Grid)(this.FindName("grid")));
            this.gridSide = ((System.Windows.Controls.Grid)(this.FindName("gridSide")));
            this.txtSever = ((MangaOL.UserControls.Mdl2)(this.FindName("txtSever")));
            this.lbItem = ((System.Windows.Controls.ListBox)(this.FindName("lbItem")));
            this.btnMenu = ((MangaOL.UserControls.Mdl2)(this.FindName("btnMenu")));
            this.prLoading = ((System.Windows.Controls.ProgressBar)(this.FindName("prLoading")));
        }
    }
}

