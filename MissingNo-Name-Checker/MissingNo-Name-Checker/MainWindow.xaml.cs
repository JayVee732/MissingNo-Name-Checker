using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MissingNo_Name_Checker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] nameInput = new char[7];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userNameInput.MaxLength = 7;
        }

        private void userNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            userNameInput.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nameInput = userNameInput.Text.ToCharArray();
            
            tblkResult.Text = "";

            try
            {
                using (StreamReader sr = new StreamReader("levelList.txt"))
                {
                    const int LEVELSEARCH = 1;
                    SearchFiles(sr, LEVELSEARCH);
                }

                using (StreamReader sr = new StreamReader("pokemonList.txt"))
                {
                    const int POKEMONSEARCH = 2;
                    SearchFiles(sr, POKEMONSEARCH);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchFiles(StreamReader sr, int SEARCHBY)
        {
            for (int i = SEARCHBY; i < nameInput.Length; i += 2)
            {
                //next line
                string contents = sr.ReadLine();
                while (contents != null)
                {
                    //get level that goes with letter
                    string[] newLevel = contents.Split(',');
                    string levelDisplay = newLevel[1];

                    if (newLevel[0] == nameInput[i].ToString())
                    {
                        tblkResult.Text += levelDisplay.ToString();
                        newLevel = null;
                        sr.DiscardBufferedData();
                        sr.BaseStream.Seek(0, SeekOrigin.Begin);
                        break;
                    }
                    contents = sr.ReadLine();
                }
            }
        }
    }
}
