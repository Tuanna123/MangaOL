using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaOL.Models
{
    public class Manga : BaseModel
    {
        private int _id;

        private string _uriManga;

        private string _nameManga;

        private string _sever;

        private string _uriCover;

        private string _nameChaper;

        private string _dateTimeCreate;
        private bool _isNotication;
        private bool _isFavorite;
        private bool _isPin;

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                base.SetProperty<int>(ref this._id, value, "ID");
            }
        }

        public string UriManga
        {
            get
            {
                return this._uriManga;
            }
            set
            {
                base.SetProperty<string>(ref this._uriManga, value, "UriManga");
            }
        }

        public string NameManga
        {
            get
            {
                return this._nameManga;
            }
            set
            {
                base.SetProperty<string>(ref this._nameManga, value, "NameManga");
            }
        }

        public string UriCover
        {
            get
            {
                return this._uriCover;
            }
            set
            {
                base.SetProperty<string>(ref this._uriCover, value, "UriCover");
            }
        }

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

        public string Sever
        {
            get
            {
                return this._sever;
            }
            set
            {
                base.SetProperty<string>(ref this._sever, value, "Sever");
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
                base.SetProperty<string>(ref this._dateTimeCreate, value, "DateTimeCreate");
            }
        }
        public bool  IsNotication
        {
            get
            {
                return this._isNotication;
            }
            set
            {
                this.SetProperty(ref this._isNotication, value);
            }
        }
        public bool IsFavorite
        {
            get { return this._isFavorite; }
            set { this.SetProperty(ref this._isFavorite, value); }
        }
        public bool IsPin
        {
            get { return this._isPin; }
            set { this.SetProperty(ref this._isPin, value); }
        }
        public Manga()
        {
        }

        public Manga(string urlManga, string nameManga, string cover, string nameChaper, string sever, string dateTimeCreate = "", int id = 0)
        {
            this.ID = id;
            this.UriCover = cover;
            this.UriManga = urlManga;
            this.NameManga = nameManga;
            this.Sever = sever;
            this.NameChaper = nameChaper;
            this.DateTimeCreate = dateTimeCreate;
        }
    }
}
