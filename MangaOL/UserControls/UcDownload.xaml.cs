using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MangaCore.Sqlite.Models;
using Windows.Storage;
using System.IO;
using System.Windows.Media;
using System.ComponentModel;
using MangaCore;

namespace MangaOL.UserControls
{
    //public enum Status
    //{
    //    ErrorDownloadImage,
    //        [Description("Nhấn Refesh để thử lại.")]
    //    //Error = "Có lỗi trong xử lý.",
    //    //Penning="Penning...",
    //    //Success = "Success"
    //}
    public enum Status
    {
        [Description("Nhấn Refesh để thử lại.")]
        ErrorDownloadImage,
        [Description("Có lỗi trong xử lý.")]
        Error,
        [Description("Penning...")]
        Penning,
        [Description("Success")]
        Success,
    }
    public partial class UcDownload : UserControl
    {
        public UcDownload()
        {
            InitializeComponent();
        }

        public delegate void GridOff(object sender, EventArgs e);

        public delegate void GridDelete(object sender, EventArgs e);
        public event UcDownload.GridOff GridOffTap;

        public event UcDownload.GridDelete GridDeleteTap;

        private List<SqlDownLoadedImage> listSqlDownload;

        private StorageFolder folder;

        private SqlDownload itemDownload;

        private bool isDelete;
        private bool isDownloadDone;

        public static readonly DependencyProperty TotalProperty = DependencyProperty.Register("Total", typeof(int), typeof(UcDownload), new PropertyMetadata((int)0));

        public static readonly DependencyProperty TotalImageDownloadedProperty = DependencyProperty.Register("TotalImageDownloaded", typeof(int), typeof(UcDownload), new PropertyMetadata((int)-1, new PropertyChangedCallback(UcDownload.TotalImageDownLoadedChanged)));
        private static void TotalImageDownLoadedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            UcDownload ucDownload = obj as UcDownload;
            ucDownload.SetValueProbar((int)e.NewValue);
        }

        private async void SetValueProbar(int p)
        {
            if (!this.isDelete)
            {
                txblStatusTotal.Text = p + " /" + this.Total;
                proValueProBar.Value = (double)p;
                txblStatus.Text = MangaCore.Utils.GetEnumDescription(Status.Penning);
                if (p >= this.Total)
                {
                    txblStatus.Text = MangaCore.Utils.GetEnumDescription(Status.Success);
                    this.IsDownloaded = true;
                    this.isDownloadDone = true;
                    //this.listSqlDownload = null;
                    btnRefresh.IsEnabledMdl2 = true;
                    App.CountDownLoad.Remove(1);
                }
                else
                {
                    try
                    {
                        StorageFile storageFile;
                        //if (this.listSqlDownload[p].UrlImage.Contains("jpg"))
                        //{
                            storageFile = await Utils.CreateFile(this.folder, p + ".jpg", CreationCollisionOption.OpenIfExists);
                        //}
                        //else if (this.listSqlDownload[p].UrlImage.Contains(".png"))
                        //{
                        //    storageFile = await Utils.CreateFile(this.folder, p + ".png", CreationCollisionOption.OpenIfExists);
                        //}
                        //else if (this.listSqlDownload[p].UrlImage.Contains("gif"))
                        //{
                        //    storageFile = await Utils.CreateFile(this.folder, p + ".gif", CreationCollisionOption.OpenIfExists);
                        //}
                        //else if (this.listSqlDownload[p].UrlImage.Contains("jpeg"))
                        //{
                        //    storageFile = await Utils.CreateFile(this.folder, p + ".jpg", CreationCollisionOption.OpenIfExists);
                        //}
                        //else
                        //{
                        //    storageFile = await Utils.CreateFile(this.folder, p + ".jpg", CreationCollisionOption.OpenIfExists);
                        //}
                        Uri requestUri = new Uri(this.listSqlDownload[p].UrlImage);
                        Stream stream = await MangaCore.Utils.DownloadStreamToUriImage(this.listSqlDownload[p].UrlImage);
                        using (Stream stream2 = await storageFile.OpenStreamForWriteAsync())
                        {
                            await stream.CopyToAsync(stream2);
                        }
                        GC.Collect();
                        SqlDownLoadedImage sqlDownLoadedImage = this.listSqlDownload[p];
                        sqlDownLoadedImage.Path = storageFile.Path;
                        App.dbHelper.Update<SqlDownLoadedImage>(sqlDownLoadedImage);
                        this.itemDownload.TotalImageDownloaded++;
                        App.dbHelper.Update<SqlDownload>(this.itemDownload);
                        this.TotalImageDownloaded = p + 1;
                    }
                    catch
                    {
                        txblStatus.Text = MangaCore.Utils.GetEnumDescription(Status.ErrorDownloadImage);
                        btnRefresh.IsEnabledMdl2 = false;
                        this.txblStatus.Foreground = new SolidColorBrush(Colors.Orange);
                        return;
                    }
                    
                }
            }
        }
        public static readonly DependencyProperty NameForderProperty = DependencyProperty.Register("NameForder", typeof(string), typeof(UcDownload), new PropertyMetadata(null, new PropertyChangedCallback(UcDownload.NameforderChanged)));
        private static async void NameforderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UcDownload ucDownload = d as UcDownload;
            await ucDownload.GetItemDownLoadimage((string)e.NewValue);
        }

        private async System.Threading.Tasks.Task GetItemDownLoadimage(string p)
        {
            if (this.listSqlDownload == null)
            {
                try
                {
                    this.listSqlDownload = App.dbHelper.Select<SqlDownLoadedImage>().Where(t => t.NameFolder == p).ToList();
                    this.folder = await Utils.CreateFolder(this.NameForder, App.ForderDownload);
                    this.itemDownload = App.dbHelper.Select<SqlDownload>(t => t.NameForder == p);
                    //bingd value
                   // txtNameChaper.Text = this.itemDownload.NameChaper;
                    proValueProBar.Maximum = this.Total = this.itemDownload.Total;
                   // txtDateTimeCreate.Text = this.itemDownload.DateTimeCreate;
                    //end bind value
                    if (this.listSqlDownload.Count == 0)
                    {
                        this.txblStatus.Foreground = new SolidColorBrush(Colors.Red);
                        btnRefresh.IsEnabledMdl2 = false;
                        txblStatus.Text = Utils.GetEnumDescription(Status.Error);
                    }
                    else
                    {
                        App.CountDownLoad.Add(1);
                        this.TotalImageDownloaded = this.itemDownload.TotalImageDownloaded;
                    }
                }
                catch
                {
                    this.txblStatus.Foreground = new SolidColorBrush(Colors.Red);
                    btnRefresh.IsEnabledMdl2 = false;
                    txblStatus.Text = Utils.GetEnumDescription(Status.Error);
                }
                
            }
        }

        public static readonly DependencyProperty StatusTotalProperty = DependencyProperty.Register("StatusTotal", typeof(string), typeof(UcDownload), new PropertyMetadata(null));

        public static readonly DependencyProperty IsDownloadedProperty = DependencyProperty.Register("IsDownloaded", typeof(bool), typeof(UcDownload), new PropertyMetadata(false));


        public int Total
        {
            get
            {
                return (int)base.GetValue(UcDownload.TotalProperty);
            }
            set
            {
                base.SetValue(UcDownload.TotalProperty, value);
            }
        }

        public int TotalImageDownloaded
        {
            get
            {
                return (int)base.GetValue(UcDownload.TotalImageDownloadedProperty);
            }
            set
            {
                base.SetValue(UcDownload.TotalImageDownloadedProperty, value);
            }
        }

        public string NameForder
        {
            get
            {
                return (string)base.GetValue(UcDownload.NameForderProperty);
            }
            set
            {
                base.SetValue(UcDownload.NameForderProperty, value);
            }
        }


        public bool IsDownloaded
        {
            get
            {
                return (bool)base.GetValue(UcDownload.IsDownloadedProperty);
            }
            set
            {
                base.SetValue(UcDownload.IsDownloadedProperty, value);
            }
        }
        private void GridOff_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            if (this.GridOffTap != null && this.isDownloadDone)
            {
                this.GridOffTap(this.DataContext, EventArgs.Empty);
            }
        }

        private void BtnDeleteItemOff_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.isDelete = true;
            if (this.TotalImageDownloaded < this.Total) App.CountDownLoad.Remove(1);
            if (this.GridDeleteTap != null)
            {
                this.GridDeleteTap(this.DataContext, EventArgs.Empty);
            }
        }

        private void BtnRefresh_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {

            this.txblStatus.Foreground = System.Windows.Application.Current.Resources["ColorGridHeader"] as SolidColorBrush;
            btnRefresh.IsEnabledMdl2 = true;
            SetValueProbar(this.TotalImageDownloaded);
        }
    }
}
