using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace siuraWEB
{
    public class MISC
    {
        // :::::::::: *************** FUNCIONES EXTRAS  *************** ::::::::::
        // FUNCION QUE CREA UNA CADENA MD5
        public static string CrearMD5(string cadena)
        {
            StringBuilder constructor = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] dato = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(cadena));
                for (int i = 0; i < dato.Length; i++)
                {
                    constructor.Append(dato[i].ToString("x2"));
                }
            }
            return constructor.ToString();
        }

        // FUNCION QUE CREA UNA CADENA DE CARACTERES LARGA PARA EL USO DE REALIZAR TOKENS
        public static string CrearIdSession()
        {
            string IdSession = ""; Int64 cadFecha = 0; string cadFechaTXT = ""; Random num = new Random();
            var f = new DateTime(1970, 1, 1);
            TimeSpan t = (DateTime.Now.ToUniversalTime() - f);
            cadFecha = (Int64)(t.TotalMilliseconds + 0.5);
            cadFechaTXT = cadFecha.ToString();
            foreach (char c in cadFechaTXT)
            {
                int l = num.Next(0, 26);
                char let = (char)('a' + l);
                IdSession += c + "" + let;
            }
            return IdSession;
        }

        // FUNCION QUE CREA UNA CADENA ALEATORIA 
        // [ TIPO::: 1 - 3 NUMEROS , 2 - ALFANUMERICA (CUALQUIER TAMAÑO) , 3 - SOLO NUMEROS (CUALQUIER TAMAÑO) , 4 - SOLO LETRAS (CUALQUIER TAMAÑO) ]
        public static string CrearCadAleatoria(int tipo, int longitud)
        {
            Random aleatorio = new Random();
            string cadenaRetorno = "";
            if (tipo == 1)
            {
                int NumAl = aleatorio.Next(100, 999);
                cadenaRetorno = NumAl.ToString();
            }
            else if (tipo == 2)
            {
                const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                cadenaRetorno = new string(Enumerable.Range(1, longitud).Select(_ => caracteres[aleatorio.Next(caracteres.Length)]).ToArray());
            }
            else if (tipo == 3)
            {
                const string caracteres = "0123456789";
                cadenaRetorno = new string(Enumerable.Repeat(caracteres, longitud).Select(s => s[aleatorio.Next(s.Length)]).ToArray());
            }
            else if (tipo == 4)
            {
                const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                cadenaRetorno = new string(Enumerable.Repeat(caracteres, longitud).Select(s => s[aleatorio.Next(s.Length)]).ToArray());
            }
            return cadenaRetorno;
        }

        // FUNCION QUE GENERAUNA CADENA ALEATORIA (EXPERIEMNTAL)
        public static string GenerarCadAleatoria(int length, Random random)
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        // FUNCION QUE DEVUELVE UNA CAD TIPO "000" PARA EXPENDIENTES
        public static string CadExpediente(int num)
        {
            if(num < 10)
            {
                return "00" + num.ToString();
            }
            else if(num < 100)
            {
                return "0" + num.ToString();
            }
            else
            {
                return num.ToString();
            }
        }

        // FUNCION QUE DEVUELVE LA FECHA DE HOY
        public static DateTime FechaHoy()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
        }

        // FUNCION QUE DEVUELVE EL COLOR EN FORMATO DE HTML PARA LAS COORDINACIONES (USADO PARA EL CALENDARIO)
        public static string ColorCalendarioHTML(string coordcad, int accion)
        {
            string Valor = "";
            string[] Coords = { "CA", "CD", "CM", "CP", "CE", "CC" },
                CoordColores = { "#5DADE2", "#EC7063", "#52BE80", "#DC7633", "#A569BD", "#F4D03F" },
                CoordNombres = { "AL-ANON", "COORD. DEPORTIVA", "COORD. MÉDICA", "COORD. PSICOLÓGICA", "COORD. ESPIRITUAL", "COORD. CONSEJERÍA" };
            if(accion == 1)
            {
                Valor = CoordColores[Array.IndexOf(Coords, coordcad)];
            }
            else if(accion == 2)
            {
                Valor = CoordNombres[Array.IndexOf(Coords, coordcad)];
            }
            return Valor;
        }

        // FUNCION QUE DEVUELVE 2 FECHAS PARA LA CONSULTA DE CALENDARIO (EXCLUSIVO DEL CALENDARIO)
        public static DateTime[] FechasArr()
        {
            DateTime Hoy = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")),
                MesAux = Hoy.AddMonths(2),
                Ini = new DateTime(Hoy.Year, Hoy.Month, 1).AddDays(-1), Fin = new DateTime(Hoy.Year, MesAux.Month, 1).AddDays(-1);
            DateTime[] Fechas = { Ini, Fin };
            return Fechas;
        }

        // FUNCION QUE DEVUELVE UN ARRAY CON FECHAS EN STRING CON UN RANGO DE 2 FECHAS
        public static List<string> FechasArrString(DateTime fini, DateTime ffin)
        {
            List<string> fechasArr = new List<string>() {
                fini.ToString("MM/dd/yyyy")
            };
            var dias = (ffin - fini).Days;
            if(dias > 1)
            {
                for(int i = 0; i < dias; i++)
                {
                    fini = fini.AddDays(1);
                    fechasArr.Add(fini.ToString("MM/dd/yyyy"));
                }
            }
            else
            {
                if (dias > 0)
                {
                    fechasArr.Add(ffin.ToString("MM/dd/yyyy"));
                }
            }
            return fechasArr;
        }

        // FUNCION SENCILLA QUE DEVUELVE UN TEXTO DESCRIPTIVO ENTRE FECHAS (PARA EL HORARIO SEMANAL) [ DEBE TENER LA STRING UNA FORMA ESPECIAL ]
        public static string FechasSemanalString(string fini, string ffin)
        {
            string fechaReturn = "";
            DateTime FIni = new DateTime(int.Parse(fini.Split('/')[2]), int.Parse(fini.Split('/')[0]), int.Parse(fini.Split('/')[1])),
                FFin = new DateTime(int.Parse(ffin.Split('/')[2]), int.Parse(ffin.Split('/')[0]), int.Parse(ffin.Split('/')[1]));
            if(FIni.ToString("MMMM").ToUpper() != FFin.ToString("MMMM").ToUpper())
            {
                fechaReturn = "DEL " + fini.Split('/')[1] + " DE " + FIni.ToString("MMMM").ToUpper() + " AL " + ffin.Split('/')[1] + " DE " + FFin.ToString("MMMM").ToUpper() + " DEL " + FFin.ToString("yyyy");
            }
            else
            {
                fechaReturn = "DEL " + fini.Split('/')[1] + " AL " + ffin.Split('/')[1] + " DE " + FFin.ToString("MMMM").ToUpper() + " DEL " + FFin.ToString("yyyy");
            }
            return fechaReturn;
        }

        // FUNCION QUE COMPRARA DOS FECHAS CON LA ACTUAL Y DEVUELVE SI O NO, AL ESTAR DENTRO DEL RANGO
        public static bool FechasRango(DateTime fini, DateTime ffin)
        {
            bool respuesta = false;
            DateTime Hoy = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")),
                Comparar = new DateTime(Hoy.Year, Hoy.Month, Hoy.Day),
                FechaI = new DateTime(fini.Year, fini.Month, fini.Day),
                FechaF = new DateTime(ffin.Year, ffin.Month, ffin.Day);
            if(Comparar >= FechaI && Comparar <= FechaF)
            {
                respuesta = true;
            }
            return respuesta;
        }

        // FUNCION QUE DEVUELVE DE MANERA GLOBAL LA ESTRUCTURA DEL  HTML CORREO
        public static string MailHTML()
        {
            return "<!DOCTYPE html><html lang='es'><head><meta charset='UTF-8' /></head><body style='font-family: -apple-system,BlinkMacSystemFont,Roboto,Arial,sans-serif;'><center><div align='left' style='width: 600px; position: relative;display: -webkit-box;display: -ms-flexbox;display: flex;-webkit-box-orient: vertical;-webkit-box-direction: normal;-ms-flex-direction: column;flex-direction: column;min-width: 0;word-wrap: break-word;background-color: #fff;background-clip: border-box;border: 1px solid rgba(0,0,0,.125);border-radius: .25rem;'><div style='-webkit-box-flex: 1;-ms-flex: 1 1 auto;flex: 1 1 auto;padding: 1.25rem;'><img src='https://drive.google.com/uc?export=view&id=1JcTqF6LPm_9TY-rCZcbS3-i2WFkHINmd' style='height: 50px;' /><br /><hr />×ØCUERPOMAILØ×<br /><h6>Correo generado automáticamente desde el sistema SIURA &copy;" + DateTime.Now.ToString("yyyy") + ". No es necesario contestar a este correo, ya que no recibirá respuesta. Si desea información, consulte con el usuario administrador de su centro.</h6></div></div></center></body></html>";
        }

        // FUNCION QUE DEDEVUELVE UN NUEVO CODIGO PARA ALTA DE INVENTARIO
        public static string CodigoInventario(string area, int numeroarticulo)
        {
            string NuevoCodigo = "";
            if (area == "CD")
            {
                NuevoCodigo = "ICD";
            }
            else if (area == "CM")
            {
                NuevoCodigo = "ICM";
            }
            else if (area == "CP")
            {
                NuevoCodigo = "ICP";
            }
            else if (area == "INSUMOS")
            {
                NuevoCodigo = "IIG";
            }
            return NuevoCodigo + numeroarticulo.ToString();
        }

        // FUNCION QUE DEVUELVE EN TEXTO COMPLETO EL TIPO DE AREA PARA EL INVENTARIO DE ACUERDO A SU SIGLA
        public static string CodigoInventarioTxt(string areasigla)
        {
            string Area = "";
            if (areasigla == "CD")
            {
                Area = "COORD. DEPORTIVA";
            }
            else if (areasigla == "CM")
            {
                Area = "COORD. MÉDICA";
            }
            else if (areasigla == "CP")
            {
                Area = "COORD. PSICOLÓGICA";
            }
            else if (areasigla == "INSUMOS")
            {
                Area = "INSUMOS GENERALES";
            }
            return Area;
        }

        // FUNCION QUE DEVUELVE LAS CELDAS DE LA TABLA PARA EL REPORTE DE INVENTARIO DE ACUERDO  A  LA GESTION
        public static string[] CeldasReporteInventario(string gestion)
        {
            string[] Celdas = { };
            if(gestion == "G1" || gestion == "G2")
            {
                Celdas = new string[]{ "CODIGOøA", "NOMBREøB", "PRESENTACIÓNøC", "PRECIO COMPRAøD", "PRECIO VENTAøE", "EXISTENCIASøF", "STOCK/MINIMOSøG" };
            }
            else if (gestion == "E1" || gestion == "E2" || gestion == "E3")
            {
                Celdas = new string[] { "CODIGOøA", "NOMBREøB", "PRESENTACIÓNøC", "ACCIONøD", "CANTIDADøE", "NOMBRE USUARIOøF", "FECHA Y HORAøG" };
            }
            return Celdas;
        }

        // FUNCION QUE DEVUELVE UN NOMBRE DEL REPORTE DE INVENTARIO DE ACUERDO A SU GESTION
        public static string[] InventarioNombreGestion(string gestion)
        {
            string[] Data = { };
            if (gestion == "G1")
            {
                Data = new string[]{ "Inventario", "Lista de Inventario General" };
            }
            else if (gestion == "G2")
            {
                Data = new string[] { "Stock", "Lista de Inventario (Debajo del Stock/Minimo)" };
            }
            else if (gestion == "E1")
            {
                Data = new string[] { "EntradasSalidas", "Lista de Entradas y Salidas" };
            }
            else if (gestion == "E2")
            {
                Data = new string[] { "Entradas", "Lista de Entradas" };
            }
            else if (gestion == "E3")
            {
                Data = new string[] { "Salidas", "Lista de Salidas" };
            }
            return Data;
        }

        // FUNCION QUE CREA EL ARRAY CON LA LISTA DE LETRAS PARA EL DOC DE EXCEL - 30
        public static string[] CrearExcelEncabezadosLetras(int total)
        {
            List<string> letrasLista = new List<string>();
            string[] letrasArr = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string letraBase = "";
            int limiteGral = 0, addLetra = 0;
            double grupos = Math.Ceiling((double)total / letrasArr.Length);
            for (int i = 0; i < grupos; i++)
            {
                int limiteInt = 0;
                if (i > 0)
                {
                    if (addLetra < letrasArr.Length)
                    {
                        addLetra++;
                    }
                    else
                    {
                        addLetra = 0;
                    }
                    letraBase = letrasArr[addLetra - 1];
                    if (limiteGral > letrasArr.Length)
                        limiteInt = letrasArr.Length;
                    else
                        limiteInt = limiteGral;
                }
                else
                {
                    limiteGral = total;
                    if (total > letrasArr.Length)
                        limiteInt = letrasArr.Length;
                    else
                        limiteInt = total;
                }
                for (int j = 0; j < limiteInt; j++)
                {
                    letrasLista.Add(letraBase + letrasArr[j]);
                }
                limiteGral = limiteGral - letrasArr.Length;
            }

            return letrasLista.ToArray();
        }

        // FUNCION QUE CREA UN ARRAY CON LOS DIAS NOMBRE Y NUMERO (USADO PARA LAS CITAS Y ACTIVIDADES)
        public static List<string> FechaDiaArr()
        {
            double[] nums = { };
            DateTime fechaHoy = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            string diaHoy = fechaHoy.ToString("dddd").Replace("á", "a").Replace("é","e");
            if(diaHoy == "lunes")
            {
                nums = new double[] { 0, 1, 2, 3, 4, 5, 6 };
            }
            else if (diaHoy == "martes")
            {
                nums = new double[] { -1, 0, 1, 2, 3, 4, 5 };
            }
            else if (diaHoy == "miercoles")
            {
                nums = new double[] { -2, -1, 0, 1, 2, 3, 4 };
            }
            else if (diaHoy == "jueves")
            {
                nums = new double[] { -3, -2, -1, 0, 1, 2, 3 };
            }
            else if (diaHoy == "viernes")
            {
                nums = new double[] { -4, -3, -2, -1, 0, 1, 2 };
            }
            else if (diaHoy == "sabado")
            {
                nums = new double[] { -5, -4, -3, -2, -1, 0, 1 };
            }
            else if (diaHoy == "domingo")
            {
                nums = new double[] { -6, -5, -4, -3, -2, -1, 0 };
            }
            List<string> diasLista = new List<string>()
            {
                fechaHoy.AddDays(nums[0]).ToString("dddd dd MM/dd/yyyy").Replace("á", "a").Replace("é","e"),
                fechaHoy.AddDays(nums[1]).ToString("dddd dd MM/dd/yyyy").Replace("á", "a").Replace("é","e"),
                fechaHoy.AddDays(nums[2]).ToString("dddd dd MM/dd/yyyy").Replace("á", "a").Replace("é","e"),
                fechaHoy.AddDays(nums[3]).ToString("dddd dd MM/dd/yyyy").Replace("á", "a").Replace("é","e"),
                fechaHoy.AddDays(nums[4]).ToString("dddd dd MM/dd/yyyy").Replace("á", "a").Replace("é","e"),
                fechaHoy.AddDays(nums[5]).ToString("dddd dd MM/dd/yyyy").Replace("á", "a").Replace("é","e"),
                fechaHoy.AddDays(nums[6]).ToString("dddd dd MM/dd/yyyy").Replace("á", "a").Replace("é","e"),
            };
            return diasLista;
        }

        // ------------------------ [ FUNCIONES AUXILIARES PARA LOS CONTROLADORES DE LA APP ] ------------------------
        // FUMCION QUE VERIFICA LA CADENA DE SEGURIDAD PARA ACCEDER A LAS  FUNCIONES DE LA API
        public static bool VerifSecAPI(string Key)
        {
            if(Key == "xQtHfLgRAyVppSvPB8ABEmR2mQckxknFzs9e}bGbHtkZDs/MVt1jIGsr}+Wm-QYR")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}