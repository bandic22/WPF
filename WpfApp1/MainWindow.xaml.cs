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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Slots slots = new Slots();
        public Websites sites = new Websites();
        public Selections selections = new Selections();

        public string solutionPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Publish();
        }

        private void Publish()
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                solutionPath = dialog.FileName;
                PathDisplay.Content = solutionPath;
            }
        }

        private void SiteSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selections.SelectedSite = SiteSelector.SelectedValue.ToString();
        }

        private void SlotSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selections.SelectedSlot = SlotSelector.SelectedValue.ToString();
        }
    }

    public class Websites : ObservableCollection<string>
    {
        public Websites()
        {
            var Sites = ConfigurationManager.AppSettings["eventSites"].Split('|').ToList();

            foreach (var site in Sites)
            {
                Add(site);
            }
        }
    }

    public class Slots : ObservableCollection<string>
    {
        public Slots()
        {
            var Slots = ConfigurationManager.AppSettings["eventSlots"].Split('|').ToList();

            foreach (var slot in Slots)
            {
                Add(slot);
            }
        }
    }

    public class Selections
    {
        public string SelectedSlot { get; set; }
        public string SelectedSite { get; set; }
    }
}
