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
    public class MCDeportiva : Controller
    {
        // ::::::::::::::::::::::::: [ COORDINACION DEPORTIVA ] :::::::::::::::::::::::::
        // --------- CLASES PUBLICAS ---------
        // CLASES DE DOCUMENTOS (ARCHIVERO)
        public class DocsListaCD
        {
            public List<Dictionary<string, object>> DocsInformativos { get; set; }
            public string UrlFolderCliente { get; set; }
            public string Error { get; set; }
        }
        public class DocCDeportiva
        {
            public string TokenCentro { get; set; }
            public string Nombre { get; set; }
            public string Extension { get; set; }
            public string Archivo { get; set; }
            public string TokenUsuario { get; set; }
        }

        // FUNCION QUE DEVUELVE LA LISTA DE DOCUMENTOS [ ARCHIVERO ]
        public DocsListaCD CDeportivaListaDocs(string tokencentro)
        {
            try
            {
                SQL.comandoSQLTrans("ListaDocsCD");

                List<Dictionary<string, object>> DocsInformativos = new List<Dictionary<string, object>>();
                SQL.commandoSQL = new SqlCommand("SELECT * FROM dbo.deportivodocumentos WHERE idcentro = (SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroDATA) AND estatus > 0", SQL.conSQL, SQL.transaccionSQL);
                SQL.commandoSQL.Parameters.Add(new SqlParameter("@TokenCentroDATA", SqlDbType.VarChar) { Value = tokencentro });
                using (var lector = SQL.commandoSQL.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Dictionary<string, object> docInformativo = new Dictionary<string, object>()
                        {
                            { "Nombre", lector["nombre"].ToString() },
                            { "Extension", lector["extension"].ToString() },
                            { "Archivo", lector["archivo"].ToString() }
                        };
                        DocsInformativos.Add(docInformativo);
                    }
                }

                DocsListaCD Docs = new DocsListaCD()
                {
                    DocsInformativos = DocsInformativos
                };

                SQL.transaccionSQL.Commit();
                return Docs;
            }
            catch (Exception e)
            {
                SQL.transaccionSQL.Rollback();
                DocsListaCD err = new DocsListaCD()
                {
                    Error = e.ToString()
                };
                return err;
            }
            finally
            {
                SQL.conSQL.Close();
            }
        }

        // FUNCION QUE ALMACENA UN DOCUMENTO INFORMATIVO [ ARCHIVERO ]
        public string AltaDocCDeportiva(DocCDeportiva docinformativo)
        {
            try
            {
                SQL.comandoSQLTrans("DocInformativoCD");
                SQL.commandoSQL = new SqlCommand("INSERT INTO dbo.deportivodocumentos (idcentro, nombre, extension, archivo, fechahora, admusuario) VALUES ((SELECT id FROM dbo.centros WHERE tokencentro = @TokenCentroParam), @NombreParam, @ExtensionParam, @ArchivoParam, @FechaParam, (SELECT usuario FROM dbo.usuarios WHERE tokenusuario = @TokenUsuarioParam))", SQL.conSQL, SQL.transaccionSQL);
                SqlParameter[] altaDocDeportivo =
                {
                    new SqlParameter("@TokenCentroParam", SqlDbType.VarChar) { Value = docinformativo.TokenCentro },
                    new SqlParameter("@NombreParam", SqlDbType.VarChar) { Value = docinformativo.Nombre },
                    new SqlParameter("@ExtensionParam", SqlDbType.VarChar) { Value = docinformativo.Extension },
                    new SqlParameter("@ArchivoParam", SqlDbType.VarChar) { Value = docinformativo.Archivo },
                    new SqlParameter("@FechaParam", SqlDbType.DateTime) { Value = MISC.FechaHoy() },
                    new SqlParameter("@TokenUsuarioParam", SqlDbType.VarChar) { Value = docinformativo.TokenUsuario }
                };
                SQL.commandoSQL.Parameters.AddRange(altaDocDeportivo);
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
    }
}