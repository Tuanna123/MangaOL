using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaOL.Models
{
    public class Chaper : BaseModel
    {
        private string _nameChaper;

        private string _urlChaper;

        private string _dateTime;

        private bool _isRead;

        private bool _isReading;

        private bool _isDownload;

        private bool _isFavorite;

        private string _index;
        private bool _isSelect;

        public string NameChaper
        {
            get
            {
                return this._nameChaper;
            }
            set
            {
                base.SetProperty<string>(ref this._nameChaper, value, "NameChaper");
            }
        }

        public string UrlChaper
        {
            get
            {
                return this._urlChaper;
            }
            set
            {
                base.SetProperty<string>(ref this._urlChaper, value, "UrlChaper");
            }
        }

        public string DateTime
        {
            get
            {
                return this._dateTime;
            }
            set
            {
                base.SetProperty<string>(ref this._dateTime, value, "DateTime");
            }
        }

        public bool IsRead
        {
            get
            {
                return this._isRead;
            }
            set
            {
                base.SetProperty<bool>(ref this._isRead, value, "IsRead");
            }
        }

        public bool IsReading
        {
            get
            {
                return this._isReading;
            }
            set
            {
                base.SetProperty<bool>(ref this._isReading, value, "IsReading");
            }
        }

        public bool IsDownload
        {
            get
            {
                return this._isDownload;
            }
            set
            {
                base.SetProperty<bool>(ref this._isDownload, value, "IsDownload");
            }
        }

        public bool IsFavorite
        {
            get
            {
                return this._isFavorite;
            }
            set
            {
                base.SetProperty<bool>(ref this._isFavorite, value, "IsFavorite");
            }
        }

        public string Index
        {
            get
            {
                return this._index;
            }
            set
            {
                base.SetProperty<string>(ref this._index, value, "Index");
            }
        }
        public bool IsSelect {
            get
            {
                return this._isSelect;
            }
            set
            {
                base.SetProperty(ref this._isSelect, value);
            }
        }
        public Chaper()
        {
        }

        public Chaper(string nameChaper, string urlChaper, string dateTime, bool isRead, bool isReading, bool isDownload, bool isFavorite, string index,bool isSelect)
        {
            this.NameChaper = nameChaper;
            this.UrlChaper = urlChaper;
            this.DateTime = dateTime;
            this.IsRead = isRead;
            this.IsReading = isReading;
            this.IsDownload = isDownload;
            this.IsFavorite = isFavorite;
            this.Index = index;
            this.IsSelect = isSelect;
        }
    }
}
