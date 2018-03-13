//using Fidelidad.Config;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Fidelidad.Procesos
//{
//    public static class GenerarLOAFUNNTT
//    {

//        public static Archivo Generar()
//        {
//            Archivo archivo = new Archivo()
//            {
//                Nombre = "LOAFUNTT",
//                IsUnixSaltoLinea = true
//            };
//            archivo.CamposCabecera = GenerarCabecera();
//            archivo.CamposRegistro = GenerarRegistro();

//            return archivo;
//        }

//        private static List<CampoCabecera> GenerarCabecera()
//        {
//            List<CampoCabecera> cabeceraList = new List<CampoCabecera>();

//            CampoCabecera cabecera = new CampoCabecera()
//            {
//                NombreCampo = "COD-EESS",
//                NombreBaseDeDatos = "CodigoEstacion",
//                Descripcion = "Código de Estación",
//                Longitud = 5,
//                Offset = 0,
//                PadCaracter = '0',
//                IsPadLeft = true
//            };
//            cabeceraList.Add(cabecera);

//            cabecera = new CampoCabecera()
//            {
//                NombreCampo = "FECHA",
//                NombreBaseDeDatos = "Fecha",
//                Descripcion = "Fecha de creacion archivo AAAAMMDD",
//                Longitud = 8,
//                Offset = 5,
//                PadCaracter = '0',
//                IsPadLeft = true
//            };
//            cabeceraList.Add(cabecera);

//            cabecera = new CampoCabecera()
//            {
//                NombreCampo = "HORA",
//                NombreBaseDeDatos = "hora",
//                Descripcion = "Hora de creacion archivo HHMM",
//                Longitud = 4,
//                Offset = 13,
//                PadCaracter = '0',
//                IsPadLeft = true
//            };
//            cabeceraList.Add(cabecera);

//            cabecera = new CampoCabecera()
//            {
//                NombreCampo = "FRECAMBIO",
//                NombreBaseDeDatos = "FlagRecambio",
//                Descripcion = "Flag de Actualización de Lista N = Novedades",
//                Longitud = 1,
//                Offset = 17,
//                PadCaracter = '0',
//                IsPadLeft = true
//            };
//            cabeceraList.Add(cabecera);

//            cabecera = new CampoCabecera()
//            {
//                NombreCampo = "VERSIONACES",
//                NombreBaseDeDatos = "version",
//                Descripcion = "Versión de los datos de Serviclub",
//                Longitud = 5,
//                Offset = 18,
//                PadCaracter = '0',
//                IsPadLeft = true
//            };
//            cabeceraList.Add(cabecera);

//            return cabeceraList;
//        }

//        private static List<CampoRegistro> GenerarRegistro()
//        {
//            List<CampoRegistro> registroList = new List<CampoRegistro>();

//            CampoRegistro regsitro = new CampoRegistro()
//            {
//                NombreCampo = "TIPO-TARJ",
//                NombreBaseDeDatos = "TipoTarjeta",
//                Descripcion = "Tipo de Tarjeta",
//                Longitud = 2,
//                Offset = 0,
//                PadCaracter = '0',
//                IsPadLeft = true
//            };
//            registroList.Add(regsitro);

//            regsitro = new CampoRegistro()
//            {
//                NombreCampo = "COD-FUN",
//                NombreBaseDeDatos = "CondigoFuncionalidad",
//                Descripcion = "Código de la funcionalidad",
//                Longitud = 2,
//                Offset = 2,
//                PadCaracter = '0',
//                IsPadLeft = true
//            };
//            registroList.Add(regsitro);

//            regsitro = new CampoRegistro()
//            {
//                NombreCampo = "HABILITA",
//                NombreBaseDeDatos = "Habilita",
//                Descripcion = "Indica si la funcionalidad está habilitada o no.",
//                Longitud = 1,
//                Offset = 4,
//                PadCaracter = '0',
//                IsPadLeft = true
//            };
//            registroList.Add(regsitro);

//            return registroList;
//        }
//    }
//}
