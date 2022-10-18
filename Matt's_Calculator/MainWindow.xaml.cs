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

namespace Matt_s_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TabSettings> Tabs { get; set; } = new List<TabSettings>();
        public MainWindow()
        {


            InitializeComponent();

            var path = System.IO.Directory.GetCurrentDirectory() + @"\tabsettings.json";
            Tabs = System.Text.Json.JsonSerializer.Deserialize<List<TabSettings>>(System.IO.File.ReadAllText(path));
            foreach(TabSettings tab in Tabs)
            {
                TabItem tabItem = new TabItem();
                tabItem.Header = tab.Title;
                tabItem.Tag = tab;
                tcMain.Items.Add(tabItem);
            }


        }
    }
}
