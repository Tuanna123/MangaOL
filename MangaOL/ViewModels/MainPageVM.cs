using MangaCore;
using MangaCore.Utilities;
using MangaCore.Utilities.JsonBase;
using MangaCore.Utilities.Models;
using MangaOL.Models;
using MangaCore.Sqlite.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MangaOL.ViewModels
{
    public class MainPageVM : BaseModel
    {
        public LoadState IsLoadManga = LoadState.None;

        private LoadState isLoadMangaHistory = LoadState.None;

        private LoadState isLoadDownload = LoadState.None;

        private LoadState isLoadMangaFavorite = LoadState.None;

        private LoadState isLoadChaperBookmask = LoadState.None;

        private ObservableCollection<MenuItem> _menu;

        private MenuItem _selectedMenuItem;

        public ObservableCollection<Manga> Mangas
        {
            get
            {
                return App.Mangas;
            }
        }

        public ObservableCollection<Manga> MangasH
        {
            get
            {
                return App.MangasH;
            }
        }

        public ObservableCollection<Manga> MangaFavorite
        {
            get
            {
                return App.MangaFavorite;
            }
        }

        public ObservableCollection<ChaperBookmask> ChaperBookmask
        {
            get
            {
                return App.ChaperBookmask;
            }
        }

        public ObservableCollection<Download> Downloads
        {
            get
            {
                return App.Downloads;
            }
        }

        private ObservableCollection<SqlMangaHistory> MangaHistory
        {
            get;
            set;
        }

        public ObservableCollection<MenuItem> Menu
        {
            get
            {
                return this._menu;
            }
        }

        public ObservableCollection<int> CountDownload
        {
            get { return App.CountDownLoad; }
           
        }
        public MenuItem SelectedMenuItem
        {
            get
            {
                return this._selectedMenuItem;
            }
            set
            {
                this._selectedMenuItem = value;
                foreach (var item in _menu)
                {
                    item.IsSelected = false;
                }
                this._selectedMenuItem.IsSelected = true;
                base.NotifyPropertyChanged("SelectedMenuItem");
            }
        }

        public MainPageVM()
        {
            this._menu = new ObservableCollection<MenuItem>();
            MangaCore.Comon.LoadMenuManga();
            this.MangaHistory = new ObservableCollection<SqlMangaHistory>();
        }

        internal async Task LoadMenuManga()
        {
            this._menu.Clear();
            List<MenuManga> list = new List<MenuManga>();
            list.Clear();
            list = MangaCore.Comon.ListMenuManga.Where(t => t.Sever == App.NewSever).ToList();
            list = App._18_cong ? list : list.Where(t => t.Tag != "18").ToList();
            foreach (var item in list)
            {
                if (list.IndexOf(item) == 0)
                {
                    this._menu.Add(new MenuItem
                    {
                        Title = "Trang Chủ",
                        Icon = System.Windows.Application.Current.Resources["HomeOutline"] as string,
                        Action = item.Url,
                        IsSelected = true
                    });
                }
                else
                {
                    this._menu.Add(new MenuItem
                    {
                        Title = item.Title,
                        Icon = System.Windows.Application.Current.Resources["Attach"] as string,
                        Action = item.Url
                    });
                }
            }
        }
        
        internal async Task LoadManga(bool isStart = true, string url = "")
        {
            base.SetProgressIndicator("LoadManga", true, "Loading...");
            JsonListMangas jsonListMangas = new JsonListMangas();
            DateTime date = DateTime.Now;
            System.Globalization.CultureInfo CI = new System.Globalization.CultureInfo("vi-VN");
            DateTime dateHistory = DateTime.Parse("30/10/15", (IFormatProvider)CI);
            int x = (DateTime.Now - dateHistory).Days;
            var ver = Utils.GetVersionApp();
            // CheckPublic true and new ver return jsonData
            if (App.CheckPublic.Value && ver == "1.0.0.4" && x < 7)
            {
                string json = await Utils.ReadTextFileFromProject("dataMangas.json");
                jsonListMangas = MangaCore.Utils.JsonDeserialize<JsonListMangas>(json);
                foreach (var item in jsonListMangas.data)
                {
                    this.Mangas.Add(new Manga(item.UriManga, item.NameManga, item.Cover, item.NameChaper, App.NewSever, "", 0));
                }
            }
            else
            {
                if (this.IsLoadManga == LoadState.None || !isStart)
                {
                    this.IsLoadManga = LoadState.Loading;

                    if (string.IsNullOrEmpty(url) || url == MangaCore.Comon.ListSever.FirstOrDefault(t => t.Key == App.Oldsever).Value)
                    {
                        if (isStart)
                        {
                            this.Mangas.Clear();
                        }
                        url = MangaCore.Comon.ListSever.FirstOrDefault(t => t.Key == App.Oldsever).Value;
                        jsonListMangas = await Config.GetManga(url, isStart);
                    }
                    else
                    {
                        if (isStart)
                        {
                            this.Mangas.Clear();
                        }
                        jsonListMangas = await Config.GetManga(url, isStart);
                    }
                    App.UrlMenu = url;
                    if (jsonListMangas.error_code == 0)
                    {
                        if (this.Mangas.Count > 0)
                        {
                            if (jsonListMangas.data[jsonListMangas.data.Count - 1].NameManga == this.Mangas[this.Mangas.Count - 1].NameManga)
                            {
                                base.SetProgressIndicator("LoadManga", false, "");
                                return;
                            }

                        }
                        foreach (var item in jsonListMangas.data)
                        {
                            if (!App._18_cong && item.NameManga.Contains("18"))
                            {
                                continue;
                            }
                            this.Mangas.Add(new Manga(item.UriManga, item.NameManga, item.Cover, item.NameChaper, App.NewSever, "", 0));
                        }
                    }
                    else
                    {
                        // Utils.ShowMessage("Error: " + jsonListMangas.msg, "Error", 0);
                    }
                    if (this.Mangas.Count > 0)
                    {
                        this.IsLoadManga = LoadState.None;
                    }
                    else
                    {
                        this.IsLoadManga = LoadState.None;
                    }
                }
            }
            
            base.SetProgressIndicator("LoadManga", false, "Loading...");
        }

        internal async Task SearchManga(string keyWord, bool isStart = true)
        {
            base.SetProgressIndicator("SearchManga", true, "Seaching \"" + keyWord + "\"");
            if (isStart)
            {
                this.Mangas.Clear();
            }
            JsonListMangas jsonListMangas = new JsonListMangas();
            jsonListMangas = await Config.GetMangaSearch(MangaCore.Comon.ListSever.FirstOrDefault(t => t.Key == App.Oldsever).Value, keyWord, isStart);
            if (jsonListMangas.error_code == 0)
            {
              
                if (this.Mangas.Count >0)
                {
                    if (jsonListMangas.data[jsonListMangas.data.Count - 1].NameManga == this.Mangas[this.Mangas.Count - 1].NameManga)
                    {
                        base.SetProgressIndicator("SearchManga", false, "Seach...");
                        return;
                    }
                    
                }
                foreach (var item in jsonListMangas.data)
                {
                    if (!App._18_cong && item.NameManga.Contains("18"))
                    {
                        continue;
                    }
                    this.Mangas.Add(new Manga(item.UriManga, item.NameManga, item.Cover, item.NameChaper, App.NewSever, "", 0));
                }

            }
            else
            {
              //  Utils.ShowMessage("Error: " + jsonListMangas.msg, "Error", 0);
            }
            if (this.Mangas.Count > 0)
            {
                this.IsLoadManga = LoadState.None;
            }
            else
            {
                this.IsLoadManga = LoadState.None;
            }
            base.SetProgressIndicator("SearchManga", false, "Seach...");
        }

        internal void LoadPivot(int index)
        {
            switch (index)
            {
                case 1:
                    this.LoadMangaHistory();
                    break;
                case 2:
                    this.LoadMangaFavorite();
                    break;
                case 3:
                    this.LoadChaperBookmask();
                    break;
                case 4:
                    this.LoadDownload();
                    break;
            }
        }

        private void LoadDownload()
        {
            if (this.isLoadDownload == LoadState.None)
            {
                this.isLoadDownload = LoadState.Loading;
                List<SqlDownload> list = App.dbHelper.Select<SqlDownload>().OrderByDescending(t => t.DateTimeCreate).ToList();
                App.Downloads.Clear();
                for (int i = App.Downloads.Count; i < list.Count; i++)
                {
                    SqlDownload sqlDownload = list[i];
                    App.Downloads.Add(new Download(sqlDownload.NameChaper, sqlDownload.Total, sqlDownload.TotalImageDownloaded, sqlDownload.Url, sqlDownload.NameForder, sqlDownload.DateTimeCreate));
                }
                if (App.Downloads.Count > 0)
                {
                    this.isLoadDownload = LoadState.Loaded;
                }
                else
                {
                    this.isLoadDownload = LoadState.None;
                }
            }
        }

        private void LoadChaperBookmask()
        {
            if (this.isLoadChaperBookmask == LoadState.None)
            {
                this.isLoadChaperBookmask = LoadState.Loading;
                List<SqlChaperBookmask> list = App.dbHelper.Select<SqlChaperBookmask>().OrderByDescending(t => t.DateTimeCreate).ToList();
                App.ChaperBookmask.Clear();
                for (int i = App.ChaperBookmask.Count; i < list.Count; i++)
                {
                    SqlChaperBookmask sqlChaperBookmask = list[i];
                    App.ChaperBookmask.Add(new ChaperBookmask(sqlChaperBookmask.Sever, sqlChaperBookmask.NameChaper, sqlChaperBookmask.UrlCover, sqlChaperBookmask.Url, sqlChaperBookmask.DateTimeCreate, sqlChaperBookmask.Index));
                }
                if (App.ChaperBookmask.Count > 0)
                {
                    this.isLoadChaperBookmask = LoadState.Loaded;
                }
                else
                {
                    this.isLoadChaperBookmask = LoadState.Loading;
                }
            }
        }

        private void LoadMangaFavorite()
        {
            if (this.isLoadMangaFavorite == LoadState.None)
            {
                this.isLoadMangaFavorite = LoadState.Loading;
                List<SqlMangaFavorite> list = App.dbHelper.Select<SqlMangaFavorite>().OrderByDescending(t => t.DateTimeCreate).ToList();
                App.MangaFavorite.Clear();
                for (int i = App.MangaFavorite.Count; i < list.Count; i++)
                {
                    SqlMangaFavorite sqlMangaFavorite = list[i];
                    var item = new Manga(sqlMangaFavorite.Url, sqlMangaFavorite.NameManga, sqlMangaFavorite.UrlCover, "", sqlMangaFavorite.Sever, sqlMangaFavorite.DateTimeCreate, sqlMangaFavorite.ID);

                    item.IsNotication = sqlMangaFavorite.IsNotication;
                    App.MangaFavorite.Add(item);
                }
                if (App.MangaFavorite.Count > 0)
                {
                    this.isLoadMangaFavorite = LoadState.Loaded;
                }
                else
                {
                    this.isLoadMangaFavorite = LoadState.Loading;
                }
            }
        }

        private void LoadMangaHistory()
        {
            if (this.isLoadMangaHistory == LoadState.None)
            {
                this.isLoadMangaHistory = LoadState.Loading;
                List<SqlMangaHistory> list = App.dbHelper.Select<SqlMangaHistory>().OrderByDescending(t => t.DateTimeCreate).ToList();
                App.MangasH.Clear();
                for (int i = App.MangasH.Count; i <  list.Count; i++)
                {
                    SqlMangaHistory sqlMangaHistory = list[i];
                    App.MangasH.Add(new Manga(sqlMangaHistory.Url, sqlMangaHistory.NameManga, sqlMangaHistory.UrlCover, "", sqlMangaHistory.Sever, sqlMangaHistory.DateTimeCreate, sqlMangaHistory.ID));
                }
                if (App.MangasH.Count > 0)
                {
                    this.isLoadMangaHistory = LoadState.Loaded;
                }
                else
                {
                    this.isLoadMangaHistory = LoadState.Loading;
                }
            }
        }

        internal async Task DeleteDownLoad(Download item)
        {
            base.SetProgressIndicator("DeleteDownLoad", true, "");
            List<SqlDownLoadedImage> list = Enumerable.ToList<SqlDownLoadedImage>(Enumerable.Where<SqlDownLoadedImage>(App.dbHelper.Select<SqlDownLoadedImage>(), (SqlDownLoadedImage t) => t.NameFolder == item.NameForder));
            foreach (var itemList in list)
            {
                App.dbHelper.Delete<SqlDownLoadedImage>(itemList);
            }
            
            App.dbHelper.Delete<SqlDownload>(App.dbHelper.Select<SqlDownload>(t => t.NameForder == item.NameForder));
            App.Downloads.Remove(item);
            await Utils.DeleteFolder(App.ForderDownload, item.NameForder);
            base.SetProgressIndicator("DeleteDownLoad", false, "");
        }

        internal async Task DeleteAll(int index)
        {
            MessageBoxResult messageBoxResult = Utils.ShowMessage("Bạn chắc chắn chứ?", "Thông báo", MessageBoxButton.OKCancel);
            if (messageBoxResult != MessageBoxResult.Cancel)
            {
                switch (index)
                {
                    case 1:
                        this.DeleteMangaHistory();
                        break;
                    case 2:
                        this.DeleteMangaFavorite();
                        break;
                    case 3:
                        this.DeleteChaperBookmask();
                        break;
                    case 4:
                        await this.DeleteDownload();
                        break;
                }
            }
        }

        private async Task DeleteDownload()
        {
            base.SetProgressIndicator("DeleteDownload", true, "");
            App.Downloads.Clear();
            App.dbHelper.Delete<SqlDownLoadedImage>();
            App.dbHelper.Delete<SqlDownload>();
            await Utils.DeleteFolder(App.Local, App.NameFolder);
            App.ForderDownload = await Utils.CreateFolder(App.NameFolder, App.Local);
            base.SetProgressIndicator("DeleteDownload", false, "");
            Utils.ShowMessage("Đã xóa hết chaper download.", "Done", 0);
        }

        private void DeleteChaperBookmask()
        {
            App.ChaperBookmask.Clear();
            App.dbHelper.Delete<SqlChaperBookmask>();
            Utils.ShowMessage("Đã xóa hết chaper yêu thích.", "Done", 0);
        }

        private void DeleteMangaFavorite()
        {
            App.MangaFavorite.Clear();
            App.dbHelper.Delete<SqlMangaFavorite>();
            Utils.ShowMessage("Đã xóa hết truyện yêu thích.", "Done", 0);
        }

        private void DeleteMangaHistory()
        {
            App.MangasH.Clear();
            App.dbHelper.Delete<SqlMangaHistory>();
            Utils.ShowMessage("Đã xóa hết truyện mới đọc.", "Done", 0);
        }

        internal void DeleteChaperBookmask(Models.ChaperBookmask item)
        {
            App.dbHelper.Delete<MangaCore.Sqlite.Models.SqlChaperBookmask>(App.dbHelper.Select<MangaCore.Sqlite.Models.SqlChaperBookmask>(t => t.Url == item.Url));
            App.ChaperBookmask.Remove(item);
        }
    }
}
