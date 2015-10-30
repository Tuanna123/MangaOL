using MangaCore.Utilities;
using MangaOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaOL.UserControls.UcViewModels
{
    public class TemplateHomeHozVM : Models.BaseModel
    {
        ViewModels.MainPageVM MainPageVM = new ViewModels.MainPageVM();
        public TemplateHomeHozVM()
        {

        }
        public void UpdatePinOrUpPin(Models.Manga item)
        {
            bool Pin;
            Utils.SecondaryTileUriSource = "/Views/DetailPage.xaml?UriManga=" + item.UriManga + "";
            var shellTitle = Utils.FindTile(Utils.SecondaryTileUriSource);
            if (shellTitle != null)
                Pin = true;
            else
                Pin = false;
            var listMangas = MainPageVM.Mangas.Where(t => t.UriManga == item.UriManga).ToList();
            var listHistorys = MainPageVM.MangasH.Where(t => t.UriManga == item.UriManga).ToList();
            var listFavorites = MainPageVM.MangaFavorite.Where(t => t.UriManga == item.UriManga).ToList();
            if (listHistorys != null)
            {
                foreach (var itemHistory in listHistorys)
                {
                    itemHistory.IsPin = Pin;
                }
                
            }
            if (listMangas != null)
            {
                foreach (var itemManga in listMangas)
                {
                    itemManga.IsPin = Pin;
                }
            }
            if (listFavorites != null)
            {
                foreach (var itemFavorite in listFavorites)
                {
                    itemFavorite.IsPin = Pin;
                }
            }
        }
        public async Task InsertDeleteFavorite(MangaOL.Models.Manga item, bool isFavorite)
        {
            var listMangas = MainPageVM.Mangas.Where(t => t.UriManga == item.UriManga).ToList();
            var listHistorys = MainPageVM.MangasH.Where(t => t.UriManga == item.UriManga).ToList();
            if (listHistorys != null)
            {
                foreach (var itemHistory in listHistorys)
                {
                    itemHistory.IsFavorite = !isFavorite;
                }
            }
            if (listMangas != null)
            {
                foreach (var itemManga in listMangas)
                {
                    itemManga.IsFavorite = !isFavorite;
                }
            }

            if (isFavorite)
            {
                MangaCore.Sqlite.Models.SqlMangaFavorite i = App.dbHelper.Select<MangaCore.Sqlite.Models.SqlMangaFavorite>(t => t.Url == item.UriManga);
                App.dbHelper.Delete<MangaCore.Sqlite.Models.SqlMangaFavorite>(i);
                //MainPageVM.MangaFavorite.Remove(MainPageVM.MangaFavorite.FirstOrDefault(t => t.UriManga == item.UriManga));
                App.MangaFavorite.Remove(App.MangaFavorite.FirstOrDefault(t => t.UriManga == item.UriManga));
            }
            else
            {
                var jsonDetail = await Config.GetDetail(item.UriManga);
                if (jsonDetail.status)
                {
                    App.dbHelper.Insert<MangaCore.Sqlite.Models.SqlMangaFavorite>(new MangaCore.Sqlite.Models.SqlMangaFavorite(App.NewSever, item.NameManga, item.UriCover, item.UriManga, DateTime.Now.ToString(MangaCore.Comon.FormatDateTime), jsonDetail.data.listChap.Count, true));
                    var itemManga = new Manga(item.UriManga, item.NameManga, item.UriCover, "", App.NewSever, DateTime.Now.ToString(MangaCore.Comon.FormatDateTime), 0);
                    itemManga.IsNotication = true;
                    App.MangaFavorite.Insert(0, itemManga);
                }
            }

        }

    }
}
