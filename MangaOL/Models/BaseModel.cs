using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MangaOL.Models
{
    public enum LoadState
    {
        None,
        Loading,
        Loaded
    }
    public class IndicatorStatus
    {
        public bool IsRunning
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }
    }
    public class BaseModel : INotifyPropertyChanged
    {
        private Dictionary<string, IndicatorStatus> dicIncompleteProcess;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsBusy
        {
            get;
            set;
        }

        public string BusyText
        {
            get;
            set;
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            bool result;
            if (object.Equals(storage, value))
            {
                result = false;
            }
            else
            {
                storage = value;
                this.NotifyPropertyChanged(propertyName);
                result = true;
            }
            return result;
        }

        public async void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void SetProgressIndicator(string processId, bool enable, string text = "")
        {
            if (dicIncompleteProcess == null) dicIncompleteProcess = new Dictionary<string, IndicatorStatus>();
            dicIncompleteProcess[processId] = new IndicatorStatus { Status = text, IsRunning = enable };
            if (enable)
            {
                IsBusy = true;
                NotifyPropertyChanged("IsBusy");
                if (!string.IsNullOrEmpty(text))
                {
                    BusyText = text;
                    NotifyPropertyChanged("BusyText");
                }
            }
            else
            {
                foreach (var key in dicIncompleteProcess.Keys)
                {
                    if (dicIncompleteProcess[key].IsRunning)
                    {
                        BusyText = dicIncompleteProcess[key].Status;
                        NotifyPropertyChanged("BusyText");
                        return;
                    }
                }
                IsBusy = false;
                NotifyPropertyChanged("IsBusy");
            }


        }
    }
}
