using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaOL.Models
{
    public class Download : BaseModel
    {
        private string _nameChaper;

        private int _total;

        private int _totalImageDownloaded;

        private string _url;

        private string _nameForder;

        private string _dateTimeCreate;

        public string NameChaper
        {
            get
            {
                return this._nameChaper;
            }
            set
            {
                base.SetProperty(ref this._nameChaper, value);
            }
        }

        public int Total
        {
            get
            {
                return this._total;
            }
            set
            {
                base.SetProperty(ref this._total, value);
            }
        }

        public int TotalImageDownloaded
        {
            get
            {
                return this._totalImageDownloaded;
            }
            set
            {
                base.SetProperty(ref this._totalImageDownloaded, value);
            }
        }

        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                base.SetProperty(ref this._url, value);
            }
        }

        public string NameForder
        {
            get
            {
                return this._nameForder;
            }
            set
            {
                base.SetProperty(ref this._nameForder, value);
            }
        }

        public string DateTimeCreate
        {
            get
            {
                return this._dateTimeCreate;
            }
            set
            {
                base.SetProperty(ref this._dateTimeCreate, value);
            }
        }

        public Download()
        {
        }

        public Download(string nameChaper, int total, int totalImageDownloaded, string url, string nameFolder, string dateTimeCreate)
        {
            this.NameChaper = nameChaper;
            this.Total = total;
            this.TotalImageDownloaded = totalImageDownloaded;
            this.Url = url;
            this.DateTimeCreate = dateTimeCreate;
            this.NameForder = nameFolder;
        }
    }
}
