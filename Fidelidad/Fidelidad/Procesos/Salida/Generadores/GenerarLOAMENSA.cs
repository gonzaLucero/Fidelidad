using Fidelidad.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fidelidad.Procesos
{
    public static class GenerarLOAMENSA
    {

        public static Archivo Generar()
        {
            Archivo archivo = new Archivo()
            {
                Nombre = "LOAMENSA",
                OrigenDatos = "mockLOAMENSA",
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

        private static List<CampoRegistro> GenerarRegistro()
        {
            List<CampoRegistro> registroList = new List<CampoRegistro>();

            CampoRegistro regsitro = new CampoRegistro()
            {
                NombreCampo = "COD-MENSA",
                NombreBaseDeDatos = "CodigoMensaje",
                Descripcion = "Código del mensaje",
                Longitud = 4,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "DESCR",
                NombreBaseDeDatos = "DescripcionMensaje",
                Descripcion = "Descripción del mensaje",
                Longitud = 35,
                Offset = 4,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "FEDESDE",
                NombreBaseDeDatos = "FechaDesde",
                Descripcion = "Fecha de inicio de vigencia",
                Longitud = 8,
                Offset = 39,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "FEHASTA",
                NombreBaseDeDatos = "FechaHasta",
                Descripcion = "Fecha de fin de vigencia",
                Longitud = 8,
                Offset = 47,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "COD-LISTA",
                NombreBaseDeDatos = "CodigoLista",
                Descripcion = "Código de la lista",
                Longitud = 4,
                Offset = 55,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "COD-TEXTO",
                NombreBaseDeDatos = "CodigoTexto",
                Descripcion = "Código de texto",
                Longitud = 4,
                Offset = 59,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "AB",
                NombreBaseDeDatos = "AltaBaja",
                Descripcion = "Alta o Baja de registros",
                Formato = "00",
                Longitud = 1,
                Offset = 63,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            regsitro = new CampoRegistro()
            {
                NombreCampo = "COD-CRITE",
                NombreBaseDeDatos = "CodigoCriterio",
                Descripcion = "Código del criterio",
                Longitud = 4,
                Offset = 64,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(regsitro);

            return registroList;
        }
    }
}
