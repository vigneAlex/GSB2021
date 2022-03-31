using GSBFraisModel.Buisness;
using GSBFraisModel.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionFrais.ViewModel
{
    class ViewModelGestionFrais : ViewModelBase
    {
        private ObservableCollection<Visiteur> listVisiteurs;
        private ObservableCollection<FicheFrais> listFicheFrais;
        private ObservableCollection<string> listMois;
        private ObservableCollection<string> listEtat;
        private ObservableCollection<FraisForfait> listFraisForfait;
        private ObservableCollection<LigneFraisForfait> listLignesFraisForfait;
        private ObservableCollection<LigneFraisHorsForfait> listLignesFraisHorsForfait;

        private string suiviEtat;
        private string selectedMois;
        private FicheFrais selectedFiche;
        private Etat selectedEtat;
        private string idEtat;

        private ICommand updateFicheFrais;

        private DaoEtat vmDaoEtat;
        private DaoFicheFrais vmDaoFicheFrais;
        private DaoFraisForfait vmDaoFraisForfait;
        private DaoLigneFraisForfait vmDaoLigneFraisForfait;
        private DaoVisiteurs vmDaoVisiteur;
        private DaoLigneFraisHorsForfait vmDaoLigneFraisHorsForfait;
        public ObservableCollection<Visiteur> ListVisiteurs { get { return listVisiteurs; } set { listVisiteurs = value; } }
        public ObservableCollection<FicheFrais> ListFicheFrais { get { return listFicheFrais; } set { listFicheFrais = value; } }
        public ObservableCollection<string> ListMois { get { return listMois; } set { listMois = value; } }
        public ObservableCollection<string> ListEtat { get { return listEtat; } set { listEtat = value; } }

        public ObservableCollection<FraisForfait> ListFraisForfait { get { return listFraisForfait; } set { listFraisForfait = value; } }
        public ObservableCollection<LigneFraisForfait> ListLignesFraisForfait { get { return listLignesFraisForfait; } set { listLignesFraisForfait = value;} }

        public ObservableCollection<LigneFraisHorsForfait> ListLignesFraisHorsForfait { get { return listLignesFraisHorsForfait; } set { listLignesFraisHorsForfait = value;} }

        public string SelectedMois
        {
            get
            {
                return selectedMois;
            }

            set
            {
                selectedMois = value;
                OnPropertyChanged("SelectedMois");
                listFicheFrais = new ObservableCollection<FicheFrais>(vmDaoFicheFrais.SelectByMois(selectedMois));
                OnPropertyChanged("ListFicheFrais");
            }
        }


        public FicheFrais SelectedFiche
        {
            get
            {
                return selectedFiche;
            }

            set
            {
                selectedFiche = value;
                OnPropertyChanged("SelectedFiche");
                if(selectedFiche != null)
                {
                    SuiviEtat = selectedFiche.UnEtat.Libelle;
                    listLignesFraisForfait = new ObservableCollection<LigneFraisForfait>(selectedFiche.LesLigneFraisForfait);
                    listLignesFraisHorsForfait = new ObservableCollection<LigneFraisHorsForfait>(selectedFiche.LesLigneFraisHorsForfait);
                    OnPropertyChanged("ListLignesFraisForfait");
                    OnPropertyChanged("ListLignesFraisHorsForfait");
                    OnPropertyChanged("SuiviEtat");
                }
                else
                {
                    listLignesFraisForfait = null;
                    listLignesFraisHorsForfait = null;
                    OnPropertyChanged("ListLignesFraisForfait");
                    OnPropertyChanged("ListLignesFraisHorsForfait");
                }        
            }
        }

        public ICommand UpdateFicheFrais
        {
            get
            {
                if (this.updateFicheFrais == null)
                {
                    this.updateFicheFrais = new RelayCommand(() => UpdateLesFichesFrais(), () => true);
                }
                return this.updateFicheFrais;
            }
        }

        public string SuiviEtat
        {
            get
            {
                return suiviEtat;
            }

            set
            {
                suiviEtat = value;
            }
        }

        public Etat SelectedEtat
        {
            get
            {
                return selectedEtat;
            }

            set
            {
                selectedEtat = value;
                OnPropertyChanged("SelectedEtat");

            }
        }

        private void TrackThePayment()
        {
            if (selectedFiche != null)
            {
                SuiviEtat = selectedFiche.UnEtat.Libelle;
                OnPropertyChanged("SuiviEtat");

            }
        }

        public ViewModelGestionFrais(DaoFicheFrais thedaoFicheFrais, DaoFraisForfait thedaoFraisForfait, DaoLigneFraisForfait thedaoLigneFraisForfait, DaoLigneFraisHorsForfait thedaoLigneFraisHorsForfait, DaoEtat theDaoEtat)
        {
            vmDaoFicheFrais = thedaoFicheFrais;
            vmDaoLigneFraisForfait = thedaoLigneFraisForfait;
            vmDaoLigneFraisHorsForfait = thedaoLigneFraisHorsForfait;
            vmDaoEtat = theDaoEtat;

            listEtat = new ObservableCollection<string>(theDaoEtat.SelectListEtat());
            listMois = new ObservableCollection<string>(thedaoFicheFrais.SelectListMois());
            listFicheFrais = new ObservableCollection<FicheFrais>(thedaoFicheFrais.SelectAll());
        }

        private void UpdateLesFichesFrais()
        {
            if(selectedFiche != null)
            {
                if (selectedEtat != null)
                {
                    selectedFiche.UnEtat.Id = selectedEtat.Id;
                }
                vmDaoFicheFrais.Update(selectedFiche);
                foreach (LigneFraisForfait lff in ListLignesFraisForfait)
                {
                    vmDaoLigneFraisForfait.Update(lff);
                }
                foreach (LigneFraisHorsForfait lfhf in ListLignesFraisHorsForfait)
                {
                    vmDaoLigneFraisHorsForfait.Update(lfhf);
                }
            }
            
        }

    }
}
