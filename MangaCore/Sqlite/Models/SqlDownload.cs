using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.Sqlite.Models
{
    public class SqlDownload
    {
        [AutoIncrement, PrimaryKey]
        public int ID
        {
            get;
            set;
        }

        public string NameChaper
        {
            get;
            set;
        }

        public int Total
        {
            get;
            set;
        }

        public int TotalImageDownloaded
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public string NameForder
        {
            get;
            set;
        }

        public string DateTimeCreate
        {
            get;
            set;
        }

        public SqlDownload()
        {
        }

        public SqlDownload(string nameChaper, int total, int totalImageDownloaded, string url, string datetimeCreate, string nameFolder)
        {
            this.NameChaper = nameChaper;
            this.Total = total;
            this.TotalImageDownloaded = totalImageDownloaded;
            this.Url = url;
            this.DateTimeCreate = datetimeCreate;
            this.NameChaper = nameChaper;
        }
    }
}
