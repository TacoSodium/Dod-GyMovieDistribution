using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API.models;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieDistributionController : ControllerBase
    {
        static public List<Movie> Movies = new List<Movie>();
        SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
        IConfiguration configuration;
        string connectionString;

        public MovieDistributionController(IConfiguration iConfig)
        {
            this.configuration = iConfig;

            try
            {
                this.stringBuilder.DataSource = "no.database.here.com";
                this.stringBuilder.InitialCatalog = "Is";
                this.stringBuilder.UserID = "Wally";
                this.stringBuilder.Password = "Where";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.stringBuilder.DataSource = this.configuration.GetSection("DBConnectionString").GetSection("Url").Value;
                this.stringBuilder.InitialCatalog = this.configuration.GetSection("DBConnectionString").GetSection("Database").Value;
                this.stringBuilder.UserID = this.configuration.GetSection("DBConnectionString").GetSection("User").Value;
                this.stringBuilder.Password = this.configuration.GetSection("DBConnectionString").GetSection("Password").Value;

                this.connectionString = stringBuilder.ConnectionString;
            }
        }

        //READ TASKS
        //gets movies
        [HttpGet]
        public string GetMovies()
        {
            Movies.Clear();

            SqlConnection conn = new SqlConnection(this.connectionString);
            conn.Open();

            string queryString = "SELECT * FROM MOVIE";
            SqlCommand command = new SqlCommand(queryString, conn);

            string result = "";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result += reader[0].ToString() + " " + reader[1].ToString() + " | Year: " + reader[2].ToString() + " Runtime: " + reader[3].ToString() + "\n";

                    Movies.Add(
                        new Movie() { MovieNo = (int)reader[0], Title = reader[1].ToString(), RelYear = (int)reader[2], RunTime = (int)reader[3] });
                }
            }

            conn.Close();

            return result;
        }

        //movies with "The"
        [HttpGet("TheMovies")]
        public string GetTheMovies()
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            conn.Open();

            string queryString = "SELECT * FROM MOVIE WHERE TITLE LIKE 'The %'";
            SqlCommand command = new SqlCommand(queryString, conn);

            string result = "";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result += reader[0].ToString() + " " + reader[1].ToString() + " | Year: " + reader[2].ToString() + " Runtime: " + reader[3].ToString() + "\n";
                }
            }

            conn.Close();

            return result;
        }

        //movies with "Luke Wilson" 36422
        [HttpGet("LukeWilson")]
        public string GetWilsonMovies()
        {
            List<int> casts = new List<int>();

            this.GetMovies();

            SqlConnection conn = new SqlConnection(this.connectionString);
            conn.Open();

            string queryString = "SELECT * FROM CASTING WHERE ACTORNO = 36422";
            SqlCommand command = new SqlCommand(queryString, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    casts.Add((int)reader[2]);
                }
            }

            string wilsonsMovies = "";

            foreach (int movieno in casts)
            {
                Movie casted = Movies.Find(m => m.MovieNo == movieno);

                if (casted != null)
                {
                    wilsonsMovies += casted.Title + "\n";
                }
            }

            conn.Close();

            return wilsonsMovies;
        }

        //total running time
        [HttpGet("TotalRuntime")]
        public int GetTotalRunTime()
        {
            int totalRuntime = 0;

            this.GetMovies();

            foreach (Movie movie in Movies)
            {
                totalRuntime += movie.RunTime;
            }

            return totalRuntime;
        }


        //UPDATE TASKS
        //change runtime
        [HttpPost("UpdateRuntime")]
        public string UpdateRunTime([FromBody] UpdateRuntimeRequest req)
        {

            SqlConnection conn = new SqlConnection(this.connectionString);
            conn.Open();

            try
            {
                string queryString = "UPDATE MOVIE SET RUNTIME = @NEWRUNTIME WHERE TITLE = @MOVIETITLE";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@NEWRUNTIME", req.Title);
                command.Parameters.AddWithValue("@MOVIETITLE", req.NewRunTime);

                var result = command.ExecuteNonQuery();
                return result.ToString();
            }
            catch (SqlException ex)
            {
                return "Something went wrong.\n" + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        //change surname
        [HttpPost("UpdateSurname")]
        public string UpdateSurname([FromBody] UpdateSurnameRequest req)
        {
            string oldFullname = this.MakeFullname(req.Givenname, req.Surname);
            string newFullname = this.MakeFullname(req.Givenname, req.NewSurname);
            SqlConnection conn = new SqlConnection(this.connectionString);
            conn.Open();

            try
            {
                string queryString = "UPDATE ACTOR SET FULLNAME = @NEWFULLNAME, SURNAME = @NEWSURNAME WHERE FULLNAME = @OLDFULLNAME";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@NEWFULLNAME", newFullname);
                command.Parameters.AddWithValue("@NEWSURNAME", req.NewSurname);
                command.Parameters.AddWithValue("@OLDFULLNAME", oldFullname);

                var result = command.ExecuteNonQuery();
                return result.ToString();
            }
            catch (SqlException ex)
            {
                return "Something went wrong.\n" + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }


        //CREATE TASKS
        //create movie
        [HttpPost("NewMovie")]
        public Movie CreateMovie()
        {
            return null;
        }

        //create actor
        [HttpPost("NewActor")]
        public Actor CreateActor()
        {
            return null;
        }

        //cast actor
        [HttpPost("CastActor")]
        public Movie CastActor()
        {
            return null;
        }

        public string MakeFullname(string givennname, string surname)
        {
            string fullname = givennname + " " + surname;
            return fullname;
        }
    }
}
