using GSBFraisModel.Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFraisModel.Buisness
{
   public class FraisForfait
    {
        private string id;
        private string libelle;
        private decimal montant;

        public FraisForfait(string unId, string unLibelle, decimal unMontant)
        {
            this.id = unId;
            this.libelle = unLibelle;
            this.montant = unMontant;
        }

        public string Id
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
    }
}
