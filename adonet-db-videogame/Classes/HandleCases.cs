using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame.Classes
{
    static public class HandleCases
    {
        static public void AddVideoGameCase()
        {
            Console.WriteLine("\nAggiungere un nome: ");
            string name = Console.ReadLine();
            Console.WriteLine("\nAggiungere una descrizione: ");
            string overview = Console.ReadLine();
            DateTime release = DateTime.Now;


            Videogame videogame = new Videogame(name, overview, release);
            bool isCreated = DbVideogameManager.CreateVideogame(videogame);
            if (!isCreated)
                Console.WriteLine("\nC'è stato un problema nella creazione del videogame");

            Console.WriteLine("\nVideogame creato con successo");
        }

        static public void SearchByIdCase()
        {
            Console.WriteLine("\nScegliere l'ID del gioco da cercare: ");
            long videoGameId = long.Parse(Console.ReadLine());
            try
            {
                Console.WriteLine(DbVideogameManager.GetVideoGameById(videoGameId).ToString());

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static public void SearchByTitleCase()
        {
            Console.WriteLine("\nScegliere il nome del gioco da cercare: ");
            string name = Console.ReadLine();
            List<Videogame> videogames = DbVideogameManager.GetVideoGameByName(name);

            if (videogames.Count() > 0)
            {
                foreach (var item in videogames)
                {
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("--------------------------------------------");
                }
                Console.WriteLine($"{Environment.NewLine}{videogames.Count()} risultati trovati{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine("Nessuna Corrispondenza");
            }
        }

        static public void DeleteByIdCase()
        {
            Console.WriteLine("\nScegliere l'ID del gioco da eliminare: ");
            long videoGameId = long.Parse(Console.ReadLine());
            if (DbVideogameManager.DeleteVideogame(videoGameId))
                Console.WriteLine("Eliminato con successo");
            else
                Console.WriteLine("Nessuna corrispondenza trovata");
        }
    }
}
