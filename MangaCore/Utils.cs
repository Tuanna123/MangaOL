using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Xna.Framework.GamerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Windows.Storage;

namespace MangaCore
{
    public class Utils
    {
        public static string SecondaryTileUriSource = "";

        #region Read File form Project
        public static async Task<string> ReadTextFileFromProject(string filePath)
        {
            StorageFile windowsRuntimeFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///" + filePath));
            string result;
            using (StreamReader streamReader = new StreamReader(await windowsRuntimeFile.OpenStreamForReadAsync()))
            {
                result = await streamReader.ReadToEndAsync();
            }
            return result;
        }
        #endregion
        #region HttpClient
        public static HttpClient GetHttpClient(CookieContainer cookieContainer)
        {

            HttpClient httpClient = new HttpClient(new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                CookieContainer = cookieContainer
            });
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.99 Safari/537.36");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, sdch");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "vi-VN,vi;q=0.8,fr-FR;q=0.6,fr;q=0.4,en-US;q=0.2,en;q=0.2");
            //  httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
            return httpClient;
        }

        public static async Task<string> HttpClientRequert(string url, Dictionary<string, string> headers = null, string method = "get", string data = "")
        {
            var cookieContainer = new CookieContainer();
            HttpClient httpClient = Utils.GetHttpClient(cookieContainer);
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
                }
            }

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            switch (method.ToLower())
            {
                case "get":
                    httpResponseMessage = await httpClient.GetAsync(url);
                    break;
                case "post":
                    StringContent queryString = new StringContent(data);
                    httpResponseMessage = await httpClient.PostAsync(new Uri(url), queryString);
                    break;
                case "delete":
                    break;
                default:
                    break;
            }
            //foreach (Cookie cookie in cookieContainer.GetCookies(new Uri(url)))
            //{
            //    System.Diagnostics.Debug.WriteLine("Cookie contains: " + cookie.Value.ToString());
            //}
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        #endregion
        #region JsonBase
        public static T JsonDeserialize<T>(string json)
        {
            T result;
            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                result = default(T);
            }
            return result;
        }

        public static JObject JObjectParse(string json)
        {
            JObject result;
            try
            {
                result = JObject.Parse(json);
            }
            catch
            {
                result = null;
            }
            return result;
        }
        #endregion
        #region SaveApp Seting
        public static string SaveAppSeting(string name, string value = "", bool checkRead = false, bool checkReadOnly = true)
        {
            System.IO.IsolatedStorage.IsolatedStorageSettings applicationSettings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;

            if (checkRead && System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains(name))
            {
                return (string)System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings[name];
            }
            else if (!checkRead)
            {
                if (!applicationSettings.Contains(name))
                {
                    applicationSettings.Add(name, value);
                }
                else
                {
                    applicationSettings[name] = value;
                }
                applicationSettings.Save();
                return value;
            }
            // Nếu checkReadOnly = true thì ta chỉ read có hay không rồi return, chứ k save
            else if (checkRead && !System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains(name) && !checkReadOnly)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    applicationSettings.Add(name, value);
                    applicationSettings.Save();
                    return value;
                }

                else
                    return null;
            }
            else
                return null;
        }
        public static void DeleteAppSeting(string name)
        {
            System.IO.IsolatedStorage.IsolatedStorageSettings applicationSettings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
            if (System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Contains(name))
            {
                System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Remove(name);
            }


        }
        #endregion
        #region Foler, file local
        public static async Task<StorageFolder> CreateFolder(string nameFolder, StorageFolder local)
        {
            return await local.CreateFolderAsync(nameFolder, CreationCollisionOption.OpenIfExists);
        }

        public static async Task<StorageFile> CreateFile(StorageFolder local, string fileName, CreationCollisionOption createOption)
        {
            return await local.CreateFileAsync(fileName, createOption);
        }

        public static async Task<bool> DeleteFile(StorageFolder local, string fileName)
        {
            StorageFile storageFile = await local.GetFileAsync(fileName);
            bool result;
            if (storageFile != null)
            {
                await storageFile.DeleteAsync();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static async Task<bool> DeleteFolder(StorageFolder local, string folderName)
        {
            StorageFolder storageFolder = await local.GetFolderAsync(folderName);
            bool result;
            if (storageFolder != null)
            {
                await storageFolder.DeleteAsync();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        #endregion
        #region Remove tieng viet

        private static readonly string[] VietnameseSigns = new string[]

    {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

    };
        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }

        #endregion
        #region GetValue enum with value string
        public static string GetEnumDescription(Enum value)
        {
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

            System.ComponentModel.DescriptionAttribute[] attributes =
                (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        #endregion
        #region DownloadByteToUriImage
        public static async System.Threading.Tasks.Task<Byte[]> DownloadByteToUriImage(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                var arrByte = await client.GetByteArrayAsync(url);
                return arrByte;

            }
            catch
            {
                return null;
            }

        }
        public static async System.Threading.Tasks.Task<Stream> DownloadStreamToUriImage(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                var stream = await client.GetStreamAsync(url);
                return stream;

            }
            catch
            {
                return null;
            }

        }
        #endregion
        #region Download string 1
        public static Task<string> DownloadString1(Uri url)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += (object s, DownloadStringCompletedEventArgs e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else
                {
                    if (e.Cancelled)
                    {
                        tcs.TrySetCanceled();
                    }
                    else
                    {
                        tcs.TrySetResult(e.Result);
                    }
                }
            };
            webClient.DownloadStringAsync(url);
            return tcs.Task;
        }
        #endregion
        #region Get File Dropbox
        public async static Task<bool> GetFileDropbox(string url)
        {
            try
            {
                var uri = new Uri(url);
                string t = await Utils.DownloadString1(uri);
                return Boolean.Parse(t);

            }
            catch
            {
                return false;
            }
        }

        #endregion
       
    }
}
