using Fidelidad.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fidelidad.Procesos
{
    public static class GenerarLOAPRE
    {

        public static Archivo Generar()
        {
            Archivo archivo = new Archivo()
            {
                Nombre = "LOAPRE",
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
                NombreCampo = "HORA",
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

            cabecera = new CampoCabecera()
            {
                NombreCampo = "COD-MONE",
                NombreBaseDeDatos = "CodigoMoneda",
                Descripcion = "Código de Moneda",
                Longitud = 2,
                Offset = 37,
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
                NombreCampo = "COD-PREMIO",
                NombreBaseDeDatos = "CodigoPremio",
                Descripcion = "Código del premio",
                Longitud = 6,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "DESCR",
                NombreBaseDeDatos = "DescripcionPremio",
                Descripcion = "Descripción del premio",
                Longitud = 25,
                Offset = 6,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "DESCRC",
                NombreBaseDeDatos = "DescripcionCortaPremio",
                Descripcion = "Descripción corta del premio",
                Longitud = 20,
                Offset = 41,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "T-PREM",
                NombreBaseDeDatos = "TipoPremio",
                Descripcion = "Tipo de premio",
                Longitud = 1,
                Offset = 61,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "PUNTOS",
                NombreBaseDeDatos = "Puntos",
                Descripcion = "Puntos requeridos para retirar el premio (puntos)",
                Longitud = 5,
                Offset = 62,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "PUN-MON",
                NombreBaseDeDatos = "PuntosMonedas",
                Descripcion = "Puntos requeridos para retirar el premio (moneda + puntos)",
                Longitud = 5,
                Offset = 67,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "CANTM",
                NombreBaseDeDatos = "CantMonedas",
                Descripcion = "Cantidad de Moneda",
                Formato = "00",
                Longitud = 7,
                Offset = 72,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "ESTADO",
                NombreBaseDeDatos = "Estado",
                Descripcion = "Estado del premio",
                Longitud = 1,
                Offset = 80,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "CODTEXV",
                NombreBaseDeDatos = "DescripcionCortaPremio",
                Descripcion = "cod de voucher (solo para premios especiales)",
                Longitud = 4,
                Offset = 81,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "DESCRC",
                NombreBaseDeDatos = "DescripcionCortaPremio",
                Descripcion = "Descripción corta del premio",
                Longitud = 20,
                Offset = 41,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            return registroList;
        }
    }
}
