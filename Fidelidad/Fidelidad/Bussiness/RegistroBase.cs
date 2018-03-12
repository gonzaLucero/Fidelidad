using System;
using System.Xml.Serialization;

namespace Fidelidad.Config
{
    [Serializable]
    public class RegistroBase
    {
        [XmlAttribute]
        public char PadCaracter { get; set; }

        [XmlAttribute]
        public bool IsPadLeft { get; set; }

        public string NombreCampo { get; set; }

        public string NombreBaseDeDatos { get; set; }

        public string Descripcion { get; set; }

        public string Tipo { get; set; }

        public int Longitud { get; set; }

        public int Offset { get; set; }

    }
}
