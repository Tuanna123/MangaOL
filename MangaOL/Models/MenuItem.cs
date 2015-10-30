using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaOL.Models
{
    public class MenuItem : BaseModel
    {
        private bool _isSelected;

        private string _title;

        private string _icon;

        private string _action;

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                base.SetProperty<string>(ref this._title, value, "Title");
            }
        }

        public string Icon
        {
            get
            {
                return this._icon;
            }
            set
            {
                base.SetProperty<string>(ref this._icon, value, "Icon");
            }
        }

        public string Action
        {
            get
            {
                return this._action;
            }
            set
            {
                base.SetProperty<string>(ref this._action, value, "Action");
            }
        }

        public bool IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                this._isSelected = value;
                base.NotifyPropertyChanged("IsSelected");
            }
        }
    }
}
