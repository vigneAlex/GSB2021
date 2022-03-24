using System.Collections.Generic;
using GSBFraisModel.Buisness;
using System.Data;
using GSBFraisModel.Data;

namespace GSBFraisModel.Data
{
    public class DaoFraisForfait
    {

        private Dbal unDbal;

        public DaoFraisForfait(Dbal myDbal)
        {
            this.unDbal = myDbal;
        }

        public void Insert(FraisForfait unFraisForfait)
        {
            string query = " fraisforfait (id, libelle, montant) VALUES ('" + unFraisForfait.Id + "','" + unFraisForfait.Libelle.Replace("'", "''") + "','" + unFraisForfait.Montant + "')";
            this.unDbal.Insert(query);
        }

        public void Update(FraisForfait unFraisForfait)
        {
            string query = " fraisforfait (id, libelle, montant) SET '" + unFraisForfait.Libelle.Replace("'", "''") + "','" + unFraisForfait.Montant + "'";
            this.unDbal.Update(query);
        }

        public void Delete(FraisForfait unFraisForfait)
        {
            string query = " fraisforfait WHERE id ='" + unFraisForfait.Id + "'";
            this.unDbal.Delete(query);
        }

        public List<FraisForfait> SelectAll()
        {
            List<FraisForfait> listVisiteurs = new List<FraisForfait>();
            DataTable myTable = this.unDbal.SelectAll("fraisforfait");

            foreach (DataRow r in myTable.Rows)
            {
                listVisiteurs.Add(new FraisForfait((string)r["id"], (string)r["libelle"], (decimal)r["montant"]));
            }
            return listVisiteurs;
        }

        public FraisForfait SelectByName(string nameFraisForfait)
        {
            DataTable result = new DataTable();
            result = this.unDbal.SelectByField("fraisforfait", "libelle = '" + nameFraisForfait.Replace("'", "''") + "'");
            FraisForfait foundFraisForfait = new FraisForfait((string)result.Rows[0]["id"], (string)result.Rows[0]["libelle"], (decimal)result.Rows[0]["montant"]);
            return foundFraisForfait;
        }

        public FraisForfait SelectById(string idFraisForfais)
        {
            DataRow result = this.unDbal.SelectById("fraisforfait", idFraisForfais);
            return new FraisForfait((string)result["id"], (string)result["libelle"], (decimal)result["montant"]);
        }
        /*public FraisForfait SelectByFicheFrais(FicheFrais uneFicheFrais)
        {
             
            DataRow result = this.unDbal.SelectByComposedFk2("fraisforfait", idFraisForfais);
            return new FraisForfait((string)result["id"], (string)result["libelle"], (decimal)result["montant"]);
        }*/
    }
}
