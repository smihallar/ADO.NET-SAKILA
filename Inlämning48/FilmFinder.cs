using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämning48
{
    internal class FilmFinder
    {
        public void StartFilmFinder(SqlConnection connection)
        {
            GetActorName(connection);
        }

        private void GetActorName(SqlConnection connection)
        {
            Console.WriteLine("Skriv in förnamn på skådespelaren:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Skriv in efternamn på skådespelaren:");
            string lastName = Console.ReadLine();

            FindActorID(firstName, lastName, connection);
        }

        private void FindActorID(string firstName, string lastName, SqlConnection connection)
        {
            SqlCommand actorIdCommand = new SqlCommand($"SELECT actor_id FROM actor WHERE first_name = '{firstName}' AND last_name = '{lastName}'", connection);
            SqlDataReader actorIDRec = actorIdCommand.ExecuteReader();

            if (actorIDRec.Read())
            {
                int actorId = actorIDRec.GetInt32(0);
                actorIDRec.Close();
                FindFilms(actorId, connection);
            }
            else
            {
                Console.WriteLine("Skådespelaren kunde inte hittas.");
            }
            
        }

        private void FindFilms(int actorId, SqlConnection connection)
        {
            List<int> filmIDs = new List<int>();

            SqlCommand filmIDCommand = new SqlCommand("SELECT film_id FROM film_actor WHERE actor_id =" + actorId, connection);

            SqlDataReader filmIDRec = filmIDCommand.ExecuteReader();


            if (filmIDRec.HasRows)
            {
                while (filmIDRec.Read())
                {
                    filmIDs.Add(filmIDRec.GetInt32(0));
                }
                filmIDRec.Close();
            }
            else
            {
                Console.WriteLine("Inga filmer hittades.");

                return;
            }

            foreach (int filmID in filmIDs)
            {
                FindFilmTitles(filmID, connection);
            }

        }

        private void FindFilmTitles(int filmID, SqlConnection connection)
        {

            SqlCommand filmTitleCommand = new SqlCommand("SELECT title FROM film WHERE film_id =" + filmID, connection);
            SqlDataReader filmTitleRec = filmTitleCommand.ExecuteReader();

            if (filmTitleRec.Read())
            {
                string filmTitle = filmTitleRec.GetString(0);
                PrintFilmTitles(filmTitle);
            }
            else
            {
                Console.WriteLine("Kunde inte hitta filmen.");
            }
            filmTitleRec.Close();

        }
        private void PrintFilmTitles(string filmTitle)
        {
            Console.WriteLine(filmTitle);
        }
    }
}