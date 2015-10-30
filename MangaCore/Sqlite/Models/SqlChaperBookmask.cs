using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.Sqlite.Models
{
    public class SqlChaperBookmask
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

        public string NameChaper
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

        public int Index
        {
            get;
            set;
        }

        public SqlChaperBookmask()
        {
        }

        public SqlChaperBookmask(string sever, string nameChaper, string urlCover, string url, string datetimeCreate, int index)
        {
            this.Sever = sever;
            this.NameChaper = nameChaper;
            this.UrlCover = urlCover;
            this.Url = url;
            this.DateTimeCreate = datetimeCreate;
            this.Index = index;
        }
    }
}
