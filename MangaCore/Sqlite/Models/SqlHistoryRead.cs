using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.Sqlite.Models
{
    public class SqlHistoryRead
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

        public int Index
        {
            get;
            set;
        }

        public string UrlChaper
        {
            get;
            set;
        }

        public string UrlManga
        {
            get;
            set;
        }

        public string DateTimeCreate
        {
            get;
            set;
        }

        public SqlHistoryRead()
        {
        }

        public SqlHistoryRead(string sever, string nameChaper, int index, string urlChaper, string urlManga)
        {
            this.Sever = sever;
            this.NameChaper = nameChaper;
            this.Index = index;
            this.UrlChaper = urlChaper;
            this.UrlManga = urlManga;
            this.DateTimeCreate = DateTime.Now.ToString(Comon.FormatDateTime);
        }
    }
}
