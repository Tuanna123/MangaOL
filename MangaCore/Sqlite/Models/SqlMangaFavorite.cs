using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.Sqlite.Models
{
    public class SqlMangaFavorite
    {
        [AutoIncrement, PrimaryKey]
        public int ID
        {
            get;
            set;
        }

        public string Sever
        {
            get;
            set;
        }

        public string NameManga
        {
            get;
            set;
        }

        public string UrlCover
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public string DateTimeCreate
        {
            get;
            set;
        }
        public int ChaperCount
        {
            get;
            set;
        }
        public bool IsNotication { get; set; }
        public SqlMangaFavorite()
        {
        }

        public SqlMangaFavorite(string sever, string nameManga, string urlCover, string url, string datetimeCreate,int chaperCount,bool isNotication)
        {
            this.Sever = sever;
            this.NameManga = nameManga;
            this.UrlCover = urlCover;
            this.Url = url;
            this.DateTimeCreate = datetimeCreate;
            this.ChaperCount = chaperCount;
            this.IsNotication = isNotication;
        }
    }
}
