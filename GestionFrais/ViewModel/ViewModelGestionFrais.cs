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
        private ObservableCollection<Etat> listEtat;
        private ObservableCollection<FraisForfait> listFraisForfait;
        private ObservableCollection<LigneFraisForfait> listLignesFraisForfait;
        private ObservableCollection<LigneFraisHorsForfait> listLignesFraisHorsForfait;

        private string suiviEtat;
        private string selectedMois;
        private FicheFrais selectedFiche;
        private LigneFraisForfait selectedLigneFraisForfait;
        private LigneFraisHorsForfait selectedLigneFraisHorsForfait;
        private Etat selectedEtat;
        private string idEtat;

        private ICommand updateFicheFrais;
        private ICommand refusLigne;
        private ICommand report;

        private DaoEtat vmDaoEtat;
        private DaoFicheFrais vmDaoFicheFrais;
        private DaoFraisForfait vmDaoFraisForfait;
        private DaoLigneFraisForfait vmDaoLigneFraisForfait;
        private DaoVisiteurs vmDaoVisiteur;
        private DaoLigneFraisHorsForfait vmDaoLigneFraisHorsForfait;
        public ObservableCollection<Visiteur> ListVisiteurs { get { return listVisiteurs; } set { listVisiteurs = value; } }
        public ObservableCollection<FicheFrais> ListFicheFrais { get { return listFicheFrais; } set { listFicheFrais = value; } }
        public ObservableCollection<string> ListMois { get { return listMois; } set { listMois = value; } }
        public ObservableCollection<Etat> ListEtat { get { return listEtat; } set { listEtat = value; } }

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

        public LigneFraisForfait SelectedLigneFraisForfait
        {
            get
            {
                return selectedLigneFraisForfait;
            }

            set
            {
                selectedLigneFraisForfait = value;
                OnPropertyChanged("SelectedLigneFraisForfait");
                selectedLigneFraisHorsForfait = null;
                OnPropertyChanged("SelectedLigneFraisHorsForfait");
            }
        }

        public LigneFraisHorsForfait SelectedLigneFraisHorsForfait
        {
            get
            {
                return selectedLigneFraisHorsForfait;
            }

            set
            {
                selectedLigneFraisHorsForfait = value;
                OnPropertyChanged("SelectedLigneFraisHorsForfait");
                selectedLigneFraisForfait = null;
                OnPropertyChanged("SelectedLigneFraisForfait");
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

        public ICommand RefusLigne
        {
            get
            {
                if (this.refusLigne == null)
                {
                    this.refusLigne = new RelayCommand(() => RefuserLaLigne(), () => true);
                }
                return this.refusLigne;
            }
        }
        public ICommand Report
        {
            get
            {
                if (this.report == null)
                {
                    this.report = new RelayCommand(() => ReporterLigne(), () => true);
                }
                return this.report;
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

        

        public ViewModelGestionFrais(DaoFicheFrais thedaoFicheFrais, DaoFraisForfait thedaoFraisForfait, DaoLigneFraisForfait thedaoLigneFraisForfait, DaoLigneFraisHorsForfait thedaoLigneFraisHorsForfait, DaoEtat theDaoEtat)
        {
            vmDaoFicheFrais = thedaoFicheFrais;
            vmDaoLigneFraisForfait = thedaoLigneFraisForfait;
            vmDaoLigneFraisHorsForfait = thedaoLigneFraisHorsForfait;
            vmDaoEtat = theDaoEtat;

            listEtat = new ObservableCollection<Etat>(theDaoEtat.SelectListEtat());
            listMois = new ObservableCollection<string>(thedaoFicheFrais.SelectListMois());
            listFicheFrais = new ObservableCollection<FicheFrais>(thedaoFicheFrais.SelectAll());
        }

        private void UpdateLesFichesFrais()
        {
            if(selectedFiche != null)
            {
                if (selectedEtat != null)
                {
                    FicheFrais laFicheFrais = new FicheFrais(selectedFiche.UnVisiteur,selectedFiche.Mois,selectedFiche.NbJustificatifs,selectedFiche.MontantValide,selectedFiche.DateModif,selectedFiche.UnEtat);
                    laFicheFrais.UnEtat.Id = selectedEtat.Id;
                    vmDaoFicheFrais.Update(laFicheFrais);
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

        private void RefuserLaLigne()
        {
            if (selectedLigneFraisForfait != null && selectedLigneFraisHorsForfait == null)
            {
                vmDaoLigneFraisForfait.Delete(selectedLigneFraisForfait);
                ListLignesFraisForfait.Remove(selectedLigneFraisForfait);
                selectedFiche.LesLigneFraisForfait = new List<LigneFraisForfait>(ListLignesFraisForfait);

            }
            else if (selectedLigneFraisHorsForfait != null && selectedLigneFraisForfait == null)
            {
                vmDaoLigneFraisHorsForfait.Delete(selectedLigneFraisHorsForfait);
                ListLignesFraisHorsForfait.Remove(selectedLigneFraisHorsForfait);
                selectedFiche.LesLigneFraisHorsForfait = new List<LigneFraisHorsForfait>(ListLignesFraisHorsForfait);
            }
            
        }
        private void ReporterLigne()
        {
            bool verif = false;
            DateTime today = DateTime.Now;
            string date = today.ToString("yyyyMM");
            if (selectedLigneFraisHorsForfait != null && selectedLigneFraisForfait == null)
            {
                foreach (FicheFrais ff in ListFicheFrais) {
                    if (ff.Mois == date && ff.UnVisiteur.Id == selectedLigneFraisHorsForfait.IdVisiteur)
                    {
                        LigneFraisHorsForfait laLfhf = new LigneFraisHorsForfait(selectedLigneFraisHorsForfait.Id, selectedLigneFraisHorsForfait.IdVisiteur, date, selectedLigneFraisHorsForfait.Libelle, selectedLigneFraisHorsForfait.Date, selectedLigneFraisHorsForfait.Montant);
                        vmDaoLigneFraisHorsForfait.Update(laLfhf);
                        ListLignesFraisHorsForfait.Remove(selectedLigneFraisHorsForfait);
                        verif = true;
                    }
                }
                if(verif == false)
                {
                    FicheFrais laFf = new FicheFrais(selectedFiche.UnVisiteur, date, selectedFiche.NbJustificatifs, selectedFiche.MontantValide, selectedFiche.DateModif, selectedFiche.UnEtat);
                    LigneFraisHorsForfait laLfhf = new LigneFraisHorsForfait(selectedLigneFraisHorsForfait.Id, selectedLigneFraisHorsForfait.IdVisiteur, date, selectedLigneFraisHorsForfait.Libelle, selectedLigneFraisHorsForfait.Date, selectedLigneFraisHorsForfait.Montant);
                    vmDaoFicheFrais.Insert(laFf);
                    vmDaoLigneFraisHorsForfait.Update(laLfhf);
                    ListLignesFraisHorsForfait.Remove(selectedLigneFraisHorsForfait);
                    selectedFiche.LesLigneFraisHorsForfait = new List<LigneFraisHorsForfait>(ListLignesFraisHorsForfait);

                }
            }
        }
    }
}
