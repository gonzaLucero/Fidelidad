using Fidelidad.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fidelidad.Procesos
{
    public static class GenerarLOAPROTT
    {

        public static Archivo Generar()
        {
            Archivo archivo = new Archivo()
            {
                Nombre = "LOAPROTT",
                OrigenDatos = "mockLOAPROTT",
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

            CampoRegistro registro = new CampoRegistro()
            {
                NombreCampo = "COD-RUBRO",
                NombreBaseDeDatos = "CodigoRubro",
                Descripcion = "Código del rubro",
                Longitud = 3,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "COD-PROD",
                NombreBaseDeDatos = "CodigoProducto",
                Descripcion = "Código del producto",
                Longitud = 5,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            CampoRegistro regsitro = new CampoRegistro()
            {
                NombreCampo = "COD-BARRA",
                NombreBaseDeDatos = "CodigoBarra",
                Descripcion = "Código de barra del producto",
                Longitud = 20,
                Offset = 8,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "DESCR",
                NombreBaseDeDatos = "DescripcionProducto",
                Descripcion = "Descripción del producto",
                Longitud = 35,
                Offset = 28,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "DESCRC",
                NombreBaseDeDatos = "DescripcionCortaProducto",
                Descripcion = "Descripción corta del producto",
                Longitud = 20,
                Offset = 63,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "AUTORIZ",
                NombreBaseDeDatos = "Autorizacion",
                Descripcion = "Autorización del administrador",
                Longitud = 1,
                Offset = 83,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "CANTM",
                NombreBaseDeDatos = "CantidadMoneda",
                Descripcion = "Moneda para la relación",
                Longitud = 7,
                Offset = 84,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "LIM-MIN-PESOS",
                NombreBaseDeDatos = "LimiteMinimoPesos",
                Descripcion = "Límite minimo en pesos",
                Longitud = 7,
                Offset = 91,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "LIM-MIN-LITROS",
                NombreBaseDeDatos = "LimiteMinimoLitros",
                Descripcion = "Límite minimo en litros",
                Longitud = 7,
                Offset = 98,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "LIM-MAX-PESOS",
                NombreBaseDeDatos = "LimiteMaximoPesos",
                Descripcion = "Límite maximo en pesos",
                Longitud = 7,
                Offset = 105,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "LIM-MAX-LITROS",
                NombreBaseDeDatos = "LimiteMaximoLitros",
                Descripcion = "Límite maximo en litros",
                Longitud = 7,
                Offset = 112,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "PRIORI",
                NombreBaseDeDatos = "Priori",
                Descripcion = "Prioridad para la asignación",
                Longitud = 1,
                Offset = 119,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "ESTAP",
                NombreBaseDeDatos = "EstadoProducto",
                Descripcion = "Estado del producto",
                Longitud = 1,
                Offset = 120,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "PTOSREL",
                NombreBaseDeDatos = "Estado",
                Descripcion = "Puntos para la relación",
                Longitud = 5,
                Offset = 121,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "COD-LISTA",
                NombreBaseDeDatos = "CodigoLista",
                Descripcion = "Codigo de la lista",
                Longitud = 4,
                Offset = 126,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "COD-CRITE",
                NombreBaseDeDatos = "CodigoCriterio",
                Descripcion = "Codigo de criterio",
                Longitud = 4,
                Offset = 130,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "TIPO-TARJ",
                NombreBaseDeDatos = "TipoTarjeta",
                Descripcion = "Tipo de tarjeta",
                Longitud = 2,
                Offset = 134,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "LIM-DIA-MAX-PESO",
                NombreBaseDeDatos = "LimiteMaxPesos",
                Descripcion = "Limite diario en pesos",
                Longitud = 7,
                Offset = 136,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            registro = new CampoRegistro()
            {
                NombreCampo = "LIM-DIA-MAX-LITRO",
                NombreBaseDeDatos = "LimiteMaxLitros",
                Descripcion = "Limite diario en litros",
                Longitud = 7,
                Offset = 143,
                PadCaracter = '0',
                IsPadLeft = true
            };
            registroList.Add(registro);

            return registroList;
        }
    }
}
