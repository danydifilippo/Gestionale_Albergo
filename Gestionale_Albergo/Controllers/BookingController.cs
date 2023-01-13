using Gestionale_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestionale_Albergo.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult ListaPren()
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
                ViewBag.msgerror=ex.Message;
            }
            finally { sql.Close(); }

            return View(Bookings);
        }

        // GET: Booking/Details/5
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

        // GET: Booking/Create
        public ActionResult Create()
        {
            ViewBag.TipoPensioni = Pensione.ListaPensione;
            ViewBag.ListaClienti = Clienti.ListaClienti;
            ViewBag.ListaCamere = Camere.ListaCamere;

            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(Prenotazioni p)
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();

            try
            {
                SqlCommand com = Connessione.GetStoreProcedure("CreateBooking", sql);


                com.Parameters.AddWithValue("NrCamera", p.NrCamera);
                com.Parameters.AddWithValue("IdPensione", p.Pensione);
                com.Parameters.AddWithValue("IdCliente", p.IdCliente);
                com.Parameters.AddWithValue("DataPrenotazione", p.DataPren);
                com.Parameters.AddWithValue("DataArrivo", p.CheckIn);
                com.Parameters.AddWithValue("DataUscita", p.CheckOut);
                com.Parameters.AddWithValue("Acconto", p.Acconto);
                com.Parameters.AddWithValue("PrezzoSoggiorno", p.Prezzo);

                com.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                ViewBag.msgerror = ex.Message;
            }
            finally { sql.Close(); }
            return RedirectToAction("ListaPren");
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
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
                    b.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                    b.NrCamera = Convert.ToInt32(reader["NrCamera"]);
                    b.DataPren = Convert.ToDateTime(reader["DataPrenotazione"]);
                    b.CheckIn = Convert.ToDateTime(reader["DataArrivo"]);
                    b.CheckOut = Convert.ToDateTime(reader["DataUscita"]);
                    b.Acconto = Convert.ToDecimal(reader["Acconto"]);
                    b.Prezzo = Convert.ToDecimal(reader["PrezzoSoggiorno"]);
                  
                    ViewBag.TipoPensioni = Pensione.ListaPensione;
                    ViewBag.ListaClienti = Clienti.ListaClienti;
                }
            }
            catch (Exception ex)
            {
                ViewBag.msgerror = ex.Message;
            }
            finally { sql.Close(); }

            return View(b);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(Prenotazioni p)
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();

            try
            {
                SqlCommand com = Connessione.GetStoreProcedure("EditBooking", sql);

                com.Parameters.AddWithValue("IdPrenotazione", p.IdPrenotazione);
                com.Parameters.AddWithValue("NrCamera", p.NrCamera);
                com.Parameters.AddWithValue("IdPensione", p.Pensione);
                com.Parameters.AddWithValue("IdCliente", p.IdCliente);
                com.Parameters.AddWithValue("DataPrenotazione", p.DataPren);
                com.Parameters.AddWithValue("DataArrivo", p.CheckIn);
                com.Parameters.AddWithValue("DataUscita", p.CheckOut);
                com.Parameters.AddWithValue("Acconto", p.Acconto);
                com.Parameters.AddWithValue("PrezzoSoggiorno", p.Prezzo);

                com.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                ViewBag.msgerror = ex.Message;
            }
            finally { sql.Close(); }
            return RedirectToAction("ListaPren");
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
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

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Prenotazioni p)
        {
            SqlConnection sql = Connessione.GetConnection();
            sql.Open();

            try
            {
                SqlCommand com = Connessione.GetCommand("DELETE FROM PRENOTAZIONE WHERE IdPrenotazione=@Id", sql);

                com.Parameters.AddWithValue("Id", id);

                com.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                ViewBag.msgerror = ex.Message;
            }
            finally { sql.Close(); }
            return RedirectToAction("ListaPren");
        }
    }
}
