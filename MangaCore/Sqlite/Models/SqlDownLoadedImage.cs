using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.Sqlite.Models
{
    public class SqlDownLoadedImage
    {
        [AutoIncrement, PrimaryKey]
        public int ID
        {
            get;
            set;
        }

        public string UrlImage
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public string NameFolder
        {
            get;
            set;
        }

        public string DateTimeCreate
        {
            get;
            set;
        }

        public SqlDownLoadedImage()
        {
        }

        public SqlDownLoadedImage(string urlImage, string path, string nameFolder)
        {
            this.UrlImage = urlImage;
            this.Path = path;
            this.NameFolder = nameFolder;
            this.DateTimeCreate = DateTime.Now.ToString(Comon.FormatDateTime);
        }
    }
}
