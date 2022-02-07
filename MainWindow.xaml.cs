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
using System.IO;

namespace DU_Binding
{
    class Osoba : INotifyPropertyChanged
    {
        public string _Jmeno, _Prijmeni;
        public DateTime _Narozeni;

        public Osoba(string jmeno, string prijmeni, DateTime narozeni)
        {
            _Jmeno = jmeno;
            _Prijmeni = prijmeni;
            _Narozeni = narozeni;
        }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) // jestli někdo poslouchá ...
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
    class Zamestnanec : Osoba, INotifyPropertyChanged
    {
        public enum Vzdelani{ ZS, SS, VS }
        public Vzdelani _TvojeVzdelani;
        public string _PracovniPozice;
        public int _Plat;

        public Zamestnanec(string jmeno, string prijimeni, DateTime narozeniny, Vzdelani tvojeVzdelani, string pracovniPozice, int plat) : base(jmeno, prijimeni, narozeniny)
        {
            _TvojeVzdelani = tvojeVzdelani;
            _PracovniPozice = pracovniPozice;
            _Plat = plat;
        }


        // Při každé změně dat chceme akutalizovat status bar
        public Vzdelani TvojeVzdelani
        {
            get => _TvojeVzdelani;
            set
            {
                _TvojeVzdelani = value;
                OnPropertyChanged("TvojeVzdelani");
                OnPropertyChanged("Status");
            }
        }
        public string PracovniPozice
        {
            get => _PracovniPozice;
            set
            {
                _PracovniPozice = value;
                OnPropertyChanged("PracovniPozice");
                OnPropertyChanged("Status");
            }
        }
        public int Plat
        {
            get => _Plat;
            set
            {
                _Plat = value;
                OnPropertyChanged("Plat");
                OnPropertyChanged("Status");
            }
        }


         public string Status
        {
            get => Jmeno + " " + Prijmeni + " " + Narozeni.ToShortDateString() + " " + TvojeVzdelani + " " + PracovniPozice + " " + Plat + "Kč";
        } 

        public override string ToString()
        {
            return Jmeno + " " + Prijmeni + " " + Narozeni.ToShortDateString() + " " + TvojeVzdelani + " " + PracovniPozice + " " + Plat + "Kč";
        }

        // pomocná metoda pro informaci o změně v datech

        public event PropertyChangedEventHandler PropertyChangedZ;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChangedZ != null) // jestli někdo poslouchá ...
                PropertyChangedZ(this, new PropertyChangedEventArgs(property));
        }
    }

    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Zamestnanec z;

        public MainWindow()
        {
            InitializeComponent();

            DataContext =
             z = new Zamestnanec(tbJmeno.Text, tbPrijmeni.Text, DateTime.Now, 0, tbPracovniPozice.Text, 0)
             {
                 Narozeni = DateTime.Now
             };
        }

        

        // Změna dat, vzhledem k bindingu, stačí i k aktualizaci grafické podoby formuláře
        private void BtClear_Click(object sender, RoutedEventArgs e)
        {
            z.Jmeno = z.Prijmeni = z.PracovniPozice = string.Empty;
            ComboNejvyssiVzdelani.Text = tbPracovniPozice.Text = "";
            tbPlat.Text = "";
            z.Plat = 0;
            z.Narozeni = DateTime.Now;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (z.Plat > 0 && z.Jmeno != "" && z.Prijmeni != "" && z.PracovniPozice != "" && ComboNejvyssiVzdelani.Text != "")
            {
                using (StreamWriter sw = new StreamWriter("MojeData.txt", true))
                {
                    sw.WriteLine(z.ToString());
                    sw.Close();
                }
            }
            else
            {
                MessageBox.Show("Něco jsi nevyplnil" + "\n");
            }
        }
    }


}
