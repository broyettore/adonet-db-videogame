using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adonet_db_videogame.Classes
{
    public static class VideogameManager
    {
        private static readonly string connectionString = "Data Source = localhost; Initial Catalog = db_videogames; Integrated Security = True";

        // gets all videogames in the database
        public static List<Videogame> GetAllVideogames()
        {
            List<Videogame> videogames = new();

            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = "SELECT id, name, overview, release_date, software_house_id FROM videogames;";

                using SqlCommand cmd = new(query, connection);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Videogame readVideogame = new(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                    videogames.Add(readVideogame);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return videogames;
        }

        // insert a game into the table
        public static bool InsertVideogame(Videogame videogameToAdd)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = "INSERT INTO videogames (name, overview, release_date, software_house_id) VALUES (@Name, @Overview, @Release_date, @software_house_id);";

                SqlCommand cmd = new(query, connection);
                cmd.Parameters.Add(new SqlParameter("@Name", videogameToAdd.Name));
                cmd.Parameters.Add(new SqlParameter("@Overview", videogameToAdd.Overview));
                cmd.Parameters.Add(new SqlParameter("@Release_date", videogameToAdd.Release_date));
                cmd.Parameters.Add(new SqlParameter("@software_house_id", videogameToAdd.Software_house_id));

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        // search game by id
        public static List<Videogame> GetVideogameById(long idToFind)
        {
            List<Videogame> videogames = new();

            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = "SELECT id, name, overview, release_date FROM videogames WHERE id=@Id;";

                using SqlCommand cmd = new(query, connection);
                cmd.Parameters.Add(new SqlParameter("@Id", idToFind));
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Videogame readVideogame = new(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                    videogames.Add(readVideogame);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return videogames;
        } 
        
        // search videogame by input string
        public static List<Videogame> GetVideogameByInput(string inputToFind)
        {
            List<Videogame> videogames = new();

            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = "SELECT id, name, overview, release_date FROM videogames WHERE name LIKE @Name;";

                using SqlCommand cmd = new(query, connection);
                cmd.Parameters.Add(new SqlParameter("@Name", "%" + inputToFind + "%"));
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Videogame readVideogame = new(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                    videogames.Add(readVideogame);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return videogames;
        }

        // deletes a videogame in the database
        public static bool DeleteVideogame(long idToDelete)
        {
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                string query = "DELETE FROM videogames WHERE id=@Id;";

                SqlCommand cmd = new(query, connection);
                cmd.Parameters.Add(new SqlParameter("@Id", idToDelete));

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        // get a new videogame Info
        public static Videogame GetVideogameInfo()
        {

            Console.Write("Insert an Id for the game: ");
            int gameId;

            while(!int.TryParse(Console.ReadLine(), out gameId) || gameId <= 0)
            {
                Console.WriteLine("Insert a valid Id");
            } 

            Console.Write("What is the game name: ");
            string gameName = Console.ReadLine();

            Console.Write("What is the game description: ");
            string gameOverview = Console.ReadLine();

            Console.Write("When was the game released: ");
            DateTime gameReleaseDate;

            while (DateTime.TryParseExact(Console.ReadLine(), "dd MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out gameReleaseDate) == false)
            {
                Console.Write("Date is written wrong: ");
            }

            Videogame gameInfo = new(gameId, gameName, gameOverview, gameReleaseDate);

            return gameInfo;
        }
    }
}
