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
        TabSettings ActiveTabSetting = null;
        public MainWindow()
        {


            InitializeComponent();

            var path = System.IO.Directory.GetCurrentDirectory() + @"\tabsettings.json";
            Tabs = System.Text.Json.JsonSerializer.Deserialize<List<TabSettings>>(System.IO.File.ReadAllText(path));
            foreach(TabSettings tab in Tabs)
            {
                TabItem tabItem = new TabItem();
                tabItem.Tag = tab;
                tabItem.Header = tab.Title;
                tabItem.Tag = tab;
                tcMain.Items.Add(tabItem);
            }
            LoadTab(Tabs[0]);
            tcMain.SelectionChanged += new SelectionChangedEventHandler (tcMain_SelectionChanged);
        }


        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var hold = ((TabSettings)((Label)sender).Tag);
            LoadTab(hold);
        }
        private void LoadTab (TabSettings ts)
        {
            ActiveTabSetting = ts;
            System.Windows.GridLength gr;
            grMainTitle.Height = GridLength.Auto;
            grBackTitle.Height = GridLength.Auto;
            grBackHeight.Height = new GridLength(1, GridUnitType.Star);
            grBackWidth.Height = new GridLength(1, GridUnitType.Star);
            if (
            ts.SideHeight == null || ts.SideWidth == null)
            {
                gr = new System.Windows.GridLength(0);
                grSideHeight.Height = gr;
                grSideWidth.Height = gr;
                grSideTitle.Height = gr;
            }
            else
            {
                grSideHeight.Height =  new GridLength(1, GridUnitType.Star);
                grSideWidth.Height =  new GridLength(1, GridUnitType.Star);
                grSideTitle.Height = GridLength.Auto;
                LoadGimpSide(ts);
            }
            LoadGimpBack(ts);
        }
        private void LoadGimpBack(TabSettings ts)
        {
            txtblkBackGimpHeight.Text = ts.BackHeight.ToString();
            txtblkBackGimpWidth.Text = ts.BackWidth.ToString();
            txtblkBackGimpHeightRatio.Text = (ts.BackHeight / ts.BackWidth).ToString();
            txtblkBackGimpWidthRatio.Text = (ts.BackWidth/ ts.BackHeight ).ToString();
        }
        private void LoadGimpSide(TabSettings ts)
        {
            txtblkSideGimpHeight.Text = ts.SideHeight.ToString();
            txtblkSideGimpWidth.Text = ts.SideWidth.ToString();
            txtblkSideGimpHeightRatio.Text = (ts.SideHeight / ts.SideWidth).ToString();
            txtblkSideGimpWidthRatio.Text = (ts.SideWidth / ts.SideHeight).ToString();
        }

        private void tcMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //(TabItem)sender
                LoadTab(
                    ((TabSettings)((TabItem)tcMain.SelectedItem).Tag));

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            var o = (TextBox)sender;
           // MessageBox.Show(e.Text + "  " + o.Text);
            if (!IsTextAllowed(e.Text))
            {
                e.Handled = true;

            }
            if (e.Text == "." && o.Text.Contains("."))
            {
                e.Handled = true;
            }

        }
        private static readonly System.Text.RegularExpressions.Regex _regex = new System.Text.RegularExpressions.Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

    
    }
}
