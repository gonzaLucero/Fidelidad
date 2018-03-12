using Fidelidad.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Fidelidad.Procesos
{
    public static class GenerarArchivoEESS
    {
        public static void Generar(string NombreArchivo)
        {
            //TODO: 
            //      Rellenar registros con el caracter configurado
            //      Exportar a un archivo txt

            //      Obterner los datos de la base de datos
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(@"C:\Users\glucero\Documents\YPF\ServiClub\Example\Fidelidad\Fidelidad\DataAccess\mock 1.xml");

            //      Leer configuracion del archivo a generar
            var xml = XDocument.Load(Constantes.ArchivoDeConfiguracion + ".xml");
            var serializer = new XmlSerializer(typeof(List<Archivo>));
            var list = serializer.Deserialize(xml.Root.CreateReader());
            var archivo = ((List<Archivo>)list).FirstOrDefault(arch => arch.Nombre == NombreArchivo);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\glucero\Documents\YPF\ServiClub\Example\Fidelidad\Fidelidad\bin\Debug\WriteLines2.txt"))
            {
                //      Mappear cabecera y registros
                string linea = "";
                foreach (var item in archivo.Cabecera)
                {
                    var campo = dataSet.Tables[1].Rows[0][item.NombreBaseDeDatos].ToString();
                    campo = CompletarRegistro(campo, item.PadCaracter, item.Longitud, item.IsPadLeft);
                    linea += campo;
                }
                file.WriteLine(linea);

                foreach (var item in archivo.Registro)
                {
                    linea = "";
                    var campo="";
                    foreach (DataRow row in dataSet.Tables[2].Rows)
                    {
                        campo = row.Field<string>(item.NombreBaseDeDatos);
                        campo = CompletarRegistro(campo, item.PadCaracter, item.Longitud, item.IsPadLeft);
                        linea += campo + '\n';
                    }
                    file.Write(linea);
                }
            }
        }

        private static string CompletarRegistro(string registro, char caracter, int longitud, bool completeLeft)
        {
            return completeLeft ? registro.PadLeft(longitud, caracter) : registro.PadRight(longitud, caracter);
        }
    }
}
