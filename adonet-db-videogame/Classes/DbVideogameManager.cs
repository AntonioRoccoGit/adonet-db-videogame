using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adonet_db_videogame.Classes
{
    public static class DbVideogameManager
    {
        private const string CONNECTION_SETTINGS = "Data Source=localhost;Initial Catalog=db-videogames;Integrated Security=True";
        private const string VIDEOGAME_TABLE = "videogames";


        public static bool CreateVideogame(Videogame newVideogame)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_SETTINGS))
            {
                try
                {

                connection.Open();

                string query = $"INSERT INTO {VIDEOGAME_TABLE} (name, overview, release_date, software_house_id) VALUES (@Name, @Overview, @ReleaseDate, @SoftwareHouseId)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add(new SqlParameter("@Name", newVideogame.Name));
                cmd.Parameters.Add(new SqlParameter("@Overview", newVideogame.Overview));
                cmd.Parameters.Add(new SqlParameter("@ReleaseDate", newVideogame.ReleaseDate));
                cmd.Parameters.Add(new SqlParameter("@SoftwareHouseId", newVideogame.SoftwareHouseId));
                bool rowsAffected = cmd.ExecuteNonQuery() > 0;
                if(!rowsAffected)
                    return false;
                } 
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                
                return true;


            }
        }

        public static Videogame GetVideoGameById(long Id)
        {
            
            using (SqlConnection connection = new SqlConnection(CONNECTION_SETTINGS))
            {
                try
                {

                    connection.Open();


                    string query = $"SELECT v.id, v.name, v.overview, v.release_date, softerH.id " +
                                   $"FROM {VIDEOGAME_TABLE} v " +
                                   $"JOIN software_houses softerH ON softerH.id = v.software_house_id " +
                                   $"WHERE v.id=@Id;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        using SqlDataReader data = cmd.ExecuteReader();
                        while (data.Read()) 
                        {
                            Videogame videogameFounded = new Videogame(data.GetInt64(0), data.GetString(1), data.GetString(2), data.GetDateTime(3), data.GetInt64(4));
                            return videogameFounded;
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            throw new Exception("Nessun videogame trovato");
        }

        public static List<Videogame> GetVideoGameByName(string name)
        {
            List<Videogame> videogames = new List<Videogame> ();
            using (SqlConnection connection = new SqlConnection(CONNECTION_SETTINGS))
            {
                try
                {

                    connection.Open();


                    string query = $"SELECT v.id, v.name, v.overview, v.release_date, softerH.id " +
                                   $"FROM {VIDEOGAME_TABLE} v " +
                                   $"JOIN software_houses softerH ON softerH.id = v.software_house_id " +
                                   $"WHERE v.name LIKE(@Name);";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Name", "%" +  name + "%"));
                        using SqlDataReader data = cmd.ExecuteReader();
                        while (data.Read())
                        {
                            Videogame videogameFounded = new Videogame(data.GetInt64(0), data.GetString(1), data.GetString(2), data.GetDateTime(3), data.GetInt64(4));
                            videogames.Add(videogameFounded);
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            return videogames;
        }

        public static bool DeleteVideogame(long idToDelete)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_SETTINGS))
            {

                try
                {
                    connection.Open();

                    string query = $"DELETE FROM {VIDEOGAME_TABLE} WHERE id=@Id";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.Add(new SqlParameter("@Id", idToDelete));


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return false;

            }

        }
   }
}
