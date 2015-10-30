using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore.JsonBase
{
    public class JsonSearch24h
    {
        public string name { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string author { get; set; }
        public int stt { get; set; }
        public string genre { get; set; }
        public int isCheck { get; set; }
        public int chapterCount { get; set; }
        public string source { get; set; }
        public int? views { get; set; }
        public string status { get; set; }
        public object rating { get; set; }
        public int hotRanking { get; set; }
        public string lastChapter { get; set; }
        public int reset { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
