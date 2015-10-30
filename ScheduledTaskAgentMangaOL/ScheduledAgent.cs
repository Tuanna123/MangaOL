using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using Microsoft.Phone.Net.NetworkInformation;

namespace ScheduledTaskAgentMangaOL
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        public string nameApp = MangaCore.Comon.AppName;
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected async override void OnInvoke(ScheduledTask task)
        {
            DateTime now = DateTime.Now;
            int hour = now.Hour;
        //    ShowToast(now.ToString(), "Manga OL", "/pag.xaml/To=true");
            if (task.Name == "PeriodicTaskManga")
            {
                await funNoticationChaper(hour);
                if (hour >= 20)
                {
                    funcNhacNho(task);

                }
            }
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromMinutes(10));
            //TODO: Add code to perform your task in background
            NotifyComplete();
        }

        private async System.Threading.Tasks.Task funNoticationChaper(int hour)
        {
           // System.Threading.Mutex mutex = new System.Threading.Mutex(true, MangaCore.Comon.OnlyNetworkNotication);
           // mutex.WaitOne();
            if (!NetworkInterface.GetIsNetworkAvailable()) return;
            System.IO.IsolatedStorage.IsolatedStorageSettings setting = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
            if (!setting.Contains(MangaCore.Comon.NoticationChaper))
            {

                setting.Add(MangaCore.Comon.NoticationChaper, "true");
                setting.Add(MangaCore.Comon.OnlyWifi, "false");
                setting.Save();
                await ShowNoticationChaperNewManga(hour);
            }
            else
            {
                var notication = Boolean.Parse(setting[MangaCore.Comon.NoticationChaper].ToString());
                if (!notication) return;
                var onlyWifi = Boolean.Parse(setting[MangaCore.Comon.OnlyWifi].ToString());
                //Debug.WriteLine(DateTime.Parse(dateTimeNotication).Date.ToString());
                if (onlyWifi)
                {
                    var net = NetworkInterface.NetworkInterfaceType.ToString();
                    if (!net.Contains("Wireless")) return;
                    await ShowNoticationChaperNewManga(hour);
                }
                else
                {
                    await ShowNoticationChaperNewManga(hour);
                }
            }
          //  mutex.ReleaseMutex();

        }
        private async System.Threading.Tasks.Task ShowNoticationChaperNewManga(int hour)
        {
             if ((hour >= 6 && hour < 7) || (hour >= 15 && hour < 17) || (hour >= 19 && hour < 21))
             {
            var listNameManga = await MangaCore.Notication.GetCountChapMangaFavorite();
            if (listNameManga.Count > 0)
            {
                foreach (var item in listNameManga)
                {
                    ShowToast("Có "+item.count + " chaper mới.", item.name, "/Views/DetailPage.xaml?Toast=" + item.url);
                }

            }
             }
        }
        private void funcNhacNho(ScheduledTask task)
        {
            string toastMessage = "";
            // If your application uses both PeriodicTask and ResourceIntensiveTask
            // you can branch your application code here. Otherwise, you don't need to.
            if (task is PeriodicTask)
            {
                System.Threading.Mutex mutex = new System.Threading.Mutex(true, "DateTimeNotication");
                mutex.WaitOne();
                System.IO.IsolatedStorage.IsolatedStorageSettings setting = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
                string dateTimeNotication = "";
                if (!setting.Contains("DateTimeNotication"))
                {

                    setting.Add("DateTimeNotication", DateTime.Now.ToString("dd/MM/yy"));
                    setting.Save();
                    ShowToast("Đọc truyện cùng "+nameApp+" nào ^^.", nameApp);
                }
                else
                {
                    dateTimeNotication = setting["DateTimeNotication"].ToString();
                    System.Globalization.CultureInfo CI = new System.Globalization.CultureInfo("vi-VN");
                    Debug.WriteLine(DateTime.Parse(dateTimeNotication).Date.ToString());
                    if (DateTime.Parse(dateTimeNotication,(IFormatProvider)CI).Date < DateTime.Now.Date)
                    {
                        setting["DateTimeNotication"] = DateTime.Now.ToString("dd/MM/yy");
                        setting.Save();
                        ShowToast("Đọc truyện cùng " + nameApp + " nào ^^.", nameApp);
                    }
                }
                mutex.ReleaseMutex();
                // Execute periodic task actions here.

            }


        }
        private void ShowToast(string content, string title,string param = null)
        {
            ShellToast toast = new ShellToast();
            toast.Title = title;
            toast.Content = content;
            if (param != null)
                toast.NavigationUri = new Uri(param,UriKind.RelativeOrAbsolute);
            toast.Show();
        }
      

    }
}