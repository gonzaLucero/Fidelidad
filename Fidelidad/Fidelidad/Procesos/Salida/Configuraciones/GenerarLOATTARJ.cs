using Hexacta.YPF.Fidelizacion.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexacta.YPF.Fidelizacion.Core.Procesos
{
    public static class GenerarLOATTARJ
    {

        public static Archivo Generar()
        {
            Archivo archivo = new Archivo()
            {
                Nombre = "LOATTARJ",
                OrigenDatos = "mockLOATTARJ",
                IsUnixSaltoLinea = true
            };
            archivo.Cabecera.Campos = GenerarCabecera();
            archivo.Detalle.Campos = GenerarRegistro();

            return archivo;
        }

        private static List<CampoCabecera> GenerarCabecera()
        {
            List<CampoCabecera> cabeceraList = new List<CampoCabecera>();

            CampoCabecera cabecera = new CampoCabecera()
            {
                NombreCampo = "COD-EESS",
                NombreBaseDeDatos = "CodigoEstacion",
                Descripcion = "Código de Estación",
                Longitud = 5,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabeceraList.Add(cabecera);

            cabecera = new CampoCabecera()
            {
                NombreCampo = "FECHA",
                NombreBaseDeDatos = "Fecha",
                Descripcion = "Fecha de creacion archivo AAAAMMDD",
                Longitud = 8,
                Offset = 5,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabeceraList.Add(cabecera);

            cabecera = new CampoCabecera()
            {
                NombreCampo = "HORA",
                NombreBaseDeDatos = "hora",
                Descripcion = "Hora de creacion archivo HHMM",
                Longitud = 4,
                Offset = 13,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabeceraList.Add(cabecera);

            cabecera = new CampoCabecera()
            {
                NombreCampo = "FRECAMBIO",
                NombreBaseDeDatos = "FlagRecambio",
                Descripcion = "Flag de Actualización de Lista N = Novedades",
                Longitud = 1,
                Offset = 17,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabeceraList.Add(cabecera);

            cabecera = new CampoCabecera()
            {
                NombreCampo = "VERSIONACES",
                NombreBaseDeDatos = "version",
                Descripcion = "Versión de los datos de Serviclub",
                Longitud = 5,
                Offset = 18,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabeceraList.Add(cabecera);

            return cabeceraList;
        }

        private static List<CampoDetalle> GenerarRegistro()
        {
            List<CampoDetalle> registroList = new List<CampoDetalle>();

            CampoDetalle registro = new CampoDetalle()
            {
                NombreCampo = "TIPO-TARJ",
                NombreBaseDeDatos = "TipoTarjeta",
                Descripcion = "Tipo de tarjeta",
                Longitud = 2,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "DESCR",
                NombreBaseDeDatos = "DescripcionTipoTarjeta",
                Descripcion = "Descripción del tipo de tarjeta",
                Longitud = 35,
                Offset = 2,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "NRODESDE",
                NombreBaseDeDatos = "NumeroTarjetaDesde",
                Descripcion = "Número de tarjeta desde",
                Longitud = 16,
                Offset = 37,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "NROHASTA",
                NombreBaseDeDatos = "NumeroTarjetaHasta",
                Descripcion = "Número de tarjeta hasta",
                Longitud = 16,
                Offset = 53,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "FEDESDE",
                NombreBaseDeDatos = "FechaDesde",
                Descripcion = "Fecha de inicio vigencia",
                Longitud = 8,
                Offset = 69,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "FEHASTA",
                NombreBaseDeDatos = "FechaHasta",
                Descripcion = "Fecha de fin vigencia",
                Longitud = 8,
                Offset = 77,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "FECADU",
                NombreBaseDeDatos = "FechaCaducidad",
                Descripcion = "Fecha de caducidad para vencimiento de puntos",
                Longitud = 8,
                Offset = 85,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "VEN-PUNTOS",
                NombreBaseDeDatos = "ConVencimientoPuntos",
                Descripcion = "Habilita-Inhabilita vencimiento de puntos",
                Longitud = 1,
                Offset = 93,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "MAX-DIARIO",
                NombreBaseDeDatos = "MaxPuntoDia",
                Descripcion = "Indica cual es la máxima cantidad de puntos que puede asignarse a una tarjeta en el mismo lote día sin solicitar autorización",
                Longitud = 4,
                Offset = 94,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "MAX-SIN-AUTORIZ",
                NombreBaseDeDatos = "MaxPuntoSinAutoriz",
                Descripcion = "Indica cual es la máxima cantidad de puntos que puede asignarse en una operación de carga de puntos sin solicitar autorización",
                Longitud = 4,
                Offset = 98,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "IMPORTE-MAX",
                NombreBaseDeDatos = "MaxImporte",
                Descripcion = "Indica cual es el importe hasta el cual no pedirá confirmación",
                Longitud = 4,
                Offset = 102,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "PUNTAJE-MAX",
                NombreBaseDeDatos = "MaxPuntaje",
                Descripcion = "Indica el puntaje máximo de disponibles que podrá tener una tarjeta",
                Longitud = 5,
                Offset = 106,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            return registroList;
        }
    }
}
