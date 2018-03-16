using Hexacta.YPF.Fidelizacion.Core.Config;
using Hexacta.YPF.Fidelizacion.Core.DataAccess;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Hexacta.YPF.Fidelizacion.Core.Procesos
{
    public static class GenerarArchivoEESS
    {
        private static char saltoLinea;

        public static void Generar(string NombreArchivo)
        {
            var archivo = ObtenerConfiguracion.Obtener(NombreArchivo);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(ArchivosMock.ObtenerArchivoMock(archivo.OrigenDatos));

            using (StreamWriter writer = new StreamWriter(Constantes.DirectorioArchivosDeSalida + NombreArchivo + ".dsc", false, Encoding.UTF8))
            {
                saltoLinea = '\r';
                if (archivo.IsUnixSaltoLinea)
                {
                    saltoLinea = '\n';
                }

                string linea = string.Empty;
                foreach (var item in archivo.Cabecera.Campos)
                {
                    var campo = dataSet.Tables[archivo.Cabecera.NombreTabla].Rows[0][item.NombreBaseDeDatos].ToString();
                    campo = CompletarRegistro(campo, item.PadCaracter, item.Longitud, item.IsPadLeft);
                    linea += campo;
                }
                writer.Write(linea + saltoLinea);


                writer.Write(ObtenerRegistros(dataSet.Tables[archivo.Detalle.NombreTabla].Select(), archivo.Detalle));
            }
        }

        private static string CompletarRegistro(string registro, char caracter, int longitud, bool completeLeft)
        {
            return completeLeft ? registro.PadLeft(longitud, caracter) : registro.PadRight(longitud, caracter);
        }

        private static string ObtenerRegistros(DataRow[] rows, Detalle detalle)
        {
            string lineas = string.Empty;
            string linea;
            if (detalle != null)
            {
                foreach (DataRow row in rows)
                {
                    linea = string.Empty;
                    foreach (var item in detalle.Campos.OrderBy(reg => reg.Offset))
                    {
                        var campo = row.Field<string>(item.NombreBaseDeDatos);
                        campo = CompletarRegistro(campo, item.PadCaracter, item.Longitud, item.IsPadLeft);
                        linea += campo;
                    }
                    lineas += linea + saltoLinea;
                    var dataRelation = row.Table.ChildRelations[0];
                    if (dataRelation != null)
                    {
                        lineas += ObtenerRegistros(row.GetChildRows(dataRelation), detalle.SubDetalle);
                    }
                }
                
            }

            return lineas;
        }
    }
}
