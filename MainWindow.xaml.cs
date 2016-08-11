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

namespace WPFParisTraining
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StaffPage());
        }

        private void TeamsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TeamsPage());
        }

        private void SessionsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SessionsPage());
        }

        private void CoursesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CoursesPage());
        }

        private void LocationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LocationsPage());
        }

        private void BulkReqButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BulkReq());
        }

        private void TemplateButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EMailTemplatesPage());
        }
    }
}
