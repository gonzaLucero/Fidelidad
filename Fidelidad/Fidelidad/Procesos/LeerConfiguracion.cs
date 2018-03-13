using Fidelidad.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Fidelidad.Procesos
{
    public static class ObtenerConfiguracion
    {
        public static Archivo Obtener(string NombreArchivo)
        {
            var xml = XDocument.Load(Constantes.ArchivoDeConfiguracion + ".xml");
            var serializer = new XmlSerializer(typeof(List<Archivo>));
            var list = serializer.Deserialize(xml.Root.CreateReader());
            var archivo = ((List<Archivo>)list).SingleOrDefault(arch => arch.Nombre == NombreArchivo);
            return archivo;
        }
    }
}
