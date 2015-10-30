using HtmlAgilityPack;
using MangaCore.Utilities.JsonBase;
using MangaCore.Utilities.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaCore.Utilities
{
    public class GetData
    {
        public enum TypeDivMangaTTT
        {
            mangafocus = 1,
            newupdate
        }

        public static List<ListMangas> listManga;

        public static List<Chap> listChap;

        public static Detail detail;

        public static List<string> listImage;

        public static JsonListMangas jsonMangas = new JsonListMangas();

        public static JsonDetail jsonDetail = new JsonDetail();

        public static JsonImage jsonImage = new JsonImage();
        #region Json Object
        private static string ConvertListToJsonManga()
        {
            if (GetData.jsonMangas.status)
            {
                GetData.jsonMangas.msg = "Thành công";
                GetData.jsonMangas.error_code = 0;
                GetData.jsonMangas.data = GetData.listManga;
            }
            else
            {
                GetData.jsonMangas.msg = "Lỗi: x011";
                GetData.jsonMangas.error_code = 1;
                GetData.jsonMangas.data = GetData.listManga;
            }
            return JsonConvert.SerializeObject(GetData.jsonMangas);
        }

        private static string ConvertDetailToJsonDetail()
        {
            if (GetData.jsonDetail.status)
            {
                GetData.jsonDetail.error_code = 0;
                GetData.jsonDetail.msg = "Thành công";
                GetData.jsonDetail.data = GetData.detail;
            }
            else
            {
                GetData.jsonDetail.error_code = 1;
                GetData.jsonDetail.msg = "Lỗi x011";
                GetData.jsonDetail.data = GetData.detail;
            }
            return JsonConvert.SerializeObject(GetData.jsonDetail);
        }

        private static string ConvertListStringToJsonImage()
        {
            if (GetData.jsonImage.status)
            {
                GetData.jsonImage.error_code = 0;
                GetData.jsonImage.msg = "Thành công";
                GetData.jsonImage.data = GetData.listImage;
            }
            else
            {
                GetData.jsonImage.error_code = 0;
                GetData.jsonImage.msg = "Lỗi x011";
                GetData.jsonImage.data = GetData.listImage;
            }
            return JsonConvert.SerializeObject(GetData.jsonImage);
        }
        #endregion
        #region truyentranh8

        public static async Task<string> GetMangaTruyenTranh8(string html)
        {
            GetData.listManga = new List<ListMangas>();
            HtmlDocument htmlDocument = new HtmlDocument();
            try
            {
                List<HtmlNode> list = new List<HtmlNode>();
                list = GetValueNode.GetNode.GetListTag(html, "a", "class", "post");
                foreach (var item in list)
                {
                    string uriManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "href");
                    string nameManga = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(item, new int[] { 3, 0 }, ""));
                    Dictionary<string, string> replaceCover = new Dictionary<string, string>();
                    replaceCover.Add("w44", "h180");
                    string cover = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "data-original", replaceCover);
                    string nameChaper = GetValueNode.GetNode.GetValueHtml(item, new int[] { 5, 0 }, "");
                    if (string.IsNullOrEmpty(nameChaper))
                    {
                        nameChaper = "Đang cập nhật...";
                    }
                    GetData.listManga.Add(new ListMangas
                    {
                        Cover = cover,
                        NameManga = nameManga,
                        NameChaper = nameChaper,
                        UriManga = uriManga,
                        DateTime = ""
                    });
                }
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                GetData.jsonMangas.status = false;
            }
            return GetData.ConvertListToJsonManga();
        }

        public static async Task<List<Chap>> GetChapTT8(string htmlPage, string nameManga)
        {
            GetData.listChap = new List<Chap>();
            try
            {
                List<HtmlNode> list = GetValueNode.GetNode.GetListTag(htmlPage, "div", "class", "ChapList");
                List<HtmlNode> list1 = GetValueNode.GetNode.GetListNodeToNode(list[0], new int[] { 7 });
                foreach (var item in list1[0].ChildNodes)
                {
                    if (item.Name == "#text")
                        continue;
                    string urlChap = GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "href");
                    Dictionary<string, string> replace = new Dictionary<string, string>();
                    replace.Add(GetData.detail._detailNameManga, "");
                    string chap = GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "", replace);
                    GetData.listChap.Add(new Chap
                    {
                        _chap = chap,
                        _urlChap = urlChap
                    });
                }
            }
            catch
            {
            }
            return GetData.listChap;
        }

        public static async Task<string> GetDetailTT8(string html)
        {
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                HtmlNode parentNode = GetValueNode.GetNode.GetOneTag(html, "div", "class", "MangaInfo");
                string detailUrlCover = GetValueNode.GetNode.GetValueHtml(parentNode, new int[] { 5, 1, 0 }, "src");
                string detailNameManga = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(parentNode, new int[] { 1, 0 }, "").Trim());
                string text = GetValueNode.GetNode.GetValueHtml(parentNode, new int[] { 9, 5 }, "data-average").Trim();

                string detailDescription = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(parentNode, new int[] { 11 }, "").Trim());
                if (string.IsNullOrEmpty(detailDescription))
                {
                    detailDescription = "Đang cập nhật...";
                }
                text = text.Replace(".", ",");
                GetData.detail = new Detail
                {
                    _detailNameManga = detailNameManga,
                    _rating = text,
                    _detailUrlCover = detailUrlCover,
                    _detailDescription = detailDescription
                };
                GetData.detail.listChap = await GetData.GetChapTT8(html, detailNameManga);
                if (GetData.detail != null)
                {
                    GetData.jsonDetail.status = true;
                }
                else
                {
                    GetData.jsonDetail.status = false;
                }
            }
            catch
            {
                GetData.jsonDetail.status = false;
            }
            return GetData.ConvertDetailToJsonDetail();
        }

        public static async Task<string> GetImageTT8(string html)
        {
            GetData.listImage = new List<string>();
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                List<string> list = new List<string>();
                string text = GetValueNode.GetNode.GetOneTag(html, "script", "type", "text/javascript", false, 2).InnerText;
                string text2 = "lstImagesVIP.push\\(\"(?<imgUrl>[^\"]+)";
                Regex regex = new Regex(text2);
                MatchCollection matchCollection = regex.Matches(text);
                IEnumerator enumerator = matchCollection.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        Match match = (Match)enumerator.Current;
                        string text3 = match.Groups["imgUrl"].ToString();
                        text3.Replace("http://down.truyentranh8.com/hdd4/", "http://s131.truyentranh8.net/");
                        text3.Replace("http://down.truyentranh8.com/hdd3/", "http://s131.truyentranh8.net/");
                        GetData.listImage.Add(text3);
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                if (GetData.listImage.Count > 0)
                {
                    GetData.jsonImage.status = true;
                }
                else
                {
                    GetData.jsonImage.status = false;
                }
            }
            catch
            {
                GetData.jsonImage.status = false;
            }
            return GetData.ConvertListStringToJsonImage();
        }
        #endregion
        #region Truyện tranh tuần
        public static async Task<string> GetMangaTTT(string html, string keyWord = null)
        {
            try
            {
                GetData.listManga = new List<ListMangas>();
                List<HtmlNode> list = new List<HtmlNode>();
                GetData.TypeDivMangaTTT typeDivMangaTTT = GetData.TypeDivMangaTTT.newupdate;
                list = GetValueNode.GetNode.GetListTag(html, "div", "class", "new-update");
                if (list.Count == 0)
                {
                    typeDivMangaTTT = GetData.TypeDivMangaTTT.mangafocus;
                    list = GetValueNode.GetNode.GetListTag(html, "div", "class", "manga-focus");
                }
                foreach (var item in list)
                {
                    if (typeDivMangaTTT == GetData.TypeDivMangaTTT.newupdate)
                    {
                        var listNode = GetValueNode.GetNode.GetListNode(item);
                        string nameManga = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 3, 1 }, ""));
                        string uriManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 3, 1 }, "href");
                        string cover = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 1 }, "src");
                        string nameChaper = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 7, 0 }, "");
                        if (!string.IsNullOrEmpty(nameChaper))
                        {
                            nameChaper = WebUtility.HtmlDecode(nameChaper);
                        }
                        else
                        {
                            nameChaper = "Đang cập nhật...";
                        }
                        GetData.listManga.Add(new ListMangas
                        {
                            Cover = cover,
                            NameManga = nameManga,
                            NameChaper = nameChaper,
                            UriManga = uriManga,
                            DateTime = ""
                        });
                    }
                    else
                    {
                        var listNode = GetValueNode.GetNode.GetListNode(item);
                        string nameManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "");
                        if (!string.IsNullOrEmpty(keyWord) &&  !nameManga.ToLower().Contains(keyWord.ToLower()))
                        {
                            continue;
                        }
                        string uriManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 0 }, "href");
                        string cover = "";
                        string nameChaper = GetValueNode.GetNode.GetValueHtml(item, new int[] { 3, 1 }, "");
                        if (!string.IsNullOrEmpty(nameChaper))
                        {
                            nameChaper = WebUtility.HtmlDecode(nameChaper);
                        }
                        else
                        {
                            nameChaper = "Đang cập nhật...";
                        }
                        GetData.listManga.Add(new ListMangas
                        {
                            Cover = cover,
                            NameManga = nameManga,
                            NameChaper = nameChaper,
                            UriManga = uriManga,
                            DateTime = ""
                        });
                    }
                }
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            return GetData.ConvertListToJsonManga();
        }

        public static async Task<List<Chap>> GetChapTTT(string html)
        {
            try
            {
                GetData.listChap = new List<Chap>();
                var list2 = GetValueNode.GetNode.GetListTag(html, "span", "class", "chapter-name");
                foreach (var item in list2)
                {
                    string urlChap = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "href");
                    Dictionary<string, string> replace = new Dictionary<string, string>();
                    replace.Add(GetData.detail._detailNameManga, "Chaper");
                    string chap = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "", replace);
                    GetData.listChap.Add(new Chap
                    {
                        _chap = chap,
                        _urlChap = urlChap
                    });
                }
            }
            catch
            {
            }
            return GetData.listChap;
        }

        public static async Task<string> GetDetailTTT(string html)
        {
            try
            {
                var node = GetValueNode.GetNode.GetOneTag(html, "div", "id", "infor-box");
                string detailUrlCover = GetValueNode.GetNode.GetValueHtml(node, new int[] { 1, 1 }, "src");
                string detailNameManga = GetValueNode.GetNode.GetValueHtml(node, new int[] { 3, 1 }, "");
                string rate = GetValueNode.GetNode.GetValueHtml(GetValueNode.GetNode.GetOneTag(html, "span", "id", "rating-point"), new int[] { }, "").Trim();
                string detailDecription = GetValueNode.GetNode.GetValueHtml(node, new int[] { 3, 3, 12 }, "");
                if (!string.IsNullOrEmpty(detailDecription))
                {
                    detailDecription = WebUtility.HtmlDecode(detailDecription);
                }
                else
                {
                    detailDecription = "Đang cập nhật...";
                }
                rate = rate.Replace(".", ",");
                GetData.detail = new Detail
                {
                    _detailNameManga = detailNameManga,
                    _rating = rate,
                    _detailUrlCover = detailUrlCover,
                    _detailDescription = detailDecription
                };
                GetData.detail.listChap = await GetData.GetChapTTT(html);
                if (GetData.detail != null)
                {
                    GetData.jsonDetail.status = true;
                }
                else
                {
                    GetData.jsonDetail.status = false;
                }
            }
            catch
            {
                GetData.jsonDetail.status = false;
            }
            return GetData.ConvertDetailToJsonDetail();
        }

        public static async Task<string> GetImageTTT(string html)
        {
            GetData.listImage = new List<string>();
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                string text = "path.=.\\[.*\\];";
                Regex regex = new Regex(text);
                MatchCollection matchCollection = regex.Matches(html);
                int num = 0;
                string text2;
                if (matchCollection[1].Value.ToString() != "path = [];")
                {
                    num = 0;
                    text2 = matchCollection[1].Value.ToString();
                }
                else
                {
                    num = 1;
                    text2 = matchCollection[0].Value.ToString();
                }
                text = "\"http(?<imgUrl>[^\"]+)";
                Regex regex2 = new Regex(text);
                matchCollection = regex2.Matches(text2);
                IEnumerator enumerator = matchCollection.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        Match match = (Match)enumerator.Current;
                        string text3 = "http" + match.Groups["imgUrl"].ToString();
                        GetData.listImage.Add(text3);
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                if (num == 1 && GetData.listImage.Count > 1)
                {
                    int num2 = GetData.listImage.Count - 1;
                    for (int i = 0; i < num2; i++)
                    {
                        for (int j = i + 1; j < GetData.listImage.Count; j++)
                        {
                            string text4 = GetData.listImage[j].Substring(GetData.listImage[j].Length - 7, 3);
                            string text5 = GetData.listImage[i].Substring(GetData.listImage[i].Length - 7, 3);
                            if (int.Parse(text4) < int.Parse(text5))
                            {
                                string text6 = GetData.listImage[j];
                                GetData.listImage[j] = GetData.listImage[i];
                                GetData.listImage[i] = text6;
                            }
                        }
                    }
                }
                if (GetData.listImage.Count > 0)
                {
                    GetData.jsonImage.status = true;
                }
                else
                {
                    GetData.jsonImage.status = false;
                }
            }
            catch
            {
                GetData.jsonImage.status = false;
            }
            return GetData.ConvertListStringToJsonImage();
        }
        #endregion
        #region manga24h

        public static async Task<string> GetManga24H(string html)
        {
            GetData.listManga = new List<ListMangas>();

            try
            {
                var list = GetValueNode.GetNode.GetListTag(html, "div", "class", "col-md-4 col-xs-12 col-sm-12");
                foreach (var item in list)
                {
                    var listNode = GetValueNode.GetNode.GetListNode(item);
                    string uriManga = "http://manga24h.com/" + GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 1, 2, 0 }, "href");
                    string nameManga = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 1, 2, 0, 0 }, ""));
                    //
                    string cover = GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 1, 1, 1 }, "data-original");
                    //G
                    string nameChaper = GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 1, 4, 1, 0 }, "");

                    if (!string.IsNullOrEmpty(nameChaper))
                    {
                        nameChaper = WebUtility.HtmlDecode(nameChaper);
                    }
                    else
                    {
                        nameChaper = "Đang cập nhật...";
                    }
                    GetData.listManga.Add(new ListMangas
                    {
                        Cover = cover,
                        NameManga = nameManga,
                        NameChaper = nameChaper,
                        UriManga = uriManga,
                        DateTime = ""
                    });
                }

                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                GetData.jsonMangas.status = false;
            }
            return GetData.ConvertListToJsonManga();
        }

        public static async Task<List<Chap>> GetChap24H(string html, string nameManga)
        {
            GetData.listChap = new List<Chap>();
            try
            {
                var list = GetValueNode.GetNode.GetListTag(html, "a", "class", "chap-name");
                foreach (var item in list)
                {
                    string urlChap = "http://manga24h.com/" + GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "href");
                    Dictionary<string, string> replace = new Dictionary<string, string>();
                    replace.Add(GetData.detail._detailNameManga, "");
                    string chap = GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "", replace);
                    GetData.listChap.Add(new Chap
                    {
                        _chap = chap,
                        _urlChap = urlChap
                    });
                }
            }
            catch
            {
            }
            return GetData.listChap;
        }

        public static async Task<string> GetDetail24H(string html)
        {
            try
            {
                var parentNode = GetValueNode.GetNode.GetOneTag(html, "div", "class", "row detail-info");
                var listNode = GetValueNode.GetNode.GetListNode(parentNode);
                string detailUrlCover = GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 5, 1 }, "src");
                string detailNameManga = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 5, 1 }, "alt").Trim());

                List<HtmlNode> list = GetValueNode.GetNode.GetListNodeToNode(listNode[0], new int[] { 7, 1, 3, 1, 1 });
                string text = GetValueNode.GetNode.GetValueHtml(list[0], new int[] { 13, 3,1, 3,1,1,1 }, "", null, "5.5");
                string detailDescription = "";

                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        detailDescription += "\n" + GetValueNode.GetNode.GetValueHtml(item, new int[] {},"");
                    }
                    detailDescription += "\n" + GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 12 }, "");
                }
                else
                {
                    detailDescription = "Đang cập nhật...";
                }
                text = text.Replace(".", ",");
                GetData.detail = new Detail
                {
                    _detailNameManga = detailNameManga,
                    _rating = text,
                    _detailUrlCover = detailUrlCover,
                    _detailDescription = detailDescription
                };
                GetData.detail.listChap = await GetData.GetChap24H(html, detailNameManga);
                if (GetData.detail != null)
                {
                    GetData.jsonDetail.status = true;
                }
                else
                {
                    GetData.jsonDetail.status = false;
                }
            }
            catch
            {
                GetData.jsonDetail.status = false;
            }
            return GetData.ConvertDetailToJsonDetail();
        }

        public static async Task<string> GetImage24H(string html)
        {
            GetData.listImage = new List<string>();
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                List<string> list = new List<string>();
                var node = GetValueNode.GetNode.GetOneTag(html, "div", "id", "manga-chapt-detail-tab-read");
                var listNode = GetValueNode.GetNode.GetListNode(node);
                string script = GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 8 }, "");
                string text2 = "data='.*?;";
                Regex regex = new Regex(text2);
                Match match = regex.Match(script);
                while (match.Success)
                {
                    string data = match.Groups[0].ToString();
                    data = data.Replace("data='", "").Replace("';", "");
                    list = data.Split('|').ToList();
                    match = match.NextMatch();
                }
                GetData.listImage = list;

                if (GetData.listImage.Count > 0)
                {
                    GetData.jsonImage.status = true;
                }
                else
                {
                    GetData.jsonImage.status = false;
                }
            }
            catch
            {
                GetData.jsonImage.status = false;
            }
            return GetData.ConvertListStringToJsonImage();
        }
        public static async Task<string> GetSearch24H(string html,string keyWord)
        {
            GetData.listManga = new List<ListMangas>();
            List<MangaCore.JsonBase.JsonSearch24h> jsonSearch24h = Utils.JsonDeserialize <List<MangaCore.JsonBase.JsonSearch24h>> (html);
            foreach (var item in jsonSearch24h)
            {
                GetData.listManga.Add(new ListMangas
                {
                    Cover = item.image,
                    NameManga = item.name,
                    NameChaper = item.lastChapter,
                    UriManga = item.link,
                    DateTime = ""
                });
            }
            if (GetData.listManga.Count > 0)
            {
                GetData.jsonMangas.status = true;
            }
            else
            {
                GetData.jsonMangas.status = false;
            }
            string result = GetData.ConvertListToJsonManga();
            return result;
        }
        #endregion
        #region ComicVN
        public static async Task<string> GetMangaComicVN(string html)
        {
            GetData.listManga = new List<ListMangas>();

            try
            {
                var list = GetValueNode.GetNode.GetListTag(html, "div", "class", "div_img_cn");
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var listNode = GetValueNode.GetNode.GetListNode(item);
                        string uriManga = "http://comicvn.net/" + GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 3, 1 }, "href");
                        string nameManga = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 3, 1 }, ""));
                        //
                        string cover = GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 1, 3, 1 }, "src");
                        //G
                        string nameChaper = GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 1, 5 }, "");

                        if (!string.IsNullOrEmpty(nameChaper))
                        {
                            nameChaper = WebUtility.HtmlDecode(nameChaper);
                        }
                        else
                        {
                            nameChaper = "Đang cập nhật...";
                        }
                        GetData.listManga.Add(new ListMangas
                        {
                            Cover = cover,
                            NameManga = nameManga,
                            NameChaper = nameChaper,
                            UriManga = uriManga,
                            DateTime = ""
                        });
                    } 
                }
                else
                {
                    var node = GetValueNode.GetNode.GetOneTag(html, "ul", "id", "category");
                    list = GetValueNode.GetNode.GetListNodeToNode(node, new int[] { })[0].ChildNodes.ToList();
                    foreach (var item in list)
                    {
                        if (item.Name == "#text")
                        {
                            continue;
                        }
                        var listNode = GetValueNode.GetNode.GetListNode(item);
                        string uriManga = "http://comicvn.net/" + GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] {1,1 }, "href");
                        string nameManga = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 3, 1 }, ""));
                        //
                        string cover = GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 1, 1, 0 }, "data-original");
                        //G
                        string nameChaper = GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 3,3 }, "");

                        if (!string.IsNullOrEmpty(nameChaper))
                        {
                            nameChaper = WebUtility.HtmlDecode(nameChaper);
                        }
                        else
                        {
                            nameChaper = "Đang cập nhật...";
                        }
                        GetData.listManga.Add(new ListMangas
                        {
                            Cover = cover,
                            NameManga = nameManga,
                            NameChaper = nameChaper,
                            UriManga = uriManga,
                            DateTime = ""
                        });
                    } 
                }

                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            return GetData.ConvertListToJsonManga();
        }

        public static async Task<List<Chap>> GetChapComicVN(string html, string nameManga)
        {
            GetData.listChap = new List<Chap>();
            try
            {
                var node = GetValueNode.GetNode.GetOneTag(html, "div", "class", "list-chapter-content");
                var list = GetValueNode.GetNode.GetListNodeToNode(node, new int[] { 3 })[0].ChildNodes.ToList();
                foreach (var item in list)
                {
                    if (item.Name == "#text" || list.IndexOf(item)==1)
                        continue;
                    string urlChap = "http://comicvn.net/" + GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 0 }, "href");
                    Dictionary<string, string> replace = new Dictionary<string, string>();
                    replace.Add(GetData.detail._detailNameManga, "");
                    string chap = GetValueNode.GetNode.GetValueHtml(item, new int[] {1,0 }, "", replace);
                    GetData.listChap.Add(new Chap
                    {
                        _chap = chap,
                        _urlChap = urlChap
                    });
                }
            }
            catch
            {
            }
            return GetData.listChap;
        }

        public static async Task<string> GetDetailComicVN(string html)
        {
            try
            {
                var parentNode = GetValueNode.GetNode.GetOneTag(html, "div", "class", "item-detail-left");
                var listNode = GetValueNode.GetNode.GetListNode(parentNode);
                string detailUrlCover = GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] {3,3,1 }, "src");
                string detailNameManga = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(listNode[0], new int[] { 3,3,1 }, "alt").Trim());

                List<HtmlNode> list = GetValueNode.GetNode.GetListNodeToNode(listNode[0], new int[] { 3,5,1 });
                string text = "5.5";
                string detailDescription = "";

                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (item.Name == "#text")
                        {
                            continue;
                        }
                        detailDescription += "\n" + GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "");
                    }
                }
                else
                {
                    detailDescription = "Đang cập nhật...";
                }
                text = text.Replace(".", ",");
                GetData.detail = new Detail
                {
                    _detailNameManga = detailNameManga,
                    _rating = text,
                    _detailUrlCover = detailUrlCover,
                    _detailDescription = detailDescription
                };
                GetData.detail.listChap = await GetData.GetChapComicVN(html, detailNameManga);
                if (GetData.detail != null)
                {
                    GetData.jsonDetail.status = true;
                }
                else
                {
                    GetData.jsonDetail.status = false;
                }
            }
            catch
            {
                GetData.jsonDetail.status = false;
            }
            return GetData.ConvertDetailToJsonDetail();
        }

        public static async Task<string> GetImageComicVN(string html)
        {
            GetData.listImage = new List<string>();
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                List<string> list = new List<string>();
                var node = GetValueNode.GetNode.GetOneTag(html, "textarea", "id", "txtarea");
                var listNode = GetValueNode.GetNode.GetListNodeToNode(node,new int[]{1})[0].ChildNodes.ToList();
                if (listNode.Count == 0)
                {
                    listNode = node.ChildNodes.ToList();
                }
                foreach (var item in listNode)
                {
                    if (item.Name =="img")
                    {
                        list.Add(GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "src"));
                    }
                    
                }
                GetData.listImage = list;

                if (GetData.listImage.Count > 0)
                {
                    GetData.jsonImage.status = true;
                }
                else
                {
                    GetData.jsonImage.status = false;
                }
            }
            catch
            {
                GetData.jsonImage.status = false;
            }
            return GetData.ConvertListStringToJsonImage();
        }
        public static async Task<string> GetSearchComicVN(string html, string keyWord)
        {
            GetData.listManga = new List<ListMangas>();
            List<MangaCore.JsonBase.JsonSearch24h> jsonSearch24h = Utils.JsonDeserialize<List<MangaCore.JsonBase.JsonSearch24h>>(html);
            foreach (var item in jsonSearch24h)
            {
                GetData.listManga.Add(new ListMangas
                {
                    Cover = item.image,
                    NameManga = item.name,
                    NameChaper = item.lastChapter,
                    UriManga = item.link,
                    DateTime = ""
                });
            }
            if (GetData.listManga.Count > 0)
            {
                GetData.jsonMangas.status = true;
            }
            else
            {
                GetData.jsonMangas.status = false;
            }
            string result = GetData.ConvertListToJsonManga();
            return result;
        }
        #endregion
        #region Vui truyện tranh
        public static async Task<string> GetMangaVTT(string html)
        {
            try
            {
                GetData.listManga = new List<ListMangas>();
                List<HtmlNode> list = new List<HtmlNode>();
                list = GetValueNode.GetNode.GetListTag(html, "div", "class", "box photo col3");
                foreach (var item in list)
                {
                    string uriManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "href");
                    string nameManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 3 }, "");
                    string cover = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 1 }, "src");
                    string nameChaper = GetValueNode.GetNode.GetValueHtml(item, new int[] { 3 }, "");
                    if (!string.IsNullOrEmpty(nameChaper))
                    {
                        nameChaper = WebUtility.HtmlDecode(nameChaper);
                    }
                    else
                    {
                        nameChaper = "Đang cập nhật...";
                    }
                    GetData.listManga.Add(new ListMangas
                    {
                        Cover = cover,
                        UriManga = uriManga,
                        NameManga = nameManga,
                        NameChaper = nameChaper,
                        DateTime = ""
                    });
                }
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                GetData.jsonMangas.status = false;
            }
            return GetData.ConvertListToJsonManga();
        }

        public static async Task<List<Chap>> GetChapVTT(string html)
        {
            try
            {
                GetData.listChap = new List<Chap>();
                List<HtmlNode> list = GetValueNode.GetNode.GetListTag(html, "ul", "class", "chap-story-show");
                foreach (var item in list[0].ChildNodes)
                {
                    if (item.Name == "#text") continue;
                    string urlChap = GetValueNode.GetNode.GetValueHtml(item, new int[] { 0, 0 }, "href");
                    Dictionary<string, string> replace = new Dictionary<string, string>();
                    replace.Add(GetData.detail._detailNameManga, "");
                    string chap = GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "", replace);
                    GetData.listChap.Add(new Chap
                    {
                        _urlChap = urlChap,
                        _chap = chap
                    });
                }
            }
            catch
            {
            }
            return GetData.listChap;
        }

        public static async Task<string> GetDetailVTT(string html)
        {
            try
            {
                var node = GetValueNode.GetNode.GetOneTag(html, "div", "class", "storyInfo");
                string detailUrlCover = GetValueNode.GetNode.GetValueHtml(node, new int[] { 1, 1 }, "src");
                string detailNameManga = GetValueNode.GetNode.GetValueHtml(node, new int[] { 1, 3, 1 }, "");
                string rate = GetValueNode.GetNode.GetValueHtml(node, new int[] { 1, 3, 7, 1 }, "data-rating");
                string detailDescription = GetValueNode.GetNode.GetValueHtml(node, new int[] { 5, 3, 1 }, ""); ;
                if (string.IsNullOrEmpty(detailDescription))
                {
                    detailDescription = "Đang cập nhật...";
                }
                rate = rate.Replace(".", ",");
                GetData.detail = new Detail
                {
                    _detailNameManga = detailNameManga,
                    _rating = rate,
                    _detailUrlCover = detailUrlCover,
                    _detailDescription = detailDescription
                };
                GetData.detail.listChap = await GetData.GetChapVTT(html);
                if (GetData.detail != null)
                {
                    GetData.jsonDetail.status = true;
                }
                else
                {
                    GetData.jsonDetail.status = false;
                }
            }
            catch
            {
                GetData.jsonDetail.status = false;
            }
            return GetData.ConvertDetailToJsonDetail();
        }

        public static async Task<string> GetImageVTT(string html)
        {
            GetData.listImage = new List<string>();
            try
            {
                var list = GetValueNode.GetNode.GetListTag(html, "img", "id", "comicpage", true);
                foreach (var item in list)
                {
                    GetData.listImage.Add(GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "data-src"));
                }
                if (GetData.listImage.Count > 0)
                {
                    GetData.jsonImage.status = true;
                }
                else
                {
                    GetData.jsonImage.status = false;
                }
            }
            catch
            {
                GetData.jsonImage.status = false;
            }
            return GetData.ConvertListStringToJsonImage();
        }
        #endregion
        #region IZManga
        public static async Task<string> GetMangaIZManga(string html)
        {
            try
            {
                GetData.listManga = new List<ListMangas>();
                var list = GetValueNode.GetNode.GetListTag(html, "div", "class", "list-truyen-item-wrap");
                foreach (var item in list)
                {
                    string uriManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "href");
                    string nameManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 3 }, "");
                    string cover = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 1 }, "src");
                    cover = cover.StartsWith("http") ? cover : "http://izmanga.com" + cover;
                    string nameChaper = GetValueNode.GetNode.GetValueHtml(item, new int[] { 5 }, "");
                    if (!string.IsNullOrEmpty(nameChaper))
                    {
                        nameChaper = WebUtility.HtmlDecode(nameChaper);
                    }
                    else
                    {
                        nameChaper = "Đang cập nhật...";
                    }
                    GetData.listManga.Add(new ListMangas
                    {
                        Cover = cover,
                        UriManga = uriManga,
                        NameManga = nameManga,
                        NameChaper = nameChaper,
                        DateTime = ""
                    });
                }
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                GetData.jsonMangas.status = false;
            }
            return GetData.ConvertListToJsonManga();
        }

        public static async Task<List<Chap>> GetChapIZManga(string html)
        {
            try
            {
                GetData.listChap = new List<Chap>();
                List<HtmlNode> list = new List<HtmlNode>();
                list = GetValueNode.GetNode.GetListTag(html, "div", "class", "chapter-list");
                foreach (var item in list[0].ChildNodes)
                {
                    string urlChap = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 0 }, "href");
                    string chap = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "");
                    GetData.listChap.Add(new Chap
                    {
                        _chap = chap,
                        _urlChap = urlChap
                    });
                }
            }
            catch
            {
            }
            return GetData.listChap;
        }

        public static async Task<string> GetDetailIZManga(string html)
        {
            try
            {
                var node = GetValueNode.GetNode.GetOneTag(html, "div", "class", "manga-info-top");
                string detailUrlCover = "http://izmanga.com" + GetValueNode.GetNode.GetValueHtml(node, new int[] { 1, 1 }, "src");
                string detailNameManga = GetValueNode.GetNode.GetValueHtml(node, new int[] { 3, 1, 1 }, "");
                string detailDescription = GetValueNode.GetNode.GetValueHtml(node, new int[] { 3 }, "");
                if (string.IsNullOrEmpty(detailDescription))
                {
                    detailDescription = "Đang cập nhật...";
                }
                string rating = "40";
                GetData.detail = new Detail
                {
                    _detailNameManga = detailNameManga,
                    _rating = rating,
                    _detailUrlCover = detailUrlCover,
                    _detailDescription = detailDescription
                };
                GetData.detail.listChap = await GetData.GetChapIZManga(html);
                if (GetData.detail != null)
                {
                    GetData.jsonDetail.status = true;
                }
                else
                {
                    GetData.jsonDetail.status = false;
                }
            }
            catch
            {
                GetData.jsonDetail.status = false;
            }
            return GetData.ConvertDetailToJsonDetail();
        }

        public static async Task<string> GetImageIZManga(string html)
        {
            GetData.listImage = new List<string>();
            try
            {
                string text = "data.=.*?;";
                Regex regex = new Regex(text);
                MatchCollection matchCollection = regex.Matches(html);
                string[] array = matchCollection[0].Value.ToString().Replace("data = '", "").Replace(";", "").Split('|');
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string text2 = array2[i];
                    GetData.listImage.Add(text2);
                }

                if (GetData.listImage.Count > 0)
                {
                    GetData.jsonImage.status = true;
                }
                else
                {
                    GetData.jsonImage.status = false;
                }
            }
            catch
            {
                GetData.jsonImage.status = false;
            }
            return GetData.ConvertListStringToJsonImage();
        }
        public static async Task<string> GetSearchIZManga(string html)
        {
            try
            {
                GetData.listManga = new List<ListMangas>();
                var list = GetValueNode.GetNode.GetListTag(html, "div", "class", "daily-update-item");
                foreach (var item in list)
                {
                    string uriManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 3 }, "href");
                    string nameManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "");
                    string cover = "";
                    string nameChaper = GetValueNode.GetNode.GetValueHtml(item, new int[] { 3 }, "");
                    if (!string.IsNullOrEmpty(nameChaper))
                    {
                        nameChaper = WebUtility.HtmlDecode(nameChaper);
                    }
                    else
                    {
                        nameChaper = "Đang cập nhật...";
                    }
                    GetData.listManga.Add(new ListMangas
                    {
                        Cover = cover,
                        UriManga = uriManga,
                        NameManga = nameManga,
                        NameChaper = nameChaper,
                        DateTime = ""
                    });
                }
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                GetData.jsonMangas.status = false;
            }
            return GetData.ConvertListToJsonManga();
        }
        #endregion
        #region Hamtruyen
        public static async Task<string> GetMangaHamTruyen(string html)
        {
            try
            {
                GetData.listManga = new List<ListMangas>();
                var list = GetValueNode.GetNode.GetListTag(html, "div", "class", "item_truyennendoc");
                foreach (var item in list)
                {
                    string uriManga = "http://hamtruyen.vn" + GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "href");
                    string nameManga = WebUtility.HtmlDecode(GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, ""));
                    string cover = GetValueNode.GetNode.GetValueHtml(item, new int[] { 0, 0, 0 }, "src");
                    string nameChaper = GetValueNode.GetNode.GetValueHtml(item, new int[] { 0, 1 }, "");
                    if (string.IsNullOrEmpty(nameChaper))
                    {
                        nameChaper = "Đang cập nhật...";
                    }
                    GetData.listManga.Add(new ListMangas
                    {
                        Cover = cover,
                        NameManga = nameManga,
                        NameChaper = nameChaper,
                        UriManga = uriManga,
                        DateTime = ""
                    });
                }
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                GetData.jsonMangas.status = false;
            }
            return GetData.ConvertListToJsonManga();
        }

        public static async Task<List<Chap>> GetChapHamTruyen(string html)
        {
            try
            {
                GetData.listChap = new List<Chap>();
                var list = GetValueNode.GetNode.GetListTag(html, "section", "class", "row_chap");
                foreach (var item in list)
                {
                    string urlChap = "http://hamtruyen.vn" + GetValueNode.GetNode.GetValueHtml(item, new int[] { 0, 0 }, "href");
                    string chap = GetValueNode.GetNode.GetValueHtml(item, new int[] { 0, 0 }, "");
                    GetData.listChap.Add(new Chap
                    {
                        _chap = chap,
                        _urlChap = urlChap
                    });
                }
            }
            catch
            {
            }
            return GetData.listChap;
        }

        public static async Task<string> GetDetailHamTruyen(string html)
        {
            try
            {
                var node = GetValueNode.GetNode.GetOneTag(html, "div", "id", "content_truyen");
                string detailUrlCover = GetValueNode.GetNode.GetValueHtml(node, new int[] { 1, 1 }, "src");
                string detailNameManga = GetValueNode.GetNode.GetValueHtml(node, new int[] { 2, 0 }, "");
                string rate = GetValueNode.GetNode.GetValueHtml(node, new int[] { 2, 8 }, "");
                string detailDecription = GetValueNode.GetNode.GetValueHtml(node, new int[] { 2, 11 }, "");
                if (string.IsNullOrEmpty(detailDecription))
                {
                    detailDecription = "Đang cập nhật...";
                }
                GetData.detail = new Detail
                {
                    _detailNameManga = detailNameManga,
                    _rating = rate,
                    _detailUrlCover = detailUrlCover,
                    _detailDescription = detailDecription
                };
                GetData.detail.listChap = await GetData.GetChapHamTruyen(html);
                if (GetData.detail != null)
                {
                    GetData.jsonDetail.status = true;
                }
                else
                {
                    GetData.jsonDetail.status = false;
                }
            }
            catch
            {
                GetData.jsonDetail.status = false;
            }
            return GetData.ConvertDetailToJsonDetail();
        }

        public static async Task<string> GetImageHamTruyen(string html)
        {
            GetData.listImage = new List<string>();
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                HtmlNode htmlNode = Enumerable.ToList<HtmlNode>(Enumerable.Where<HtmlNode>(htmlDocument.DocumentNode.Descendants("div"), (HtmlNode div) => div.GetAttributeValue("id", "").ToString() == "content_chap"))[0];
                var node = GetValueNode.GetNode.GetOneTag(html, "div", "id", "content_chap");
                foreach (var item in node.ChildNodes)
                {
                    if (item.Name != "img")
                        continue;
                    GetData.listImage.Add(GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "src"));
                }
                if (GetData.listImage.Count > 0)
                {
                    GetData.jsonImage.status = true;
                }
                else
                {
                    GetData.jsonImage.status = false;
                }
            }
            catch
            {
                GetData.jsonImage.status = false;
            }
            return GetData.ConvertListStringToJsonImage();
        }
        #endregion
        #region Blogtruyen
        public static async Task<string> GetMangaBlogTruyen(string html)
        {
            try
            {
                GetData.listManga = new List<ListMangas>();
                var list = GetValueNode.GetNode.GetListTag(html, "section", "class", "bg-white storyitem");
                if (list.Count == 0)
                {
                    await GetSearchBlogTruyen(html);
                }
                else
                {
                    foreach (var item in list)
                    {
                        string uriManga = "http://blogtruyen.com" + GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 1 }, "href");
                        string nameManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 1, 1 }, "alt");
                        string cover = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 1, 1 }, "src");
                        string nameChaper = GetValueNode.GetNode.GetValueHtml(item, new int[] { 7, 1, 9 }, "");
                        if (string.IsNullOrEmpty(nameChaper))
                        {
                            nameChaper = "Đang cập nhật...";
                        }
                        GetData.listManga.Add(new ListMangas
                        {
                            Cover = cover,
                            UriManga = uriManga,
                            NameManga = nameManga,
                            NameChaper = nameChaper,
                            DateTime = ""
                        });
                    }
                }
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                GetData.jsonMangas.status = false;
            }
            return GetData.ConvertListToJsonManga();
        }

        public static async Task<List<Chap>> GetChapBlogTruyen(string html)
        {
            try
            {
                GetData.listChap = new List<Chap>();
                var list = GetValueNode.GetNode.GetListTag(html, "p", "id", "chapter", true);
                foreach (var item in list)
                {
                    string urlChap = "http://blogtruyen.com" + GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 0 }, "href");
                    Dictionary<string, string> replace = new Dictionary<string, string>();
                    replace.Add(GetData.detail._detailNameManga, "");
                    string chap = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1, 0 }, "",replace);
                    GetData.listChap.Add(new Chap
                    {
                        _chap = chap,
                        _urlChap = urlChap
                    });
                }
            }
            catch
            {
            }
            return GetData.listChap;
        }

        public static async Task<string> GetDetailBlogTruyen(string html)
        {
            try
            {
                var node = GetValueNode.GetNode.GetOneTag(html, "section", "class", "bg-white story-detail");
                Dictionary<string, string> replace = new Dictionary<string, string>();
                replace.Add("truyện tranh ", "");
                string detailUrlCover = GetValueNode.GetNode.GetValueHtml(node, new int[] { 5, 1 }, "src");
                string detailNameManga =GetValueNode.GetNode.GetValueHtml(node, new int[] { 3, 1 }, "title", replace);
                string rating = "3.5";
                string detailDecription = GetValueNode.GetNode.GetValueHtml(node, new int[] { 7, 3 }, "");
                if (string.IsNullOrEmpty(detailDecription))
                {
                    detailDecription = "Đang cập nhật...";
                }
                GetData.detail = new Detail
                {
                    _detailNameManga = detailNameManga,
                    _rating = rating,
                    _detailUrlCover = detailUrlCover,
                    _detailDescription = detailDecription
                };
                GetData.detail.listChap = await GetData.GetChapBlogTruyen(html);
                if (GetData.detail != null)
                {
                    GetData.jsonDetail.status = true;
                }
                else
                {
                    GetData.jsonDetail.status = false;
                }
            }
            catch
            {
                GetData.jsonDetail.status = false;
            }
            return GetData.ConvertDetailToJsonDetail();
        }

        public static async Task<string> GetImageBlogTruyen(string html)
        {
            GetData.listImage = new List<string>();
            try
            {
                var node = GetValueNode.GetNode.GetOneTag(html, "article", "id", "content");
                foreach (var item in node.ChildNodes)
                {
                    if (item.Name == "img")
                    {
                        GetData.listImage.Add(GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "src"));
                    }
                }
                if (GetData.listImage.Count > 0)
                {
                    GetData.jsonImage.status = true;
                }
                else
                {
                    GetData.jsonImage.status = false;
                }
            }
            catch
            {
                GetData.jsonImage.status = false;
            }
            return GetData.ConvertListStringToJsonImage();
        }

        public static async Task<string> GetSearchBlogTruyen(string html)
        {
            try
            {
                GetData.listManga = new List<ListMangas>();
                var list = GetValueNode.GetNode.GetListTag(html, "div", "class", "hidden tiptip-content");
                var list2 = GetValueNode.GetNode.GetOneTag(html, "div", "class", "list").Descendants("p").Where(t=>t.Name!="#text").ToList();
                foreach (var item in list)
                {
                    var node = list2[list.IndexOf(item) + 1];
                    string uriManga = "http://blogtruyen.com" + GetValueNode.GetNode.GetValueHtml(node, new int[] { 1,1 }, "href");
                    string nameManga = GetValueNode.GetNode.GetValueHtml(item, new int[] { 3 }, "");
                    string cover = GetValueNode.GetNode.GetValueHtml(item, new int[] { 1 }, "src");
                    string nameChaper = "Chaper " + GetValueNode.GetNode.GetValueHtml(item, new int[] {3 }, "");
                    if (string.IsNullOrEmpty(nameChaper))
                    {
                        nameChaper = "Đang cập nhật...";
                    }

                    GetData.listManga.Add(new ListMangas
                    {
                        Cover = cover,
                        UriManga = uriManga,
                        NameManga = nameManga,
                        NameChaper = nameChaper,
                        DateTime = ""
                    });
                }
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                GetData.jsonMangas.status = false;
            }
            return GetData.ConvertListToJsonManga();
        }
        #endregion
        #region FPhimSex
        public static async Task<string> GetMangaFPhimSex(string html)
        {
            try
            {
                GetData.listManga = new List<ListMangas>();
                var list = GetValueNode.GetNode.GetListTag(html, "div", "class", "row photostory");
                foreach (var item in list[0].ChildNodes)
                {
                    if (item.Name =="a")
                    {
                        var node = GetValueNode.GetNode.GetListNode(item);
                        string uriManga = GetValueNode.GetNode.GetValueHtml(node[0], new int[] { }, "href");
                        string nameManga = GetValueNode.GetNode.GetValueHtml(node[0], new int[] { 1, 3 }, "");
                        string cover = GetValueNode.GetNode.GetValueHtml(node[0], new int[] { 1, 1, 1 }, "src");
                        string nameChaper = GetValueNode.GetNode.GetValueHtml(node[0], new int[] { 1, 1, 3, 7 }, "");
                        if (!string.IsNullOrEmpty(nameChaper))
                        {
                            nameChaper = "Chap " + nameChaper;
                        }
                        else
                        {
                            nameChaper = "Đang cập nhật...";
                        }
                        GetData.listManga.Add(new ListMangas
                        {
                            Cover = cover,
                            UriManga = uriManga,
                            NameManga = nameManga,
                            NameChaper = nameChaper,
                            DateTime = ""
                        });
                    }
                }
                using (List<HtmlNode>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        HtmlNode current = enumerator.Current;
                        
                    }
                }
                if (GetData.listManga.Count > 0)
                {
                    GetData.jsonMangas.status = true;
                }
                else
                {
                    GetData.jsonMangas.status = false;
                }
            }
            catch
            {
                GetData.jsonMangas.status = false;
            }
            return GetData.ConvertListToJsonManga();
        }

        public static async Task<List<Chap>> GetChapFPhimSex(string html)
        {
            try
            {
                GetData.listChap = new List<Chap>();
                var list = GetValueNode.GetNode.GetListTag(html, "a", "class", "link_");
                foreach (var item in list)
                {
                    string urlChap = GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "href");
                    string chap = GetValueNode.GetNode.GetValueHtml(item, new int[] { }, "");
                    GetData.listChap.Add(new Chap
                    {
                        _chap = chap,
                        _urlChap = urlChap
                    });
                }
               
            }
            catch
            {
            }
            return GetData.listChap;
        }

        public static async Task<string> GetDetailFPhimSex(string html)
        {
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                var node = GetValueNode.GetNode.GetOneTag(html, "div", "class", "col-md-9 baiviet baiviet_tintuc");
                string detailUrlCover = GetValueNode.GetNode.GetValueHtml(node, new int[] { 5,1}, "src");
                string detailNameManga = GetValueNode.GetNode.GetValueHtml(node, new int[] { 5,1}, "alt");
                string rating = "3.5";
                string description = "Đang cập nhật...";
                GetData.detail = new Detail
                {
                    _detailNameManga = detailNameManga,
                    _rating = rating,
                    _detailUrlCover = detailUrlCover,
                    _detailDescription = description
                };
                GetData.detail.listChap = await GetData.GetChapFPhimSex(html);
                if (GetData.detail != null)
                {
                    GetData.jsonDetail.status = true;
                }
                else
                {
                    GetData.jsonDetail.status = false;
                }
            }
            catch
            {
                GetData.jsonDetail.status = false;
            }
            return GetData.ConvertDetailToJsonDetail();
        }

        public static async Task<string> GetImageFPhimSex(string html)
        {
            GetData.listImage = new List<string>();
            try
            {
                var list = GetValueNode.GetNode.GetListTag(html, "img", "chuyenmuc", "photo");
                foreach (var item in list)
                {
                    GetData.listImage.Add(GetValueNode.GetNode.GetValueHtml(item,new int[]{},"src"));
                }
                if (GetData.listImage.Count > 0)
                {
                    GetData.jsonImage.status = true;
                }
                else
                {
                    GetData.jsonImage.status = false;
                }
            }
            catch
            {
                GetData.jsonImage.status = false;
            }
            return GetData.ConvertListStringToJsonImage();
        }
        #endregion
    }
}
