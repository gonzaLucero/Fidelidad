using Hexacta.YPF.Fidelizacion.Core.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hexacta.YPF.Fidelizacion.Core.Procesos
{
    public static class ObtenerConfiguracion
    {
        public static Archivo Obtener(string NombreArchivo)
        {
            try
            {
                var xml = XDocument.Load(Constantes.ArchivoDeConfiguracion);
                var serializer = new XmlSerializer(typeof(List<Archivo>));
                var list = serializer.Deserialize(xml.Root.CreateReader());
                var archivo = ((List<Archivo>)list).SingleOrDefault(arch => arch.Nombre == NombreArchivo);
                return archivo;
            }
            catch (FileNotFoundException e)
            {
                Console.Write(NombreArchivo + " archivo no encontrado.");
                throw;
            }
        }
    }
}
