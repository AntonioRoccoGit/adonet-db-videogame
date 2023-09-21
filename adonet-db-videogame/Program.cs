using adonet_db_videogame.Classes;

namespace adonet_db_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int userChoice = 1;
            string videoGameName = "";
            string videoGameOverview = "";
            long videoGameId;
            DateTime videoGamenReleaseDate;

            Console.WriteLine("Gestore Videogame");
            do
            {
                Console.WriteLine("\nSeleziona un operazione: ");
                Console.WriteLine("-1 Aggiungi un videogame\n-2 Cerca videogame per ID\n-3 Cerca videogame per Titolo\n-4 Elimina un videogioco\n-5 Chiudi il programma");

                userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("\nAggiungere un nome: ");
                        videoGameName = Console.ReadLine();
                        Console.WriteLine("\nAggiungere una descrizione: ");
                        videoGameOverview = Console.ReadLine();
                        videoGamenReleaseDate = DateTime.Now;


                        Videogame videogame = new Videogame(videoGameName, videoGameOverview, videoGamenReleaseDate);
                        bool isCreated = DbVideogameManager.CreateVideogame(videogame);
                        if (!isCreated)
                            Console.WriteLine("\nC'è stato un problema nella creazione del videogame");

                        Console.WriteLine("\nVideogame creato con successo");

                        break;

                    case 2:
                        Console.WriteLine("\nScegliere l'ID del gioco da cercare: ");
                        videoGameId = long.Parse(Console.ReadLine());
                        
                        Console.WriteLine(DbVideogameManager.GetVideoGameById(videoGameId)?.ToString() ?? "404 NOT FOUND :(");

                        break;

                    case 3:
                        Console.WriteLine("\nScegliere il nome del gioco da cercare: ");
                        videoGameName = Console.ReadLine();
                        List<Videogame> videogames = DbVideogameManager.GetVideoGameByName(videoGameName);

                        if(videogames.Count() > 0)
                        {
                            foreach (var item in videogames)
                            {
                                Console.WriteLine("--------------------------------------------");
                                Console.WriteLine(item?.ToString() ?? "404 NOT FOUND :(");
                                Console.WriteLine("--------------------------------------------");
                            }
                            Console.WriteLine($"{Environment.NewLine}{videogames.Count()} risultati trovati{Environment.NewLine}");
                        }
                        else
                        {
                            Console.WriteLine("Nessuna Corrispondenza");
                        }

                        break;
                    case 4:
                        Console.WriteLine("\nScegliere l'ID del gioco da eliminare: ");
                        videoGameId = long.Parse(Console.ReadLine());
                        if (DbVideogameManager.DeleteVideogame(videoGameId))
                            Console.WriteLine("Eliminato con successo");
                        else
                            Console.WriteLine("Nessuna corrispondenza trovata");

                        break;
                    default:
                        userChoice = 5;
                        break;
                }


            }
            while (userChoice != 5);
        }
    }
}