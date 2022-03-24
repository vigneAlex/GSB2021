using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSBFraisModel.Buisness;
using System.Data;
using GSBFraisModel.Data;

namespace GSBFraisModel.Data
{
    public class DaoVisiteurs
    {
        private Dbal unDbal;

        public DaoVisiteurs(Dbal myDbal)
        {
            this.unDbal = myDbal;
        }

        public void Insert(Visiteur unVisiteur)
        {
            string query = " visiteur (id, nom, prenom , login, mdp) VALUES ('" + unVisiteur.Id + "', '" + unVisiteur.Nom.Replace("'", "''") + "','" + unVisiteur.Prenom.Replace("'", "''") + "','" + unVisiteur.Login + "','" + unVisiteur.Mdp + "')";
            this.unDbal.Insert(query);
        }

        public void Update(Visiteur unVisiteur)
        {
            string query = " visiteur (nom, prenom , login, mdp) SET '" + unVisiteur.Nom.Replace("'", "''") + "','" + unVisiteur.Prenom.Replace("'", "''") + "','" + ",'" + unVisiteur.Login + "','" + unVisiteur.Mdp + "'" + "WHERE id = " + unVisiteur.Id + "'";
            this.unDbal.Update(query);
        }

        public void Delete(Visiteur unVisiteur)
        {
            string query = " visiteur WHERE id =" + unVisiteur.Id + "'";
            this.unDbal.Delete(query);
        }

        public List<Visiteur> SelectAll()
        {
            List<Visiteur> listVisiteurs = new List<Visiteur>();
            DataTable myTable = this.unDbal.SelectAll("visiteur");

            foreach (DataRow r in myTable.Rows)
            {
                listVisiteurs.Add(new Visiteur((string)r["id"], (string)r["nom"], (string)r["prenom"], (string)r["login"], (string)r["mdp"], (string)r["adresse"], (string)r["cp"], (string)r["ville"], (DateTime)r["dateEmbauche"]));
            }
            return listVisiteurs;
        }

        public Visiteur SelectByName(string nameVisiteur)
        {
            DataTable result = new DataTable();
            result = this.unDbal.SelectByField("visiteur", "nom = '" + nameVisiteur.Replace("'", "''") + "'");
            Visiteur foundVisiteur = new Visiteur((string)result.Rows[0]["id"], (string)result.Rows[0]["nom"], (string)result.Rows[0]["prenom"], (string)result.Rows[0]["login"], (string)result.Rows[0]["mdp"], (string)result.Rows[0]["adresse"], (string)result.Rows[0]["cp"], (string)result.Rows[0]["ville"], (DateTime)result.Rows[0]["dateEmbauche"]);
            return foundVisiteur;
        }

        public Visiteur SelectById(string idVisiteur)
        {
            DataRow result = this.unDbal.SelectById("visiteur", idVisiteur );
            return new Visiteur((string)result["id"], (string)result["nom"], (string)result["prenom"], (string)result["login"], (string)result["mdp"], (string)result["adresse"], (string)result["cp"], (string)result["ville"], (DateTime)result["dateEmbauche"]);
        }
    }
}
