using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MangaOL.Models
{
    public class Image : BaseModel
    {
        private string _urlImage;

        private Visibility _visibility;

        private string _tag;

        private int _stt;

        private int _totalImage;
        private byte[] _bytes;
        

        public string URLImage
        {
            get
            {
                return this._urlImage;
            }
            set
            {
                base.SetProperty<string>(ref this._urlImage, value, "URLImage");
            }
        }

        public Visibility Visibility
        {
            get
            {
                return this._visibility;
            }
            set
            {
                base.SetProperty<Visibility>(ref this._visibility, value, "Visibility");
            }
        }

        public string Tag
        {
            get
            {
                return this._tag;
            }
            set
            {
                base.SetProperty<string>(ref this._tag, value, "Tag");
            }
        }

        public int Stt
        {
            get
            {
                return this._stt;
            }
            set
            {
                base.SetProperty<int>(ref this._stt, value, "Stt");
            }
        }

        public int TotalImage
        {
            get
            {
                return this._totalImage;
            }
            set
            {
                base.SetProperty<int>(ref this._totalImage, value, "TotalImage");
            }
        }

        public string Index
        {
            get
            {
                return (this.Stt + 1).ToString() + " /" + this.TotalImage.ToString();
            }
        }

        public byte[] Bytes
        {
            get { return _bytes; }
            set { this.SetProperty(ref this._bytes, value); }
        }
        public Image()
        {
        }

        public Image(string urlImage, string idChap, string nameChap, Visibility visibility, string tag, int stt, int totalImage)
        {
            this.URLImage = urlImage;
            this.Visibility = visibility;
            this.Tag = tag;
            this.Stt = stt;
            this.TotalImage = totalImage;
        }
    }
}
