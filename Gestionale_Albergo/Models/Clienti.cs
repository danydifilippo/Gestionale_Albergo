using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Gestionale_Albergo.Models
{
    public class Clienti
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        [Display(Name = "Codice Fiscale")]
        public string CF { get; set;}

        [Display(Name = "Città")]
        public string Citta { get; set; }
        public string Prov { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "Nr Pren.")]
        public int IdPrenotazione { get; set; }

        public static List<SelectListItem> ListaClienti
        {
            get
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                SqlConnection sql = Connessione.GetConnection();
                sql.Open();
                SqlCommand com = Connessione.GetCommand("SELECT * FROM CLIENTI", sql);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    SelectListItem s = new SelectListItem
                    {
                        Text = reader["Cognome"].ToString() + " "+ reader["Nome"].ToString(),
                        Value = reader["IdCliente"].ToString()
                    };

                    selectListItems.Add(s);
                }
                return selectListItems;
            }

        }
    }
}