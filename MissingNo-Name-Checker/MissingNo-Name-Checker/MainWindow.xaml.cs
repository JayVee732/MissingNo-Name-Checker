using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        //The users name is stored here as 7 seperate characters
        char[] nameArray = new char[7];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //A players name in Gen I can only be 7 characters long
            userNameInput.MaxLength = 7;
        }

        private void userNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            //When the textbox is clicked, the contents of it are cleared
            userNameInput.Text = "";
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            AddValuesToArray();

            try
            {
                //2 Different files are used
                using (StreamReader sr = new StreamReader("levelList.txt"))
                {
                    //const value used as it will never change
                    const int LEVEL_SEARCH = 1;
                    SearchFiles(sr, LEVEL_SEARCH);
                }

                using (StreamReader sr = new StreamReader("pokemonList.txt"))
                {
                    const int POKEMON_SEARCH = 2;
                    SearchFiles(sr, POKEMON_SEARCH);
                }
            }
            catch (Exception ex)
            {
                //If there's a problem
                MessageBox.Show(ex.Message);
            }
        }

        private void AddValuesToArray()
        {
            if (userNameInput.Text.Length < userNameInput.MaxLength)
            {
                for (int i = 0; i < userNameInput.MaxLength; i++)
                {
                    userNameInput.Text += " ";
                    if (userNameInput.Text.Length == userNameInput.MaxLength)
                    {
                        break;
                    }
                }
            }

            ValidateUsername();

            //Textbox is converted to char array
            //Cleared if there was a previous search
            tblkLevel.Text = "";
            tblkPokemon.Text = "";
        }

        private void ValidateUsername()
        {
            Regex regex = new Regex(@"^[a-zA-Z\s\#\\\-\?\!\*\.\/]{1,7}$");
            Match match = regex.Match(userNameInput.Text);
            if (match.Success)
            {
                nameArray = userNameInput.Text.ToCharArray();
            }
            else
            {
                MessageBox.Show("Invalid characters where found in your username, please try again!");
            }
        }

        private void SearchFiles(StreamReader sr, int SEARCH_BY)
        {
            //Starts on the const value and increments by 2 each time for every even or odd number
            for (int i = SEARCH_BY; i < nameArray.Length; i += 2)
            {
                //Reads the first line
                string contents = sr.ReadLine();
                while (contents != null) //Keeps reading until the end of the file
                {
                    //Splits the line from the textfile and reads the first value
                    string[] newLine = contents.Split('|');
                    string checkLine = newLine[1];

                    if (newLine[0] == nameArray[i].ToString())
                    {
                        //Appends to textbox depending on if it's the level or Pokemon file
                        if (SEARCH_BY == 2)
                        {
                            tblkPokemon.Text += String.Format("\n{0}", checkLine.ToString());
                        }
                        else
                        {
                            tblkLevel.Text += String.Format("\n{0}", checkLine.ToString());
                        }
                        newLine = null;
                        //Starts reading the text file from the start for the next character
                        sr.DiscardBufferedData();
                        sr.BaseStream.Seek(0, SeekOrigin.Begin);
                        break;
                    }
                    //Reads the next line
                    contents = sr.ReadLine();
                }
            }
        }
        #region Additional Buttons
        private void btn_PK_Click(object sender, RoutedEventArgs e)
        {
            if (userNameInput.Text.Length < userNameInput.MaxLength)
            {
                userNameInput.Text += "#";
            }
        }

        private void btn_MN_Click(object sender, RoutedEventArgs e)
        {
            if (userNameInput.Text.Length < userNameInput.MaxLength)
            {
                userNameInput.Text += "\\";
            }
        }
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hi there, and thanks for using this program! ^_^" +
                "\n\nThe purpose of this application is to allow you to enter your trainer name from Pokémon Red and Blue and see what Pokémon (or trainers) you'll get from the Old Man Glitch." +
                "\n\nThe PK and MN buttons are for the extra characters in the games that aren't on a normal keyboard. They are represented as '#' and '\\' respectively in the program, however, they will still produce the same results as if they were the PK and MN values." +
                "\n\n- Jamie");
        }
        #endregion
    }
}
