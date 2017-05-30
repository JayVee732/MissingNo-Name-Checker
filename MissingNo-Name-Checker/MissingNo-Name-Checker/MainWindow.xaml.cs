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
        string[] nameInput = new string[7];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nameInput[0] = userName.Text;
            nameInput[1] = userName_Copy.Text;
            nameInput[2] = userName_Copy1.Text;
            nameInput[3] = userName_Copy2.Text;
            nameInput[4] = userName_Copy3.Text;
            nameInput[5] = userName_Copy4.Text;
            nameInput[6] = userName_Copy5.Text;

            //just for testing
            tblkResult.Text = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", nameInput[0], nameInput[1], nameInput[2], nameInput[3], nameInput[4], nameInput[5], nameInput[6]);

            try
            {
                using (StreamReader sr = new StreamReader("levelList.txt"))
                {
                    //i += 2 to get to next part for array
                    for (int i = 1; i < nameInput.Length; i += 2)
                    {
                        //next line
                        string contents = sr.ReadLine();
                        while (contents != null)
                        {
                            //get level that goes with letter
                            string[] newLevel = contents.Split(',');
                            string levelDisplay = newLevel[1];

                            if (newLevel[0] == nameInput[i])
                            {
                                Trace.Write(levelDisplay);
                                tblkResult.Text += levelDisplay.ToString(); //so I can see what's happening
                                newLevel = null;
                                //this only works with alphabetical names for now, will fix
                                i += 2;
                            }
                            contents = sr.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
