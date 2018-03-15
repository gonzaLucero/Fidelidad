using Hexacta.YPF.Fidelizacion.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexacta.YPF.Fidelizacion.Core.Procesos
{
    public static class GenerarLOALIINH
    {

        public static Archivo Generar()
        {
            Archivo archivo = new Archivo()
            {
                Nombre = "LOALIINH",
                OrigenDatos = "mockLOALIINH",
                IsUnixSaltoLinea = true
            };
            archivo.Cabecera.Campos = GenerarCabecera();
            archivo.Detalle.Campos = GenerarDetalle();
            archivo.Detalle.SubDetalle = GenerarSubDetalle();

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

        private static List<CampoDetalle> GenerarDetalle()
        {
            List<CampoDetalle> registroList = new List<CampoDetalle>();

            CampoDetalle registro = new CampoDetalle()
            {
                NombreCampo = "T-LISTA",
                NombreBaseDeDatos = "TipoLista",
                Descripcion = "Tipo de lista",
                Longitud = 2,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "COD-TEXTO",
                NombreBaseDeDatos = "CodigoTexto",
                Descripcion = "Código de texto",
                Longitud = 35,
                Offset = 6,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            return registroList;
        }

        private static Detalle GenerarSubDetalle()
        {
            Detalle subDetalle = new Detalle();
            subDetalle.Campos = new List<CampoDetalle>();

            CampoDetalle registro = new CampoDetalle()
            {
                NombreCampo = "NTARJCLI",
                NombreBaseDeDatos = "NumeroTarjetaCliente",
                Descripcion = "Número de Tarjeta socio",
                Longitud = 16,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            subDetalle.Campos.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "AB",
                NombreBaseDeDatos = "AltaBaja",
                Descripcion = "Altas o Bajas de Listas",
                Longitud = 1,
                Offset = 16,
                PadCaracter = '0',
                IsPadLeft = true
            };
            subDetalle.Campos.Add(registro);

            return subDetalle;
        }
    }
}
