using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.GamerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Storage;

namespace MangaOL
{
    public class Utils
    {
        public static string SecondaryTileUriSource = "";
        #region Message
        public static MessageBoxResult ShowMessage(string content, string title = "", MessageBoxButton button = MessageBoxButton.OK)
        {
            return MessageBox.Show(content, title, button);
        }
        public static int Show(string title, string message, string[] textButtons, int focusButton, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            IAsyncResult asyncResult = Guide.BeginShowMessageBox(title, message, textButtons, focusButton, icon, new AsyncCallback(Utils.CallBack), "");
            asyncResult.AsyncWaitHandle.WaitOne();
            int? num = Guide.EndShowMessageBox(asyncResult);
            int result;
            if (num.HasValue)
            {
                result = num.Value;
            }
            else
            {
                result = -1;
            }
            return result;
        }

        private static void CallBack(IAsyncResult ar)
        {
        }
        #endregion
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
        #region Send mail
        public static void SendMail()
        {
            Microsoft.Phone.Tasks.EmailComposeTask emailComposeTask = new Microsoft.Phone.Tasks.EmailComposeTask();
            emailComposeTask.Subject = "Feedback MangaOL";
            emailComposeTask.Body = "Bạn có thể cung cấp cho chúng tôi thêm nhưng thông tin sau:\nOS:\nThiết bị:\nGóp ý phản hồi:\nKhó chịu hay lỗi bạn thường gặp:\nCảm ơn bạn.";
            emailComposeTask.To = "a5wap123@gmail.com";
            emailComposeTask.Show();
        }
        #endregion
        #region Show toast
        public static void ShowToast(string content, string title = "")
        {
            Microsoft.Phone.Shell.ShellToast shellToast = new Microsoft.Phone.Shell.ShellToast();
            shellToast.Title = title;
            shellToast.Content = content;
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
        #region ShellTitle
        private static bool TrySaveImage(string fileName, string url)
        {
            bool result;
            using (System.IO.IsolatedStorage.IsolatedStorageFile store = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    Image image = new Image();
                    image.Width = 100;
                    image.Height = 100;
                    image.Stretch = System.Windows.Media.Stretch.Uniform;
                    image.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                    image.Measure(new Size(100, 100));
                    image.Arrange(new Rect(0.0, 0.0, 100, 100));
                    WriteableBitmap writeableBitmap = new WriteableBitmap(image, null);
                    string directory = Path.GetDirectoryName(fileName);
                    if (!store.DirectoryExists(directory))
                    {
                        store.CreateDirectory(directory);
                    }
                    using (var stream = store.OpenFile(fileName, FileMode.OpenOrCreate))
                    {
                        writeableBitmap.SaveJpeg(stream, 100, 100, 0, 100);
                    }
                }
                catch
                {
                    result = false;
                    return result;
                }
            }
            result = true;
            return result;
        }

        private static async Task<StandardTileData> GetSecondaryTileData(string nameImage, string urlImage)
        {
            string text = @"Shared\ShellContent\" + nameImage + ".jpg";
            StandardTileData result;
            if (Utils.TrySaveImage(text, urlImage))
            {
                Uri backgroundImage = new Uri("isostore:/" + text, UriKind.RelativeOrAbsolute);
                StandardTileData standardTileData = new StandardTileData();
                standardTileData.Title = nameImage;
                standardTileData.BackgroundImage = backgroundImage;
                standardTileData.BackTitle = "";
                standardTileData.BackBackgroundImage = new Uri("", UriKind.RelativeOrAbsolute);
                standardTileData.BackContent = nameImage;
                StandardTileData standardTileData2 = standardTileData;
                result = standardTileData2;
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static ShellTile FindTile(string partOfUri)
        {
            return Enumerable.FirstOrDefault<ShellTile>(ShellTile.ActiveTiles, (ShellTile tile) => tile.NavigationUri.ToString().Contains(partOfUri));
        }

        public static async void Pin(string nameImage, string urlImage)
        {
            ShellTile shellTile = Utils.FindTile(Utils.SecondaryTileUriSource);
            if (shellTile == null)
            {
                StandardTileData standardTileData = await Utils.GetSecondaryTileData(nameImage, urlImage);
                string text = Utils.SecondaryTileUriSource;
                ShellTile.Create(new Uri(text, UriKind.RelativeOrAbsolute), standardTileData);
            }
        }

        public static void UnPin()
        {
            ShellTile shellTile = Utils.FindTile(Utils.SecondaryTileUriSource);
            if (shellTile != null)
            {
                shellTile.Delete();
                MessageBox.Show("Đã gỡ bỏ.");
            }
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
        #region netWork
        public static void PrintNetworkStatus(bool p)
        {
            if (p)
            {
                ShowMessage("Đã kết nối.", "Thông báo");
            }
            else
            {
                ShowMessage("Mất kết nối.", "Thông báo");
            }
        }
        #endregion
        #region SaveApp Seting
        public static string SaveAppSeting(string name, string value = "", bool checkRead = false)
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
                return null;
            }
            else
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
        #region ConvertColor
        internal async static void SetColorUI()
        {
            bool result = Boolean.Parse(MangaCore.Utils.SaveAppSeting("UIColor", "false", true, false).ToString());
            // Windows.UI.Core.CoreDispatcher dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            // await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            // {
            SetUI(result);
            //});
        }

        private static void SetUI(bool value)
        {
            Dictionary<string, SolidColorBrush> dricColor = new Dictionary<string, SolidColorBrush>();
            SolidColorBrush colorItemAppBar = new SolidColorBrush();
            SolidColorBrush colorMain = new SolidColorBrush();
            SolidColorBrush colorGridPivotHeader = new SolidColorBrush();
            SolidColorBrush colorGridHeader = new SolidColorBrush();
            SolidColorBrush colorBackgroundItem = new SolidColorBrush();
            SolidColorBrush colorForegroundItem = new SolidColorBrush();
            if (!value)
            {
                // UI Sáng
                colorItemAppBar = ConvertStringToColor("#FF646262");
                colorMain = ConvertStringToColor("#e4e7ea");
                colorGridPivotHeader = ConvertStringToColor("#FFFFFFFF");
                //colorGridHeader = ConvertStringToColor("#FF39AAAA");
                colorBackgroundItem = ConvertStringToColor("#FFFFFF");
                colorForegroundItem = ConvertStringToColor("#FF000000");
            }
            else
            {
                //UI Tối
                // Main #FF4B4949
                //BackGroudChaper = #FF000000
                //ColoGridPivotHeader #FF383737
                //ColorGridHeader #FF582E76
                //ItemAppBar #FFCBCBCB
                colorItemAppBar = ConvertStringToColor("#FFCBCBCB");
                colorMain = ConvertStringToColor("#FF4B4949");
                colorGridPivotHeader = ConvertStringToColor("#FF383737");
                //  colorGridHeader = ConvertStringToColor("#FF4C2D9E");
                colorBackgroundItem = ConvertStringToColor("#FF443E3E");
                colorForegroundItem = ConvertStringToColor("#FFFFFF");
            }
            dricColor.Add("Main", colorMain);
            dricColor.Add("ItemAppBar", colorItemAppBar);
            dricColor.Add("ColoGridPivotHeader", colorGridPivotHeader);
            // dricColor.Add("ColorGridHeader", colorGridHeader);
            dricColor.Add("BackgroundItem", colorBackgroundItem);
            dricColor.Add("ForegroundItem", colorForegroundItem);
            foreach (var item in dricColor)
            {
                App.Current.Resources.Remove(item.Key);
                App.Current.Resources.Add(item.Key, item.Value);
            }

        }
        private static SolidColorBrush ConvertStringToColor(String hex)
        {
            //remove the # at the front
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);
            SolidColorBrush resultColor = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            return resultColor;
        }
        #endregion
        #region Check Submit Store
        private static string SubmitPass = "SubmitPass";
        public async static Task CheckPubic()
        {
            var submitPass = Utils.SaveAppSeting(SubmitPass, "", true);
            if (string.IsNullOrEmpty(submitPass))
            {
                if (App.CheckPublic == null)
                {
                    App.CheckPublic = await MangaCore.Utils.GetFileDropbox(MangaCore.Comon.UrlFileDropBox);
                    if (!App.CheckPublic.Value)
                    {
                        Utils.SaveAppSeting(SubmitPass, "fasle");
                    }
                }
            }
            else
            {
                App.CheckPublic = false;
            }
        }
        #endregion
        #region Select DependencyObject
        internal static childItem FindVisualChild<childItem>(DependencyObject obj)
where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        #endregion
        #region Get VerSion App
        public static string GetVersionApp()
        {
            AssemblyName assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
            Version version = assemblyName.Version;
            return version.ToString();
        }
        #endregion
    }
}
