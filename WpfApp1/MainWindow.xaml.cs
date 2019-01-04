using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
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
using System.Windows.Forms;
using System.Management.Automation;
using Path = System.IO.Path;

namespace MicrosoftEventsPublisher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindow()
        {
            MainFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }
    }
}
