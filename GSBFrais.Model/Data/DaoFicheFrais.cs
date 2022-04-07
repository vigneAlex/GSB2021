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
    public class DaoFicheFrais
    {
        private Dbal thedbal;
        private DaoVisiteurs unDaoVisiteur;
        private DaoEtat unDaoEtat;
        private DaoLigneFraisForfait unDaoLigneFraisForfait;
        private DaoLigneFraisHorsForfait unDaoLigneFraisHorsForfait;

        public DaoFicheFrais(Dbal mydbal, DaoVisiteurs myDaoVisiteur, DaoEtat myDaoEtat, DaoLigneFraisForfait myDaoLigneFraisForfait, DaoLigneFraisHorsForfait myDaoLigneFraisHorsForfait)
        {
            this.thedbal = mydbal;
            this.unDaoVisiteur = myDaoVisiteur;
            this.unDaoEtat = myDaoEtat;
            this.unDaoLigneFraisForfait = myDaoLigneFraisForfait;
            this.unDaoLigneFraisHorsForfait = myDaoLigneFraisHorsForfait;
        }

        public void Insert(FicheFrais theFicheFrais)
        {
            string montant = theFicheFrais.MontantValide.ToString();
            string query = "FicheFrais (idVisiteur, mois, nbJustificatifs, montantValide, dateModif, idEtat) VALUES ('" + theFicheFrais.UnVisiteur.Id + "','" + theFicheFrais.Mois + "','" + theFicheFrais.NbJustificatifs + "','" + montant.Replace(",", ".") + "','" + theFicheFrais.DateModif.ToString("yyyy-MM-dd") + "','" + theFicheFrais.UnEtat.Id + "')";
            this.thedbal.Insert(query);
        }

        public void Delete(FicheFrais theFicheFrais)
        {
            string query = "FicheFrais where idVisiteur='" + theFicheFrais.UnVisiteur.Id + "'AND mois='" + theFicheFrais.Mois + "';";
            this.thedbal.Delete(query);
        }

        public void Update(FicheFrais theFicheFrais)
        {
            string montant = theFicheFrais.MontantValide.ToString();
            string query = "FicheFrais SET nbJustificatifs= " + theFicheFrais.NbJustificatifs
                + ",montantValide = " + montant.Replace(",", ".")
                + ",dateModif = '" + theFicheFrais.DateModif.ToString("yyyy-MM-dd")
                + "',idEtat= '" + theFicheFrais.UnEtat.Id
                + "' WHERE idVisiteur = '" + theFicheFrais.UnVisiteur.Id.Replace("'", "''")
                + "' AND mois= '" + theFicheFrais.Mois.Replace("'", "''") + "'";
            this.thedbal.Update(query);
        }

        public List<FicheFrais> SelectAll()
        {
            List<FicheFrais> listVisiteur = new List<FicheFrais>();
            DataTable myTable = this.thedbal.SelectAll("FicheFrais");
            List<LigneFraisForfait> listLigneFraisForfait = new List<LigneFraisForfait>();
            List<LigneFraisHorsForfait> listLigneFraisHorsForfait = new List<LigneFraisHorsForfait>();
            foreach (DataRow r in myTable.Rows)
            {
                Visiteur unVisiteur = unDaoVisiteur.SelectById((string)r["idVisiteur"]);
                Etat unEtat = unDaoEtat.SelectById((string)r["idEtat"]);
                FicheFrais uneFicheFrais = new FicheFrais(unVisiteur, (string)r["mois"], (int)r["nbJustificatifs"], (decimal)r["montantValide"], (DateTime)r["dateModif"], unEtat);
                uneFicheFrais.LesLigneFraisForfait = unDaoLigneFraisForfait.SelectByFicheFrais(uneFicheFrais);
                uneFicheFrais.LesLigneFraisHorsForfait = unDaoLigneFraisHorsForfait.SelectByFicheFrais(uneFicheFrais);
                listVisiteur.Add(uneFicheFrais);
            }
            return listVisiteur;
        }

        public List<string> SelectListMois()
        {
            List<string> listMois = new List<string>();
            string query = "DISTINCT(mois) FROM FicheFrais ORDER BY mois DESC";
            DataTable myTable = this.thedbal.Select(query);

            foreach (DataRow r in myTable.Rows)
            {
                listMois.Add((string)r["Mois"]);
            }
            return listMois;
        }

        public List<FicheFrais> SelectByMois(string unMois)
        {
            List<FicheFrais> listFicheFrais = new List<FicheFrais>();
            DataTable myTable = this.thedbal.SelectByField("FicheFrais", "mois = '" + unMois + "'");
            List<LigneFraisForfait> lesLignesFraisForfaits = new List<LigneFraisForfait>();
            List<LigneFraisHorsForfait> lesLignesFraisHorsForfaits = new List<LigneFraisHorsForfait>();

            foreach (DataRow r in myTable.Rows)
            {
                Visiteur leVisiteur = unDaoVisiteur.SelectById((string)r["idVisiteur"]);
                Etat unEtat = unDaoEtat.SelectById((string)r["idEtat"]);
                FicheFrais uneFicheFrais = new FicheFrais(leVisiteur, unMois, (int)r["nbJustificatifs"], (decimal)r["montantValide"], (DateTime)r["dateModif"], unEtat);
                lesLignesFraisForfaits = unDaoLigneFraisForfait.SelectByFicheFrais(uneFicheFrais);
                lesLignesFraisHorsForfaits = unDaoLigneFraisHorsForfait.SelectByFicheFrais(uneFicheFrais);
                uneFicheFrais.LesLigneFraisForfait = lesLignesFraisForfaits;
                uneFicheFrais.LesLigneFraisHorsForfait = lesLignesFraisHorsForfaits;
                listFicheFrais.Add(uneFicheFrais);
            }
            return listFicheFrais;
        }

        public FicheFrais SelectById(string idVisiteur, string mois)
        {
            DataRow result = this.thedbal.SelectById2("FicheFrais", idVisiteur, mois);
            Visiteur unVisiteur = unDaoVisiteur.SelectById(idVisiteur);
            Etat unEtat = unDaoEtat.SelectById((string)result["idEtat"]);
            return new FicheFrais(unVisiteur, (string)result["mois"], (int)result["nbJustificatifs"], (decimal)result["montantValide"], (DateTime)result["dateModif"], unEtat);

            
        }

        public List<FicheFrais> SelectByNom(string unId)
        {
            List<FicheFrais> listFicheFrais = new List<FicheFrais>();
            DataTable myTable = this.thedbal.SelectByField("FicheFrais", "idVisiteur = '" + unId + "'");
            List<LigneFraisForfait> lesLignesFraisForfaits = new List<LigneFraisForfait>();
            List<LigneFraisHorsForfait> lesLignesFraisHorsForfaits = new List<LigneFraisHorsForfait>();

            foreach (DataRow r in myTable.Rows)
            {
                Visiteur leVisiteur = unDaoVisiteur.SelectById(unId);
                Etat unEtat = unDaoEtat.SelectById((string)r["idEtat"]);
                FicheFrais uneFicheFrais = new FicheFrais(leVisiteur, unId, (int)r["nbJustificatifs"], (decimal)r["montantValide"], (DateTime)r["dateModif"], unEtat);
                lesLignesFraisForfaits = unDaoLigneFraisForfait.SelectByFicheFrais(uneFicheFrais);
                lesLignesFraisHorsForfaits = unDaoLigneFraisHorsForfait.SelectByFicheFrais(uneFicheFrais);
                uneFicheFrais.LesLigneFraisForfait = lesLignesFraisForfaits;
                uneFicheFrais.LesLigneFraisHorsForfait = lesLignesFraisHorsForfaits;
                listFicheFrais.Add(uneFicheFrais);
            }
            return listFicheFrais;
        }
    }

}
