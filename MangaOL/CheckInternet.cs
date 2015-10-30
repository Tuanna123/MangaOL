using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MangaOL
{
    public class CheckInternet
    {
        private Thread mDownloadThread;
        internal void StartDownload()
        {
            if (this.mDownloadThread == null)
            {
                this.mDownloadThread = new Thread(new ThreadStart(this.RUN));
                this.mDownloadThread.Start();
            }
        }

        private void RUN()
        {
                
        }
    }
}
