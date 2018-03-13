using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fidelidad.Config
{
    [Serializable]
    public class CampoRegistro : RegistroBase
    {
        public string Formato { get; set; }

    }
}
