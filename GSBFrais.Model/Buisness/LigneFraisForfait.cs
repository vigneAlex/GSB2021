using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFraisModel.Buisness
{
    public class LigneFraisForfait
    {
        private FicheFrais ficheFrais;
        private string idVisiteur;
        private string idFraitForfait;
        private string mois;
        private int quantite;

        public LigneFraisForfait(string unIdVisiteur, string unMois,string unIdFraitForfait , int uneQuantite, FicheFrais uneFicheFrais)
        {
            this.idVisiteur = unIdVisiteur;
            this.mois = unMois;
            this.idFraitForfait = unIdFraitForfait;
            this.quantite = uneQuantite;
            this.FicheFrais = uneFicheFrais;
        }

        public LigneFraisForfait(string unIdVisiteur, string unMois, string unIdFraitForfait, int uneQuantite)
        {
            this.idVisiteur = unIdVisiteur;
            this.mois = unMois;
            this.idFraitForfait = unIdFraitForfait;
            this.quantite = uneQuantite;
        }

        public string IdVisiteur
        {
            get
            {
                return idVisiteur;
            }

            set
            {
                idVisiteur = value;
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

        public int Quantite
        {
            get
            {
                return quantite;
            }

            set
            {
                quantite = value;
            }
        }

        public string IdFraitForfait
        {
            get
            {
                return idFraitForfait;
            }

            set
            {
                idFraitForfait = value;
            }
        }

        public FicheFrais FicheFrais
        {
            get
            {
                return ficheFrais;
            }

            set
            {
                ficheFrais = value;
            }
        }
    }
}
