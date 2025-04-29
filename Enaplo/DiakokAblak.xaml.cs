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
    /// Interaction logic for DiakokAblak.xaml
    /// </summary>
    public partial class DiakokAblak : Window
    {
        private string fajlEleres = "diakok.txt";
        private List<Diak> diakok = new();

        public DiakokAblak()
        {
            InitializeComponent();
            BetoltDiakok();
        }

        private void BetoltDiakok()
        {
            if (System.IO.File.Exists(fajlEleres))
            {
                try
                {
                    diakok = Adatkezelo.BeolvasDiakok(fajlEleres);
                    DiakDataGrid.ItemsSource = null;
                    DiakDataGrid.ItemsSource = diakok;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt a fájl beolvasásakor: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show($"A fájl nem található: {fajlEleres}");
            }
        }


        private void HozzaadButton_Click(object sender, RoutedEventArgs e)
        {
            // Csak akkor adja hozzá, ha a név nem üres
            if (!string.IsNullOrWhiteSpace(NevTextBox.Text))
            {
                // Az ID automatikusan az utolsó ID + 1 lesz
                int ujId = diakok.Count > 0 ? diakok.Max(d => d.Id) + 1 : 1;

                diakok.Add(new Diak { Id = ujId, Nev = NevTextBox.Text.Trim() });
                Adatkezelo.MentesDiakok(fajlEleres, diakok);
                BetoltDiakok();
                NevTextBox.Clear();
            }
            else
            {
                MessageBox.Show("A név megadása kötelező.");
            }
        }


        private string jegyFajlEleres = "jegyek.txt";

        private void TorolButton_Click(object sender, RoutedEventArgs e)
        {

            if (DiakDataGrid.SelectedItem is Diak kivalasztott)
            {
                var valasz = MessageBox.Show($"Biztosan törölni szeretnéd: {kivalasztott.Nev}?", "Megerősítés", MessageBoxButton.YesNo);
                if (valasz == MessageBoxResult.Yes)
                {
                    // 1. Törlés a listából
                    diakok.Remove(kivalasztott);
                    Adatkezelo.MentesDiakok(fajlEleres, diakok);

                    // 2. Törlés a jegyek.txt fájlból
                    if (File.Exists(jegyFajlEleres))
                    {
                        var sorok = File.ReadAllLines(jegyFajlEleres).ToList();
                        var megtartott = sorok
                            .Where(s =>
                            {
                                var p = s.Split(';');
                                return p.Length == 3 && int.Parse(p[0]) != kivalasztott.Id;
                            })
                            .ToList();

                        File.WriteAllLines(jegyFajlEleres, megtartott);
                    }

                    // 3. Frissítés
                    BetoltDiakok();
                }
            }
            else
            {
                MessageBox.Show("Nincs kijelölt diák.");
            }
        }


    }
}
