using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hexacta.YPF.Fidelizacion.Core.Config
{
    [Serializable]
    public class Cabecera
    { 
        public List<CampoCabecera> Campos { get; set; }
    }
}
