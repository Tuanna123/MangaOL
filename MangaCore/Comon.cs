using MangaCore.Utilities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCore
{
    public class Comon
    {
        public enum TitleSever
        {
            [Description("truyentranh8")]
            truyentranh8,
            [Description("truyentranhtuan.com")]
            truyentranhtuan,
            [Description("manga24h")]
            manga24h,
            [Description("comicVN.net")]
            comicVN,
            [Description("vuitruyentranh")]
            vuitruyentranh,
            [Description("izmanga")]
            izmanga,
            [Description("hamtruyen")]
            hamtruyen,
            [Description("blogtruyen")]
            blogtruyen,
            [Description("Muoi tam cong 1")]
            fphimsex,
        }
        static Comon()
        {
            // Note: this type is marked as 'beforefieldinit'.
            ListSever = new List<Models.Severs>();
            ListSever.Add(new Models.Severs { Key = Utils.GetEnumDescription(TitleSever.truyentranh8), Value = "http://m.truyentranh8.net/page=1" });
            ListSever.Add(new Models.Severs { Key = Utils.GetEnumDescription(TitleSever.truyentranhtuan), Value = "http://truyentranhtuan.com/" });
            ListSever.Add(new Models.Severs { Key = Utils.GetEnumDescription(TitleSever.manga24h), Value = "http://manga24h.com/capnhat/" });
            ListSever.Add(new Models.Severs { Key = Utils.GetEnumDescription(TitleSever.comicVN), Value = "http://comicvn.net/truyentranh/manga/list?page=1&orderBy=moi" });
            ListSever.Add(new Models.Severs { Key = Utils.GetEnumDescription(TitleSever.vuitruyentranh), Value = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=0&cat_seo=" });
            ListSever.Add(new Models.Severs { Key = Utils.GetEnumDescription(TitleSever.izmanga), Value = "http://izmanga.com/danh_sach_truyen?type=new&category=all&alpha=all&page=1&state=all&group=alll" });
            ListSever.Add(new Models.Severs { Key = Utils.GetEnumDescription(TitleSever.hamtruyen), Value = "http://hamtruyen.vn/danhsach/P1/index.html?sort=2" });
            ListSever.Add(new Models.Severs { Key = Utils.GetEnumDescription(TitleSever.blogtruyen), Value = "http://blogtruyen.com/trangchu/" });
            ListSever.Add(new Models.Severs { Key = Utils.GetEnumDescription(TitleSever.fphimsex), Value = "http://fphimsex.com/truyen-tranh/top/tat-ca/1", Tag = 18 });
        }
        public static string AppName = "Hub Comics";
        public static string DBName = "Database/Manga.sqlite";
        public static string NoticationChaper = "NoticationChaper";
        public static string OnlyWifi = "OnlyWifi";
        public static List<Models.Severs> ListSever = new List<Models.Severs>();
        public static string FormatDateTime = " dd/MM/yy HH:mm:ss";
        public static string UrlFileDropBox = "https://www.dropbox.com/s/7p8vt002jxqfrhf/hubComicsV1.txt?dl=1";
        public static List<MenuManga> ListMenuManga = new List<MenuManga>();

        //  public static Dictionary<string, string> DicUrlServer;
        public static void LoadMenuManga()
        {
            Comon.ListMenuManga.Clear();
            #region truyentranh8
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Trang chủ",
                Url = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Value

            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Nhiều người xem nhất",
                Url = "http://m.truyentranh8.net/truyen_xem_nhieu/page=1"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Action",
                Url = "http://m.truyentranh8.net/loai/Action-52/page=1"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Adventure",
                Url = "http://m.truyentranh8.net/loai/Adventure-65/page=1",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Anime",
                Url = "http://m.truyentranh8.net/loai/Anime-107/page=1",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Comedy",
                Url = "http://m.truyentranh8.net/loai/Comedy-50/page=1",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Doujinshi",
                Url = "http://m.truyentranh8.net/loai/Doujinshi-72/page=1"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Drama",
                Url = "http://m.truyentranh8.net/loai/Drama-73/page=1",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Ecchi",
                Url = "http://m.truyentranh8.net/loai/Ecchi-74/page=1",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Fantasy",
                Url = "http://m.truyentranh8.net/loai/Fantasy-75/page=1",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Gender-Bender",
                Url = "http://m.truyentranh8.net/loai/GenderBender-76/page=1"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Harem",
                Url = "http://m.truyentranh8.net/loai/Harem-77/page=1",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Historical",
                Url = "http://m.truyentranh8.net/loai/Historical-78/page=1"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Yuri",
                Url = "http://m.truyentranh8.net/loai/Yuri-111/page=1"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Truyện Màu",
                Url = "http://m.truyentranh8.net/loai/Truyen-Mau-113/page=1"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranh8)).Key,
                Title = "Truyện scan",
                Url = "http://m.truyentranh8.net/loai/Truyen-scan-105/page=1"
            });
            #endregion
            #region truyện tranh tuần
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Trang chủ",
                Url = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Value
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Đang tiến hành",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/trang-thai/dang-tien-hanh"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Hoàn thành",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/trang-thai/hoan-thanh/"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Top 50",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/top/top-50"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Tạm ngừng",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/trang-thai/tam-dung"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Adventure",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/adventure",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Comedy",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/comedy",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Drama",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/drama",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Gender Bender",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/gender-bender",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Horror",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/horror"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Live Action",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/live-action"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Manhwa",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/manhwa",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Mecha",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/mecha"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "One Shot",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/one-shot"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Romance",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/romance"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Sci Fi",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/sci-fi"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Shounen",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/shounen"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Anime",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/anime"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Action",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/action"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Comic",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/comic"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Fantaty",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/fantasy",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Manhua",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/manhua"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.truyentranhtuan)).Key,
                Title = "Truyện tranh Việt",
                Url = "http://truyentranhtuan.com/danh-sach-truyen/the-loai/truyen-tranh-viet-nam"
            });
            #endregion
            #region Manga24h
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Trang chủ",
                Url = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Value
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Action",
                Url = "http://manga24h.com/Action.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Adult",
                Url = "http://manga24h.com/Adult.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Adventure",
                Url = "http://manga24h.com/Adventure.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Anime",
                Url = "http://manga24h.com/Anime.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Gender Bender",
                Url = "http://manga24h.com/Gender-Bender.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Baseball",
                Url = "http://manga24h.com/Baseball.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Comedy",
                Url = "http://manga24h.com/Comedy.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Comic",
                Url = "http://manga24h.com/Comic.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Cooking",
                Url = "http://manga24h.com/Cooking.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Detective",
                Url = "http://manga24h.com/Detective.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Doujinshi",
                Url = "http://manga24h.com/Doujinshi.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Drama",
                Url = "http://manga24h.com/Drama.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Ecchi",
                Url = "http://manga24h.com/Ecchi.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Game",
                Url = "http://manga24h.com/Game.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Fantasy",
                Url = "http://manga24h.com/Fantasy.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Football",
                Url = "http://manga24h.com/Football.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.manga24h)).Key,
                Title = "Futanari",
                Url = "http://manga24h.com/Futanari.html"
            });
            #endregion
            #region ComicVN
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Trang chủ",
                Url = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Value
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Tất cả",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=&id_category=1&page=1&orderBy=&type=0&sortBy=date&sortPri=desc&typeView=image"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Trưởng thành",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=truong-thanh&id_category=10&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Phiêu lưu",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=phieu-luu&id_category=11&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Hài",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=hai&id_category=12&page=1&orderBy=&type=0"
            });
           
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Comic",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=comic&id_category=13&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Kinh dị",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=kinh-di&id_category=14&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Võ thuật",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=vo-thuat&id_category=25&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "One shot",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=one-shot&id_category=16&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Tình cảm",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=tinh-cam&id_category=&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Manhwa",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=manhwa&id_category=24&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Học đường",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=hoc-duong&id_category=32&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Trinh thám",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=trinh-tham-t&id_category=71&page=1&orderBy=&type=0"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Hàn quốc bựa",
                Url = "http://comicvn.net/truyen-tranh/ban-bua"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.comicVN)).Key,
                Title = "Truyện Comic VN",
                Url = "http://comicvn.net/truyentranh/categorymanga/search?parent_slug=truyen-tranh&name_slug=truyen-comicvn-net&id_category=60&page=1&orderBy=&type=0"
            });
            #endregion
            #region vui truyện tranh
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Trang chủ",
                Url = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Value
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Hành động",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=1&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Tình cảm",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=2&cat_seo=",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Trinh thám",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=3&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Viễn tưởng",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=4&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Truyện chế",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=5&cat_seo=",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Thể thao",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=6&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Học đường",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=7&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Kinh dị",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=8&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Hài hước",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=11&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Đặc sắc",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=12&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Xì tin Manga",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=13&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Cung hoàng đạo",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=14&cat_seo="
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.vuitruyentranh)).Key,
                Title = "Comic",
                Url = "http://vuitruyentranh.vn/vmgmw/content/filter?page=1&cat_id=15&cat_seo="
            });
            #endregion
            #region IZmanga
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.izmanga)).Key,
                Title = "Trang chủ",
                Url = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.izmanga)).Value
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.izmanga)).Key,
                Title = "Truyện hot",
                Url = "http://izmanga.com/danh_sach_truyen?type=hot&category=all&alpha=all&page=1&state=all&group=all"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.izmanga)).Key,
                Title = "Truyện hoàn thành",
                Url = "http://izmanga.com/danh_sach_truyen?type=new&category=all&alpha=all&page=1&state=hoanthanh&group=all"
            });
            #endregion
            #region Ham truyen
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Trang chủ",
                Url = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Value
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Action",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd09/P1/action.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Adult",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd13/P1/adult.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Adventure",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd1d/P1/adventure.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Comedy",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd26/P1/comedy.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Doujinshi",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd0a/P1/doujinshi.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Ecchi",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd1e/P1/ecchi.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Fantasy",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd27/P1/fantasy.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Gender bender",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd0b/P1/gender-bender.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Ham truyện",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd2e/P1/hamtruyen.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Harem",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd15/P1/harem.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "History",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd1f/P1/history.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Horor",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd07/P1/horor.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Huyền huyễn",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd08/P1/huyen-huyen.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Josei",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd0c/P1/josei.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Lolicon",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd16/P1/lolicon.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Manga",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd05/P1/manga.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Manhua",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd04/P1/manhua.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Manhwa",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd06/P1/manhwa.html",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Martial art",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd17/P1/martial-art.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Marvel",
                Url = "http://hamtruyen.vn/Theloai/558127b41788b51898dcc2b5/P1/marvel.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Mature",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd20/P1/mature.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Mystery",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd0d/P1/mystery.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "One shot",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd18/P1/one-shot.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Psychological",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd21/P1/psychological.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Romance",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd29/P1/romance.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "School life",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd0e/P1/school-life.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Scifi",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd19/P1/sci-fi.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Seinen",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd22/P1/seinen.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Shounen",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd23/P1/shounen.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Shounen ai",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd2b/P1/shounen-ai.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Slice of life",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd10/P1/slice-of-life.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Smut",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd1b/P1/smut.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Sport",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd24/P1/sport.html"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.hamtruyen)).Key,
                Title = "Webtoom",
                Url = "http://hamtruyen.vn/Theloai/549f819f1788b6107431bd1c/P1/webtoon.html"
            });
            #endregion
            #region blogtruyen
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Trang chủ",
                Url = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Value,
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Adult",
                Url = "http://blogtruyen.com/theloai/adult",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Action",
                Url = "http://blogtruyen.com/theloai/action",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Adventure",
                Url = "http://blogtruyen.com/theloai/adventure",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Anime",
                Url = "http://blogtruyen.com/theloai/anime",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Comedy",
                Url = "http://blogtruyen.com/theloai/comedy"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Comic",
                Url = "http://blogtruyen.com/theloai/comic"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Doujinshi",
                Url = "http://blogtruyen.com/theloai/doujinshi"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Drama",
                Url = "http://blogtruyen.com/theloai/drama-new",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Ecchi",
                Url = "http://blogtruyen.comtheloai/ecchi-new",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Fantasy",
                Url = "http://blogtruyen.com/theloai/fantasy-new",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Gender bender",
                Url = "http://blogtruyen.com/theloai/gender-bender-new",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Harem",
                Url = "http://blogtruyen.com/theloai/harem",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Historical",
                Url = "http://blogtruyen.com/theloai/historical"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Horror",
                Url = "http://blogtruyen.com/theloai/horror"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Josei",
                Url = "http://blogtruyen.com/theloai/josei"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Live Acticon",
                Url = "http://blogtruyen.com/theloai/live-action"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Magic",
                Url = "http://blogtruyen.com/theloai/magic"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Manga",
                Url = "http://blogtruyen.com/theloai/manga"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Manhua",
                Url = "http://blogtruyen.com/theloai/manhua"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "Manhwa",
                Url = "http://blogtruyen.com/theloai/manhwa"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "16",
                Url = "http://blogtruyen.com/theloai/16"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.blogtruyen)).Key,
                Title = "18",
                Url = "http://blogtruyen.com/theloai/18"
            });
            #endregion
            #region FPhimSex
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.fphimsex)).Key,
                Title = "Trang chủ",
                Url = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.fphimsex)).Value,
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.fphimsex)).Key,
                Title = "Top tháng",
                Url = "http://fphimsex.com/truyen-tranh/top/thang/1",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.fphimsex)).Key,
                Title = "Top tuần",
                Url = "http://fphimsex.com/truyen-tranh/top/tuan/1",
                Tag = "18"
            });
            Comon.ListMenuManga.Add(new MenuManga
            {
                Sever = ListSever.FirstOrDefault(t => t.Key == Utils.GetEnumDescription(TitleSever.fphimsex)).Key,
                Title = "Top ngày",
                Url = "http://fphimsex.com/truyen-tranh/top/ngay/1",
                Tag = "18"
            });
            #endregion
        }
    }
}
