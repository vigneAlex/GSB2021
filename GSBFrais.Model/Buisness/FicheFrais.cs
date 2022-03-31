using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFraisModel.Buisness
{
    public class FicheFrais
    {
        private Visiteur unVisiteur;
        private string mois;
        private Etat unEtat;
        private decimal montantValide;
        private int nbJustificatifs;
        private DateTime dateModif;

        public List<LigneFraisForfait> LesLigneFraisForfait { get; set; }
        public List<LigneFraisHorsForfait> LesLigneFraisHorsForfait { get; set; }

        public FicheFrais(Visiteur unVisiteur, string unMois, int unNbJustificatifs, decimal unMontantValide, DateTime uneDateModif, Etat unEtat)
        {
            this.unVisiteur = unVisiteur;
            this.mois = unMois;
            this.nbJustificatifs = unNbJustificatifs;
            this.montantValide = unMontantValide;
            this.dateModif = uneDateModif;
            this.unEtat = unEtat;
        }

        internal Visiteur UnVisiteur
        {
            get
            {
                return unVisiteur;
            }

            set
            {
                unVisiteur = value;
            }
        }

        public string Mois
        {
            get
            {
                return mois;
            }

            set
            {
                mois = value;
            }
        }

        public Etat UnEtat
        {
            get
            {
                return unEtat;
            }

            set
            {
                unEtat = value;
            }
        }

        public decimal MontantValide
        {
            get
            {
                return montantValide;
            }

            set
            {
                montantValide = value;
            }
        }

        public int NbJustificatifs
        {
            get
            {
                return nbJustificatifs;
            }

            set
            {
                nbJustificatifs = value;
            }
        }

        public DateTime DateModif
        {
            get
            {
                return dateModif;
            }

            set
            {
                dateModif = value;
            }
        }

        public override string ToString()
        {
            return "Fiche frais " + mois + " - " + unVisiteur.Id + " - " + unVisiteur.Nom +" "+ unVisiteur.Prenom + "\n";
        } 
    }
}
