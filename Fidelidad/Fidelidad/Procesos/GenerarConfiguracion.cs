using Fidelidad.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fidelidad.Procesos
{
    public static class GenerarConfiguracion
    {
        public static void Generar()
        {
            IList<Archivo> listConfig = GenerarArchivos();
            FileInfo file = new FileInfo(Constantes.ArchivoDeConfiguracion + ".xml");
            StreamWriter sw = file.CreateText();
            XmlSerializer writer = new XmlSerializer(typeof(List<Archivo>));
            writer.Serialize(sw, listConfig);
            sw.Close();
        }

        private static IList<Archivo> GenerarArchivos()
        {
            IList<Archivo> listConfig = new List<Archivo>();

            listConfig.Add(GenerarLOAFUNNTT.Generar());

            return listConfig;

        }
    }
}
