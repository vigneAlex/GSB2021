using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFraisModel.Buisness
{
    public class Visiteur
    {
        // attributs
        private string id;
        private string nom;
        private string prenom;
        private string login;
        private string mdp;
        private string adresse;
        private string cp;
        private string ville;
        private DateTime dateEmbauche;

        public Visiteur(string unId, string unNom, string unPrenom, string unLogin, string unMdp, string uneAdresse, string unCp, string uneVille, DateTime uneDateEmbauche)
        {
            this.id = unId;
            this.nom = unNom;
            this.prenom = unPrenom;
            this.login = unLogin;
            this.mdp = unMdp;
            this.adresse = uneAdresse;
            this.cp = unCp;
            this.ville = uneVille;
            this.dateEmbauche = uneDateEmbauche;
        }

        // propriétés
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

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        public string Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
            }
        }

        public string Mdp
        {
            get
            {
                return mdp;
            }

            set
            {
                mdp = value;
            }
        }

        public string Adresse
        {
            get
            {
                return adresse;
            }

            set
            {
                adresse = value;
            }
        }

        public string Cp
        {
            get
            {
                return cp;
            }

            set
            {
                cp = value;
            }
        }

        public string Ville
        {
            get
            {
                return ville;
            }

            set
            {
                ville = value;
            }
        }

        public DateTime DateEmbauche
        {
            get
            {
                return dateEmbauche;
            }

            set
            {
                dateEmbauche = value;
            }
        }

        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                prenom = value;
            }
        }
    }
}