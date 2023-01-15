using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Gestionale_Albergo.Models
{
    public class Servizi
    {
        public int IdServizio { get; set; }

        public string Servizio { get; set; }

        [Display(Name = "Data Richiesta")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Display(Name = "Quantità")]
        public int Quantita { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Prezzo { get; set; }

        [Display(Name = "Nr Pren.")]
        public int IdPrenotazione { get; set; }

        [Display(Name = "Nr Camera")]
        public int NrCamera { get; set; }
        public string Cliente { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Tot_Service { get; set; }

        public decimal TotServices(int id )
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();

            Servizi c = new Servizi();

            try
            {
                SqlCommand com = Connessione.GetCommand("SELECT SUM(PrezzoTot) AS TotServices, IdPrenotazione FROM SERVIZIO group by " +
                    "IdPrenotazione having IdPrenotazione = @IdPrenotazione", sql);
                com.Parameters.AddWithValue("IdPrenotazione", id);
               
                SqlDataReader reader = com.ExecuteReader();
               
                while (reader.Read())
                {
                    c.Tot_Service = Convert.ToDecimal(reader["TotServices"]);
                }
            }
            catch 
            {

            }
            finally { sql.Close(); }

            return c.Tot_Service;
        }

    }
}