using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSBFraisModel.Data;
using GSBFraisModel.Buisness;
using System.Data;
using GSBFraisModel.Data;

namespace GSBFraisModel.Data
{
    public class DaoEtat
    {
        private Dbal unDbal;

        public DaoEtat(Dbal myDbal)
        {
            this.unDbal = myDbal;
        }

        public void Insert(Etat unEtat)
        {
            string query = " etat (id, libelle) VALUES ('" + unEtat.Id + "',' " + unEtat.Libelle.Replace("'", "''") + "')";
            this.unDbal.Insert(query);
        }

        public void Update(Etat unEtat)
        {
            string query = " etat (id, libelle) SET '" + unEtat.Libelle.Replace("'", "''") + "'";
            this.unDbal.Update(query);
        }

        public void Delete(Etat unEtat)
        {
            string query = " visiteur WHERE id ='" + unEtat.Id + "'";
            this.unDbal.Delete(query);
        }

        public List<Etat> SelectAll()
        {
            List<Etat> listEtat = new List<Etat>();
            DataTable myTable = this.unDbal.SelectAll("etat");

            foreach (DataRow r in myTable.Rows)
            {
                listEtat.Add(new Etat((string)r["id"], (string)r["libelle"]));
            }
            return listEtat;
        }

        public Etat SelectByName(string nameEtat)
        {
            DataTable result = new DataTable();
            result = this.unDbal.SelectByField("etat", "libelle = '" + nameEtat.Replace("'", "''") + "'");
            Etat foundEtat = new Etat((string)result.Rows[0]["id"], (string)result.Rows[0]["libelle"]);
            return foundEtat;
        }

        public Etat SelectById(string idEtat)
        {
            DataRow result = this.unDbal.SelectById("etat", idEtat);
            return new Etat((string)result["id"], (string)result["libelle"]);
        }

        public List<Etat> SelectListEtat()
        {
            List<Etat> listEtat = new List<Etat>();
            string query = "id,libelle FROM Etat";
            DataTable myTable = this.unDbal.Select(query);

            foreach (DataRow r in myTable.Rows)
            {
                listEtat.Add(new Etat((string)r["id"], (string)r["libelle"]));
            }
            return listEtat;
        }
    }
}
