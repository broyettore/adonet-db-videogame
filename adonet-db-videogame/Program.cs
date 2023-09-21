using adonet_db_videogame.Classes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adonet_db_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Videogame Manager 2.0 \n");
            Console.WriteLine(@"These are the options provided by the manager:
                            - 0 : Show all videogames
                            - 1 : To Insert a new videogame
                            - 2 : To search a videogame by id
                            - 3 : To search  all videogames having an inserted input in their name
                            - 4 : To cancel videogame
                            - 5 : To close the program");
            Console.WriteLine(); // empty line

            bool condition = true;

            while (condition)
            {
                Console.Write("Insert a number to run the associated program: ");
                int choice;
                while ((int.TryParse(Console.ReadLine(), out choice)) == false)
                {
                    Console.WriteLine("Insert a number please");
                }
                Console.WriteLine(); // empty line

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Here are all the videogames in the database \n");
                        List<Videogame> Allgames = VideogameManager.GetAllVideogames();
                        foreach (var game in Allgames)
                        {
                            Console.WriteLine($"{game}\n");
                        }
                        break;

                    case 1:
                        Videogame newGame = VideogameManager.GetVideogameInfo();
                        VideogameManager.InsertVideogame(newGame);
                        break;

                    case 2:
                        Console.Write("Insert the id of the game you want to find: ");
                        int gameId;
                        while (!int.TryParse(Console.ReadLine(), out gameId) || gameId <= 0)
                        {
                            Console.WriteLine("Insert a valid number please");
                        }
                        Console.WriteLine(); // empty line

                        List<Videogame> foundGames = VideogameManager.GetVideogameById(gameId);

                        if (foundGames.Count == 0)
                        {
                            Console.WriteLine($"No games found with id {gameId}");
                        }
                        else
                        {
                            foreach (var game in foundGames)
                            {
                                Console.WriteLine($"{game}\n");
                            }
                        }
                        break;

                    case 3:
                        Console.Write("Insert the name of the game you want to find: ");
                        string gameToFind = Console.ReadLine();
                        Console.WriteLine(); // empty line

                        if (string.IsNullOrEmpty(gameToFind))
                        {
                            Console.WriteLine("Write a valid name");
                        }

                        List<Videogame> result = VideogameManager.GetVideogameByInput(gameToFind);

                        if (result.Count == 0)
                        {
                            Console.WriteLine($"No games found with name {gameToFind}");
                        }
                        else
                        {
                            foreach (var game in result)
                            {
                                Console.WriteLine($"{game}\n");
                            }
                        }
                        break;

                    case 4:
                        Console.Write("Insert the id of the game youn want to delete: ");
                        int gameToDelete = int.Parse(Console.ReadLine());
                        VideogameManager.DeleteVideogame(gameToDelete);
                        break;

                    case 5:
                        condition = false;
                        break;

                    default:
                        Console.WriteLine("You did not insert a correct number");
                        break;
                }
            }

            if (condition == false) { Console.WriteLine("Program Terminated"); }
        }
    }
}