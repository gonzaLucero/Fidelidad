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
            for (int i = 0; i < 1500; i++)
            {
                GenerarArchivoEESS.Generar(nombreArchivo);
            }
        }
    }
}
