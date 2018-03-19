using Hexacta.Core.Tools.Utilities;
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
            var nombreArchivo = args.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(nombreArchivo))
            {
                System.Console.WriteLine("Por favor ingrese en  nombre del archivo que desea generar.");
                return;
            }
            GenerarConfiguracion.Generar();

            LoggerUtility.Info("SVC_DEBUG", (object)string.Format("Generando {0}", nombreArchivo));
            GenerarArchivoEESS.Generar(nombreArchivo);
            LoggerUtility.Info("SVC_DEBUG", (object)string.Format("{0} generado", nombreArchivo));
        }
    }
}
