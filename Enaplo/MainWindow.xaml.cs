using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Enaplo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Diákok ablak megnyitása
        private void DiakokButton_Click(object sender, RoutedEventArgs e)
        {
            DiakokAblak diakokAblak = new DiakokAblak();
            diakokAblak.Show(); // Megnyitja a DiakokAblak ablakot
        }

        // Tantárgyak ablak megnyitása
        private void TantargyakButton_Click(object sender, RoutedEventArgs e)
        {
            TantargyakAblak tantargyakAblak = new TantargyakAblak();
            tantargyakAblak.Show(); // Megnyitja a TantargyakAblak ablakot
        }

        private void JegyekButton_Click(object sender, RoutedEventArgs e)
        {
            List<Diak> diakok = Adatkezelo.BeolvasDiakok("diakok.txt");
            List<Tantargy> tantargyak = Adatkezelo.BeolvasTantargyak("tantargyak.txt");

            JegyekAblak jegyekAblak = new JegyekAblak(diakok, tantargyak);  
            jegyekAblak.Show(); 
        }

    }
}
