using Fidelidad.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fidelidad.DataAccess
{
    public static class ArchivosMock
    {
        public static string ObtenerArchivoMock(string NombreOrigen)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("mockLOAFUNTT", Constantes.UrlMockLOAFUNTT);
            dictionary.Add("mockLOAPRE", Constantes.UrlMockLOAPRE);

            return dictionary[NombreOrigen];
        }
    }
}
