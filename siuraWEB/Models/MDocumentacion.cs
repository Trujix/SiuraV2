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
    public class MDocumentacion : Controller
    {
        // ----------- CLASES Y VARIABLES PUBLICAS -----------
        // CLASE DE REGISTRO DE PACIENTE
        public class PacienteData
        {
            public string Nombre { get; set; }
            public string PacienteApellidoP { get; set; }
            public string PacienteApellidoM { get; set; }
            public string PacienteFechaNac { get; set; }
            public int Edad { get; set; }
            public string SexoSigno { get; set; }
            public string Sexo { get; set; }
            public string EstadoCivil { get; set; }
            public string PacienteCalle { get; set; }
            public double PacienteCP { get; set; }
            public string PacienteCalleNumero { get; set; }
            public string PacienteColoniaPoblacion { get; set; }
            public string PacienteMunicipio { get; set; }
            public string PacienteEstado { get; set; }
            public string CURP { get; set; }
            public string PacienteAlias { get; set; }
            public string ParienteNombre { get; set; }
            public string ParienteApellidoP { get; set; }
            public string ParienteApellidoM { get; set; }

            public string ParienteCalle { get; set; }
            public double ParienteCP { get; set; }
            public string ParienteCalleNumero { get; set; }
            public string ParienteColoniaPoblacion { get; set; }
            public string ParienteMunicipio { get; set; }
            public string ParienteEstado { get; set; }

            public string ParentescoIndx { get; set; }
            public string Parentesco { get; set; }
            public double TelefonoCasa { get; set; }
            public double TelefonoPariente { get; set; }
            public double TelefonoUsuario { get; set; }
            public int Estatus { get; set; }
        }

        // CLASE DE REGISTRO DE INGRESO DEL PACIENTE
        public class PacienteIngreso
        {
            public int IdPaciente { get; set; }
            public string TipoIngreso { get; set; }
            public int TiempoEstancia { get; set; }
            public string TipoEstancia { get; set; }
            public string TipoEstanciaIndx { get; set; }
            public string TestigoNombre { get; set; }
            public string TipoTratamiento { get; set; }
            public string TipoTratamientoIndx { get; set; }
            public string FasesCantTratamiento { get; set; }
            public string FasesCantTratamientoIndx { get; set; }
            public string FasesTratamiento { get; set; }
            public string FasesTratamientoIndx { get; set; }
            public string EstadoAlerta { get; set; }
            public int EstadoAlertaIndx { get; set; }
            public string NivelIntoxicacion { get; set; }
            public int NivelIntoxicacionIndx { get; set; }
            public string EstadoAnimo { get; set; }
            public int EstadoAnimoIndx { get; set; }
        }
        // CLASE DE REGISTRO DE FINANZAS DEL PACIENTE
        public class PacienteFinazasData
        {
            public double MontoPagar { get; set; }
            public string TipoMoneda { get; set; }
            public bool Becario { get; set; }
            public double BecaValor { get; set; }
            public string BecaTipo { get; set; }
            public bool Parcialidad { get; set; }
            public int CantidadPagos { get; set; }
            public double MontoPagoParcial { get; set; }
            public string TipoPago { get; set; }
            public int TipoPagoIndx { get; set; }
            public int TipoPagoCantPers { get; set; }
            public string FechaInicioPago { get; set; }
            public string FechaFinPago { get; set; }
        }
        // CLASE DE REGISTRO DE CARGO ADICIONAL PARA PACIENTE
        public class PacienteCargosAdicionales
        {
            public string Descripcion { get; set; }
            public double Importe { get; set; }
        }

        // CLASE DE REGISTRO DE DATOS GENERALES DEL PACIENTE
        public class PacienteDatosGenerales
        {
            public bool Alcohol { get; set; }
            public bool Marihuana { get; set; }
            public bool Disolventes { get; set; }
            public bool Alucinogenos { get; set; }
            public bool OpioMorfina { get; set; }
            public bool Sedantes { get; set; }
            public bool Anfetaminas { get; set; }
            public bool Rohypnol { get; set; }
            public bool Basuco { get; set; }
            public bool Tranquilizantes { get; set; }
            public bool Metanfetamina { get; set; }
            public bool Opiaceos { get; set; }
            public bool ProvieneDomicilio { get; set; }
            public bool ProvieneInstPublica { get; set; }
            public bool ProvieneInstPrivada { get; set; }
            public bool ProvienePsiquiatrico { get; set; }
            public bool ProvieneCereso { get; set; }
            public bool ProvieneOtro { get; set; }
            public string ProvieneOtroTexto { get; set; }
            public bool AcudeSolo { get; set; }
            public bool AcudeAmigo { get; set; }
            public bool AcudeVecino { get; set; }
            public bool AcudeFamiliar { get; set; }
            public string AcudeFamiliarParentesco { get; set; }
            public bool AcudeOtro { get; set; }
            public string AcudeOtroTexto { get; set; }

            public string PacienteObservacionesGenerales { get; set; }
        }

        // ----- CLASES DE  HORARIOS --------
        // CLASE DE PARAMETROS GENERALES HORARIO
        public class Horarios
        {
            public int IdHorario { get; set; }
            public string Descripcion { get; set; }
            public string HoraInicio { get; set; }
            public int Duracion { get; set; }
            public string Tipo { get; set; }
            public string Reloj { get; set; }
            public bool Activo { get; set; }
            public DateTime FechaCreado { get; set; }
        }
        // CLASE DE CONFIGURACION DE HORARIO
        public class HorariosConfig
        {
            public string IdHTML { get; set; }
            public string HoraInicio24hrs { get; set; }
            public string HoraInicio12hrs { get; set; }
            public string HoraTermino24hrs { get; set; }
            public string HoraTermino12hrs { get; set; }
            public string Lunes { get; set; }
            public string Martes { get; set; }
            public string Miercoles { get; set; }
            public string Jueves { get; set; }
            public string Viernes { get; set; }
            public string Sabado { get; set; }
            public string Domingo { get; set; }
            public bool Receso { get; set; }
            public int NumOrden { get; set; }
        }
        // CLASE AUXILIAR PARA EL HORARIO SEMANAL
        public class HorarioSemanalAG
        {
            public string Nombre { get; set; }
            public string Coordinacion { get; set; }
            public List<string> FechasArray { get; set; }
        }

        // ----------- FUNCIONES GENERALES -----------
        // FUNCION QUE DEVUELVE LA  LISTA DE PACIENTES EN PRE REGISTRO [ PRE-REGISTROS ]
        public string ListaPreRegistros(string tokencentro)
        {
            try
            {
                List<Dictionary<string, object>> ListaPreRegistro = new List<Dictionary<string, object>>();
                SQL.comandoSQLTrans("PacientesPagosPend");
                SQL.commandoSQL = new SqlCommand("SELECT PR.*, PP.id AS idFinanzas, PP.montototal FROM dbo.pacienteregistro PR JOIN dbo.pacienteregistrofinanzas PP ON PP.idpaciente = PR.id WHERE PR.idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND PR.estatus = 1", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Dictionary<string, object> Preregistro = new Dictionary<string, object>()
                        {
                            { "IdFinanzas", int.Parse(lector["idFinanzas"].ToString()) },
                            { "IdPaciente", int.Parse(lector["id"].ToString()) },
                            { "Nombre", lector["nombre"].ToString() },
                            { "ApellidoP", lector["apellidopaterno"].ToString() },
                            { "ApellidoM", lector["apellidomaterno"].ToString() },
                            { "NombreCompleto", lector["nombre"].ToString().ToUpper() + " " + lector["apellidopaterno"].ToString().ToUpper()+ " " + lector["apellidomaterno"].ToString().ToUpper() },
                            { "FechaRegistro", lector["fechahora"].ToString() },
                            { "Monto", double.Parse(lector["montototal"].ToString()) }
                        };
                        ListaPreRegistro.Add(Preregistro);
                    }
                }

                SQL.transaccionSQL.Commit();
                return JsonConvert.SerializeObject(ListaPreRegistro);
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

        // ---------------------------------------------------
        // FUNCION QUE CAMBIA EL ESTATUS DE UN PACIENTE (POSIBLEMENTE SEA MULTIUSOS)
        public string ActEstatusPaciente(int idpaciente, int estatus, string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("ActEstatusPaciente");
                SQL.commandoSQL = new SqlCommand("UPDATE dbo.pacienteregistro SET estatus = @EstatusDATA WHERE id = @IDPacienteDATA AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDPacienteDATA", SqlDbType.Int) { Value = idpaciente });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@EstatusDATA", SqlDbType.Int) { Value = estatus });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.ExecuteNonQuery();

                SQL.transaccionSQL.Commit();
                return "true";
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
        //----------------------------------------------------

        // FUNCION QUE GUARDA EL REGISTRO DEL PACIENTE [ PREVIO ] ( :: PACIENTES ::)
        public string GuardarPacienteRegistro(PacienteData pacienteinfo, PacienteIngreso pacienteingreso, PacienteFinazasData pacientefinanzas, PacienteCargosAdicionales[] pacientecargos, PacienteDatosGenerales pacientedatosgenerales, string tokenusuario, string tokencentro)
        {
            try
            {
                string IDClavePaciente = "IP-" + MISC.CrearCadAleatoria(2, 8).ToUpper();
                SQL.comandoSQLTrans("RegistrarPaciente");
                SQL.commandoSQL = new SqlCommand("INSERT INTO dbo.pacienteregistro (idcentro, idpaciente, nombre, apellidopaterno, apellidomaterno, fechanacimiento, edad, estadocivil, sexo, " +
                    "sexosigno, curp, alias, domcalle, domnumero, domcp, coloniapoblacion, municipio, entidadfederativa, parientenombre, parienteapellidop, " +
                    "parienteapellidom, parentesco, parentescoindx, parientedomcalle, parientedomnumero, parientedomcp, parientecoloniapoblacion, parientemunicipio, " +
                    "parienteentfederativa, telefonocasa, telefonopariente, telefonousuario, estatus, fechahora, admusuario " +
                    ") VALUES ( " +
                    "(SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroParam), @IDPacienteParam, @PacNombreParam, @PacApellidoPParam, @PacApellidoMParam, " +
                    "@FechaNacParam, @EdadParam, @EstadoCivilParam, @SexoParam, @SexoSignoParam, @CURPParam, @AliasParam, @PacienteCalleParam,  " +
                    "@PacienteCalleNumeroParam, @PacienteCPParam, @PacienteColoniaPoblacionParam, @PacienteMunicipioParam, @PacienteEstadoParam, @ParNombreParam, " +
                    "@ParApellidoPParam, @ParApellidoMParam, @ParentescoParam, @ParentescoIndxParam, @ParienteCalleParam, @ParienteCalleNumeroParam, " +
                    "@ParienteCPParam, @ParienteColoniaPoblacionParam, @ParienteMunicipioParam, @ParienteEstadoParam, @TelefonoCasaParam, @TelefonoParienteParam, " +
                    "@TelefonoUsuParam, @EstatusParam, @FechaParam, (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenParam))", SQL.conSQL, SQL.transaccionSQL);
                SqlParameter[] registroPaciente =
                {
                    new SqlParameter("@TokenCentroParam", SqlDbType.VarChar){Value = tokencentro },
                    new SqlParameter("@IDPacienteParam", SqlDbType.VarChar){Value = IDClavePaciente },
                    new SqlParameter("@PacNombreParam", SqlDbType.VarChar){Value = pacienteinfo.Nombre },
                    new SqlParameter("@PacApellidoPParam", SqlDbType.VarChar){Value = pacienteinfo.PacienteApellidoP },
                    new SqlParameter("@PacApellidoMParam", SqlDbType.VarChar){Value = pacienteinfo.PacienteApellidoM },
                    new SqlParameter("@FechaNacParam", SqlDbType.DateTime){Value = pacienteinfo.PacienteFechaNac },
                    new SqlParameter("@EdadParam", SqlDbType.Float){Value = pacienteinfo.Edad },
                    new SqlParameter("@EstadoCivilParam", SqlDbType.VarChar){Value = pacienteinfo.EstadoCivil },
                    new SqlParameter("@SexoParam", SqlDbType.VarChar){Value = pacienteinfo.Sexo },
                    new SqlParameter("@SexoSignoParam", SqlDbType.VarChar){Value = pacienteinfo.SexoSigno },
                    new SqlParameter("@CURPParam", SqlDbType.VarChar){Value = pacienteinfo.CURP },
                    new SqlParameter("@AliasParam", SqlDbType.VarChar){Value = pacienteinfo.PacienteAlias },
                    new SqlParameter("@PacienteCalleParam", SqlDbType.VarChar){Value = pacienteinfo.PacienteCalle },
                    new SqlParameter("@PacienteCalleNumeroParam", SqlDbType.VarChar){Value = pacienteinfo.PacienteCalleNumero },
                    new SqlParameter("@PacienteCPParam", SqlDbType.Float){Value = pacienteinfo.PacienteCP },
                    new SqlParameter("@PacienteColoniaPoblacionParam", SqlDbType.VarChar){Value = pacienteinfo.PacienteColoniaPoblacion },
                    new SqlParameter("@PacienteMunicipioParam", SqlDbType.VarChar){Value = pacienteinfo.PacienteMunicipio },
                    new SqlParameter("@PacienteEstadoParam", SqlDbType.VarChar){Value = pacienteinfo.PacienteEstado },
                    new SqlParameter("@ParNombreParam", SqlDbType.VarChar){Value = pacienteinfo.ParienteNombre },
                    new SqlParameter("@ParApellidoPParam", SqlDbType.VarChar){Value = pacienteinfo.ParienteApellidoP },
                    new SqlParameter("@ParApellidoMParam", SqlDbType.VarChar){Value = pacienteinfo.ParienteApellidoM },
                    new SqlParameter("@ParentescoParam", SqlDbType.VarChar){Value = pacienteinfo.Parentesco },
                    new SqlParameter("@ParentescoIndxParam", SqlDbType.VarChar){Value = pacienteinfo.ParentescoIndx },
                    new SqlParameter("@TelefonoCasaParam", SqlDbType.Float){Value = pacienteinfo.TelefonoCasa },
                    new SqlParameter("@TelefonoParienteParam", SqlDbType.Float){Value = pacienteinfo.TelefonoPariente },
                    new SqlParameter("@ParienteCalleParam", SqlDbType.VarChar){Value = pacienteinfo.ParienteCalle },
                    new SqlParameter("@ParienteCalleNumeroParam", SqlDbType.VarChar){Value = pacienteinfo.ParienteCalleNumero },
                    new SqlParameter("@ParienteCPParam", SqlDbType.Float){Value = pacienteinfo.ParienteCP },
                    new SqlParameter("@ParienteColoniaPoblacionParam", SqlDbType.VarChar){Value = pacienteinfo.ParienteColoniaPoblacion },
                    new SqlParameter("@ParienteMunicipioParam", SqlDbType.VarChar){Value = pacienteinfo.ParienteMunicipio },
                    new SqlParameter("@ParienteEstadoParam", SqlDbType.VarChar){Value = pacienteinfo.ParienteEstado },
                    new SqlParameter("@TelefonoUsuParam", SqlDbType.Float){Value = pacienteinfo.TelefonoUsuario },
                    new SqlParameter("@EstatusParam", SqlDbType.Int){Value = pacienteinfo.Estatus },
                    new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                    new SqlParameter("@TokenParam", SqlDbType.VarChar){Value = tokenusuario }
                };
                SQL.commandoSQL.Parameters.AddRange(registroPaciente);
                SQL.commandoSQL.ExecuteNonQuery();

                string SiglaLegal = "", NombreCentro = "", ClaveCentro = "", DireccionCentro = "", CPCentro = "", ColoniaCentro = "", EstadoCentro = "", MunicipioCentro = "", Director = "", LogoCad = "";
                double TelefonoCentro = 0;
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.usuarioscentro WHERE id = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        SiglaLegal = lector["siglalegal"].ToString();
                        NombreCentro = lector["nombrecentro"].ToString();
                        ClaveCentro = lector["clavecentro"].ToString();
                        DireccionCentro = lector["direccion"].ToString();
                        CPCentro = lector["cp"].ToString();
                        ColoniaCentro = lector["colonia"].ToString();
                        EstadoCentro = lector["estado"].ToString();
                        MunicipioCentro = lector["municipio"].ToString();
                        Director = lector["nombredirector"].ToString();
                        TelefonoCentro = double.Parse(lector["telefono"].ToString());

                        if (bool.Parse(lector["logopersonalizado"].ToString()))
                        {
                            LogoCad = "«~LOGOPERS~»";
                        }
                        if (bool.Parse(lector["alanonlogo"].ToString()))
                        {
                            LogoCad = "«~LOGOALANON~»";
                        }
                    }
                }

                int IdPaciente = 0;
                SQL.commandoSQL = new SqlCommand("SELECT MAX(id) AS PacienteNuevo FROM dbo.pacienteregistro WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroParam)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroParam", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        IdPaciente = int.Parse(lector["PacienteNuevo"].ToString());
                    }
                }

                string folioContrato = ((pacienteingreso.TipoIngreso == "I") ? "CI" : "CV") + "-" + SiglaLegal + MISC.CrearCadAleatoria(2, 8);
                SQL.commandoSQL = new SqlCommand("INSERT INTO dbo.pacienteingreso (idcentro, idpaciente, tipoingreso, foliocontrato, tiempoestancia, tipoestancia, tipoestanciaindx, nombretestigo, tipotratamiento, tipotratamientoindx, fasescantratamiento, fasescantratamientoindx, fasestratamiento, fasestratamientoindx, fechahora, admusuario) VALUES ((SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroParam), @IdPacienteParam, @TipoIngresoParam, @FolioContratoParam, @TiempoEstanciaParam, @TipoEstanciaParam, @TipoEstanciaIndxParam, @TestigoNombreParam, @TipoTratamientoParam, @TipoTratamientoIndxParam, @FasesCanTratamientoParam, @FasesCanTratamientoIndxParam, @FasesTratamientoParam, @FasesTratamientoIndxParam, @FechaParam, (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenParam))", SQL.conSQL, SQL.transaccionSQL);
                SqlParameter[] registroPacienteIngreso =
                {
                    new SqlParameter("@TokenCentroParam", SqlDbType.VarChar){Value = tokencentro },
                    new SqlParameter("@IdPacienteParam", SqlDbType.Int){Value = IdPaciente },
                    new SqlParameter("@TipoIngresoParam", SqlDbType.VarChar){Value = pacienteingreso.TipoIngreso },
                    new SqlParameter("@FolioContratoParam", SqlDbType.VarChar){Value = folioContrato },
                    new SqlParameter("@TiempoEstanciaParam", SqlDbType.Int){Value = pacienteingreso.TiempoEstancia },
                    new SqlParameter("@TipoEstanciaParam", SqlDbType.VarChar){Value = pacienteingreso.TipoEstancia },
                    new SqlParameter("@TipoEstanciaIndxParam", SqlDbType.VarChar){Value = pacienteingreso.TipoEstanciaIndx },
                    new SqlParameter("@TestigoNombreParam", SqlDbType.VarChar){Value = pacienteingreso.TestigoNombre },
                    new SqlParameter("@TipoTratamientoParam", SqlDbType.VarChar){Value = pacienteingreso.TipoTratamiento },
                    new SqlParameter("@TipoTratamientoIndxParam", SqlDbType.VarChar){Value = pacienteingreso.TipoTratamientoIndx },
                    new SqlParameter("@FasesCanTratamientoParam", SqlDbType.VarChar){Value = pacienteingreso.FasesCantTratamiento },
                    new SqlParameter("@FasesCanTratamientoIndxParam", SqlDbType.VarChar){Value = pacienteingreso.FasesCantTratamientoIndx },
                    new SqlParameter("@FasesTratamientoParam", SqlDbType.VarChar){Value = pacienteingreso.FasesTratamiento },
                    new SqlParameter("@FasesTratamientoIndxParam", SqlDbType.VarChar){Value = pacienteingreso.FasesTratamientoIndx },
                    new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                    new SqlParameter("@TokenParam", SqlDbType.VarChar){Value = tokenusuario }
                };
                SQL.commandoSQL.Parameters.AddRange(registroPacienteIngreso);
                SQL.commandoSQL.ExecuteNonQuery();

                SQL.commandoSQL = new SqlCommand("INSERT INTO dbo.pacienteregistrofinanzas (idcentro, idpaciente, montototal, becario,becavalor, becatipo, parcialidad, cantidadpagos, montopagoparcial, tipopago, tipopagoindx, tipopagocantpers, fechainiciopago, fechafinpago, fechahora, admusuario) VALUES ((SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroParam), @IdPacienteParam, @MontoTotalParam, @BecarioParam, @BecaValorParam, @BecaTipoParam, @ParcialidadParam, @CantidadPagosParam, @MontoPagoParcialParam, @TipoPagoParam, @TipoPagoIndxParam, @TipoPagoCantPersParam, @FechaInicioPagoParam, @FechaFinPagoParam, @FechaParam, (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenParam))", SQL.conSQL, SQL.transaccionSQL);
                SqlParameter[] registroPacienteFinanzas =
                {
                    new SqlParameter("@TokenCentroParam", SqlDbType.VarChar){Value = tokencentro },
                    new SqlParameter("@IdPacienteParam", SqlDbType.Int){Value = IdPaciente },
                    new SqlParameter("@MontoTotalParam", SqlDbType.Float){Value = pacientefinanzas.MontoPagar },
                    new SqlParameter("@BecarioParam", SqlDbType.Bit){Value = pacientefinanzas.Becario },
                    new SqlParameter("@BecaValorParam", SqlDbType.Float){Value = pacientefinanzas.BecaValor },
                    new SqlParameter("@BecaTipoParam", SqlDbType.VarChar){Value = pacientefinanzas.BecaTipo },
                    new SqlParameter("@ParcialidadParam", SqlDbType.Bit){Value = pacientefinanzas.Parcialidad },
                    new SqlParameter("@CantidadPagosParam", SqlDbType.Int){Value = pacientefinanzas.CantidadPagos },
                    new SqlParameter("@MontoPagoParcialParam", SqlDbType.Float){Value = pacientefinanzas.MontoPagoParcial },
                    new SqlParameter("@TipoPagoParam", SqlDbType.VarChar){Value = pacientefinanzas.TipoPago },
                    new SqlParameter("@TipoPagoIndxParam", SqlDbType.Int){Value = pacientefinanzas.TipoPagoIndx },
                    new SqlParameter("@TipoPagoCantPersParam", SqlDbType.Int){Value = pacientefinanzas.TipoPagoCantPers },
                    new SqlParameter("@FechaInicioPagoParam", SqlDbType.VarChar){Value = pacientefinanzas.FechaInicioPago },
                    new SqlParameter("@FechaFinPagoParam", SqlDbType.VarChar){Value = pacientefinanzas.FechaFinPago },
                    new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                    new SqlParameter("@TokenParam", SqlDbType.VarChar){Value = tokenusuario }
                };
                SQL.commandoSQL.Parameters.AddRange(registroPacienteFinanzas);
                SQL.commandoSQL.ExecuteNonQuery();

                int IdFinanzas = 0;
                SQL.commandoSQL = new SqlCommand("SELECT MAX(id) AS FinanzasNuevo FROM dbo.pacienteregistrofinanzas WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroParam)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroParam", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        IdFinanzas = int.Parse(lector["FinanzasNuevo"].ToString());
                    }
                }

                if (pacientecargos != null)
                {
                    foreach(PacienteCargosAdicionales pacientecargo in pacientecargos)
                    {
                        SQL.commandoSQL = new SqlCommand("INSERT INTO dbo.pacientecargosadicionales (idcentro, idfinanzas, folio, descripcion, importe, cargoinicial, pagado, fechahora, admusuario) VALUES ((SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroParam), @IdFinanzasParam, @FolioParam, @DescripcionParam, @ImporteParam, @CargoInicialParam, @PagadoParam, @FechaParam, (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenParam))", SQL.conSQL, SQL.transaccionSQL);
                        SqlParameter[] registroPacienteCargoAdic =
                        {
                            new SqlParameter("@TokenCentroParam", SqlDbType.VarChar){Value = tokencentro },
                            new SqlParameter("@IdFinanzasParam", SqlDbType.Int){Value = IdFinanzas },
                            new SqlParameter("@FolioParam", SqlDbType.VarChar){Value = "CA-" + MISC.CrearCadAleatoria(2, 8).ToUpper() },
                            new SqlParameter("@DescripcionParam", SqlDbType.VarChar){Value = pacientecargo.Descripcion },
                            new SqlParameter("@ImporteParam", SqlDbType.Float){Value = pacientecargo.Importe },
                            new SqlParameter("@CargoInicialParam", SqlDbType.Bit){Value = true },
                            new SqlParameter("@PagadoParam", SqlDbType.Bit){Value = true },
                            new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                            new SqlParameter("@TokenParam", SqlDbType.VarChar){Value = tokenusuario }
                        };
                        SQL.commandoSQL.Parameters.AddRange(registroPacienteCargoAdic);
                        SQL.commandoSQL.ExecuteNonQuery();
                    }
                }

                SQL.commandoSQL = new SqlCommand("INSERT INTO dbo.pacientedatosgenerales (idcentro, idpaciente, alcohol, mariguana, disolventes, alucinogenos, opiomorfina, sedantes, anfetaminas, rohypnol, basuco, tranquilizantes, metanfetamina, provienedomicilio, provieneinstpublica, " +
                    "provieneinstprivada, provienepsiquiatrico, provienecereso, provieneotro, provieneotrotexto, opiaceos, acudesolo, acudeamigo, acudevecino, acudefamiliar, acudefamiliarnombre, acudeotro, acudeotrotexto, observacionesgenerales, fechahora, admusuario) " +
                    "VALUES ((SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroParam), @IdPacienteParam, " +
                    "@AlcoholParam, @MarihuanaParam, @DisolventesParam, @AlucinogenosParam, @OpioMorfinaParam, @SedantesParam, @AnfetaminasParam, " +
                    "@RohypnolParam, @BasucoParam, @TranquilizantesParam, @MetanfetaminaParam, @ProvieneDomicilioParam, @ProvieneInstPublicaParam, " +
                    "@ProvieneInstPrivadaParam, @ProvienePsiquiatricoParam, @ProvieneCeresoParam, @ProvieneOtroParam, @ProvieneOtroTextoParam, " +
                    "@OpiaceosParam, @AcudeSoloParam, @AcudeAmigoParam, @AcudeVecinoParam, @AcudeFamiliarParam, @AcudeFamiliarParentescoParam, " +
                    "@AcudeOtroParam, @AcudeOtroTextoParam, @PacienteObservacionesGeneralesParam, " +
                    "@FechaParam, (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenParam))", SQL.conSQL, SQL.transaccionSQL);
                SqlParameter[] registroPacienteDatosGenerales =
                {
                    new SqlParameter("@TokenCentroParam", SqlDbType.VarChar){Value = tokencentro },
                    new SqlParameter("@IdPacienteParam", SqlDbType.Int){Value = IdPaciente },
                    new SqlParameter("@AlcoholParam", SqlDbType.Bit){Value = pacientedatosgenerales.Alcohol },
                    new SqlParameter("@MarihuanaParam", SqlDbType.Bit){Value = pacientedatosgenerales.Marihuana },
                    new SqlParameter("@DisolventesParam", SqlDbType.Bit){Value = pacientedatosgenerales.Disolventes },
                    new SqlParameter("@AlucinogenosParam", SqlDbType.Bit){Value = pacientedatosgenerales.Alucinogenos },
                    new SqlParameter("@OpioMorfinaParam", SqlDbType.Bit){Value = pacientedatosgenerales.OpioMorfina },
                    new SqlParameter("@SedantesParam", SqlDbType.Bit){Value = pacientedatosgenerales.Sedantes },
                    new SqlParameter("@AnfetaminasParam", SqlDbType.Bit){Value = pacientedatosgenerales.Anfetaminas },
                    new SqlParameter("@RohypnolParam", SqlDbType.Bit){Value = pacientedatosgenerales.Rohypnol },
                    new SqlParameter("@BasucoParam", SqlDbType.Bit){Value = pacientedatosgenerales.Basuco },
                    new SqlParameter("@TranquilizantesParam", SqlDbType.Bit){Value = pacientedatosgenerales.Tranquilizantes },
                    new SqlParameter("@MetanfetaminaParam", SqlDbType.Bit){Value = pacientedatosgenerales.Metanfetamina },
                    new SqlParameter("@ProvieneDomicilioParam", SqlDbType.Bit){Value = pacientedatosgenerales.ProvieneDomicilio },
                    new SqlParameter("@ProvieneInstPublicaParam", SqlDbType.Bit){Value = pacientedatosgenerales.ProvieneInstPublica },
                    new SqlParameter("@ProvieneInstPrivadaParam", SqlDbType.Bit){Value = pacientedatosgenerales.ProvieneInstPrivada },
                    new SqlParameter("@ProvienePsiquiatricoParam", SqlDbType.Bit){Value = pacientedatosgenerales.ProvienePsiquiatrico },
                    new SqlParameter("@ProvieneCeresoParam", SqlDbType.Bit){Value = pacientedatosgenerales.ProvieneCereso },
                    new SqlParameter("@ProvieneOtroParam", SqlDbType.Bit){Value = pacientedatosgenerales.ProvieneOtro },
                    new SqlParameter("@ProvieneOtroTextoParam", SqlDbType.VarChar){Value = pacientedatosgenerales.ProvieneOtroTexto },
                    new SqlParameter("@OpiaceosParam", SqlDbType.Bit){Value = pacientedatosgenerales.Opiaceos },
                    new SqlParameter("@AcudeSoloParam", SqlDbType.Bit){Value = pacientedatosgenerales.AcudeSolo },
                    new SqlParameter("@AcudeAmigoParam", SqlDbType.Bit){Value = pacientedatosgenerales.AcudeAmigo },
                    new SqlParameter("@AcudeVecinoParam", SqlDbType.Bit){Value = pacientedatosgenerales.AcudeVecino },
                    new SqlParameter("@AcudeFamiliarParam", SqlDbType.Bit){Value = pacientedatosgenerales.AcudeFamiliar },
                    new SqlParameter("@AcudeFamiliarParentescoParam", SqlDbType.VarChar){Value = pacientedatosgenerales.AcudeFamiliarParentesco },
                    new SqlParameter("@AcudeOtroParam", SqlDbType.Bit){Value = pacientedatosgenerales.AcudeOtro },
                    new SqlParameter("@AcudeOtroTextoParam", SqlDbType.VarChar){Value = pacientedatosgenerales.AcudeOtroTexto },
                    new SqlParameter("@PacienteObservacionesGeneralesParam", SqlDbType.VarChar){Value = pacientedatosgenerales.PacienteObservacionesGenerales },
                    new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                    new SqlParameter("@TokenParam", SqlDbType.VarChar){Value = tokenusuario }
                };
                SQL.commandoSQL.Parameters.AddRange(registroPacienteDatosGenerales);
                SQL.commandoSQL.ExecuteNonQuery();

                Dictionary<string, object> Contrato = new Dictionary<string, object>() {
                    { "ClavePaciente", IDClavePaciente },
                    { "TipoContrato", pacienteingreso.TipoIngreso },
                    { "FolioContrato", folioContrato },
                    { "NombrePaciente", pacienteinfo.Nombre.ToUpper() + " " + pacienteinfo.PacienteApellidoP.ToUpper() + " " + pacienteinfo.PacienteApellidoM.ToUpper() },
                    { "NombreCentro", NombreCentro.ToUpper() },
                    { "ClaveCentro", ClaveCentro.ToUpper() },
                    { "FechaIngreso",  MISC.FechaHoy().ToString("dddd, dd MMMM yyyy HH:mm:ss") },
                    { "SexoPaciente", pacienteinfo.Sexo.ToUpper() },
                    { "EdadPaciente", pacienteinfo.Edad.ToString() },
                    { "DomicilioDoc", DireccionCentro + " Colonia: " + ColoniaCentro + " - C.P. " + CPCentro + ", " + MunicipioCentro + ", " + EstadoCentro },
                    { "Estancia", pacienteingreso.TiempoEstancia.ToString() + " " + pacienteingreso.TipoEstancia },
                    { "MontoPago", pacientefinanzas.MontoPagar },
                    { "TipoMoneda", pacientefinanzas.TipoMoneda },
                    { "FechaFirma",  MISC.FechaHoy().ToString("dddd, dd MMMM yyyy") },
                    { "Estado", EstadoCentro },
                    { "Municipio", MunicipioCentro },
                    { "NombreDirector", Director.ToUpper() },
                    { "Testigo", pacienteingreso.TestigoNombre.ToUpper() },
                    { "FamiliarNombre", pacienteinfo.ParienteNombre.ToUpper() + " " + pacienteinfo.ParienteApellidoP.ToUpper() + " " + pacienteinfo.ParienteApellidoM.ToUpper() },
                    { "Parentesco", pacienteinfo.Parentesco },
                    { "TipoTratamiento", pacienteingreso.TipoTratamiento },
                    { "NumeroExpediente", MISC.CadExpediente(IdPaciente) },
                    { "FasesCantTratamiento", pacienteingreso.FasesCantTratamiento },
                    { "FasesTratamiento", pacienteingreso.FasesTratamiento },
                    { "Parcialidad", pacientefinanzas.Parcialidad },
                    { "CantidadPagos", pacientefinanzas.CantidadPagos },
                    { "MontoPagoParcial", pacientefinanzas.MontoPagoParcial },
                    { "TipoPago", pacientefinanzas.TipoPago },
                    { "FechaInicioPago", (pacientefinanzas.FechaInicioPago != "--") ? new DateTime(int.Parse(pacientefinanzas.FechaInicioPago.Split('/')[2]),int.Parse(pacientefinanzas.FechaInicioPago.Split('/')[1]),int.Parse(pacientefinanzas.FechaInicioPago.Split('/')[0])).ToString("dddd, dd MMMM yyyy") : "--" },
                    { "FechaFinPago", (pacientefinanzas.FechaFinPago != "--") ? new DateTime(int.Parse(pacientefinanzas.FechaFinPago.Split('/')[2]),int.Parse(pacientefinanzas.FechaFinPago.Split('/')[1]),int.Parse(pacientefinanzas.FechaFinPago.Split('/')[0])).ToString("dddd, dd MMMM yyyy") : "--" },
                };

                SQL.transaccionSQL.Commit();
                return JsonConvert.SerializeObject(Contrato) + LogoCad;
            }
            catch(Exception e)
            {
                SQL.transaccionSQL.Rollback();
                return e.ToString();
            }
            finally
            {
                SQL.conSQL.Close();
            }
        }

        // FUNCION QUE REIMPRIME UN CONTRATO - PREREGISTRO [ PACIENTES ]
        public string ReimprimirContrato(int idpaciente, string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("ReimprimirContrato");
                string SiglaLegal = "", NombreCentro = "", ClaveCentro = "", DireccionCentro = "", CPCentro = "", ColoniaCentro = "", EstadoCentro = "", MunicipioCentro = "", Director = "", LogoCad = "";
                double TelefonoCentro = 0;
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.usuarioscentro WHERE id = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        SiglaLegal = lector["siglalegal"].ToString();
                        NombreCentro = lector["nombrecentro"].ToString();
                        ClaveCentro = lector["clavecentro"].ToString();
                        DireccionCentro = lector["direccion"].ToString();
                        CPCentro = lector["cp"].ToString();
                        ColoniaCentro = lector["colonia"].ToString();
                        EstadoCentro = lector["estado"].ToString();
                        MunicipioCentro = lector["municipio"].ToString();
                        Director = lector["nombredirector"].ToString();
                        TelefonoCentro = double.Parse(lector["telefono"].ToString());

                        if (bool.Parse(lector["logopersonalizado"].ToString()))
                        {
                            LogoCad = "«~LOGOPERS~»";
                        }
                        if (bool.Parse(lector["alanonlogo"].ToString()))
                        {
                            LogoCad = "«~LOGOALANON~»";
                        }
                    }
                }

                string NombrePaciente = "", ApellidoPacienteP = "", ApellidoPacienteM = "", SexoPaciente = "", EdadPaciente = "", ParienteNombre = "", ParienteApP = "", ParienteApM = "", Parentesco = "";
                DateTime FechaIngreso = new DateTime();
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.pacienteregistro WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND id = @IDPacienteDATA", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDPacienteDATA", SqlDbType.Int) { Value = idpaciente });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        NombrePaciente = lector["nombre"].ToString();
                        ApellidoPacienteP = lector["apellidopaterno"].ToString();
                        ApellidoPacienteM = lector["apellidomaterno"].ToString();
                        SexoPaciente = lector["sexo"].ToString();
                        FechaIngreso = DateTime.Parse(lector["fechahora"].ToString());
                        EdadPaciente = lector["edad"].ToString();
                        ParienteNombre = lector["parientenombre"].ToString();
                        ParienteApP = lector["parienteapellidop"].ToString();
                        ParienteApM = lector["parienteapellidom"].ToString();
                        Parentesco = lector["parentesco"].ToString();
                    }
                }

                string FolioContrato = "", TipoIngreso = "", TiempoEstancia = "", TipoEstancia = "", TestigoNombre = "", TipoTratamiento = "", FasesCantTratamiento = "", FasesTratamiento = "";
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.pacienteingreso WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND idpaciente = @IDPacienteDATA", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDPacienteDATA", SqlDbType.Int) { Value = idpaciente });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        FolioContrato = lector["foliocontrato"].ToString();
                        TipoIngreso = lector["tipoingreso"].ToString();
                        TiempoEstancia = lector["tiempoestancia"].ToString();
                        TipoEstancia = lector["tipoestancia"].ToString();
                        TestigoNombre = lector["nombretestigo"].ToString();
                        TipoTratamiento = lector["tipotratamiento"].ToString();
                        FasesCantTratamiento = lector["fasescantratamiento"].ToString();
                        FasesTratamiento = lector["fasestratamiento"].ToString();
                    }
                }

                string TipoMoneda = "PESOS MEXICANOS";
                double MontoPagar = 0, MontoPagoParcial = 0; bool Parcialidad = false; int CantidadPagos = 0; string TipoPago = "", FechaInicioPago = "", FechaFinPago = "";
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.pacienteregistrofinanzas WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND idpaciente = @IDPacienteDATA", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDPacienteDATA", SqlDbType.Int) { Value = idpaciente });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        MontoPagar = double.Parse(lector["montototal"].ToString());
                        Parcialidad = bool.Parse(lector["parcialidad"].ToString());
                        CantidadPagos = int.Parse(lector["cantidadpagos"].ToString());
                        MontoPagoParcial = double.Parse(lector["montopagoparcial"].ToString());
                        TipoPago = lector["tipopago"].ToString();
                        FechaInicioPago = (lector["fechainiciopago"].ToString() != "--") ? new DateTime(int.Parse(lector["fechainiciopago"].ToString().Split('/')[2]), int.Parse(lector["fechainiciopago"].ToString().Split('/')[1]), int.Parse(lector["fechainiciopago"].ToString().Split('/')[0])).ToString("dddd, dd MMMM yyyy") : lector["fechainiciopago"].ToString();
                        FechaFinPago = (lector["fechafinpago"].ToString() != "--") ? new DateTime(int.Parse(lector["fechafinpago"].ToString().Split('/')[2]), int.Parse(lector["fechafinpago"].ToString().Split('/')[1]), int.Parse(lector["fechafinpago"].ToString().Split('/')[0])).ToString("dddd, dd MMMM yyyy") : lector["fechafinpago"].ToString();
                    }
                }

                Dictionary<string, object> Contrato = new Dictionary<string, object>() {
                    { "TipoContrato", TipoIngreso },
                    { "FolioContrato", FolioContrato },
                    { "NombrePaciente", NombrePaciente.ToUpper() + " " + ApellidoPacienteP.ToUpper() + " " + ApellidoPacienteM.ToUpper() },
                    { "NombreCentro", NombreCentro.ToUpper() },
                    { "ClaveCentro", ClaveCentro.ToUpper() },
                    { "FechaIngreso",  FechaIngreso.ToString("dddd, dd MMMM yyyy HH:mm:ss") },
                    { "SexoPaciente", SexoPaciente.ToUpper() },
                    { "EdadPaciente", EdadPaciente.ToString() },
                    { "DomicilioDoc", DireccionCentro + " Colonia: " + ColoniaCentro + " - C.P. " + CPCentro + ", " + MunicipioCentro + ", " + EstadoCentro },
                    { "Estancia", TiempoEstancia + " " + TipoEstancia },
                    { "MontoPago", MontoPagar },
                    { "TipoMoneda", TipoMoneda },
                    { "FechaFirma",  MISC.FechaHoy().ToString("dddd, dd MMMM yyyy") },
                    { "Estado", EstadoCentro },
                    { "Municipio", MunicipioCentro },
                    { "NombreDirector", Director.ToUpper() },
                    { "Testigo",TestigoNombre.ToUpper() },
                    { "FamiliarNombre", ParienteNombre.ToUpper() + " " + ParienteApP.ToUpper() + " " + ParienteApM.ToUpper() },
                    { "Parentesco", Parentesco },
                    { "TipoTratamiento", TipoTratamiento },
                    { "NumeroExpediente", MISC.CadExpediente(idpaciente) },
                    { "FasesCantTratamiento", FasesCantTratamiento },
                    { "FasesTratamiento", FasesTratamiento },
                    { "Parcialidad", Parcialidad },
                    { "CantidadPagos", CantidadPagos },
                    { "MontoPagoParcial", MontoPagoParcial },
                    { "TipoPago", TipoPago },
                    { "FechaInicioPago", FechaInicioPago },
                    { "FechaFinPago", FechaFinPago },
                };

                SQL.transaccionSQL.Commit();
                return JsonConvert.SerializeObject(Contrato) + LogoCad;
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

        // FUNCION QUE ACTUALIZA LOS PARAMETROS DE PARAMETROS DE INGRESO DEL PACIENTE AL CENTRO [ INGRESO PACIENTES ]
        public string ActIngresoPaciente(PacienteIngreso pacienteingreso, string tokenusuario, string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("ActIngresoPaciente");
                SQL.commandoSQL = new SqlCommand("UPDATE dbo.pacienteregistro SET estatus = @EstatusDATA WHERE id = @IDPacienteDATA AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@EstatusDATA", SqlDbType.Int) { Value = 3 });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDPacienteDATA", SqlDbType.Int) { Value = pacienteingreso.IdPaciente });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.ExecuteNonQuery();

                SQL.commandoSQL = new SqlCommand("UPDATE dbo.pacienteingreso SET estadoalerta = @EstadoAlertaParam, estadoalertaindx = @EstadoAlertaIndxParam, nivelintoxicacion = @NivelIntoxicacionParam, nivelintoxicacionindx = @NivelIntoxicacionIndxParam, estadoanimo = @EstadoAnimoParam, estadoanimoindx = @EstadoAnimoIndxParam, fechahora = @FechaParam, admusuario = (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenUsuarioParam) WHERE id = @IDPacienteDATA AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SqlParameter[] actIngresoPaciente =
                {
                    new SqlParameter("@EstadoAlertaParam", SqlDbType.VarChar){Value = pacienteingreso.EstadoAlerta },
                    new SqlParameter("@EstadoAlertaIndxParam", SqlDbType.Int){Value = pacienteingreso.EstadoAlertaIndx },
                    new SqlParameter("@NivelIntoxicacionParam", SqlDbType.VarChar){Value = pacienteingreso.NivelIntoxicacion },
                    new SqlParameter("@NivelIntoxicacionIndxParam", SqlDbType.Int){Value = pacienteingreso.NivelIntoxicacionIndx },
                    new SqlParameter("@EstadoAnimoParam", SqlDbType.VarChar){Value = pacienteingreso.EstadoAnimo },
                    new SqlParameter("@EstadoAnimoIndxParam", SqlDbType.Int){Value = pacienteingreso.EstadoAnimoIndx },
                    new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                    new SqlParameter("@TokenUsuarioParam", SqlDbType.VarChar){Value = tokenusuario },
                    new SqlParameter("@IDPacienteDATA", SqlDbType.Int){Value = pacienteingreso.IdPaciente },
                    new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar){Value = tokencentro },
                };
                SQL.commandoSQL.Parameters.AddRange(actIngresoPaciente);
                SQL.commandoSQL.ExecuteNonQuery();

                SQL.commandoSQL = new SqlCommand("INSERT INTO dbo.pacienteevalucacioncoords (idcentro, idpaciente, fechahora, admusuario) VALUES ((SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroParam), @IDPacienteParam, @FechaParam, (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenUsuarioParam))", SQL.conSQL, SQL.transaccionSQL);
                SqlParameter[] registroPacientesCoords =
                {
                    new SqlParameter("@TokenCentroParam", SqlDbType.VarChar){Value = tokencentro },
                    new SqlParameter("@IDPacienteParam", SqlDbType.Int){Value = pacienteingreso.IdPaciente },
                    new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                    new SqlParameter("@TokenUsuarioParam", SqlDbType.VarChar){Value = tokenusuario }
                };
                SQL.commandoSQL.Parameters.AddRange(registroPacientesCoords);
                SQL.commandoSQL.ExecuteNonQuery();

                SQL.transaccionSQL.Commit();
                return "true";
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

        // FUNCION QUE DEVUELVE LA LISTA DE PACIENTES CON PAGOS PENDIENTES ( :: ADMINISTRACION ::)
        public string ListaPacientesPagosPendientes(string consulta, string tokencentro)
        {
            try
            {
                List<Dictionary<string, object>> ListaPacientesPagos = new List<Dictionary<string, object>>();
                SQL.comandoSQLTrans("PacientesPagosPend");
                SQL.commandoSQL = new SqlCommand("SELECT PR.*, PP.id AS idFinanzas, PP.montototal FROM dbo.pacienteregistro PR JOIN dbo.pacienteregistrofinanzas PP ON PP.idpaciente = PR.id WHERE PR.idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND PR.estatus > 1 AND (UPPER(PR.idpaciente) + ' ' + UPPER(PR.nombre) + ' ' + UPPER(PR.apellidopaterno) + ' ' + UPPER(PR.apellidomaterno)) LIKE @ConsultaDATA", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@ConsultaDATA", SqlDbType.VarChar) { Value = "%" + consulta + "%" });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Dictionary<string, object> Pacientes = new Dictionary<string, object>()
                        {
                            { "ClavePaciente", lector["idpaciente"].ToString() },
                            { "IdFinanzas", int.Parse(lector["idFinanzas"].ToString()) },
                            { "IdPaciente", int.Parse(lector["id"].ToString()) },
                            { "Nombre", lector["nombre"].ToString() },
                            { "ApellidoP", lector["apellidopaterno"].ToString() },
                            { "ApellidoM", lector["apellidomaterno"].ToString() },
                            { "NombreCompleto", lector["nombre"].ToString().ToUpper() + " " + lector["apellidopaterno"].ToString().ToUpper()+ " " + lector["apellidomaterno"].ToString().ToUpper() },
                            { "FechaRegistro", lector["fechahora"].ToString() },
                            { "Monto", double.Parse(lector["montototal"].ToString()) }
                        };
                        ListaPacientesPagos.Add(Pacientes);
                    }
                }

                SQL.transaccionSQL.Commit();
                return JsonConvert.SerializeObject(ListaPacientesPagos);
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

        // FUNCION QUE DEVUELVE LA INFORMACION GLOBAL DE UN PACIENTE (PARA POSIBLES REPORTES GENERALES)
        public Dictionary<string, object> ObtenerDatosGlobalesPaciente(int idpaciente, string tokencentro)
        {
            var datosGlobalesPaciente = new Dictionary<string, object>();
            try
            {
                SQL.comandoSQLTrans("DatosGlobalesPaciente");
                string SiglaLegal = "", NombreCentro = "", ClaveCentro = "", DireccionCentro = "", CPCentro = "", ColoniaCentro = "", EstadoCentro = "", MunicipioCentro = "", Director = "", LogoCad = "";
                double TelefonoCentro = 0;
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.usuarioscentro WHERE id = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        SiglaLegal = lector["siglalegal"].ToString();
                        NombreCentro = lector["nombrecentro"].ToString();
                        ClaveCentro = lector["clavecentro"].ToString();
                        DireccionCentro = lector["direccion"].ToString();
                        CPCentro = lector["cp"].ToString();
                        ColoniaCentro = lector["colonia"].ToString();
                        EstadoCentro = lector["estado"].ToString();
                        MunicipioCentro = lector["municipio"].ToString();
                        Director = lector["nombredirector"].ToString();
                        TelefonoCentro = double.Parse(lector["telefono"].ToString());

                        if (bool.Parse(lector["logopersonalizado"].ToString()))
                        {
                            LogoCad = "«~LOGOPERS~»";
                        }
                        if (bool.Parse(lector["alanonlogo"].ToString()))
                        {
                            LogoCad = "«~LOGOALANON~»";
                        }
                    }
                }

                var PacienteRegistro = new List<Dictionary<string, object>>();
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.pacienteregistro WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND id = @IDPacienteDATA", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDPacienteDATA", SqlDbType.Int) { Value = idpaciente });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        var Elemento = new Dictionary<string, object>();
                        for (int col = 0; col < lector.FieldCount; col++)
                        {
                            Elemento.Add(lector.GetName(col), lector.GetValue(col));
                        }
                        PacienteRegistro.Add(Elemento);
                    }
                }

                var PacienteDatosGenerales = new List<Dictionary<string, object>>();
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.pacientedatosgenerales WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND idpaciente = @IDPacienteDATA", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDPacienteDATA", SqlDbType.Int) { Value = idpaciente });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        var Elemento = new Dictionary<string, object>();
                        for (int col = 0; col < lector.FieldCount; col++)
                        {
                            Elemento.Add(lector.GetName(col), lector.GetValue(col));
                        }
                        PacienteDatosGenerales.Add(Elemento);
                    }
                }

                var PacienteIngreso = new List<Dictionary<string, object>>();
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.pacienteingreso WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND idpaciente = @IDPacienteDATA", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDPacienteDATA", SqlDbType.Int) { Value = idpaciente });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        var Elemento = new Dictionary<string, object>();
                        for (int col = 0; col < lector.FieldCount; col++)
                        {
                            Elemento.Add(lector.GetName(col), lector.GetValue(col));
                        }
                        PacienteIngreso.Add(Elemento);
                    }
                }

                datosGlobalesPaciente = new Dictionary<string, object>()
                {
                    { "Logo", LogoCad },
                    { "PacienteRegistro", PacienteRegistro },
                    { "PacienteDatosGenerales", PacienteDatosGenerales },
                    { "PacienteIngreso", PacienteIngreso },
                };

                SQL.transaccionSQL.Commit();
                return datosGlobalesPaciente;
            }
            catch (Exception e)
            {
                SQL.transaccionSQL.Rollback();
                datosGlobalesPaciente = new Dictionary<string, object>()
                {
                    { "Logo", "«~LOGOALANON~»" },
                    { "Error", e.ToString() },
                };
                return datosGlobalesPaciente;
            }
            finally
            {
                SQL.conSQL.Close();
            }
        }

        // ::::::::::::::::::::::::::: [ HORARIOS ] :::::::::::::::::::::::::::
        // FUNCION QUE DEVUELVE LA LISTA DE HORARIOS [ HORARIOS ]
        public string ObtenerListaHorarios(string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("ListaHorarios");
                List<Horarios> HorariosData = new List<Horarios>();
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.horarios WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        HorariosData.Add(new Horarios()
                        {
                            IdHorario = int.Parse(lector["id"].ToString()),
                            Descripcion = lector["descripcion"].ToString(),
                            HoraInicio = lector["horainicio"].ToString(),
                            Duracion = int.Parse(lector["duracion"].ToString()),
                            Tipo = lector["tipo"].ToString(),
                            Reloj = lector["reloj"].ToString(),
                            Activo = bool.Parse(lector["activo"].ToString()),
                            FechaCreado = DateTime.Parse(lector["fechahora"].ToString()),
                        });
                    }
                }

                List<Dictionary<string, object>> ListaHorarios = new List<Dictionary<string, object>>();
                foreach (Horarios Horario in HorariosData)
                {
                    List<Dictionary<string, object>> horarioConfig = new List<Dictionary<string, object>>();
                    SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.horariosconfig WHERE idhorario = @IDHorarioParam AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) ORDER BY numorden ASC", SQL.conSQL, SQL.transaccionSQL);
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDHorarioParam", SqlDbType.Int) { Value = Horario.IdHorario });
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                    using (var lector = SQL.commandoSQL.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            horarioConfig.Add(new Dictionary<string, object>()
                            {
                                { "IdHTML", lector["idhtml"].ToString() },
                                { "HoraInicio24hrs", lector["hrinicio24hrs"].ToString() },
                                { "HoraInicio12hrs", lector["hrinicio12hrs"].ToString() },
                                { "HoraTermino24hrs", lector["hrtermino24hrs"].ToString() },
                                { "HoraTermino12hrs", lector["hrtermino12hrs"].ToString() },
                                { "Lunes", lector["lunes"].ToString() },
                                { "Martes", lector["martes"].ToString() },
                                { "Miercoles", lector["miercoles"].ToString() },
                                { "Jueves", lector["jueves"].ToString() },
                                { "Viernes", lector["viernes"].ToString() },
                                { "Sabado", lector["sabado"].ToString() },
                                { "Domingo", lector["domingo"].ToString() },
                                { "Receso",  bool.Parse(lector["receso"].ToString()) },
                                { "NumOrden", int.Parse(lector["numorden"].ToString()) },
                            });
                        }
                    }
                    ListaHorarios.Add(new Dictionary<string, object>()
                    {
                        { "IdHorario", Horario.IdHorario },
                        { "Descripcion", Horario.Descripcion },
                        { "HoraInicio", Horario.HoraInicio },
                        { "Duracion", Horario.Duracion },
                        { "Tipo", Horario.Tipo },
                        { "Reloj", Horario.Reloj },
                        { "FechaCreado", Horario.FechaCreado.ToString("dddd, dd MMMM yyyy HH:mm:ss") },
                        { "Activo", Horario.Activo },
                        { "HorarioConfig", horarioConfig },
                    });
                }

                SQL.transaccionSQL.Commit();
                return JsonConvert.SerializeObject(ListaHorarios);
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

        // FUNCION QUE GUARDA LOS DATOS DEL HORARIO [ HORARIOS ]
        public string GuardarHorario(Horarios horarioinfo, HorariosConfig[] horarioconfig, string tokenusuario, string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("GuardarHorario");
                int IdHorarioVerif = 0, IdHorarioConfig = 0;
                bool activo = false, activoVerif = false;
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.horarios WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        if (bool.Parse(lector["activo"].ToString()))
                        {
                            activoVerif = true;
                            IdHorarioVerif = int.Parse(lector["id"].ToString());
                        }
                    }
                }
                if(!activoVerif || IdHorarioVerif == horarioinfo.IdHorario)
                {
                    activo = true;
                }

                if (horarioinfo.IdHorario > 0)
                {
                    SQL.commandoSQL = new SqlCommand("UPDATE dbo.horarios SET descripcion = @DescripcionParam, horainicio = @HoraInicioParam, duracion = @DuracionParam, tipo = @TipoParam, reloj = @RelojParam, activo = @ActivoParam, fechahora = @FechaParam, admusuario = (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenUsuarioParam) WHERE id = @IDHorarioParam AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                    SqlParameter[] actHorario =
                    {
                        new SqlParameter("@DescripcionParam", SqlDbType.VarChar){Value = horarioinfo.Descripcion },
                        new SqlParameter("@HoraInicioParam", SqlDbType.VarChar){Value = horarioinfo.HoraInicio },
                        new SqlParameter("@DuracionParam", SqlDbType.Int){Value = horarioinfo.Duracion },
                        new SqlParameter("@TipoParam", SqlDbType.VarChar){Value = horarioinfo.Tipo },
                        new SqlParameter("@RelojParam", SqlDbType.VarChar){Value = horarioinfo.Reloj },
                        new SqlParameter("@ActivoParam", SqlDbType.Bit){Value = activo },
                        new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                        new SqlParameter("@TokenUsuarioParam", SqlDbType.VarChar){Value = tokenusuario },
                        new SqlParameter("@IDHorarioParam", SqlDbType.Int){Value = horarioinfo.IdHorario },
                        new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar){Value = tokencentro },
                    };
                    SQL.commandoSQL.Parameters.AddRange(actHorario);
                    SQL.commandoSQL.ExecuteNonQuery();

                    SQL.commandoSQL = new SqlCommand("DELETE FROM dbo.horariosconfig WHERE idhorario = @IDHorarioParam AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDHorarioParam", SqlDbType.Int) { Value = horarioinfo.IdHorario });
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                    SQL.commandoSQL.ExecuteNonQuery();

                    IdHorarioConfig = horarioinfo.IdHorario;
                }
                else
                {
                    SQL.commandoSQL = new SqlCommand("INSERT INTO dbo.horarios (idcentro, descripcion, horainicio, duracion, tipo, reloj, activo, fechahora, admusuario) VALUES ((SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA), @DescripcionParam, @HoraInicioParam, @DuracionParam, @TipoParam, @RelojParam, @ActivoParam, @FechaParam, (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenUsuarioParam))", SQL.conSQL, SQL.transaccionSQL);
                    SqlParameter[] nuevoHorario =
                    {
                        new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar){Value = tokencentro },
                        new SqlParameter("@DescripcionParam", SqlDbType.VarChar){Value = horarioinfo.Descripcion },
                        new SqlParameter("@HoraInicioParam", SqlDbType.VarChar){Value = horarioinfo.HoraInicio },
                        new SqlParameter("@DuracionParam", SqlDbType.Int){Value = horarioinfo.Duracion },
                        new SqlParameter("@TipoParam", SqlDbType.VarChar){Value = horarioinfo.Tipo },
                        new SqlParameter("@RelojParam", SqlDbType.VarChar){Value = horarioinfo.Reloj },
                        new SqlParameter("@ActivoParam", SqlDbType.Bit){Value = activo },
                        new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                        new SqlParameter("@TokenUsuarioParam", SqlDbType.VarChar){Value = tokenusuario },
                    };
                    SQL.commandoSQL.Parameters.AddRange(nuevoHorario);
                    SQL.commandoSQL.ExecuteNonQuery();

                    SQL.commandoSQL = new SqlCommand("SELECT MAX(id) AS Maximo FROM dbo.horarios WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                    using (var lector = SQL.commandoSQL.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            IdHorarioConfig = int.Parse(lector["Maximo"].ToString());
                        }
                    }
                }

                if (horarioconfig != null)
                {
                    foreach(HorariosConfig horario in horarioconfig)
                    {
                        SQL.commandoSQL = new SqlCommand("INSERT INTO dbo.horariosconfig (idcentro, idhorario, idhtml, hrinicio24hrs, hrinicio12hrs, hrtermino24hrs, hrtermino12hrs, lunes, martes, miercoles, jueves, viernes, sabado, domingo, receso, numorden, fechahora, admusuario) VALUES ((SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA), @IDHorarioParam, @IDHTMLParam, @HRInicio24hrs, @HRInicio12hrs, @HRTermino24hrs, @HRTermino12hrs, @LunesParam, @MartesParam, @MiercolesParam, @JuevesParam, @ViernesParam, @SabadoParam, @DomingoParam, @RecesoParam, @NumOrdenParam, @FechaParam, (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenUsuarioParam))", SQL.conSQL, SQL.transaccionSQL);
                        SqlParameter[] agregarHorarioConfig =
                        {
                            new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar){Value = tokencentro },
                            new SqlParameter("@IDHorarioParam", SqlDbType.Int){Value = IdHorarioConfig },
                            new SqlParameter("@IDHTMLParam", SqlDbType.VarChar){Value = horario.IdHTML },
                            new SqlParameter("@HRInicio24hrs", SqlDbType.VarChar){Value = horario.HoraInicio24hrs },
                            new SqlParameter("@HRInicio12hrs", SqlDbType.VarChar){Value = horario.HoraInicio12hrs },
                            new SqlParameter("@HRTermino24hrs", SqlDbType.VarChar){Value = horario.HoraTermino24hrs },
                            new SqlParameter("@HRTermino12hrs", SqlDbType.VarChar){Value = horario.HoraTermino12hrs },
                            new SqlParameter("@LunesParam", SqlDbType.VarChar){Value = horario.Lunes },
                            new SqlParameter("@MartesParam", SqlDbType.VarChar){Value = horario.Martes },
                            new SqlParameter("@MiercolesParam", SqlDbType.VarChar){Value = horario.Miercoles },
                            new SqlParameter("@JuevesParam", SqlDbType.VarChar){Value = horario.Jueves },
                            new SqlParameter("@ViernesParam", SqlDbType.VarChar){Value = horario.Viernes },
                            new SqlParameter("@SabadoParam", SqlDbType.VarChar){Value = horario.Sabado },
                            new SqlParameter("@DomingoParam", SqlDbType.VarChar){Value = horario.Domingo },
                            new SqlParameter("@RecesoParam", SqlDbType.Bit){Value = horario.Receso },
                            new SqlParameter("@NumOrdenParam", SqlDbType.Int){Value = horario.NumOrden },
                            new SqlParameter("@FechaParam", SqlDbType.DateTime){Value = MISC.FechaHoy() },
                            new SqlParameter("@TokenUsuarioParam", SqlDbType.VarChar){Value = tokenusuario }, 
                        };
                        SQL.commandoSQL.Parameters.AddRange(agregarHorarioConfig);
                        SQL.commandoSQL.ExecuteNonQuery();
                    }
                }

                SQL.transaccionSQL.Commit();
                return "true";
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

        // FUNCION QUE ACTIVA (Y DESACTIVA) HORARIOS [ HORARIOS ]
        public string ActivarHorario(int idhorario, string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("ActivarHorario");
                SQL.commandoSQL = new SqlCommand("UPDATE dbo.horarios SET activo = @ActivoParam WHERE id <> @IDHorarioParam AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDHorarioParam", SqlDbType.Int) { Value = idhorario });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@ActivoParam", SqlDbType.Int) { Value = false });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.ExecuteNonQuery();

                SQL.commandoSQL = new SqlCommand("UPDATE dbo.horarios SET activo = @ActivoParam WHERE id = @IDHorarioParam AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDHorarioParam", SqlDbType.Int) { Value = idhorario });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@ActivoParam", SqlDbType.Int) { Value = true });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.ExecuteNonQuery();

                SQL.transaccionSQL.Commit();
                return "true";
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

        // FUNCION QUE BORRA UN HORARIO [ HORARIOS ]
        public string BorrarHorario(int idhorario, string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("BorrarHorario");
                bool eraActivo = false;
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.horarios WHERE id = @IDHorarioParam AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDHorarioParam", SqlDbType.Int) { Value = idhorario });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        if (bool.Parse(lector["activo"].ToString()))
                        {
                            eraActivo = true;
                        }
                    }
                }

                SQL.commandoSQL = new SqlCommand("DELETE FROM dbo.horarios WHERE id = @IDHorarioParam AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDHorarioParam", SqlDbType.Int) { Value = idhorario });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.ExecuteNonQuery();

                if (eraActivo)
                {
                    SQL.commandoSQL = new SqlCommand("UPDATE dbo.horarios SET activo = @ActivoParam WHERE id <> @IDHorarioParam AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@IDHorarioParam", SqlDbType.Int) { Value = idhorario });
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@ActivoParam", SqlDbType.Int) { Value = false });
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                    SQL.commandoSQL.ExecuteNonQuery();

                    SQL.commandoSQL = new SqlCommand("UPDATE dbo.horarios SET activo = @ActivoParam WHERE id = (SELECT MIN(id) AS Minimo FROM dbo.horarios WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)) AND idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@ActivoParam", SqlDbType.Int) { Value = true });
                    SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                    SQL.commandoSQL.ExecuteNonQuery();
                }

                SQL.transaccionSQL.Commit();
                return "true";
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

        // FUNCION QUE  DEVUELVE LA ESTRUCTURA DE UN HORARIO SEMANAL  EN CURSO [ HORARIOS ]
        public string CrearHorarioSemanal(string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("HorarioSemanal");
                DateTime Hoy = MISC.FechaHoy();
                List<string> FechasArray = MISC.FechaDiaArr();
                List<HorarioSemanalAG> ListaHorarioSemanalAG = new List<HorarioSemanalAG>();
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.actividadesgrupales WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND fechainicio BETWEEN @FechaIniParam AND @FechaFinParam", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@FechaIniParam", SqlDbType.DateTime) { Value = new DateTime(int.Parse(FechasArray[0].Split(' ')[2].Split('/')[2]), int.Parse(FechasArray[0].Split(' ')[2].Split('/')[0]), int.Parse(FechasArray[0].Split(' ')[2].Split('/')[1])).AddDays(-1) });
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@FechaFinParam", SqlDbType.DateTime) { Value = new DateTime(int.Parse(FechasArray[6].Split(' ')[2].Split('/')[2]), int.Parse(FechasArray[6].Split(' ')[2].Split('/')[0]), int.Parse(FechasArray[6].Split(' ')[2].Split('/')[1])).AddDays(1) });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        string[] FechaI = DateTime.Parse(lector["fechainicio"].ToString()).ToString("dd/MM/yyyy").Split('/');
                        string[] FechaF = DateTime.Parse(lector["fechafin"].ToString()).ToString("dd/MM/yyyy").Split('/');
                        ListaHorarioSemanalAG.Add(new HorarioSemanalAG()
                        {
                            Nombre = lector["nombre"].ToString(),
                            Coordinacion = lector["coordinacion"].ToString(),
                            FechasArray = MISC.FechasArrString(new DateTime(int.Parse(FechaI[2]), int.Parse(FechaI[1]), int.Parse(FechaI[0])), new DateTime(int.Parse(FechaF[2]), int.Parse(FechaF[1]), int.Parse(FechaF[0]))),
                        });
                    }
                }

                List<Dictionary<string, object>> ListaHorariosConfig = new List<Dictionary<string, object>>();
                string Reloj = "";
                SQL.commandoSQL = new SqlCommand("SELECT HC.*, H.reloj from dbo.horariosconfig HC JOIN dbo.horarios H ON H.id = HC.idhorario AND H.activo = 'true' WHERE HC.idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) ORDER BY HC.numorden ASC", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        List<string> actsArray = new List<string>();
                        if (!bool.Parse(lector["receso"].ToString()))
                        {
                            foreach(string fecha in FechasArray)
                            {
                                string[] fechaArr = fecha.Split(' ');
                                string valorAct = "";
                                if (lector[fechaArr[0]].ToString() != "-")
                                {
                                    foreach (HorarioSemanalAG Horario in ListaHorarioSemanalAG)
                                    {
                                        if(Horario.Coordinacion == lector[fechaArr[0]].ToString() && Horario.FechasArray.Contains(fechaArr[2]))
                                        {
                                            valorAct = "\n(" + Horario.Nombre + ")";
                                        }
                                    }
                                }
                                actsArray.Add(valorAct);
                            }
                        }

                        Reloj = lector["reloj"].ToString();
                        ListaHorariosConfig.Add(new Dictionary<string, object>()
                        {
                            { "IdHTML", lector["idhtml"].ToString() },
                            { "HoraInicio24hrs", lector["hrinicio24hrs"].ToString() },
                            { "HoraInicio12hrs", lector["hrinicio12hrs"].ToString() },
                            { "HoraTermino24hrs", lector["hrtermino24hrs"].ToString() },
                            { "HoraTermino12hrs", lector["hrtermino12hrs"].ToString() },
                            { "Lunes", lector["lunes"].ToString() },
                            { "Martes", lector["martes"].ToString() },
                            { "Miercoles", lector["miercoles"].ToString() },
                            { "Jueves", lector["jueves"].ToString() },
                            { "Viernes", lector["viernes"].ToString() },
                            { "Sabado", lector["sabado"].ToString() },
                            { "Domingo", lector["domingo"].ToString() },
                            { "Receso",  bool.Parse(lector["receso"].ToString()) },
                            { "NumOrden", int.Parse(lector["numorden"].ToString()) },
                            { "LunesAct", (actsArray.Count > 0) ? actsArray[0] : "" },
                            { "MartesAct", (actsArray.Count > 0) ? actsArray[1] : "" },
                            { "MiercolesAct", (actsArray.Count > 0) ? actsArray[2] : "" },
                            { "JuevesAct", (actsArray.Count > 0) ? actsArray[3] : "" },
                            { "ViernesAct",(actsArray.Count > 0) ? actsArray[4] : "" },
                            { "SabadoAct", (actsArray.Count > 0) ? actsArray[5] : "" },
                            { "DomingoAct",(actsArray.Count > 0) ? actsArray[6] : "" },
                        });
                    }
                }

                string[] FechasArrPack = {
                    FechasArray[0].Split(' ')[2],
                    FechasArray[1].Split(' ')[2],
                    FechasArray[2].Split(' ')[2],
                    FechasArray[3].Split(' ')[2],
                    FechasArray[4].Split(' ')[2],
                    FechasArray[5].Split(' ')[2],
                    FechasArray[6].Split(' ')[2],
                };
                Dictionary<string, object> HorarioSemanalData = new Dictionary<string, object>()
                {
                    { "HorarioConfig", ListaHorariosConfig },
                    { "Reloj", Reloj },
                    { "TituloHorario", "HORARIO DE ACTIVIDADES\n" +  MISC.FechasSemanalString(FechasArray[0].Split(' ')[2], FechasArray[6].Split(' ')[2]) },
                    { "FechasArray", FechasArrPack },
                };

                SQL.transaccionSQL.Commit();
                return JsonConvert.SerializeObject(HorarioSemanalData);
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


        // ------------------ [ FUNCIONES COMPLEMENTARIOS ] ------------------
        // FUNCION QUE DEVUELVE LOS PARAMETROS DEL CENTRO PARA COMPLEMENTAR UN DOCUMENTO
        public Dictionary<string, object> DocCentroInfo(string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("DocCentroInfo");
                Dictionary<string, object> CentroInfo = new Dictionary<string, object>();
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.usuarioscentro WHERE id = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA)", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        CentroInfo = new Dictionary<string, object>()
                        {
                            { "SiglaLegal",lector["siglalegal"].ToString() },
                            { "NombreCentro", lector["nombrecentro"].ToString() },
                            { "ClaveCentro", lector["clavecentro"].ToString() },
                            { "DireccionCentro", lector["direccion"].ToString() },
                            { "CPCentro", lector["cp"].ToString() },
                            { "ColoniaCentro", lector["colonia"].ToString() },
                            { "EstadoCentro", lector["estado"].ToString() },
                            { "MunicipioCentro", lector["municipio"].ToString() },
                            { "Director", lector["nombredirector"].ToString() },
                            { "TelefonoCentro", double.Parse(lector["telefono"].ToString()) },
                            { "LogoCentro", (bool.Parse(lector["logopersonalizado"].ToString())) ? "«~LOGOPERS~»" : "«~LOGOALANON~»" },
                        };
                    }
                }

                SQL.transaccionSQL.Commit();
                return CentroInfo;
            }
            catch (Exception e)
            {
                SQL.transaccionSQL.Rollback();
                Dictionary<string, object> Error = new Dictionary<string, object>() {
                    { "Error",  e.ToString() },
                };
                return Error;
            }
            finally
            {
                SQL.conSQL.Close();
            }
        }
    }
}