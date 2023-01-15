using Gestionale_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestionale_Albergo.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult ListaCk()
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();
            List<Prenotazioni> Bookings = new List<Prenotazioni>();

            try
            {
                SqlCommand com = Connessione.GetStoreProcedure("GetBookings", sql);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Prenotazioni p = new Prenotazioni
                    {
                        IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"]),
                        IdCliente = Convert.ToInt32(reader["IdCliente"]),
                        Cliente = reader["Cognome"].ToString() + " " + reader["Nome"].ToString(),
                        NrCamera = Convert.ToInt32(reader["NrCamera"]),
                        DataPren = Convert.ToDateTime(reader["DataPrenotazione"]),
                        CheckIn = Convert.ToDateTime(reader["DataArrivo"]),
                        CheckOut = Convert.ToDateTime(reader["DataUscita"]),
                        Acconto = Convert.ToDecimal(reader["Acconto"]),
                        Prezzo = Convert.ToDecimal(reader["PrezzoSoggiorno"]),
                        Pensione = reader["TipoPensione"].ToString(),
                        Tot = Prenotazioni.TotServizi(Convert.ToInt32(reader["IdPrenotazione"])),
                        Saldo = Prenotazioni.DaSaldare(Convert.ToDecimal(reader["PrezzoSoggiorno"]), Convert.ToDecimal(reader["Acconto"]), Prenotazioni.TotServizi(Convert.ToInt32(reader["IdPrenotazione"])))
                    };
                    Bookings.Add(p);

                }
            }
            catch (Exception ex)
            {
                ViewBag.msgerror = ex.Message;
            }
            finally { sql.Close(); }

            return View(Bookings);
        }

        // GET: Checkout/Details/5
        public ActionResult Details(int id)
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();
            Prenotazioni b = new Prenotazioni();

            try
            {
                SqlCommand com = Connessione.GetStoreProcedure("GetBookingById", sql);
                com.Parameters.AddWithValue("IdPrenotazione", id);

                SqlDataReader reader = com.ExecuteReader();



                while (reader.Read())
                {

                    b.IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"]);
                    b.Cliente = reader["Cognome"].ToString() + " " + reader["Nome"].ToString();
                    b.NrCamera = Convert.ToInt32(reader["NrCamera"]);
                    b.DataPren = Convert.ToDateTime(reader["DataPrenotazione"]);
                    b.CheckIn = Convert.ToDateTime(reader["DataArrivo"]);
                    b.CheckOut = Convert.ToDateTime(reader["DataUscita"]);
                    b.Acconto = Convert.ToDecimal(reader["Acconto"]);
                    b.Prezzo = Convert.ToDecimal(reader["PrezzoSoggiorno"]);
                    b.Pensione = reader["TipoPensione"].ToString();
                    b.Tot = Prenotazioni.TotServizi(Convert.ToInt32(reader["IdPrenotazione"]));
                    b.Saldo = Prenotazioni.DaSaldare(Convert.ToDecimal(reader["PrezzoSoggiorno"]), Convert.ToDecimal(reader["Acconto"]), Prenotazioni.TotServizi(Convert.ToInt32(reader["IdPrenotazione"])));

                }
            }
            catch (Exception ex)
            {
                ViewBag.msgerror = ex.Message;
            }
            finally { sql.Close(); }

            return View(b);
        }
    }
}
        
