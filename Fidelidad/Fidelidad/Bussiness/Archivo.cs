using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fidelidad.Config
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

        public List<CampoCabecera> CamposCabecera { get; set; }

        public List<CampoRegistro> CamposRegistro { get; set; }
    }
}
