using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API.models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        static public List<Movie> Movies = new List<Movie>();
        SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
        IConfiguration configuration;
        string connectionString;

        public MovieController(IConfiguration iConfig)
        {
            this.configuration = iConfig;

            try
            {
                this.stringBuilder.DataSource = "no.database.here.com";
                this.stringBuilder.InitialCatalog = "Is";
                this.stringBuilder.UserID = "Wally";
                this.stringBuilder.Password = "Where";
            }
            catch (SqlException ex)
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
            SqlConnection conn = new SqlConnection(this.connectionString);
            conn.Open();

            string queryString = "SELECT * FROM MOVIE";
            SqlCommand command = new SqlCommand(queryString, conn);

            string result = "";
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result += reader[0].ToString() + reader[1].ToString() + reader[2].ToString() + "\n";

                    Movies.Add(
                        new Movie() { MovieNo = (int)reader[0], Title = reader[1].ToString(), RelYear = (int)reader[2], RunTime = (int)reader[3] });
                }
            }

            return result;
        }

        //movies with "The"
        [HttpGet("The")]
        public List<Movie> GetTheMovies()
        {
            return null;
        }

        //movies with "Luke Wilson"
        [HttpGet("Wilson")]
        public List<Movie> GetWilsonMovies()
        {
            return null;
        }

        //total running time
        [HttpGet("Runtime")]
        public int GetTotalRunTime()
        {
            return -1;
        }


        //UPDATE TASKS
        //change runtime
        [HttpPost]
        public Movie UpdateRunTime()
        {
            return null;
        }

        //change surname
        [HttpPost]
        public Actor UpdateSurname()
        {
            return null;
        }


        //CREATE TASKS
        //create movie
        [HttpPost]
        public Movie CreateMovie()
        {
            return null;
        }

        //create actor
        [HttpPost]
        public Actor CreateActor()
        {
            return null;
        }

        //cast actor
        [HttpPost]
        public Movie CastActor()
        {
            return null;
        }
    }
}
