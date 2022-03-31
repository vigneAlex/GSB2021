using GSBFraisModel.Data;
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
using GestionFrais.ViewModel;

namespace GSB
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class GestionFrais : Window
    {
        public GestionFrais(DaoEtat theDaoEtat, DaoLigneFraisForfait thedaolignefraisforfait, DaoLigneFraisHorsForfait thedaolignefraishorsforfait, DaoVisiteurs thedaovisiteurs, DaoFicheFrais thedaoFicheFrais, DaoFraisForfait thedaoFraisForfait)
        {
            InitializeComponent();
            mainGrid.DataContext = new ViewModelGestionFrais(thedaoFicheFrais, thedaoFraisForfait, thedaolignefraisforfait, thedaolignefraishorsforfait, theDaoEtat);
        }

        private void btnSuivrePaiement_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItem != null)
            {
                suivi.Visibility = Visibility.Visible;
            }  
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            suivi.Visibility = Visibility.Hidden;
        }
    }
}
