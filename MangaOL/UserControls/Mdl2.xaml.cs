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
    public partial class Mdl2 : UserControl
    {
        public Mdl2()
        {
            this.InitializeComponent();

            //this.ButtonSize = 54.0;
            //  this.ButtonType = ButtonType.Rectange;
        }
        public delegate void TapEvent(object sender, EventArgs e);
        public event TapEvent TapMdl2;
        public bool IsEnabledMdl2
        {
            get { return (bool)GetValue(IsEnabledMdl2Property); }
            set { SetValue(IsEnabledMdl2Property, value); }
        }

        // Using a DependencyProperty as the backing store for IsEnabledMdl2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledMdl2Property =
            DependencyProperty.Register("IsEnabledMdl2", typeof(bool), typeof(Mdl2), new PropertyMetadata((bool)false, IsEnablendChanged));
        public static void IsEnablendChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Mdl2 mdl = obj as Mdl2;
            mdl.SetIsEnablend((bool)e.NewValue);
        }

        private void SetIsEnablend(bool p)
        {
            if (p)
            {
                gridIsEnabled.Visibility = System.Windows.Visibility.Visible;
                uc.Opacity = 0.3;
            }
            else
            {
                gridIsEnabled.Visibility = System.Windows.Visibility.Collapsed;
                uc.Opacity = 1;
            }
        }


        public System.Windows.Media.Brush ColorIcon
        {
            get { return (System.Windows.Media.Brush)GetValue(ColorIconProperty); }
            set
            {
                SetValue(ColorIconProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for ColorIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorIconProperty =
            DependencyProperty.Register("ColorIcon", typeof(System.Windows.Media.Brush), typeof(Mdl2), new PropertyMetadata(null,ColorIconChanged));

        private static void ColorIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Mdl2 uc = d as Mdl2;
            uc.SetColorIcon((System.Windows.Media.Brush)e.NewValue);
        }

        private void SetColorIcon(System.Windows.Media.Brush brush)
        {
            txtIcon.Foreground = brush;
        }


        public System.Windows.Media.Brush ColorTitle
        {
            get { return (System.Windows.Media.Brush)GetValue(ColorTitleProperty); }
            set { SetValue(ColorTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColorTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorTitleProperty =
            DependencyProperty.Register("ColorTitle", typeof(System.Windows.Media.Brush), typeof(Mdl2), new PropertyMetadata(null,ColorTitleChanged));
        private static void ColorTitleChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            Mdl2 uc = d as Mdl2;
            uc.SetColorTitle((System.Windows.Media.Brush)e.NewValue);
        }

        private void SetColorTitle(System.Windows.Media.Brush brush)
        {
            txtContentBotton.Foreground = txtContentRight.Foreground = brush;
        }
        

        public static readonly DependencyProperty FontSizeTitleProperty = DependencyProperty.Register("FontSizeTitle", typeof(double), typeof(Mdl2), new PropertyMetadata((double)18.667));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(Mdl2), new PropertyMetadata((string)""));

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof(double), typeof(Mdl2), new PropertyMetadata((double)0.0));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Mdl2), new PropertyMetadata((string)""));

        public static readonly DependencyProperty ButtonTypeProperty = DependencyProperty.Register("ButtonType", typeof(ButtonType), typeof(Mdl2), new PropertyMetadata((ButtonType)ButtonType.Rectange, new PropertyChangedCallback(Mdl2.ButtonTypeChanged)));

        public static readonly DependencyProperty ButtonSizeProperty = DependencyProperty.Register("ButtonSize", typeof(double), typeof(Mdl2), new PropertyMetadata((double)54.0, new PropertyChangedCallback(Mdl2.ButtonSizeChange)));


        public double FontSizeTitle
        {
            get
            {
                return (double)(GetValue(FontSizeProperty));
            }
            set
            {
                SetValue(FontSizeTitleProperty, value);
            }
        }

        public string Icon
        {
            get
            {
                return (string)(GetValue(IconProperty));
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }

        public ButtonType ButtonType
        {
            get
            {
                return (ButtonType)GetValue(ButtonTypeProperty);
            }
            set
            {
                SetValue(ButtonTypeProperty, value);
            }
        }

        public double IconSize
        {
            get
            {
                return (double)GetValue(IconSizeProperty);
            }
            set
            {
                SetValue(IconSizeProperty, value);
            }
        }

        public double ButtonSize
        {
            get
            {
                return (double)GetValue(ButtonSizeProperty);
            }
            set
            {
                base.SetValue(ButtonSizeProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }



        public static void ButtonSizeChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Mdl2 mdl = obj as Mdl2;
            mdl.ResetButtonProperty();
        }

        public static void ButtonTypeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Mdl2 mdl = obj as Mdl2;
            mdl.ResetButtonProperty();
        }

        private void ResetButtonProperty()
        {
            ButtonType buttonType = this.ButtonType;
            switch (buttonType)
            {
                case ButtonType.Rectange:
                    this.grid.Width = base.Width;
                    this.grid.Height = base.Height;
                    break;
                case ButtonType.Circle:
                    this.grid.Width = this.ButtonSize;
                    this.grid.Height = this.ButtonSize;
                    this.grid.CornerRadius = new CornerRadius(60);
                    break;
                case ButtonType.TextOnly:
                    break;
                case ButtonType.TextBotton:
                    txtContentRight.Visibility = System.Windows.Visibility.Collapsed;
                    txtContentBotton.Visibility = System.Windows.Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void uc_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!this.IsEnabledMdl2)
                this.grid.Visibility = System.Windows.Visibility.Visible;
        }

        private void uc_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.grid.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void uc_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (this.TapMdl2 != null && !this.IsEnabledMdl2)
            {
                this.TapMdl2(this, EventArgs.Empty);
            }
        }


    }
    public enum ButtonType
    {
        Rectange = 0,
        Circle = 1,
        TextOnly = 2,
        TextBotton = 3,
    }
}
