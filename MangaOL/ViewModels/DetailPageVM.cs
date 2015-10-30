using MangaCore;
using MangaCore.Utilities;
using MangaCore.Utilities.JsonBase;
using MangaOL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaOL.ViewModels
{
    public class DetailPageVM : BaseModel
    {
        private LoadState isLoadDetailAndChaper = LoadState.None;

        private LoadState isLoadImage = LoadState.None;

        public ObservableCollection<Chaper> Chapers
        {
            get;
            set;
        }

        public MangaOL.Models.Detail ItemDetail
        {
            get;
            set;
        }

        public ObservableCollection<Image> Images
        {
            get;
            set;
        }
        private static DetailPageVM _detailPageVM;
        public DetailPageVM()
        {
            ItemDetail = new Detail();
            this.Chapers = new ObservableCollection<Chaper>();
            this.Images = new ObservableCollection<Image>();
        }
        public static DetailPageVM Instance()
        {
            if (DetailPageVM._detailPageVM == null)
            {
                DetailPageVM._detailPageVM = new DetailPageVM();
            }
            return DetailPageVM._detailPageVM;
        }
        #region Load data
        internal async Task LoadDetail(string url)
        {
            base.SetProgressIndicator("LoadDetail", true, "Loading detail...");
            if (this.isLoadDetailAndChaper == LoadState.None)
            {
                this.isLoadDetailAndChaper = LoadState.Loading;
                var jsonDetail = await Config.GetDetail(url);
                if (jsonDetail.error_code == 0)
                {
                    bool isFavorite = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlMangaFavorite>(t => t.Url == url) != null;
                    bool isRead = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlHistoryRead>(t => t.UrlManga == url) != null;
                    ItemDetail.NameManga = jsonDetail.data._detailNameManga;
                    ItemDetail.UrlCover = jsonDetail.data._detailUrlCover;
                    ItemDetail.Description = jsonDetail.data._detailDescription;
                    //ItemDetail.Description = @"";
                    ItemDetail.Rating = jsonDetail.data._rating;
                    ItemDetail.IsFavorite = isFavorite;
                    ItemDetail.IsRead = isRead;
                    ItemDetail.UrlManga = url;


                    var listHistoryChaperRead = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlHistoryRead>().Where(t => t.UrlManga == url);
                    var listHistoryDownload = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlDownload>();
                    var listChaperBookmask = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlChaperBookmask>().Where(t => t.Sever == App.NewSever);

                    foreach (var itemChaper in jsonDetail.data.listChap)
                    {
                        bool isRead2 = listHistoryChaperRead.FirstOrDefault(t => t.UrlChaper == itemChaper._urlChap) != null;
                        bool isDownload = listHistoryDownload.FirstOrDefault(t => t.Url == itemChaper._urlChap) != null;
                        bool isFavorite2 = listChaperBookmask.FirstOrDefault(t => t.Url == itemChaper._urlChap) != null;


                        this.Chapers.Add(new Chaper(itemChaper._chap, itemChaper._urlChap, DateTime.Now.ToString(MangaCore.Comon.FormatDateTime), isRead2, false, isDownload, isFavorite2, "", false));
                    }
                }
                else
                {
                    Utils.ShowMessage("Error: " + jsonDetail.msg, "Error", 0);
                }

            }
            base.SetProgressIndicator("LoadDetail", false, "Loading...");
        }

        internal async Task LoadImages(string url)
        {
            this.Images.Clear();
            base.SetProgressIndicator("LoadImages", true, "Loading image...");
            try
            {
                if (this.isLoadImage == LoadState.None)
                {
                    this.isLoadImage = LoadState.Loading;
                    JsonImage jsonImage = await Config.GetImage(url);
                    if (jsonImage.error_code == 0)
                    {
                        foreach (var item in jsonImage.data)
                        {
                            var stt = jsonImage.data.IndexOf(item);
                            this.Images.Add(new Image("", "", "", System.Windows.Visibility.Visible, item, jsonImage.data.IndexOf(item), jsonImage.data.Count));
                        }

                    }
                    else
                    {
                        Utils.ShowMessage("Error: " + jsonImage.msg, "Error");
                    }
                    this.isLoadImage = LoadState.None;
                }
            }
            catch
            {

            }

            base.SetProgressIndicator("LoadImages", false, "Loading image...");

        }
        internal async Task LoadImagesOff(Download p)
        {
            this.Images.Clear();
            base.SetProgressIndicator("LoadImagesOff", true, "Loading image...");
            List<MangaCore.Sqlite.Models.SqlDownLoadedImage> list = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlDownLoadedImage>().Where(t => t.NameFolder == p.NameForder).ToList();
            foreach (var item in list)
            {
                this.Images.Add(new Image("", "", p.NameChaper, System.Windows.Visibility.Visible, item.Path, list.IndexOf(item), list.Count));
            }

            base.SetProgressIndicator("LoadImagesOff", false, "loading image...");
        }

        internal void LoadImage(Image item)
        {
            try
            {
                int indexItem = this.Images.IndexOf(item);

                if ((indexItem == this.Images.Count - 1 && this.Images.Count == 1))
                {
                    return;
                }
                var item1 = this.Images[indexItem + 1];
                if (!string.IsNullOrEmpty(item1.Tag))
                {
                    item1.URLImage = item1.Tag;
                    //item1.Tag = "";
                }
            }
            catch
            {

            }

        }
        #endregion
        #region Down
        internal async Task Download(Chaper item, ChaperBookmask itemChaperBookmask)
        {
            base.SetProgressIndicator("Download", true, "Quá trình xử lý...");
            item.IsDownload = true;
            JsonImage jsonImage = await Config.GetImage(item.UrlChaper);
            if (jsonImage.error_code == 0)
            {
                List<string> data = jsonImage.data;
                MangaCore.Sqlite.Models.SqlDownload sqlDownload = new MangaCore.Sqlite.Models.SqlDownload();
                List<MangaCore.Sqlite.Models.SqlDownload> list = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlDownload>().OrderByDescending(t => t.DateTimeCreate).ToList();
                sqlDownload.NameForder = ((list.Count == 0) ? "1" : (int.Parse(list[0].NameForder) + 1).ToString());
                if (itemChaperBookmask == null)
                {
                    sqlDownload.NameChaper = (item.NameChaper.Contains(this.ItemDetail.NameManga) ? item.NameChaper : (this.ItemDetail.NameManga + " - " + item.NameChaper));
                }
                else
                {
                    sqlDownload.NameChaper = itemChaperBookmask.NameChaper;
                }
                sqlDownload.Total = data.Count;
                sqlDownload.TotalImageDownloaded = 0;
                sqlDownload.Url = item.UrlChaper;
                sqlDownload.DateTimeCreate = DateTime.Now.ToString(MangaCore.Comon.FormatDateTime);
                App.dbHelper.Insert<MangaCore.Sqlite.Models.SqlDownload>(sqlDownload);
                await Utils.CreateFolder(sqlDownload.NameForder, App.ForderDownload);
                foreach (var itemImage in data)
                {
                    MangaCore.Sqlite.Models.SqlDownLoadedImage ob = new MangaCore.Sqlite.Models.SqlDownLoadedImage(itemImage, "", sqlDownload.NameForder);
                    App.dbHelper.Insert<MangaCore.Sqlite.Models.SqlDownLoadedImage>(ob);
                }
                App.Downloads.Add(new Download(sqlDownload.NameChaper, sqlDownload.Total, sqlDownload.TotalImageDownloaded, item.UrlChaper, sqlDownload.NameForder, sqlDownload.DateTimeCreate));
                //App.Downloads.Insert(0, new Download(sqlDownload.NameChaper, sqlDownload.Total, sqlDownload.TotalImageDownloaded, item.UrlChaper, sqlDownload.NameForder, sqlDownload.DateTimeCreate));
            }
            await Task.Delay(1500);
            base.SetProgressIndicator("Download", false, "Quá trình xử lý...");
        }
        #endregion
        #region Sqlite
        internal void InsertUpdateChaper(MangaCore.Sqlite.Models.SqlHistoryRead chaper)
        {
            this.ItemDetail.IsRead = true;
            MangaCore.Sqlite.Models.SqlHistoryRead sqlHistoryRead = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlHistoryRead>(t => t.UrlChaper == chaper.UrlChaper);
            if (sqlHistoryRead != null)
            {
                sqlHistoryRead.DateTimeCreate = chaper.DateTimeCreate;
                sqlHistoryRead.Index = chaper.Index;
                App.dbHelper.Update<MangaCore.Sqlite.Models.SqlHistoryRead>(sqlHistoryRead);
            }
            else
            {
                App.dbHelper.Insert<MangaCore.Sqlite.Models.SqlHistoryRead>(chaper);
            }
        }

        internal void InsertUpdateManga(Models.Manga manga)
        {
            MangaCore.Sqlite.Models.SqlMangaHistory item = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlMangaHistory>(t => t.Url == manga.UriManga);
            if (item != null)
            {
                item.DateTimeCreate = manga.DateTimeCreate;
                App.dbHelper.Update<MangaCore.Sqlite.Models.SqlMangaHistory>(item);
                if (App.MangasH.Count != 0)
                    App.MangasH.Remove(App.MangasH.FirstOrDefault(t => t.UriManga == manga.UriManga));
                App.MangasH.Insert(0, new Manga(item.Url, item.NameManga, item.UrlCover, "", manga.Sever, item.DateTimeCreate, 0));
            }
            else
            {
                App.dbHelper.Insert<MangaCore.Sqlite.Models.SqlMangaHistory>(new MangaCore.Sqlite.Models.SqlMangaHistory(manga.NameManga, App.NewSever, manga.UriCover, manga.UriManga, manga.DateTimeCreate));
                App.MangasH.Insert(0, new Manga(manga.UriManga, manga.NameManga, manga.UriCover, "", manga.Sever, manga.DateTimeCreate, 0));
            }
        }

        internal void InsertDeleteFavorite(MangaOL.Models.Detail item)
        {
            if (item.IsFavorite)
            {
                MangaCore.Sqlite.Models.SqlMangaFavorite i = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlMangaFavorite>(t => t.Url == item.UrlManga);
                App.dbHelper.Delete<MangaCore.Sqlite.Models.SqlMangaFavorite>(i);
                App.MangaFavorite.Remove(App.MangaFavorite.FirstOrDefault(t => t.UriManga == item.UrlManga));
            }
            else
            {
                App.dbHelper.Insert<MangaCore.Sqlite.Models.SqlMangaFavorite>(new MangaCore.Sqlite.Models.SqlMangaFavorite(App.NewSever, item.NameManga, item.UrlCover, item.UrlManga, DateTime.Now.ToString(MangaCore.Comon.FormatDateTime), this.Chapers.Count, true));
                var itemManga = new Manga(item.UrlManga, item.NameManga, item.UrlCover, "", App.NewSever, DateTime.Now.ToString(MangaCore.Comon.FormatDateTime), 0);
                itemManga.IsNotication = true;
                App.MangaFavorite.Insert(0, itemManga);
            }
            this.ItemDetail.IsFavorite = !item.IsFavorite;
        }
        #endregion
        #region Dowload Image
        private static int? i;
        public async void StartDownloadImageToByte(System.Threading.CancellationToken token)
        {
            i = this.Images.Count;
            if (i <= 0)
            {
                return;
            }
            try
            {
                foreach (var item in this.Images)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    if (!string.IsNullOrEmpty(item.Tag))
                    {
                        item.Bytes = await MangaCore.Utils.DownloadByteToUriImage(item.Tag);
                        System.Diagnostics.Debug.WriteLine("Download "+this.Images.IndexOf(item));
                        System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            valueCallBack(this.Images.IndexOf(item));
                        });

                        i--;
                    }
                }
                return;
            }
            catch
            {

            }

        }
        public Delegates.ValueProbar valueCallBack;
        #endregion

    }
}
