using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hexacta.YPF.Fidelizacion.Core.Config
{
    [Serializable]
    public class Detalle
    {
        public List<CampoDetalle> Campos { get; set; }

        [XmlAttribute]
        public string NombreTabla { get; set; }

        public Detalle SubDetalle { get; set; }
    }
}
