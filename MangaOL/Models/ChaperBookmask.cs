using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaOL.Models
{
    public class ChaperBookmask : BaseModel
    {
        private string _server;

        private string _nameChaper;

        private string _uriCover;

        private string _url;

        private string _dateTimeCreate;

        private int _index;

        public string Sever
        {
            get
            {
                return this._server;
            }
            set
            {
                base.SetProperty<string>(ref this._server, value, "Sever");
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

        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                base.SetProperty<string>(ref this._url, value, "Url");
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

        public int Index
        {
            get
            {
                return this._index;
            }
            set
            {
                base.SetProperty<int>(ref this._index, value, "Index");
            }
        }

        public ChaperBookmask()
        {
        }

        public ChaperBookmask(string sever, string nameChaper, string uriCover, string url, string datetimeCreate, int index)
        {
            this.Sever = sever;
            this.NameChaper = nameChaper;
            this.UriCover = uriCover;
            this.Url = url;
            this.DateTimeCreate = datetimeCreate;
            this.Index = index;
        }
    }
}
