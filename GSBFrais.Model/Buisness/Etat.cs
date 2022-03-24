using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFraisModel.Buisness
{
    public class Etat
    {
        private string id;
        private string libelle;

        public Etat(string unId, string unLibelle)
        {
            this.id = unId;
            this.libelle = unLibelle;
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
    }
}
