using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace MissingNo_Name_Checker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly IDictionary<string, int> levelList = new Dictionary<string, int>()
        {
            { string.Empty, 127 },
            { "A",  128 },
            { "B", 129 },
            { "C", 130 },
            { "D", 131 },
            { "E", 132 },
        };

        public readonly IDictionary<string, string> pokemonList = new Dictionary<string, string>()
        {
            { string.Empty, "MissingNo." },
            { "A", "Golduck" },
            { "B", "Hypno" },
            { "C", "Golbat" },
            { "D", "Mewtwo" },
            { "E", "Snorlax" },
            { "F", "MissingNo." },
            { "G", "MissingNo." },
            { "H", "MissingNo." },
            { "I", "MissingNo." },
            { "J", "MissingNo." },
            { "K", "MissingNo." },
            { "L", "MissingNo." },
            { "M", "MissingNo." },
            { "N", "MissingNo." },
            { "O", "MissingNo." },
            { "P", "MissingNo." },
            { "Q", "MissingNo." },
            { "R", "MissingNo." },
            { "S", "MissingNo." },
            { "T", "MissingNo." },
            { "U", "MissingNo." },
            { "V", "MissingNo." },
            { "W", "MissingNo." },
            { "X", "MissingNo." },
            { "Y", "MissingNo." },
            { "Z", "MissingNo." },
            { "(", "MissingNo." },
            { ")", "MissingNo." },
            { ":", "MissingNo." },
            { ";", "MissingNo." },
            { "[", "MissingNo." },
            { "]", "MissingNo." },
            { "a", "MissingNo." },
            { "b", "MissingNo." },
            { "c", "MissingNo." },
            { "d", "MissingNo." },
            { "e", "MissingNo." },
            { "f", "MissingNo." },
            { "g", "MissingNo." },
            { "h", "MissingNo." },
            { "i", "MissingNo." },
            { "j", "MissingNo." },
            { "k", "MissingNo." },
            { "l", "MissingNo." },
            { "m", "MissingNo." },
            { "n", "MissingNo." },
            { "o", "MissingNo." },
            { "p", "MissingNo." },
            { "q", "MissingNo." },
            { "r", "MissingNo." },
            { "s", "MissingNo." },
            { "t", "MissingNo." },
            { "u", "MissingNo." },
            { "v", "MissingNo." },
            { "w", "MissingNo." },
            { "x", "MissingNo." },
            { "y", "MissingNo." },
            { "z", "MissingNo." },
            { "#", "MissingNo." },
            { "\\", "MissingNo." },
            { "-", "MissingNo." },
            { "?", "MissingNo." },
            { "!", "MissingNo." },
            { "♂", "MissingNo." },
            { "*", "MissingNo." },
            { ".", "MissingNo." },
            { "/", "MissingNo." },
            { ",", "MissingNo." },
            { "♀", "MissingNo." },
        };

        char[] nameArray = new char[7];
        const int LEVEL_SEARCH = 1;
        const int POKEMON_SEARCH = 2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // A players name in Gen I can only be 7 characters long
            userNameInput.MaxLength = 7;
        }

        private void userNameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            // When the textbox is clicked, the contents of it are cleared
            userNameInput.Text = "";
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddValuesToArray();
                SearchLists(POKEMON_SEARCH);
                SearchLists(LEVEL_SEARCH);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Adds more spacing to the end of the characters name, the games did that same thing
        private void AddValuesToArray()
        {
            userNameInput.Text.PadRight(userNameInput.MaxLength - userNameInput.Text.Length);

            ValidateUsername();

            // Cleared if there was a previous search
            tblkLevel.Text = "";
            tblkPokemon.Text = "";
        }

        // Validate the user input
        private void ValidateUsername()
        {
            // Regex that should cover all name combinations
            Regex regex = new Regex(@"^[a-zA-Z\s\#\\\-\?\!\*\.\/\♀\♂]{1,7}$");
            Match match = regex.Match(userNameInput.Text);
            if (!match.Success)
            {
                MessageBox.Show("Invalid characters where found in your username, please try again!");
            }

            nameArray = userNameInput.Text.ToCharArray();
        }

        private void SearchLists(int SEARCH_BY)
        {
            // Starts on the const value and increments by 2 each time for every even or odd number
            for (int i = SEARCH_BY; i < nameArray.Length; i += 2)
            {
                // Pokémon
                if (SEARCH_BY % 2 == 0)
                {
                    if (pokemonList.TryGetValue(nameArray[i].ToString(), out string pokemon))
                    {
                        tblkPokemon.Text += $"\n{pokemon}";
                    }
                }
                else
                {
                    if (levelList.TryGetValue(nameArray[i].ToString(), out int level))
                    {
                        tblkLevel.Text += $"\n{level}";
                    }
                }
            }
        }
        #region Additional Buttons
        /* Each of these buttons are to represent the PK and MN characters
         * from the game, since they're not real characters, I used # and \
         * instead*/
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

        //Needs more info, works for now
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
