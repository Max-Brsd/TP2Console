using Microsoft.EntityFrameworkCore;
using Npgsql;
using TP2Console.Models.EntityFramework;

namespace TP2Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Exo3Q5();
        }

        public static void Exo2Q1()
        {
            var ctx = new FilmsDbContext();
            foreach (var film in ctx.Films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        public static void Exo2Q1Bis()
        {
            var ctx = new FilmsDbContext();
            var films = ctx.Films.FromSqlRaw("SELECT * FROM film");
            foreach (var film in films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        public static void Exo2Q2()
        {
            var ctx = new FilmsDbContext();
            foreach (var utilisateur in ctx.Utilisateurs)
            {
                Console.WriteLine("Utilisateur : " + utilisateur.Idutilisateur + " email : " + utilisateur.Email);
            }
        }

        public static void Exo2Q3()
        {
            var ctx = new FilmsDbContext();
            var orderedUtilisateurs = ctx.Utilisateurs
                                 .OrderBy(u => u.Login)
                                 .ToList();
            foreach (var utilisateur in orderedUtilisateurs)
            {
                Console.WriteLine("Utilisateur : " + utilisateur.Idutilisateur + " login : " + utilisateur.Login);
            }
        }

        public static void Exo2Q4()
        {
            var ctx = new FilmsDbContext();
            Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
            Console.WriteLine("Categorie : " + categorieAction.Nom);
            ctx.Entry(categorieAction).Collection(c => c.Films).Load();
            foreach (var film in categorieAction.Films)
            {
                Console.WriteLine("Id : " + film.Idfilm + " nom : " + film.Nom);
            }
        }
        public static void Exo2Q5()
        {
            var ctx = new FilmsDbContext();
            Console.WriteLine("Nombre de catégories : " + ctx.Categories.Count());
        }

        public static void Exo2Q6()
        {
            var ctx = new FilmsDbContext();
            Avi lowestAvi = ctx.Avis.OrderBy(a => a.Note).First();
            Console.WriteLine("Note la plus basse : " + lowestAvi.Note);
        }

        public static void Exo2Q7()
        {
            var ctx = new FilmsDbContext();
            var films = ctx.Films.Where(f => EF.Functions.Like(f.Nom.ToLower(), "le%".ToLower()));
            foreach (var film in films)
            {
                Console.WriteLine(film.Nom);
            }
        }

        public static void Exo2Q8()
        {
            var ctx = new FilmsDbContext();
            Film pulpFiction = ctx.Films.First(c => c.Nom.ToLower() == "Pulp Fiction".ToLower());
            ctx.Entry(pulpFiction).Collection(f => f.Avis).Load();
            Console.WriteLine("Note moyenne de Pupl fiction : " + pulpFiction.Avis.Average(a => a.Note));
        }

        public static void Exo2Q9()
        {
            var ctx = new FilmsDbContext();
            Avi bestAvi = ctx.Avis.OrderBy(a => a.Note).Last();
            Utilisateur utilisateur = ctx.Utilisateurs.First(u => u.Idutilisateur == bestAvi.Idutilisateur);
            Console.WriteLine("Utilisateur qui a donné la meilleure note : " + utilisateur.Login + " ( id : " + utilisateur.Idutilisateur + ", note : " + bestAvi.Note + ")");
        }

        // Ajoutez-vous en tant qu’utilisateur
        public static void Exo3Q1()
        {
            Utilisateur newUtilisateur = new Utilisateur();
            newUtilisateur.Email = "theo.clere@cpe.fr";
            newUtilisateur.Login = "tclere";
            newUtilisateur.Pwd = "tclere123";

            Console.WriteLine("Ajout de l'utilisateur : " + newUtilisateur.Login);
            var ctx = new FilmsDbContext();
            ctx.Utilisateurs.Add(newUtilisateur);
            ctx.SaveChanges();
        }

        // Modifier un film
        public static void Exo3Q2()
        {
            var ctx = new FilmsDbContext();
            Film f = ctx.Films.First(film => film.Nom.ToLower() == "L'armee des douze singes".ToLower());
            f.Description = "blabla description";

            Categorie categorie = ctx.Categories.First(c => c.Nom.ToLower() == "Drame".ToLower());
            f.IdcategorieNavigation = categorie;

            ctx.SaveChanges();
        }

        // Supprimer un film
        public static void Exo3Q3()
        {
            var ctx = new FilmsDbContext();
            Film f = ctx.Films.First(film => film.Nom.ToLower() == "L'armee des douze singes".ToLower());
            ctx.Entry(f).Collection(film => film.Avis).Load();

            ctx.Avis.RemoveRange(f.Avis);
            ctx.Films.Remove(f);

            ctx.SaveChanges();
        }

        // Ajouter un avi
        public static void Exo3Q4()
        {
            var ctx = new FilmsDbContext();
            Film gladiator = ctx.Films.First(f => f.Nom.ToLower() == "Gladiator".ToLower());
            Utilisateur moi = ctx.Utilisateurs.First(u => u.Login == "tclere");

            Avi monAvi = new Avi();
            monAvi.Commentaire = "un peu surcoté";
            monAvi.Note = 2;
            monAvi.Idutilisateur = moi.Idutilisateur;
            monAvi.Idfilm = gladiator.Idfilm;
            
            ctx.Avis.Add(monAvi);
            ctx.SaveChanges(); 
        }

        // Ajouter deux film
        public static void Exo3Q5()
        {
            var ctx = new FilmsDbContext();
            Categorie drama = ctx.Categories.First(c => c.Nom.ToLower() == "Drame".ToLower());

            Film film1 = new Film();
            film1.Nom = "film1";
            film1.Idcategorie = drama.Idcategorie;

            Film film2 = new Film();
            film2.Nom = "film2";
            film2.Idcategorie = film1.Idcategorie;

            ctx.Films.AddRange(new List<Film> { film1, film2 });
            ctx.SaveChanges();
        }
    }
}