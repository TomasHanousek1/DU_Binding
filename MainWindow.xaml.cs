using System;
using System.Collections.Generic;
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
using System.ComponentModel;

namespace DU_Binding
{
    class Osoba : INotifyPropertyChanged
    {
        private string _Jmeno, _Prijmeni;
        private DateTime _Narozeni;

        public event PropertyChangedEventHandler PropertyChanged;

        // Při každé změně dat chceme akutalizovat status bar
        public string Jmeno
        {
            get => _Jmeno;
            set
            {
                _Jmeno = value;
                OnPropertyChanged("Jmeno");
                OnPropertyChanged("Status");
            }
        }
        public string Prijmeni
        {
            get => _Prijmeni;
            set
            {
                _Prijmeni = value;
                OnPropertyChanged("Prijmeni");
                OnPropertyChanged("Status");
            }
        }
        public DateTime Narozeni
        {
            get => _Narozeni;
            set
            {
                _Narozeni = value;
                OnPropertyChanged("Narozeni");
                OnPropertyChanged("Status");
            }
        }

        public string Status
        {
            get => Jmeno + " " + Prijmeni + " " + Narozeni.ToString();
        }

        public override string ToString()
        {
            return Jmeno + " " + Prijmeni + " " + Narozeni.ToShortDateString();
        }

        // pomocná metoda pro informaci o změně v datech
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) // jestli někdo poslouchá ...
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
    class Zamestnanec : Osoba
    {
        enum Vzdelani{ ZS, SS, VS }
        string PracovniPozice;
        double Plat;

        
    }

    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Osoba p;

        public MainWindow()
        {
            InitializeComponent();

            DataContext =
            p = new Osoba()
            {
                Narozeni = DateTime.Now
            };

        }

        private void BtShow_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression expr = tbPrijmeni.GetBindingExpression(TextBox.TextProperty);
            expr?.UpdateSource();

            MessageBox.Show(p.ToString() + "\n" +
                expr.ResolvedSourcePropertyName + ": vynucená akutalizace");
        }

        // Změna dat, vzhledem k bindingu, stačí i k aktualizaci grafické podoby formuláře
        private void BtClear_Click(object sender, RoutedEventArgs e)
        {
            p.Jmeno = p.Prijmeni = string.Empty;
            p.Narozeni = DateTime.Now;
        }
    }


}
