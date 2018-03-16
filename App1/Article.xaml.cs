using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace App1
{
    public sealed partial class Article : UserControl
    {
        public Article()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public static readonly DependencyProperty PicProperty = DependencyProperty.Register("Pic", typeof(string), typeof(Article), null);



        public string Pic

        {

            get { return GetValue(PicProperty) as string; }

            set { SetValue(PicProperty, value); }

        }


        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(Article), null);



        public string Label

        {

            get { return GetValue(LabelProperty) as string; }

            set { SetValue(LabelProperty, value); }

        }

    }
}
