using CrudImpiegati.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace CrudImpiegati.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //("data source=ACADEMYNETPD07\\SQLEXPRESS;" +"database = impiegati;" +"Integrated Security = true;" +"Connection timeout = 3600;")
            string connectionString="Data Source=LAPTOP-6U512VIE\\SQLEXPRESS; Initial Catalog = Impiegati;Integrated Security = true;Connection timeout = 3600;";

            //string connectionString = @"Server=LAPTOP-6U512VIE\SQLEXPRESS;Database=Impiegati;Trusted_Connection=True; Integrated Security = true;Connection timeout = 3600";
            List<ImpiegatoViewModel> impiegatoList = new List<ImpiegatoViewModel>();
            string sql = @"select * from impiegato";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var impiegato = new ImpiegatoViewModel

                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Nome = reader["Nome"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    Salario = Convert.ToDecimal(reader["Salario"]),
                    Citta = reader["Citta"].ToString()
                };
                impiegatoList.Add(impiegato);
               
            }
            return View(impiegatoList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}