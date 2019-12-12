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
        #region Dictionaries
        public readonly IDictionary<string, int> levelList = new Dictionary<string, int>()
        {
            { " ", 127 },
            { "A",  128 },
            { "B", 129 },
            { "C", 130 },
            { "D", 131 },
            { "E", 132 },
            { "F", 133 },
            { "G", 134 },
            { "H", 135 },
            { "I", 136 },
            { "J", 137 },
            { "K", 138 },
            { "L", 139 },
            { "M", 140 },
            { "N", 141 },
            { "O", 142 },
            { "P", 143 },
            { "Q", 144 },
            { "R", 145 },
            { "S", 146 },
            { "T", 147 },
            { "U", 148 },
            { "V", 149 },
            { "W", 150 },
            { "X", 151 },
            { "Y", 152 },
            { "Z", 153 },
            { "(", 154 },
            { ")", 155 },
            { ":", 156 },
            { ";", 157 },
            { "[", 158 },
            { "]", 159 },
            { "a", 160 },
            { "b", 161 },
            { "c", 162 },
            { "d", 163 },
            { "e", 164 },
            { "f", 165 },
            { "g", 166 },
            { "h", 167 },
            { "i", 168 },
            { "j", 169 },
            { "k", 170 },
            { "l", 171 },
            { "m", 172 },
            { "n", 173 },
            { "o", 174 },
            { "p", 175 },
            { "q", 176 },
            { "r", 177 },
            { "s", 178 },
            { "t", 179 },
            { "u", 180 },
            { "v", 181 },
            { "w", 182 },
            { "x", 183 },
            { "y", 184 },
            { "z", 185 },
            { "#", 225 },
            { "\\", 226 },
            { "-", 227 },
            { "?", 230 },
            { "!", 231 },
            { "♂", 239 },
            { "*", 241 },
            { ".", 242 },
            { "/", 243 },
            { ",", 244 },
            { "♀", 245 },
        };

        public readonly IDictionary<string, string> pokemonList = new Dictionary<string, string>()
        {
            { " ", "MissingNo." },
            { "A", "Golduck" },
            { "B", "Hypno" },
            { "C", "Golbat" },
            { "D", "Mewtwo" },
            { "E", "Snorlax" },
            { "F", "Magikarp" },
            { "G", "MissingNo." },
            { "H", "MissingNo." },
            { "I", "Muk" },
            { "J", "MissingNo." },
            { "K", "Kingler" },
            { "L", "Cloyster" },
            { "M", "MissingNo." },
            { "N", "Electrode" },
            { "O", "Clefable" },
            { "P", "Weezing" },
            { "Q", "Persian" },
            { "R", "Marowak" },
            { "S", "MissingNo." },
            { "T", "Haunter" },
            { "U", "Abra" },
            { "V", "Alakazam" },
            { "W", "Pidgeotto" },
            { "X", "Pidgeot" },
            { "Y", "Starmie" },
            { "Z", "Bulbasaur" },
            { "(", "Venusaur" },
            { ")", "Tentacruel" },
            { ":", "MissingNo." },
            { ";", "Goldeen" },
            { "[", "Seaking" },
            { "]", "MissingNo." },
            { "a", "MissingNo." },
            { "b", "MissingNo." },
            { "c", "MissingNo." },
            { "d", "Ponyta" },
            { "e", "Rapidash" },
            { "f", "Rattata" },
            { "g", "Raticate" },
            { "h", "Nidorino" },
            { "i", "Nidorina" },
            { "j", "Geodude" },
            { "k", "Porygon" },
            { "l", "Aerodactyl" },
            { "m", "MissingNo." },
            { "n", "Magnemite" },
            { "o", "MissingNo." },
            { "p", "MissingNo." },
            { "q", "Charmander" },
            { "r", "Squirtle" },
            { "s", "Charmeleon" },
            { "t", "Wartortle" },
            { "u", "Charizard" },
            { "v", "MissingNo." },
            { "w", "MissingNo. (Kabutops Fossil form)" },
            { "x", "MissingNo. (Aerodactyl Fossil form)" },
            { "y", "MissingNo. (Ghost form)" },
            { "z", "Oddish" },
            { "#", "Rival Blue" },
            { "\\", "Professor Oak" },
            { "-", "Chief" },
            { "?", "Rocket" },
            { "!", "Cooltrainer" },
            { "♂", "Blaine" },
            { "*", "Gentleman" },
            { ".", "Rival Blue" },
            { "/", "Champion Blue" },
            { ",", "Lorelei" },
            { "♀", "Channeler" },
        };
        #endregion

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
                string newUsername = AddValuesToArray();
                SearchLists(POKEMON_SEARCH);
                SearchLists(LEVEL_SEARCH);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds more spacing to the end of the characters name, the games did that same thing.
        /// </summary>
        /// <returns>string containing the username.</returns>
        private string AddValuesToArray()
        {
            string newUsername = userNameInput.Text.PadRight(userNameInput.MaxLength);

            ValidateUsername(ref newUsername);

            // Cleared if there was a previous search
            tblkLevel.Text = "";
            tblkPokemon.Text = "";

            return newUsername;
        }

        /// <summary>
        /// Validate the user input.
        /// </summary>
        /// <param name="newUsername">The Trainer name.</param>
        private void ValidateUsername(ref string newUsername)
        {
            // Regex that should cover all name combinations
            Regex regex = new Regex(@"^[a-zA-Z\s\#\\\-\?\!\*\.\/\♀\♂]{1,7}$");
            Match match = regex.Match(newUsername);
            if (match.Success)
            {
                nameArray = newUsername.ToCharArray();
            }
            else
            {
                MessageBox.Show("Invalid characters where found in your username, please try again!");
            }
        }

        /// <summary>
        /// Search for the values in the dictionaries.
        /// </summary>
        /// <param name="SEARCH_BY">Determines which dictionary to look through.</param>
        private void SearchLists(int SEARCH_BY)
        {
            // Starts on the const value and increments by 2 each time for every even or odd number
            for (int i = SEARCH_BY; i < nameArray.Length; i += 2)
            {
                // Pokémon
                if (SEARCH_BY == 2)
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
