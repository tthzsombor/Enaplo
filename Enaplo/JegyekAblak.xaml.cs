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
    /// Interaction logic for JegyekAblak.xaml
    /// </summary>
    public partial class JegyekAblak : Window
    {
        private string fajlEleres = "jegyek.txt";
        private List<Jegy> jegyek = new();
        private List<Diak> diakok;
        private List<Tantargy> tantargyak;

        public JegyekAblak(List<Diak> diakok, List<Tantargy> tantargyak)
        {
            InitializeComponent();
            this.diakok = diakok;
            this.tantargyak = tantargyak;

            BetoltJegyek();
            DiakComboBox.ItemsSource = diakok;
            TantargyComboBox.ItemsSource = tantargyak;
        }

        private void BetoltJegyek()
        {
            if (System.IO.File.Exists(fajlEleres))
                jegyek = Adatkezelo.BeolvasJegyek(fajlEleres, diakok, tantargyak);

            JegyDataGrid.ItemsSource = null;
            JegyDataGrid.ItemsSource = jegyek;

            FrissitAtlagok();
        }

        private void FrissitAtlagok()
        {
            var diakAtlagok = jegyek
                .GroupBy(j => j.Diak.Nev)
                .Select(g => $"{g.Key}: {g.Average(j => j.Ertek):0.00}");

            var tantargyAtlagok = jegyek
                .GroupBy(j => j.Tantargy.Tantargynev)
                .Select(g => $"{g.Key}: {g.Average(j => j.Ertek):0.00}");

            DiakAtlagTextBlock.Text = "Diák átlagok:\n" + string.Join("\n", diakAtlagok);
            TantargyAtlagTextBlock.Text = "Tantárgy átlagok:\n" + string.Join("\n", tantargyAtlagok);
        }

        private void Hozzaad_Click(object sender, RoutedEventArgs e)
        {
            if (DiakComboBox.SelectedItem is Diak diak &&
                TantargyComboBox.SelectedItem is Tantargy tantargy &&
                int.TryParse(ErtekTextBox.Text, out int ertek) && ertek >= 1 && ertek <= 5)
            {
                jegyek.Add(new Jegy { Diak = diak, Tantargy = tantargy, Ertek = ertek });
                Adatkezelo.MentesJegyek(fajlEleres, jegyek);
                BetoltJegyek();
                ErtekTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Hibás adatbevitel!");
            }
        }

        private void Torles_Click(object sender, RoutedEventArgs e)
        {
            if (JegyDataGrid.SelectedItem is Jegy kivalasztott)
            {
                jegyek.Remove(kivalasztott);
                Adatkezelo.MentesJegyek(fajlEleres, jegyek);
                BetoltJegyek();
            }
        }
    }

}

