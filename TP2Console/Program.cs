using Microsoft.EntityFrameworkCore;
using TP2Console.Models.EntityFramework;

namespace TP2Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*using (var ctx = new FilmsDbContext())
            {
                //Desactivation du tracking => Aucun changement en base fait
                ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                //Select request
                Film titanic = ctx.Films.AsNoTracking().First(f => f.Nom.Contains("Titanic"));

                //Modification de l'entité
                titanic.Description = "Un bateaau échoué. Date : " + DateTime.Now;

                //Sauvegarde du contexte => Application de la modif en BD
                int nbchanges = ctx.SaveChanges();

                Console.WriteLine("Nombre d'enregistrements modifiés ou ajoutés : " + nbchanges);
            }

            using (var ctx = new FilmsDbContext())
            {
                //Chargement de la catégorie Action
                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                ctx.Entry(categorieAction).Collection(c => c.Films).Load();
                Console.WriteLine("Films : ");
                //Chargement des films de la catégorie Action.
                foreach (var film in ctx.Films.Where(f => f.IdcategorieNavigation.Nom ==
                categorieAction.Nom).ToList())
                {
                    Console.WriteLine(film.Nom);
                }
            }

            using (var ctx = new FilmsDbContext())
            {
                //Chargement de la catégorie Action et des films de cette catégorie
                Categorie categorieAction = ctx.Categories
                .Include(c => c.Films)
                .First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                Console.WriteLine("Films : ");
                foreach (var film in categorieAction.Films)
                {
                    Console.WriteLine(film.Nom);
                }
            }*/

            /*using (var ctx = new FilmsDbContext())
            {
                //Chargement de la catégorie Action, des films de cette catégorie et des avis
                Categorie categorieAction = ctx.Categories
                .Include(c => c.Films)
                .ThenInclude(f => f.Avis)
                .First(c => c.Nom == "Action");
            }*/

            using (var ctx = new FilmsDbContext())
            {
                //Chargement de la catégorie Action
                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                Console.WriteLine("Films : ");
                //Chargement des films de la catégorie Action.
                foreach (var film in categorieAction.Films) // lazy loading initiated
                {
                    Console.WriteLine(film.Nom);
                }
            }
        }
    }
}