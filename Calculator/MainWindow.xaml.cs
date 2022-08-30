using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Connect(string style)
        {
            Uri uri = new Uri(style, UriKind.Relative);
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(uri);
            if (Application.Current.Resources.MergedDictionaries.Count > 1) Application.Current.Resources.MergedDictionaries.RemoveAt(Application.Current.Resources.MergedDictionaries.Count - 1);
            Application.Current.Resources.MergedDictionaries.Add(res);
        }

        private void Default_Checked(object sender, RoutedEventArgs e)
        {
            if (Default.IsChecked == true) Connect("Default.xaml");
        }

        private void Matrix_Checked(object sender, RoutedEventArgs e)
        {
            if (Matrix.IsChecked == true) Connect("Matrix.xaml");
        }

        private void FireFox_Checked(object sender, RoutedEventArgs e)
        {
            if (FireFox.IsChecked == true) Connect("FireFox.xaml");
        }

        private void VisualStudio_Checked(object sender, RoutedEventArgs e)
        {
            if (VisualStudio.IsChecked == true) Connect("PurpleWhite.xaml");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Expand_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }


        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
