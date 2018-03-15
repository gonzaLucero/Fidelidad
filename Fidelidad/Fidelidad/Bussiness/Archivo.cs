using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hexacta.YPF.Fidelizacion.Core.Config
{
    [Serializable]
    public class Archivo
    {
        [XmlAttribute]
        public string Nombre { get; set; }

        [XmlAttribute]
        public string OrigenDatos { get; set; }

        [XmlAttribute]
        public bool IsUnixSaltoLinea { get; set; }

        public Cabecera Cabecera { get; set; }

        public Detalle Detalle { get; set; }

        public Archivo()
        {
            Cabecera = new Cabecera();
            Detalle = new Detalle();
        }
    }
}
