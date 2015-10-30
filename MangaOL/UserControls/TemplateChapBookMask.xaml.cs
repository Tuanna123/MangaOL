using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MangaOL.UserControls
{
    public partial class TemplateChapBookMask : UserControl
    {
        MangaOL.ViewModels.MainPageVM model = new ViewModels.MainPageVM();
        public TemplateChapBookMask()
        {
            InitializeComponent();
        }
        public delegate void TapEvent(object s, EventArgs e);
        public event TapEvent TapGridItem;

        private void BtnDeleteItemChaperBookmask_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MangaOL.Models.ChaperBookmask item = (sender as MangaOL.UserControls.Mdl2).DataContext as MangaOL.Models.ChaperBookmask;
            this.model.DeleteChaperBookmask(item);            
        }

        private void GridChaperBookmask_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (this.TapGridItem != null)
            {
                this.TapGridItem(this, EventArgs.Empty);
            }
        }
    }
}
