﻿#pragma checksum "E:\WIPHONE\MangaOL\MangaOL\Views\DetailPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E377F0D419B050494C45CADC310835EA"
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
using Telerik.Windows.Controls;


namespace MangaOL.Views {
    
    
    public partial class DetailPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Controls.PhoneApplicationPage detail;
        
        internal System.Windows.Media.Animation.Storyboard HeaderPivotMove;
        
        internal System.Windows.Media.Animation.Storyboard Hide_Image;
        
        internal System.Windows.Media.Animation.Storyboard Show_Image;
        
        internal System.Windows.Media.Animation.Storyboard Show_Notication;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid gridHeader;
        
        internal System.Windows.Controls.Grid gridMain;
        
        internal Microsoft.Phone.Controls.Pivot pivotDetail;
        
        internal MangaOL.UserControls.UcImageData imageBanner;
        
        internal System.Windows.Controls.Grid gridDetail;
        
        internal System.Windows.Controls.Grid gridLeftDetail;
        
        internal MangaOL.UserControls.UcImageData imageAva;
        
        internal System.Windows.Controls.TextBlock tbxlTManaName;
        
        internal System.Windows.Shapes.Path border3;
        
        internal System.Windows.Controls.TextBlock txtRating;
        
        internal System.Windows.Controls.Grid ads1;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtKeyWord;
        
        internal System.Windows.Controls.StackPanel StackPanelChaper;
        
        internal System.Windows.Controls.TextBlock txtTieuDe;
        
        internal System.Windows.Controls.TextBlock txtContent;
        
        internal System.Windows.Controls.ListBox lbChaper;
        
        internal System.Windows.Controls.Border border;
        
        internal System.Windows.Controls.Grid gridAppBar;
        
        internal MangaOL.UserControls.Mdl2 btnSelectAll;
        
        internal MangaOL.UserControls.Mdl2 btnDownloadAll;
        
        internal MangaOL.UserControls.Mdl2 btnPin;
        
        internal System.Windows.Controls.Grid gridImage;
        
        internal Telerik.Windows.Controls.RadSlideView pivotImage;
        
        internal System.Windows.Controls.Grid gridLeft;
        
        internal MangaOL.UserControls.Mdl2 mdl2;
        
        internal System.Windows.Controls.Grid gridRight;
        
        internal MangaOL.UserControls.Mdl2 mdl1;
        
        internal System.Windows.Controls.Grid gridChapInView;
        
        internal System.Windows.Controls.ListBox lbChaperInView;
        
        internal System.Windows.Controls.Grid gridStatusHeader;
        
        internal System.Windows.Controls.TextBlock txtStatusHeader;
        
        internal System.Windows.Controls.Grid gridTop;
        
        internal System.Windows.Controls.TextBlock txtHeaderChap;
        
        internal MangaOL.UserControls.Mdl2 btnList;
        
        internal MangaOL.UserControls.Mdl2 btnSeting;
        
        internal System.Windows.Controls.ProgressBar proBarTotalImageOneped;
        
        internal System.Windows.Controls.Grid gridBoton;
        
        internal MangaOL.UserControls.Mdl2 btnBackChaper;
        
        internal MangaOL.UserControls.Mdl2 btnBookmaskChaper;
        
        internal System.Windows.Controls.TextBlock txtIndexPivotImage;
        
        internal MangaOL.UserControls.Mdl2 btnWindowOption;
        
        internal MangaOL.UserControls.Mdl2 btnNextChaper;
        
        internal System.Windows.Controls.Grid gridSettingInView;
        
        internal System.Windows.Controls.CheckBox Check_Left_Right;
        
        internal System.Windows.Controls.CheckBox Check_FlipOrSlide;
        
        internal System.Windows.Controls.CheckBox Check_MoveChapToMoveItem;
        
        internal System.Windows.Controls.Grid ads3;
        
        internal System.Windows.Controls.Grid gridLight;
        
        internal System.Windows.Controls.Grid gridJump;
        
        internal System.Windows.Controls.Slider SliderJumpImage;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtSttJump;
        
        internal System.Windows.Controls.TextBlock txtStatusJumpImage;
        
        internal System.Windows.Controls.Grid gridNotication;
        
        internal System.Windows.Controls.TextBlock txtNotication;
        
        internal System.Windows.Controls.Grid gridOption;
        
        internal MangaOL.UserControls.Mdl2 btnDownloadInView;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MangaOL;component/Views/DetailPage.xaml", System.UriKind.Relative));
            this.detail = ((Microsoft.Phone.Controls.PhoneApplicationPage)(this.FindName("detail")));
            this.HeaderPivotMove = ((System.Windows.Media.Animation.Storyboard)(this.FindName("HeaderPivotMove")));
            this.Hide_Image = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Hide_Image")));
            this.Show_Image = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Show_Image")));
            this.Show_Notication = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Show_Notication")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.gridHeader = ((System.Windows.Controls.Grid)(this.FindName("gridHeader")));
            this.gridMain = ((System.Windows.Controls.Grid)(this.FindName("gridMain")));
            this.pivotDetail = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivotDetail")));
            this.imageBanner = ((MangaOL.UserControls.UcImageData)(this.FindName("imageBanner")));
            this.gridDetail = ((System.Windows.Controls.Grid)(this.FindName("gridDetail")));
            this.gridLeftDetail = ((System.Windows.Controls.Grid)(this.FindName("gridLeftDetail")));
            this.imageAva = ((MangaOL.UserControls.UcImageData)(this.FindName("imageAva")));
            this.tbxlTManaName = ((System.Windows.Controls.TextBlock)(this.FindName("tbxlTManaName")));
            this.border3 = ((System.Windows.Shapes.Path)(this.FindName("border3")));
            this.txtRating = ((System.Windows.Controls.TextBlock)(this.FindName("txtRating")));
            this.ads1 = ((System.Windows.Controls.Grid)(this.FindName("ads1")));
            this.txtKeyWord = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtKeyWord")));
            this.StackPanelChaper = ((System.Windows.Controls.StackPanel)(this.FindName("StackPanelChaper")));
            this.txtTieuDe = ((System.Windows.Controls.TextBlock)(this.FindName("txtTieuDe")));
            this.txtContent = ((System.Windows.Controls.TextBlock)(this.FindName("txtContent")));
            this.lbChaper = ((System.Windows.Controls.ListBox)(this.FindName("lbChaper")));
            this.border = ((System.Windows.Controls.Border)(this.FindName("border")));
            this.gridAppBar = ((System.Windows.Controls.Grid)(this.FindName("gridAppBar")));
            this.btnSelectAll = ((MangaOL.UserControls.Mdl2)(this.FindName("btnSelectAll")));
            this.btnDownloadAll = ((MangaOL.UserControls.Mdl2)(this.FindName("btnDownloadAll")));
            this.btnPin = ((MangaOL.UserControls.Mdl2)(this.FindName("btnPin")));
            this.gridImage = ((System.Windows.Controls.Grid)(this.FindName("gridImage")));
            this.pivotImage = ((Telerik.Windows.Controls.RadSlideView)(this.FindName("pivotImage")));
            this.gridLeft = ((System.Windows.Controls.Grid)(this.FindName("gridLeft")));
            this.mdl2 = ((MangaOL.UserControls.Mdl2)(this.FindName("mdl2")));
            this.gridRight = ((System.Windows.Controls.Grid)(this.FindName("gridRight")));
            this.mdl1 = ((MangaOL.UserControls.Mdl2)(this.FindName("mdl1")));
            this.gridChapInView = ((System.Windows.Controls.Grid)(this.FindName("gridChapInView")));
            this.lbChaperInView = ((System.Windows.Controls.ListBox)(this.FindName("lbChaperInView")));
            this.gridStatusHeader = ((System.Windows.Controls.Grid)(this.FindName("gridStatusHeader")));
            this.txtStatusHeader = ((System.Windows.Controls.TextBlock)(this.FindName("txtStatusHeader")));
            this.gridTop = ((System.Windows.Controls.Grid)(this.FindName("gridTop")));
            this.txtHeaderChap = ((System.Windows.Controls.TextBlock)(this.FindName("txtHeaderChap")));
            this.btnList = ((MangaOL.UserControls.Mdl2)(this.FindName("btnList")));
            this.btnSeting = ((MangaOL.UserControls.Mdl2)(this.FindName("btnSeting")));
            this.proBarTotalImageOneped = ((System.Windows.Controls.ProgressBar)(this.FindName("proBarTotalImageOneped")));
            this.gridBoton = ((System.Windows.Controls.Grid)(this.FindName("gridBoton")));
            this.btnBackChaper = ((MangaOL.UserControls.Mdl2)(this.FindName("btnBackChaper")));
            this.btnBookmaskChaper = ((MangaOL.UserControls.Mdl2)(this.FindName("btnBookmaskChaper")));
            this.txtIndexPivotImage = ((System.Windows.Controls.TextBlock)(this.FindName("txtIndexPivotImage")));
            this.btnWindowOption = ((MangaOL.UserControls.Mdl2)(this.FindName("btnWindowOption")));
            this.btnNextChaper = ((MangaOL.UserControls.Mdl2)(this.FindName("btnNextChaper")));
            this.gridSettingInView = ((System.Windows.Controls.Grid)(this.FindName("gridSettingInView")));
            this.Check_Left_Right = ((System.Windows.Controls.CheckBox)(this.FindName("Check_Left_Right")));
            this.Check_FlipOrSlide = ((System.Windows.Controls.CheckBox)(this.FindName("Check_FlipOrSlide")));
            this.Check_MoveChapToMoveItem = ((System.Windows.Controls.CheckBox)(this.FindName("Check_MoveChapToMoveItem")));
            this.ads3 = ((System.Windows.Controls.Grid)(this.FindName("ads3")));
            this.gridLight = ((System.Windows.Controls.Grid)(this.FindName("gridLight")));
            this.gridJump = ((System.Windows.Controls.Grid)(this.FindName("gridJump")));
            this.SliderJumpImage = ((System.Windows.Controls.Slider)(this.FindName("SliderJumpImage")));
            this.txtSttJump = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtSttJump")));
            this.txtStatusJumpImage = ((System.Windows.Controls.TextBlock)(this.FindName("txtStatusJumpImage")));
            this.gridNotication = ((System.Windows.Controls.Grid)(this.FindName("gridNotication")));
            this.txtNotication = ((System.Windows.Controls.TextBlock)(this.FindName("txtNotication")));
            this.gridOption = ((System.Windows.Controls.Grid)(this.FindName("gridOption")));
            this.btnDownloadInView = ((MangaOL.UserControls.Mdl2)(this.FindName("btnDownloadInView")));
            this.prLoading = ((System.Windows.Controls.ProgressBar)(this.FindName("prLoading")));
        }
    }
}

