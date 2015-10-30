using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Scheduler;

namespace MangaOL.Views
{
    public partial class SlapScreen : PhoneApplicationPage
    {
        public SlapScreen()
        {
            InitializeComponent();
            StartPeriodicTask();
            this.Loaded += SlapScreen_Loaded;
        }

        async void SlapScreen_Loaded(object sender, RoutedEventArgs e)
        {
            if (uri.Contains("Toast"))
            {
                string uriManga = NavigationContext.QueryString["Toast"];
                NavigationService.Navigate(new Uri("/Views/DetailPage.xaml?Toast=" + uriManga, UriKind.RelativeOrAbsolute));
            }
            else
            {
                await System.Threading.Tasks.Task.Delay(100);
                ShowImage.Begin();
                await System.Threading.Tasks.Task.Delay(3000);
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
            }
            this.NavigationService.RemoveBackEntry();
        }
        PeriodicTask periodicTask;
        string periodicTaskName = "PeriodicTaskManga";
        private void StartPeriodicTask()
        {
            try
            {
                periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;
                if (periodicTask != null) ScheduledActionService.Remove(periodicTaskName);
                periodicTask = new PeriodicTask(periodicTaskName);
                periodicTask.Description = "Notication";
                ScheduledActionService.Add(periodicTask);
                ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromMinutes(10));
                System.Diagnostics.Debug.WriteLine("Open the background agent success");
              //  MessageBox.Show("Open the background agent success");
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("exists already"))
                {
                    //  MessageBox.Show("Since then the background agent success is already running");
                    System.Diagnostics.Debug.WriteLine("Since then the background agent success is already running");
                }
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    //  MessageBox.Show("Background processes for this application has been prohibited");
                    System.Diagnostics.Debug.WriteLine("Background processes for this application has been prohibited");
                }
                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    //  MessageBox.Show("You open the daemon has exceeded the hardware limitations");
                    System.Diagnostics.Debug.WriteLine("You open the daemon has exceeded the hardware limitations");
                }
            }
            catch (SchedulerServiceException)
            {

            }
        }
        private string uri;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            uri = e.Uri.ToString();
            
        }
    }
}