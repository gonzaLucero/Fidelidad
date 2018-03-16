using Hexacta.YPF.Fidelizacion.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexacta.YPF.Fidelizacion.Core.DataAccess
{
    public static class ArchivosMock
    {
        public static string ObtenerArchivoMock(string NombreOrigen)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("mockLOAFUNTT", Constantes.UrlMockLOAFUNTT);
            dictionary.Add("mockLOAPRE", Constantes.UrlMockLOAPRE);
            dictionary.Add("mockLOALIINH", Constantes.UrlMockLOALIINH);

            return dictionary[NombreOrigen];
        }
    }
}
