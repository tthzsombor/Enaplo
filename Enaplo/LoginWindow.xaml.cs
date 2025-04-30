using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Enaplo
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }


        private void Bejelentkezes_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "belepes.txt";
            if (File.Exists(filePath))
            {
                string[] sor = File.ReadAllLines(filePath);
                if (sor.Length > 0)
                {
                    var adat = sor[0].Split(';');
                    if (adat.Length == 2)
                    {
                        string felhasznalo = adat[0];
                        string jelszo = adat[1];

                        if (FelhasznaloTextBox.Text == felhasznalo && JelszoBox.Password == jelszo)
                        {
                            MainWindow main = new MainWindow();
                            main.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("A belepes.txt fájl nem található!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

