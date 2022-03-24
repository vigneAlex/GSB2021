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
        private ObservableCollection<FraisForfait> listFraisForfait;
        private ObservableCollection<LigneFraisForfait> listLignesFraisForfait;
        private ObservableCollection<LigneFraisHorsForfait> listLignesFraisHorsForfait;

        private string selectedMois;
        private FicheFrais selectedFiche;
        private LigneFraisForfait toUpdateLigneFraisForfait;
        private LigneFraisHorsForfait toUpdateLigneFraisHorsForfait;
        private LigneFraisForfait activeLigneFraisForfait;
        private LigneFraisHorsForfait activeLigneFraisHorsForfait;


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

        public ObservableCollection<FraisForfait> ListFraisForfait { get { return listFraisForfait; } set { listFraisForfait = value; } }
        public ObservableCollection<LigneFraisForfait> ListLignesFraisForfait { get { return listLignesFraisForfait; } set { listLignesFraisForfait = value; } }

        public ObservableCollection<LigneFraisHorsForfait> ListLignesFraisHorsForfait { get { return listLignesFraisHorsForfait; } set { listLignesFraisHorsForfait = value; } }

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
                    listLignesFraisForfait = new ObservableCollection<LigneFraisForfait>(selectedFiche.LesLigneFraisForfait);
                    listLignesFraisHorsForfait = new ObservableCollection<LigneFraisHorsForfait>(selectedFiche.LesLigneFraisHorsForfait);
                }
                else
                {
                    listLignesFraisForfait = null;
                    listLignesFraisHorsForfait = null;
                }        
                OnPropertyChanged("ListLignesFraisForfait");
                OnPropertyChanged("ListLignesFraisHorsForfait");
            }
        }

        public ICommand UpdateFicheFrais
        {
            get
            {
                if (this.updateFicheFrais == null)
                {
                    this.updateFicheFrais = new RelayCommand(() => UpdateLaFicheFrais(), () => true);
                }
                return this.updateFicheFrais;
            }
        }

        public LigneFraisForfait ToUpdateLigneFraisForfait
        {
            get
            {
                return toUpdateLigneFraisForfait;
            }

            set
            {
                toUpdateLigneFraisForfait = value;
            }
        }

        public LigneFraisHorsForfait ToUpdateLigneFraisHorsForfait
        {
            get
            {
                return toUpdateLigneFraisHorsForfait;
            }

            set
            {
                toUpdateLigneFraisHorsForfait = value;
            }
        }

        public LigneFraisForfait ActiveLigneFraisForfait
        {
            get
            {
                return activeLigneFraisForfait;
            }

            set
            {
                if (activeLigneFraisForfait != value)
                {
                    activeLigneFraisForfait = value;
                    OnPropertyChanged("ListLignesFraisForfait");
                }
            }
        }

        public LigneFraisHorsForfait ActiveLigneFraisHorsForfait
        {
            get
            {
                return activeLigneFraisHorsForfait;
            }

            set
            {
                if (activeLigneFraisHorsForfait != value)
                {
                    activeLigneFraisHorsForfait = value;
                    OnPropertyChanged("ListLignesFraisForfait");
                }
            }
        }

        public ViewModelGestionFrais(DaoFicheFrais thedaoFicheFrais, DaoFraisForfait thedaoFraisForfait, DaoLigneFraisForfait thedaoLigneFraisForfait, DaoLigneFraisHorsForfait thedaoLigneFraisHorsForfait)
        {
            vmDaoFicheFrais = thedaoFicheFrais;

            listMois = new ObservableCollection<string>(thedaoFicheFrais.SelectListMois());
            listFicheFrais = new ObservableCollection<FicheFrais>(thedaoFicheFrais.SelectAll());
        }

        private void UpdateLaFicheFrais()
        {

            ToUpdateLigneFraisForfait = ActiveLigneFraisForfait;
            ToUpdateLigneFraisHorsForfait = ActiveLigneFraisHorsForfait;
            this.vmDaoLigneFraisForfait.Update(ToUpdateLigneFraisForfait);
            this.vmDaoLigneFraisHorsForfait.Update(ToUpdateLigneFraisHorsForfait);
            var i = listLignesFraisForfait.IndexOf(ToUpdateLigneFraisForfait);
            var ii = listLignesFraisHorsForfait.IndexOf(ToUpdateLigneFraisHorsForfait);
            listLignesFraisForfait.RemoveAt(i);
            listLignesFraisHorsForfait.RemoveAt(ii);
            listLignesFraisForfait.Insert(i, ToUpdateLigneFraisForfait);
            listLignesFraisHorsForfait.Insert(ii, ToUpdateLigneFraisHorsForfait);
            SelectedFiche = vmDaoFicheFrais.SelectById(ToUpdateLigneFraisForfait.IdVisiteur, ToUpdateLigneFraisForfait.Mois);


        }

    }
}
