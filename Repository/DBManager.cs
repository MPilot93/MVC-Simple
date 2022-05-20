using CrudImpiegati.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace CrudImpiegati.Repository
{
    public class DBManager
    {

        private static string connectionString = "Data Source=LAPTOP-6U512VIE\\SQLEXPRESS; Initial Catalog = Impiegati;Integrated Security = true;Connection timeout = 3600;";
        public List<ImpiegatoViewModel> GetAllImpiegati()
        {
            List<ImpiegatoViewModel> impiegatoList = new List<ImpiegatoViewModel>();
            string sql = @"select * from Impiegati.dbo.Impiegato";
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
                    Salario = Decimal.Parse(reader["Salario"].ToString()),
                    Citta = reader["Città"].ToString()
                };
                impiegatoList.Add(impiegato);

            }
            return impiegatoList;
        }

        public int EditImpiegato(ImpiegatoViewModel impiegato)
        {
            string sql = @"update [Impiegati].[dbo].[Impiegato] 
                        set [Nome] = @Nome,
                            [Cognome]=@Cognome,
                            [Salario]=@Salario,
                            [Città]=@Città
                        where ID= @id";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Nome", impiegato.Nome);
            command.Parameters.AddWithValue("@id", impiegato.ID);
            command.Parameters.AddWithValue("@Cognome", impiegato.Cognome);
            command.Parameters.AddWithValue("@Salario", impiegato.Salario);
            command.Parameters.AddWithValue("@Città", impiegato.Citta);
            return command.ExecuteNonQuery();
        }

        public int CancellaImpiegato(ImpiegatoViewModel impiegato)
        {

            string sql = @"delete from [Impiegati].[dbo].[Impiegato] 
                        where ID=@id";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
          
            command.Parameters.AddWithValue("@id", impiegato.ID);

            return command.ExecuteNonQuery();
        }

        public int AggiungiImpiegato(ImpiegatoViewModel impiegato)
        {
            string sql = @"insert into [Impiegati].[dbo].[Impiegato] 
                         ([Nome],[Cognome],[Salario],[Città])
                         values (@Nome,@Cognome,@Salario,@Città)";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Nome", impiegato.Nome);
            command.Parameters.AddWithValue("@Cognome", impiegato.Cognome);
            command.Parameters.AddWithValue("@Salario", impiegato.Salario);
            command.Parameters.AddWithValue("@Città", impiegato.Citta);
            return command.ExecuteNonQuery();
        }
    }
    }

