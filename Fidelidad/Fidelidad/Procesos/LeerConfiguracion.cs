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
    public static class LeerConfiguracion
    {
        public static void Leer()
        {
            var xml = XDocument.Load(Constantes.ArchivoDeConfiguracion + ".xml");
            var serializer = new XmlSerializer(typeof(List<Archivo>));
            var list = serializer.Deserialize(xml.Root.CreateReader());
        }
    }
}
