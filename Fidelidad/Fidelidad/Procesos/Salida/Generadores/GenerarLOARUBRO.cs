using Fidelidad.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fidelidad.Procesos
{
    public static class GenerarLOARUBRO
    {
        public static Archivo Generar()
        {
            Archivo archivo = new Archivo()
            {
                Nombre = "LOARUBRO",
                OrigenDatos = "mockLOARUBRO",
                IsUnixSaltoLinea = true
            };
            archivo.CamposCabecera = GenerarCabecera();
            archivo.CamposRegistro = GenerarRegistro();

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
                NombreCampo = "FECHA_APLI",
                NombreBaseDeDatos = "FechaAplicacion",
                Descripcion = "Fecha de aplicación AAAAMMDD",
                Longitud = 8,
                Offset = 18,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabeceraList.Add(cabecera);

            cabecera = new CampoCabecera()
            {
                NombreCampo = "HORA_APLI",
                NombreBaseDeDatos = "horaAplicacion",
                Descripcion = "Hora de aplicación HHMMSS",
                Longitud = 6,
                Offset = 26,
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
                Offset = 32,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabeceraList.Add(cabecera);

            return cabeceraList;
        }

        private static List<CampoRegistro> GenerarRegistro()
        {
            List<CampoRegistro> registroList = new List<CampoRegistro>();

            CampoRegistro regsitro = new CampoRegistro()
            {
                NombreCampo = "COD-RUBRO",
                NombreBaseDeDatos = "CodigoRubro",
                Descripcion = "Código del rubro",
                Longitud = 3,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "DESCR",
                NombreBaseDeDatos = "DescripcionRubro",
                Descripcion = "Descripción del rubro",
                Longitud = 35,
                Offset = 3,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "DESCRC",
                NombreBaseDeDatos = "DescripcionCortaRubro",
                Descripcion = "Descripción corta del rubro",
                Longitud = 20,
                Offset = 38,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "COD-PG",
                NombreBaseDeDatos = "CodigoProductoGenerico",
                Descripcion = "Código del Producto Genérico",
                Longitud = 5,
                Offset = 58,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "ESTAR",
                NombreBaseDeDatos = "EstadoRubro",
                Descripcion = "Estado del rubro",
                Longitud = 1,
                Offset = 63,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "TIPO-TARJ",
                NombreBaseDeDatos = "TipoTarjeta",
                Descripcion = "Tipo de Tarjeta",
                Longitud = 2,
                Offset = 64,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "PROMOCION",
                NombreBaseDeDatos = "CantMonedas",
                Descripcion = "Indica si es un rubro promoción o no",
                Longitud = 1,
                Offset = 66,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "MAX-COMBUST-DIA-LITROS",
                NombreBaseDeDatos = "MaxCombustible",
                Descripcion = "Indica el maximo diario de combustible (litros) vigente por tipode tarjeta y por rubros.",
                Longitud = 7,
                Offset = 67,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "MAX-RUBRO-DIA-MONEDA",
                NombreBaseDeDatos = "MaxRubroDia",
                Descripcion = "Indica el maximo diario vigente del rubro, distinto a combustibles, en $. (Por tipo de tarjeta y rubro)",
                Longitud = 7,
                Offset = 74,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "MAX-RUBRO-MES-MONEDA",
                NombreBaseDeDatos = "MaxRubroMes",
                Descripcion = "Indica el maximo mensual vigente del rubro, distinto a combustibles, en $. (Por tipo de tarjeta y rubro)",
                Longitud = 7,
                Offset = 81,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            return registroList;
        }
    }
}
