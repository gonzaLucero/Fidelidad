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
        public static void Generar(string NombreArchivo)
        {
            var archivo = ObtenerConfiguracion.Obtener(NombreArchivo);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(ArchivosMock.ObtenerArchivoMock(archivo.OrigenDatos));

            using (StreamWriter writer = new StreamWriter(Constantes.DirectorioArchivosDeSalida + NombreArchivo + ".dsc", false, Encoding.UTF8))
            {
                if (archivo.IsUnixSaltoLinea)
                {
                    writer.NewLine = "\r";
                }
                string linea = string.Empty;
                foreach (var item in archivo.Cabecera.Campos)
                {
                    var campo = dataSet.Tables[1].Rows[0][item.NombreBaseDeDatos].ToString();
                    campo = CompletarRegistro(campo, item.PadCaracter, item.Longitud, item.IsPadLeft);
                    linea += campo;
                }
                writer.WriteLine(linea);

                foreach (DataRow row in dataSet.Tables[2].Rows)
                {
                    linea = string.Empty;
                    foreach (var item in archivo.Detalle.Campos.OrderBy(reg => reg.Offset))
                    {
                        var campo = row.Field<string>(item.NombreBaseDeDatos);
                        campo = CompletarRegistro(campo, item.PadCaracter, item.Longitud, item.IsPadLeft);
                        linea += campo;
                    }
                    writer.WriteLine(linea);
                }
            }
        }

        private static string CompletarRegistro(string registro, char caracter, int longitud, bool completeLeft)
        {
            return completeLeft ? registro.PadLeft(longitud, caracter) : registro.PadRight(longitud, caracter);
        }
    }
}
