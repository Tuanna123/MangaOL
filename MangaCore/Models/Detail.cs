using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.Utilities.Models
{
    public class Detail
    {
        public string _detailNameManga
        {
            get;
            set;
        }

        public string _rating
        {
            get;
            set;
        }

        public string _detailUrlCover
        {
            get;
            set;
        }

        public string _detailDescription
        {
            get;
            set;
        }

        public List<Chap> listChap
        {
            get;
            set;
        }
    }
}
