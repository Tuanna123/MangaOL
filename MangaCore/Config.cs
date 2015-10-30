using MangaCore.Utilities.JsonBase;
using MangaCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaCore.Utilities
{
    public class Config
    {
        private static int indexPage = 1;
        private static string hash = "";
        public static string ProcessingUrl(string url, bool isStart)
        {
            if (isStart)
            {
                if (url.Contains("truyentranhtuan.com") || url.Contains("blogtruyen.com"))
                {
                    if (url.Contains("page"))
                    {
                        Config.indexPage = 1;
                        Regex regex = new Regex("page/\\d+");
                        url = regex.Replace(url, "");
                    }
                }
                else if (url.Contains("manga24h.com"))
                {

                    Config.indexPage = 1;
                    if (url.Contains("capnhat"))
                    {
                        Regex regex = new Regex("capnhat/\\d+");
                        url = regex.Replace(url, "/");
                    }
                    else if (url.Contains("html"))
                    {
                        Regex regex = new Regex("html/\\d+");
                        url = regex.Replace(url, "html/");
                    }
                }
                else
                {
                    Config.indexPage = 1;
                }
            }
            else if (url.Contains("truyentranhtuan.com") || url.Contains("blogtruyen.com") || url.Contains("manga24h.com"))
            {
                if (!url.Contains("page") && !url.Contains("manga24h"))
                {
                    url += "page/1";
                }

                else if (!url.Contains("html") && url.Contains("manga24h"))
                {
                    // Config.indexPage = 1;
                    url += "2";
                }
                else if (url.Contains("html") && url.Contains("manga24h"))
                {
                    //Config.indexPage = 1;
                    url += "/2";
                }
            }
            if (url.Contains("truyentranh8.net"))
            {
                Regex regex = new Regex("page=\\d+");
                url = regex.Replace(url, "page=" + Config.indexPage++);
            }
            else if (url.Contains("truyentranhtuan.com"))
            {
                Regex regex = new Regex("page/\\d+");
                url = regex.Replace(url, "page/" + Config.indexPage++);
            }
            else if (url.Contains("manga24h.com"))
            {
                if (url.Contains("capnhat"))
                {
                    Regex regex = new Regex("capnhat/\\d+");
                    url = regex.Replace(url, "capnhat/" + +Config.indexPage++);
                }
                else if (url.Contains("html"))
                {
                    Regex regex = new Regex("html/\\d+");
                    url = regex.Replace(url, "html/" + Config.indexPage++);
                }
            }
            else if (url.Contains("comicvn.net"))
            {
                Regex regex = new Regex("page=\\d+");
                url = regex.Replace(url, "page=" + Config.indexPage++);
            }
            else if (url.Contains("vuitruyentranh.vn"))
            {
                Regex regex = new Regex("page=\\d+");
                url = regex.Replace(url, "page=" + Config.indexPage++);
            }
            else if (url.Contains("izmanga.com"))
            {
                Regex regex = new Regex("page=\\d+");
                url = regex.Replace(url, "page=" + Config.indexPage++);
            }
            else if (url.Contains("hamtruyen.vn"))
            {
                Regex regex = new Regex("/P\\d+");
                url = regex.Replace(url, "/P" + Config.indexPage++);
            }
            else if (url.Contains("blogtruyen.com"))
            {
                Regex regex = new Regex("page/\\d+");
                url = regex.Replace(url, "page/" + Config.indexPage++);
            }
            else if (url.Contains("fphimsex.com"))
            {
                Regex regex = new Regex("tat-ca/\\d+");
                url = regex.Replace(url, "tat-ca/" + Config.indexPage++);
            }
            return url;
        }

        public static string ProcessingKeyWord(string server, bool isStart, string keyWord)
        {
            string text = "";
            keyWord = Utils.RemoveSign4VietnameseString(keyWord);
            if (isStart)
            {
                Config.indexPage = 1;
            }
            if (server.Contains(Utils.GetEnumDescription(Comon.TitleSever.truyentranh8)))
            {
                keyWord = keyWord.Replace(" ", "+");
                text = string.Format("http://m.truyentranh8.net/wap.mobi.php?q={0}&page=1", keyWord);
                Regex regex = new Regex("page=\\d+");
                text = regex.Replace(text, "page=" + Config.indexPage++);
            }
            else if (server.Contains(Utils.GetEnumDescription(Comon.TitleSever.truyentranhtuan)))
            {
                return "http://truyentranhtuan.com/danh-sach-truyen/";
            }
            else if (!server.Contains(Utils.GetEnumDescription(Comon.TitleSever.truyentranhtuan)))
            {
                if (server.Contains(Utils.GetEnumDescription(Comon.TitleSever.vuitruyentranh)))
                {
                    keyWord = keyWord.Replace(" ", "+");
                    text = string.Format("http://vuitruyentranh.vn/mobile/search?key={0}", keyWord);
                    Regex regex = new Regex("page=\\d+");
                    text = regex.Replace(text, "page=" + Config.indexPage++);
                }
                else if (server.Contains(Utils.GetEnumDescription(Comon.TitleSever.manga24h)))
                {
                    text = string.Format("http://haikhanh.me:8080/manga24h/search/{0}", keyWord);
                }
                else if (server.Contains(Utils.GetEnumDescription(Comon.TitleSever.izmanga)))
                {
                    Regex regex = new Regex("page=\\d+");
                    text = string.Format("http://izmanga.com/tim_kiem/{0}", keyWord);
                }
                else if (server.Contains("comicvn.net"))
                {
                    keyWord = keyWord.Replace(" ", "%20");
                    //text = string.Format("http://comicvn.net/truyentranh/manga/search?page=1&orderBy=&type=0&sortBy=date&sortPri=desc&typeView=image&key={0}&type_search=all", keyWord);
                    text = string.Format("http://comicvn.net/tim-kiem/?type_search=truyen&key={0}&hash={1}",keyWord,hash);
                }
                else if (server.Contains(Utils.GetEnumDescription(Comon.TitleSever.hamtruyen)))
                {
                    keyWord = keyWord.Replace(" ", "-");
                    text = string.Format("http://hamtruyen.vn/{0}/search.html", keyWord);
                }
                else if (server.Contains(Utils.GetEnumDescription(Comon.TitleSever.blogtruyen)))
                {
                    keyWord = keyWord.Replace(" ", "+");
                    text = string.Format("http://blogtruyen.com/timkiem?option=1&keyword={0}&page=1", keyWord);
                    Regex regex = new Regex("page=\\d+");
                    text = regex.Replace(text, "page=" + Config.indexPage++);
                }
                else if (server.Contains("fphimsex"))
                {
                    keyWord = keyWord.Replace(" ", "+");
                    text = string.Format("http://fphimsex.com/truyen-tranh/tim-kiem/{0}/{1}", keyWord, 1);
                    Regex regex = new Regex("page=\\d+");
                    text = regex.Replace(text, "page=" + Config.indexPage++);
                }
            }
            return text;
        }
        private static string GetHash(string input)
        {
            string hash = "";
            Regex reg = new Regex("hash:.'\\w*");
            Match math = reg.Match(input);
            while (math.Success)
            {
                hash = math.Groups[0].ToString();
                math = math.NextMatch();
            }
            hash = hash.Replace("hash: '","");
            return hash;
        }
        public static async Task<JsonListMangas> GetManga(string url, bool isStart = true)
        {
            url = Config.ProcessingUrl(url, isStart);
            string html;
            html = await Utils.HttpClientRequert(url, null);
            string json = "";
            if (url.Contains("truyentranh8"))
            {
                json = await GetData.GetMangaTruyenTranh8(html);
            }
            else if (url.Contains("truyentranhtuan.com"))
            {
                json = await GetData.GetMangaTTT(html);
            }
            else if (url.Contains("manga24h.com"))
            {
                json = await GetData.GetManga24H(html);
            }
            else if (url.Contains("comicvn.net"))
            {
                if (string.IsNullOrEmpty(hash))
                {
                    string htmlHash = await Utils.HttpClientRequert("http://comicvn.net/");
                    hash = GetHash(htmlHash);
                }
               //End lấy Hash search
                json = await GetData.GetMangaComicVN(html);
            }
            else if (url.Contains("vuitruyentranh.vn"))
            {
                json = await GetData.GetMangaVTT(html);
            }
            else if (url.Contains("izmanga.com"))
            {
                json = await GetData.GetMangaIZManga(html);
            }
            else if (url.Contains("hamtruyen.vn"))
            {
                json = await GetData.GetMangaHamTruyen(html);
            }
            else if (url.Contains("blogtruyen.com"))
            {
                json = await GetData.GetMangaBlogTruyen(html);
            }
            else if (url.Contains("fphimsex.com"))
            {
                json = await GetData.GetMangaFPhimSex(html);
            }
            JsonListMangas result = new JsonListMangas();
            result = Utils.JsonDeserialize<JsonListMangas>(json);
            return result;
        }

        public static async Task<JsonDetail> GetDetail(string url)
        {
            string html = await Utils.HttpClientRequert(url, null);
            string json = "";
            if (url.Contains("truyentranh8"))
            {
                json = await GetData.GetDetailTT8(html);
            }
            else if (url.Contains("truyentranhtuan.com"))
            {
                json = await GetData.GetDetailTTT(html);
            }
            else if (url.Contains("vuitruyentranh.vn"))
            {
                json = await GetData.GetDetailVTT(html);
            }
            else if (url.Contains("manga24h.com"))
            {
                json = await GetData.GetDetail24H(html);
            }
            else if (url.Contains("comicvn.net"))
            {
                json = await GetData.GetDetailComicVN(html);
            }
            else if (url.Contains("izmanga.com"))
            {
                json = await GetData.GetDetailIZManga(html);
            }
            else if (url.Contains("hamtruyen.vn"))
            {
                json = await GetData.GetDetailHamTruyen(html);
            }
            else if (url.Contains("blogtruyen.com"))
            {
                json = await GetData.GetDetailBlogTruyen(html);
            }
            else if (url.Contains("fphimsex.com"))
            {
                json = await GetData.GetDetailFPhimSex(html);
            }
            JsonDetail result = new JsonDetail();
            result = Utils.JsonDeserialize<JsonDetail>(json);
            return result;
        }

        public static async Task<JsonImage> GetImage(string url)
        {
            string html = await Utils.HttpClientRequert(url, null);
            string json = "";
            if (url.Contains("truyentranh8"))
            {
                json = await GetData.GetImageTT8(html);
            }
            else if (url.Contains("truyentranhtuan.com"))
            {
                json = await GetData.GetImageTTT(html);
            }
            else if (url.Contains("manga24h.com"))
            {
                json = await GetData.GetImage24H(html);
            }
            else if (url.Contains("comicvn.net"))
            {
                json = await GetData.GetImageComicVN(html);
            }
            else if (url.Contains("vuitruyentranh.vn"))
            {
                json = await GetData.GetImageVTT(html);
            }
            else if (url.Contains("izmanga.com"))
            {
                json = await GetData.GetImageIZManga(html);
            }
            else if (url.Contains("hamtruyen.vn"))
            {
                json = await GetData.GetImageHamTruyen(html);
            }
            else if (url.Contains("blogtruyen.com"))
            {
                json = await GetData.GetImageBlogTruyen(html);
            }
            else if (url.Contains("fphimsex.com"))
            {
                json = await GetData.GetImageFPhimSex(html);
            }
            JsonImage result = new JsonImage();
            result = Utils.JsonDeserialize<JsonImage>(json);
            return result;
        }

        public static async Task<JsonListMangas> GetMangaSearch(string server, string keyWord, bool isStart = true)
        {
            string url = Config.ProcessingKeyWord(server, isStart, keyWord);
            string html = await Utils.HttpClientRequert(url, null);

            string json = "";
            if (server.Contains("truyentranh8"))
            {
                json = await GetData.GetMangaTruyenTranh8(html);
            }
            else if (server.Contains("truyentranhtuan.com"))
            {
                json = await GetData.GetMangaTTT(html, keyWord);
            }
            else if (server.Contains("manga24h"))
            {
                json = await GetData.GetSearch24H(html, keyWord);
            }
            else if (server.Contains("comicvn.net"))
            {
                json = await GetData.GetMangaComicVN(html);
            }
            else if (server.Contains("vuitruyentranh.vn"))
            {
                json = await GetData.GetMangaVTT(html);
            }
            else if (server.Contains("izmanga.com"))
            {
                json = await GetData.GetSearchIZManga(html);
            }
            else if (server.Contains("hamtruyen.vn"))
            {
                json = await GetData.GetMangaHamTruyen(html);
            }
            else if (server.Contains("blogtruyen.com"))
            {

                json = await GetData.GetSearchBlogTruyen(html);
            }
            else if (url.Contains("fphimsex.com"))
            {
                json = await GetData.GetMangaFPhimSex(html);
            }
            JsonListMangas result = new JsonListMangas();
            result = Utils.JsonDeserialize<JsonListMangas>(json);
            return result;
        }
    }
}
