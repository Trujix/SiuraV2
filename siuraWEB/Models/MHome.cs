using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace siuraWEB.Models
{
    public class MHome : Controller
    {
        // -------- [ CLASES ] --------
        public class HorarioConsulta
        {
            public string Nombre { get; set; }
            public string Coordinacion { get; set; }
        }

        // FUNCION QUE DEVUELVE LOS PARAMETROS DEL USUARIO LOGGEADO
        public string ParametrosUsuario(string token)
        {
            try
            {
                SQL.comandoSQLTrans("Parametros");

                string TokenCentro = "";
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.usuarios WHERE tokenusuario = @TokenUsuarioDATA", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenUsuarioDATA", SqlDbType.VarChar) { Value = token });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        TokenCentro = lector["tokencentro"].ToString();
                    }
                }
                List<object> Parametros = new List<object>();
                Random aleatorio = new Random();
                string cadHTML = MISC.GenerarCadAleatoria(10, aleatorio);
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.usuariomenuprincipal WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = TokenCentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Dictionary<string, object> Parametro = new Dictionary<string, object>() {
                            { "Nombre", lector["nombre"].ToString() },
                            { "Visible", bool.Parse(lector["visible"].ToString()) },
                            { "IdHTML", cadHTML.ToLower() }
                        };
                        Parametros.Add(Parametro);
                        cadHTML = MISC.GenerarCadAleatoria(10, aleatorio);
                    }
                }

                SQL.transaccionSQL.Commit();
                return JsonConvert.SerializeObject(Parametros);
            }
            catch (Exception e)
            {
                SQL.transaccionSQL.Rollback();
                return e.ToString();
            }
            finally
            {
                SQL.conSQL.Close();
            }
        }

        // FUNCION QUE DEVUELVE LOS VALORES PARA EL CALENDARIO DE ACTIVIDADES [ CALENDARIO ]
        public string CalendarioParams(string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("CalendarioParams");
                DateTime FechaActual = MISC.FechaHoy();
                SQL.commandoSQL = new SqlCommand("DELETE FROM dbo.actividadesgrupales WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND fechainicio < @FechaActualParam", SQL.conSQL, SQL.transaccionSQL);
                SqlParameter[] limpiarActsGrupales =
                {
                    new SqlParameter("@FechaActualParam", SqlDbType.DateTime){Value = new DateTime(FechaActual.Year, FechaActual.Month, 1) },
                    new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar){Value = tokencentro },
                };
                SQL.commandoSQL.Parameters.AddRange(limpiarActsGrupales);
                SQL.commandoSQL.ExecuteNonQuery();

                SQL.commandoSQL = new SqlCommand("DELETE FROM dbo.actividadesindividuales WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND fecha < @FechaActualParam", SQL.conSQL, SQL.transaccionSQL);
                SqlParameter[] limpiarActsIndividuales =
                {
                    new SqlParameter("@FechaActualParam", SqlDbType.DateTime){Value = new DateTime(FechaActual.Year, FechaActual.Month, 1) },
                    new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar){Value = tokencentro },
                };
                SQL.commandoSQL.Parameters.AddRange(limpiarActsIndividuales);
                SQL.commandoSQL.ExecuteNonQuery();

                Dictionary<string, object> CalendarioData = new Dictionary<string, object>();
                List<Dictionary<string, object>> ActividadesGrupales = new List<Dictionary<string, object>>();
                DateTime[] FechasQuery = MISC.FechasArr();
                List<HorarioConsulta> ListaActGrup = new List<HorarioConsulta>();
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.actividadesgrupales WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND fechainicio BETWEEN @FechaIniParam AND @FechaFinParam", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@FechaIniParam", SqlDbType.DateTime) { Value = FechasQuery[0] });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@FechaFinParam", SqlDbType.DateTime) { Value = FechasQuery[1] });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        ActividadesGrupales.Add(new Dictionary<string, object>()
                        {
                            { "title", lector["nombre"].ToString() },
                            { "description", MISC.ColorCalendarioHTML(lector["coordinacion"].ToString(), 2) },
                            { "start", DateTime.Parse(lector["fechainicio"].ToString()).ToString("yyyy-MM-dd") },
                            { "end", DateTime.Parse(lector["fechafin"].ToString()).AddDays(1).ToString("yyyy-MM-dd") },
                            { "color", MISC.ColorCalendarioHTML(lector["coordinacion"].ToString(), 1) },
                            { "textColor", "#FFFFFF" },
                        });
                        if (MISC.FechasRango(DateTime.Parse(lector["fechainicio"].ToString()), DateTime.Parse(lector["fechafin"].ToString())))
                        {
                            ListaActGrup.Add(new HorarioConsulta()
                            {
                                Nombre = lector["nombre"].ToString(),
                                Coordinacion = lector["coordinacion"].ToString(),
                            });
                        }
                    }
                }

                string[] Coords = { "CA", "CD", "CM", "CP", "CE", "CC" };
                string ActividadesGrupalesHTML = "<h6 class='border-bottom border-gray pb-2 mb-0'>Horario de Actividades</h6>";
                SQL.commandoSQL = new SqlCommand("SELECT HC.hrinicio12hrs, HC.hrinicio24hrs, HC.hrtermino12hrs, HC.hrtermino24hrs, H.reloj, HC.receso, HC." + FechaActual.ToString("dddd").Replace("á", "a").Replace("é", "e").ToLower() + " AS DiaSemana from dbo.horariosconfig HC JOIN dbo.horarios H ON H.id = HC.idhorario AND H.activo = 'true' WHERE HC.idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) ORDER BY HC.numorden ASC", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        if(lector["DiaSemana"].ToString() != "-" || bool.Parse(lector["receso"].ToString()))
                        {
                            string hora = (lector["reloj"].ToString() == "12hrs") ? lector["hrinicio12hrs"].ToString() + "-" + lector["hrtermino12hrs"].ToString(): lector["hrinicio24hrs"].ToString() + " - " + lector["hrtermino24hrs"].ToString(), color = "#EAECEE", titulo = " RECESO", actividad = "";
                            if (!bool.Parse(lector["receso"].ToString()))
                            {
                                titulo = " " + lector["DiaSemana"].ToString();
                                actividad = "Sin Actividad Programada";
                                color = "#FFDEF9";
                                if (Coords.Contains(lector["DiaSemana"].ToString()))
                                {
                                    titulo = " " + MISC.ColorCalendarioHTML(lector["DiaSemana"].ToString(), 2);
                                    color = MISC.ColorCalendarioHTML(lector["DiaSemana"].ToString(), 1);
                                    foreach (HorarioConsulta Horario in ListaActGrup)
                                    {
                                        if (Horario.Coordinacion == lector["DiaSemana"].ToString())
                                        {
                                            actividad = Horario.Nombre;
                                        }
                                    }
                                }
                            }
                            ActividadesGrupalesHTML += "<div class='media text-muted pt-3'><svg class='bd-placeholder-img mr-2 rounded' width='32' height='32' xmlns='http://www.w3.org/2000/svg' preserveAspectRatio='xMidYMid slice' focusable='false' role='img' aria-label='Placeholder: 32x32'><title>" + titulo + "</title><rect width='100%' height='100%' fill='" + color + "' /><text x='50%' y='50%' fill='" + color + "' dy='.3em'>32x32</text></svg><p class='media-body pb-3 mb-0 small lh-125 border-bottom border-gray'><strong class='d-block text-gray-dark'>" + hora + titulo + "</strong>" + actividad + "</p></div>";
                        }
                    }
                }

                string ActividadesIndividualesHTML = "<h6 class='border-bottom border-gray pb-2 mb-0'>Actividades Individuales</h6>";
                string coordVerifAI = "";
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.actividadesindividuales WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND fecha = @FechaHoyDATA ORDER BY coordinacion,horainicio ASC", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@FechaHoyDATA", SqlDbType.DateTime) { Value = new DateTime(FechaActual.Year, FechaActual.Month, FechaActual.Day) });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        if(lector["coordinacion"].ToString() != coordVerifAI)
                        {
                            ActividadesIndividualesHTML += "<center><h6 style='width: 100%;'><span class='badge badge-light' style='background-color: " + MISC.ColorCalendarioHTML(lector["coordinacion"].ToString(), 1) + "; color: #FFFFFF; width: 100%;'>" + MISC.ColorCalendarioHTML(lector["coordinacion"].ToString(), 2) + "</span></h6></center>";
                            coordVerifAI = lector["coordinacion"].ToString();
                        }
                        ActividadesIndividualesHTML += "<div class='media text-muted pt-3'><p class='media-body pb-3 mb-0 small lh-125 border-bottom border-gray'><strong class='d-block text-gray-dark'>" + lector["horainicio12hrs"].ToString() + " - " + lector["horafin12hrs"].ToString() + "</strong>" + lector["nombre"].ToString() + "</p></div>";
                    }
                }

                string CitasHTML = "<h6 class='border-bottom border-gray pb-2 mb-0'>Agenda de Citas</h6>";

                CalendarioData = new Dictionary<string, object>()
                {
                    { "ActividadesGrupales", ActividadesGrupales },
                    { "HorarioActividades", ActividadesGrupalesHTML },
                    { "HorarioActIndividuales", ActividadesIndividualesHTML },
                    { "HorarioCitas", CitasHTML },
                };
                SQL.transaccionSQL.Commit();
                return JsonConvert.SerializeObject(CalendarioData);
            }
            catch (Exception e)
            {
                SQL.transaccionSQL.Rollback();
                return e.ToString();
            }
            finally
            {
                SQL.conSQL.Close();
            }
        }
    }
}