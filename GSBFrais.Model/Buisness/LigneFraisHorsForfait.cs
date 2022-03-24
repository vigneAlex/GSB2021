using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFraisModel.Buisness
{
    public class LigneFraisHorsForfait
    {
        private FicheFrais ficheFrais;
        private int id;
        private string idVisiteur;
        private string mois;
        private string libelle;
        private DateTime date;
        private decimal montant;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
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

        public string Libelle
        {
            get
            {
                return libelle;
            }

            set
            {
                libelle = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public decimal Montant
        {
            get
            {
                return montant;
            }

            set
            {
                montant = value;
            }
        }

        public LigneFraisHorsForfait(int unId, string unIdVisiteur, string unMois, string unLibelle, DateTime uneDate, decimal unMontant, FicheFrais uneFicheFrais)
        {
            this.Id = unId;
            this.IdVisiteur = unIdVisiteur;
            this.Mois = unMois;
            this.Libelle = unLibelle;
            this.Date = uneDate;
            this.Montant = unMontant;
            this.ficheFrais = uneFicheFrais;
        }

        public LigneFraisHorsForfait(int unId, string unIdVisiteur, string unMois, string unLibelle, DateTime uneDate, decimal unMontant)
        {
            this.Id = unId;
            this.IdVisiteur = unIdVisiteur;
            this.Mois = unMois;
            this.Libelle = unLibelle;
            this.Date = uneDate;
            this.Montant = unMontant;
        }

    }
}
