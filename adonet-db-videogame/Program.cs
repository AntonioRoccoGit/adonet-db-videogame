using adonet_db_videogame.Classes;

namespace adonet_db_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int userChoice = 1;

            Console.WriteLine("Gestore Videogame");
            do
            {
                Console.WriteLine("\nSeleziona un operazione: ");
                Console.WriteLine("-1 Aggiungi un videogame\n-2 Cerca videogame per ID\n-3 Cerca videogame per Titolo\n-4 Elimina un videogioco\n-5 Chiudi il programma");

                userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 1:
                        HandleCases.AddVideoGameCase();
                        break;

                    case 2:
                        HandleCases.SearchByIdCase();
                        break;

                    case 3:

                        HandleCases.SearchByTitleCase();
                        break;
                    case 4:
                        
                        HandleCases.DeleteByIdCase();
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