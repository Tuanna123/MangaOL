﻿#pragma checksum "E:\WIPHONE\MangaOL\MangaOL\UserControls\TemplateFavorite.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B7A612B902E46C582292FB2CA468BD64"
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


namespace MangaOL.UserControls {
    
    
    public partial class TemplateFavorite : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid Container;
        
        internal System.Windows.Controls.TextBlock txtIconStatus;
        
        internal System.Windows.Controls.TextBlock txtName;
        
        internal System.Windows.Controls.Border borderOption;
        
        internal MangaOL.UserControls.Mdl2 btnPinOrUnPin;
        
        internal MangaOL.UserControls.Mdl2 btnNotication;
        
        internal MangaOL.UserControls.Mdl2 btnDelete;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MangaOL;component/UserControls/TemplateFavorite.xaml", System.UriKind.Relative));
            this.Container = ((System.Windows.Controls.Grid)(this.FindName("Container")));
            this.txtIconStatus = ((System.Windows.Controls.TextBlock)(this.FindName("txtIconStatus")));
            this.txtName = ((System.Windows.Controls.TextBlock)(this.FindName("txtName")));
            this.borderOption = ((System.Windows.Controls.Border)(this.FindName("borderOption")));
            this.btnPinOrUnPin = ((MangaOL.UserControls.Mdl2)(this.FindName("btnPinOrUnPin")));
            this.btnNotication = ((MangaOL.UserControls.Mdl2)(this.FindName("btnNotication")));
            this.btnDelete = ((MangaOL.UserControls.Mdl2)(this.FindName("btnDelete")));
        }
    }
}

