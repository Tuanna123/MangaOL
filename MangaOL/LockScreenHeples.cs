using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Windows.Phone.System.UserProfile;

namespace MangaOL
{
    public class LockScreenHeples
    {
        #region SSet BackGroup LockScreen
        public static void StartDownloadImagefromServer(string imgURL)
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted -= new OpenReadCompletedEventHandler(client_OpenReadCompleted);
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);
            client.OpenReadAsync(new Uri(imgURL, UriKind.Absolute));
        }
        static void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(e.Result);

           
            String tempJPEG1 = "MyWallpaper1.jpg";
            String tempJPEG2 = "MyWallpaper2.jpg";
            String tempJPEG = tempJPEG1; ;
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (myIsolatedStorage.FileExists(tempJPEG1))
                {
                    myIsolatedStorage.DeleteFile(tempJPEG1);
                    tempJPEG = tempJPEG2;
                }
                else if (myIsolatedStorage.FileExists(tempJPEG2))
                {
                    myIsolatedStorage.DeleteFile(tempJPEG2);
                    tempJPEG = tempJPEG1;
                }
                IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(tempJPEG);

                StreamResourceInfo sri = null;
                Uri uri = new Uri(tempJPEG, UriKind.Relative);
                sri = Application.GetResourceStream(uri);

                WriteableBitmap wb = new WriteableBitmap(bitmap);

                Extensions.SaveJpeg(wb, fileStream, wb.PixelWidth, wb.PixelHeight, 0, 90);

                fileStream.Close();
            }

            LockScreenChange(tempJPEG);
        }
        private static async void LockScreenChange(string filePathOfTheImage)
        {
            if (!LockScreenManager.IsProvidedByCurrentApplication)
            {
                await LockScreenManager.RequestAccessAsync();
            }

            if (LockScreenManager.IsProvidedByCurrentApplication)
            {
                var schema = "ms-appdata:///Local/";
                var uri = new Uri(schema + filePathOfTheImage, UriKind.RelativeOrAbsolute);
                LockScreen.SetImageUri(uri);

                MessageBox.Show("Đã đổi thành công ảnh nền.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Background cant be changed. Please check your permissions to this application.");
            }
        }
        #endregion
    }
}
