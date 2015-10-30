using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.Utilities.Models
{
    public class MenuManga
    {
        public string Sever
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }
        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { 
                if(string.IsNullOrEmpty(value))
                _tag = "20" ;
            else 
                    _tag = value; 
            }
        }
    }
}
