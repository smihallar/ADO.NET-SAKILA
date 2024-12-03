using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
namespace Inlämning48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Sakila; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
            connection.Open();
            
            FilmFinder actorFinder = new FilmFinder();
            actorFinder.StartFilmFinder(connection);
            
            connection.Close();
        }
    }
}
