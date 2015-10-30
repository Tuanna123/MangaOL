using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaOL.Models
{
    public class Detail : BaseModel
    {
        private string _nameManga;

        private string _urlManga;

        private string _rating;

        private string _urlCover;

        private string _desciption;

        private bool _isFavorite;

        private bool _isRead;

        private List<string> _listImageView;

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

        public string UrlManga
        {
            get
            {
                return this._urlManga;
            }
            set
            {
                base.SetProperty<string>(ref this._urlManga, value, "UrlManga");
            }
        }

        public string Rating
        {
            get
            {
                return this._rating;
            }
            set
            {
                base.SetProperty<string>(ref this._rating, value, "Rating");
            }
        }

        public string UrlCover
        {
            get
            {
                return this._urlCover;
            }
            set
            {
                base.SetProperty<string>(ref this._urlCover, value, "UrlCover");
            }
        }

        public string Description
        {
            get
            {
                return this._desciption;
            }
            set
            {
                base.SetProperty<string>(ref this._desciption, value, "Description");
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

        public List<string> ListImageView
        {
            get
            {
                return this._listImageView;
            }
            set
            {
                base.SetProperty<List<string>>(ref this._listImageView, value, "ListImageView");
            }
        }

        public Detail()
        {
        }

        public Detail(string nameManga, string urlManga, string urlCover, string rating, string description, bool isFavorite, bool isRead)
        {
            this.NameManga = nameManga;
            this.UrlManga = urlManga;
            this.UrlCover = urlCover;
            this.Rating = rating;
            this.Description = description;
            this.IsFavorite = isFavorite;
            this.IsRead = isRead;
        }
    }
}
