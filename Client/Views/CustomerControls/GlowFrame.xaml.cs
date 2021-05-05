using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Client.CustomerControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
   
    public partial class GlowFrame : UserControl
    {
        public GlowFrame()
        {
            InitializeComponent();
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(GlowFrame));
    }
}