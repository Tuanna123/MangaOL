using MangaCore.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.Utilities.JsonBase
{
    public class JsonListMangas
    {
        public bool status;

        public int error_code;

        public string msg;

        public List<ListMangas> data;
    }
}
