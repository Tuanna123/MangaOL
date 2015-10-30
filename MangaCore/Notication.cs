using MangaCore.Sqlite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore
{
    
    public class Notication
    {

        public async static Task<List<MangaCore.Models.NoticationItem>> GetCountChapMangaFavorite()
        {
            List<MangaCore.Models.NoticationItem> listNameManga = new List<MangaCore.Models.NoticationItem>();
            DatabaseHelper dbHepler = DatabaseHelper.Instance();
            var listMangaFavorite = dbHepler.Select<Sqlite.Models.SqlMangaFavorite>();
            foreach (var item in listMangaFavorite)
            {
                if (!item.IsNotication) continue;
                var detail = await Utilities.Config.GetDetail(item.Url);
                if (detail.status)
                {
                    if (detail.data.listChap.Count > item.ChaperCount)
                    {
                      //  item.ChaperCount = detail.data.listChap.Count;
                       // dbHepler.Update<Sqlite.Models.SqlMangaFavorite>(item);
                        listNameManga.Add(new Models.NoticationItem { name = item.NameManga, count = (detail.data.listChap.Count - item.ChaperCount).ToString(), url = item.Url });
                    }
                }
            }
            return listNameManga;
        }
    }
}
