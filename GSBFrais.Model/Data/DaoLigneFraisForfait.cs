using GSBFraisModel.Buisness;
using GSBFraisModel.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFraisModel.Data
{
    public class DaoLigneFraisForfait
    {
        private Dbal unDbal;
        private DaoVisiteurs unDaoVisiteur;
        private DaoEtat unDaoEtat;

        public DaoLigneFraisForfait(Dbal myDbal)
        {
            this.unDbal = myDbal;
        }

        public void Insert(LigneFraisForfait uneLigneFraisForfait)
        {
            string query = " ligneFraisForfait (idVisiteur, mois, idFraitForfait, quantite) VALUES ('" + uneLigneFraisForfait.IdVisiteur + "','" + uneLigneFraisForfait.Mois + "','" + uneLigneFraisForfait.IdFraisForfait + "','" + uneLigneFraisForfait.Quantite + "')";
            this.unDbal.Insert(query);
        }

        public void Update(LigneFraisForfait uneLigneFraisForfait)
        {
            string query = "lignefraisforfait SET quantite = '" + uneLigneFraisForfait.Quantite + "' "
                + "WHERE idVisiteur = '" + uneLigneFraisForfait.IdVisiteur.Replace("'", "''") + "' "
                + " AND mois = '" + uneLigneFraisForfait.Mois.Replace("'", "''") + "' "
                + " AND idFraisForfait = '" + uneLigneFraisForfait.IdFraisForfait.Replace("'", "''") + "' ";
            this.unDbal.Update(query);
        }

        public void Delete(LigneFraisForfait uneLigneFraisForfait)
        {
            string query = " lignefraisforfait WHERE idVisiteur ='" + uneLigneFraisForfait.IdVisiteur + "' AND idFraisForfait ='" + uneLigneFraisForfait.IdFraisForfait + "'";
            this.unDbal.Delete(query);
        }

        public List<LigneFraisForfait> SelectAll()
        {
            List<LigneFraisForfait> listLigneFraisForfait = new List<LigneFraisForfait>();
            DataTable myTable = this.unDbal.SelectAll("ligneFraisForfait");

            foreach (DataRow r in myTable.Rows)
            {
                listLigneFraisForfait.Add(new LigneFraisForfait((string)r["idVisiteur"], (string)r["mois"], (string)r["idFraisForfait"], (int)r["quentite"]));
            }
            return listLigneFraisForfait;
        }
        public List<LigneFraisForfait> SelectByFicheFrais(FicheFrais uneFicheFrais)
        {
            List<LigneFraisForfait> listLigneFraisForfait = new List<LigneFraisForfait>();
            DataTable myTable = this.unDbal.SelectByComposedFk2("ligneFraisForfait","idVisiteur", uneFicheFrais.UnVisiteur.Id ,"mois", uneFicheFrais.Mois);
            foreach (DataRow r in myTable.Rows)
            {
                var uneLigneFraisForfait = new LigneFraisForfait((string)r["idVisiteur"], (string)r["mois"], (string)r["idFraisForfait"], (int)r["quantite"]);
                uneLigneFraisForfait.FicheFrais = uneFicheFrais;
                listLigneFraisForfait.Add(uneLigneFraisForfait);
            }
            return listLigneFraisForfait;
        }


        //public LigneFraisForfait SelectById(string idFraisForfait)
        //{
        //    DataRow result = this.unDbal.SelectById("ligneFraisForfait", "idFraisForfait = " + idFraisForfait);
        //    return new LigneFraisForfait((string)result["idVisiteur"], (DateTime)result["mois"], (string)result["idFraisForfait"], (decimal)result["quentite"]);
        //}
    }
}
