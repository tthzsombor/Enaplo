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
    /// Interaction logic for TantargyakAblak.xaml
    /// </summary>
    public partial class TantargyakAblak : Window
    {
        private string fajlEleres = "tantargyak.txt";
        private List<Tantargy> tantargyak = new();

        public TantargyakAblak()
        {
            InitializeComponent();
            BetoltTantargyak();
        }

        private void BetoltTantargyak()
        {
            if (File.Exists(fajlEleres))
            {
                tantargyak = Adatkezelo.BeolvasTantargyak(fajlEleres);
                TantargyListBox.ItemsSource = null;
                TantargyListBox.ItemsSource = tantargyak.Select(t => t.Tantargynev);
            }
        }

        private void HozzaadButton_Click(object sender, RoutedEventArgs e)
        {
            string nev = TantargyTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(nev))
            {
                if (tantargyak.Any(t => t.Tantargynev.ToLower() == nev.ToLower()))
                {
                    MessageBox.Show("Ez a tantárgy már létezik.");
                    return;
                }

                // Itt hozzáadjuk az új tantárgyat a listához, és beállítjuk a nevét
                tantargyak.Add(new Tantargy { Tantargynev = nev });

                // A fájl mentése
                Adatkezelo.MentesTantargyak(fajlEleres, tantargyak);

                // Frissítjük a listát
                BetoltTantargyak();

                // Töröljük a szöveget a text boxból
                TantargyTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Adj meg egy tantárgynevet!");
            }
        }


        private void TorlesButton_Click(object sender, RoutedEventArgs e)
        {
            if (TantargyListBox.SelectedItem is string kivalasztottNev)
            {
                // Megerősítés kérése
                var eredmeny = MessageBox.Show($"Biztosan törölni szeretnéd a '{kivalasztottNev}' tantárgyat és az összes hozzá tartozó jegyet?",
                                              "Megerősítés",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
                if (eredmeny != MessageBoxResult.Yes) return;

                var torlendo = tantargyak.FirstOrDefault(t => t.Tantargynev == kivalasztottNev);
                if (torlendo != null)
                {
                    try
                    {
                        // 1. Először töröljük a kapcsolódó jegyeket
                        string jegyekFajl = "jegyek.txt";
                        if (File.Exists(jegyekFajl))
                        {
                            // Összes jegy betöltése
                            var osszesJegy = File.ReadAllLines(jegyekFajl);

                            // Csak azokat a jegyeket megtartani, amik NEM ehhez a tantárgyhoz tartoznak
                            var ujJegyek = osszesJegy
                                .Where(sor =>
                                {
                                    var elemek = sor.Split(';');
                                    // Ellenőrizzük, hogy van-e második elem és az nem egyezik a törlendő tantárgy névvel
                                    return elemek.Length >= 2 && elemek[1].Trim() != torlendo.Tantargynev;
                                })
                                .ToArray();

                            // Fájl írása
                            File.WriteAllLines(jegyekFajl, ujJegyek);
                        }

                        // 2. Utána töröljük a tantárgyat
                        tantargyak.Remove(torlendo);
                        Adatkezelo.MentesTantargyak(fajlEleres, tantargyak);

                        // 3. Frissítés
                        BetoltTantargyak();

                        MessageBox.Show($"A '{torlendo.Tantargynev}' tantárgy és az összes hozzá tartozó jegy törölve lett.",
                                      "Törlés sikeres",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba történt a törlés során: {ex.Message}",
                                      "Hiba",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva tantárgy a törléshez.",
                                "Hiba",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }





    }
}

