using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GSBFraisModel.Data;
using GSBFraisModel.Buisness;

namespace ConsoleTest
{
    class Program

    {
        static void Main(string[] args)
        {
            
            Dbal test = new Dbal();
            DaoVisiteurs unDaoVisiteur = new DaoVisiteurs(test);
            DaoEtat unDaoEtat = new DaoEtat(test);
            DaoLigneFraisForfait unDaoLigneFraisForfait = new DaoLigneFraisForfait(test);
            DaoLigneFraisHorsForfait unDaoLigneFraisHorsForfait = new DaoLigneFraisHorsForfait(test);
            DaoFicheFrais unDaoFicheFrais = new DaoFicheFrais(test, unDaoVisiteur, unDaoEtat, unDaoLigneFraisForfait, unDaoLigneFraisHorsForfait);
            List<string> uneListMois = unDaoFicheFrais.SelectListMois();

            foreach(string r in uneListMois)
            {
                Console.WriteLine(r);
            }
            Console.Read();
            //test.Insert("visiteur VALUES ('a222','Vigne', 'Alex', 'avigne', 'azerty', '7 rue du test', '74000', 'testville', '2021-12-3' )");

            //test.Update("visiteur SET nom = 'theoLaMerde' WHERE id = 'a222' ");

            //test.Delete("visiteur WHERE id = 'a222'");

            //DataTable myTable = new DataTable();
            //myTable = test.SelectAll("visiteur");
            //Visiteur unVisiteur = unDaoVisiteur.SelectById("a222");
            //Console.WriteLine(unVisiteur.Adresse);


        }
    }
}
