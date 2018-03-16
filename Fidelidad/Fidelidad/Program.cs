using Hexacta.YPF.Fidelizacion.Core.Config;
using Hexacta.YPF.Fidelizacion.Core.Procesos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerarConfiguracion.Generar();
            //GenerarArchivoBaseDeDatos.Generar("LOAFUNTT");
            GenerarArchivoEESS.Generar("LOAFUNTT");
            GenerarArchivoEESS.Generar("LOALIINH");
        }
    }
}
