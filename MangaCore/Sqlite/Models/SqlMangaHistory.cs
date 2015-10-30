using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.Sqlite.Models
{
    public class SqlMangaHistory
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

        public SqlMangaHistory()
        {
        }

        public SqlMangaHistory(string nameManga, string sever, string urlCover, string url,string datetimeCreate)
        {
            this.Sever = sever;
            this.NameManga = nameManga;
            this.UrlCover = urlCover;
            this.Url = url;
            this.DateTimeCreate = datetimeCreate;
        }
    }
}
