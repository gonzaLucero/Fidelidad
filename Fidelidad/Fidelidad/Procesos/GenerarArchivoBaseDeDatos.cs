using Hexacta.YPF.Fidelizacion.Core.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hexacta.YPF.Fidelizacion.Core.Procesos
{
    public static class GenerarArchivoBaseDeDatos
    {
        public static void Generar(string NombreArchivo)
        {
            var xml = XDocument.Load(Constantes.ArchivoDeConfiguracion);
            var serializer = new XmlSerializer(typeof(List<Archivo>));
            var list = serializer.Deserialize(xml.Root.CreateReader());

            var archivo = ((List<Archivo>)list).FirstOrDefault(arch => arch.Nombre == NombreArchivo);

            if (archivo != null)
            {
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                foreach (var item in archivo.Cabecera.Campos)
                {
                    DataColumn dataColumn = new DataColumn();
                    dataColumn.ColumnName = item.NombreBaseDeDatos;
                    dataTable.Columns.Add(dataColumn);
                }
                dataSet.Tables.Add(dataTable);

                dataTable = new DataTable();
                foreach (var item in archivo.Detalle.Campos)
                {
                    DataColumn dataColumn = new DataColumn();
                    dataColumn.ColumnName = item.NombreBaseDeDatos;
                    dataTable.Columns.Add(dataColumn);
                }
                dataSet.Tables.Add(dataTable);
            }
        }
    }
}
