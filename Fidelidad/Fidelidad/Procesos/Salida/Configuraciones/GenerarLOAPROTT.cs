using Hexacta.YPF.Fidelizacion.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexacta.YPF.Fidelizacion.Core.Procesos
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
            archivo.Cabecera = GenerarCabecera();
            archivo.Detalle = GenerarRegistro();

            return archivo;
        }

        private static Cabecera GenerarCabecera()
        {
            Cabecera cabecera = new Cabecera();
            cabecera.NombreTabla = "cabecera";
            cabecera.Campos = new List<CampoCabecera>();
            
            CampoCabecera campoCabecera = new CampoCabecera()
            {
                NombreCampo = "COD-EESS",
                NombreBaseDeDatos = "CodigoEstacion",
                Descripcion = "Código de Estación",
                Longitud = 5,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabecera.Campos.Add(campoCabecera);

            campoCabecera = new CampoCabecera()
            {
                NombreCampo = "FECHA",
                NombreBaseDeDatos = "Fecha",
                Descripcion = "Fecha de creacion archivo AAAAMMDD",
                Longitud = 8,
                Offset = 5,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabecera.Campos.Add(campoCabecera);

            campoCabecera = new CampoCabecera()
            {
                NombreCampo = "HORA",
                NombreBaseDeDatos = "hora",
                Descripcion = "Hora de creacion archivo HHMM",
                Longitud = 4,
                Offset = 13,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabecera.Campos.Add(campoCabecera);

            campoCabecera = new CampoCabecera()
            {
                NombreCampo = "FRECAMBIO",
                NombreBaseDeDatos = "FlagRecambio",
                Descripcion = "Flag de Actualización de Lista N = Novedades",
                Longitud = 1,
                Offset = 17,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabecera.Campos.Add(campoCabecera);

            campoCabecera = new CampoCabecera()
            {
                NombreCampo = "FECHA_APLI",
                NombreBaseDeDatos = "FechaAplicacion",
                Descripcion = "Fecha de aplicación AAAAMMDD",
                Longitud = 8,
                Offset = 18,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabecera.Campos.Add(campoCabecera);

            campoCabecera = new CampoCabecera()
            {
                NombreCampo = "HORA_APLI",
                NombreBaseDeDatos = "horaAplicacion",
                Descripcion = "Hora de aplicación HHMMSS",
                Longitud = 6,
                Offset = 26,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabecera.Campos.Add(campoCabecera);

            campoCabecera = new CampoCabecera()
            {
                NombreCampo = "VERSIONACES",
                NombreBaseDeDatos = "version",
                Descripcion = "Versión de los datos de Serviclub",
                Longitud = 5,
                Offset = 32,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabecera.Campos.Add(campoCabecera);

            campoCabecera = new CampoCabecera()
            {
                NombreCampo = "COD-MONE",
                NombreBaseDeDatos = "CodigoMoneda",
                Descripcion = "Código de Moneda",
                Longitud = 2,
                Offset = 37,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabecera.Campos.Add(campoCabecera);

            return cabecera;
        }

        private static Detalle GenerarRegistro()
        {
            Detalle detalle = new Detalle();
            detalle.NombreTabla = "registro";
            detalle.Campos = new List<CampoDetalle>();

            CampoDetalle campoDetalle = new CampoDetalle()
            {
                NombreCampo = "COD-RUBRO",
                NombreBaseDeDatos = "CodigoRubro",
                Descripcion = "Código del rubro",
                Longitud = 3,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "COD-PROD",
                NombreBaseDeDatos = "CodigoProducto",
                Descripcion = "Código del producto",
                Longitud = 5,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            CampoDetalle regsitro = new CampoDetalle()
            {
                NombreCampo = "COD-BARRA",
                NombreBaseDeDatos = "CodigoBarra",
                Descripcion = "Código de barra del producto",
                Longitud = 20,
                Offset = 8,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "DESCR",
                NombreBaseDeDatos = "DescripcionProducto",
                Descripcion = "Descripción del producto",
                Longitud = 35,
                Offset = 28,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "DESCRC",
                NombreBaseDeDatos = "DescripcionCortaProducto",
                Descripcion = "Descripción corta del producto",
                Longitud = 20,
                Offset = 63,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "AUTORIZ",
                NombreBaseDeDatos = "Autorizacion",
                Descripcion = "Autorización del administrador",
                Longitud = 1,
                Offset = 83,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "CANTM",
                NombreBaseDeDatos = "CantidadMoneda",
                Descripcion = "Moneda para la relación",
                Longitud = 7,
                Offset = 84,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "LIM-MIN-PESOS",
                NombreBaseDeDatos = "LimiteMinimoPesos",
                Descripcion = "Límite minimo en pesos",
                Longitud = 7,
                Offset = 91,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "LIM-MIN-LITROS",
                NombreBaseDeDatos = "LimiteMinimoLitros",
                Descripcion = "Límite minimo en litros",
                Longitud = 7,
                Offset = 98,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "LIM-MAX-PESOS",
                NombreBaseDeDatos = "LimiteMaximoPesos",
                Descripcion = "Límite maximo en pesos",
                Longitud = 7,
                Offset = 105,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "LIM-MAX-LITROS",
                NombreBaseDeDatos = "LimiteMaximoLitros",
                Descripcion = "Límite maximo en litros",
                Longitud = 7,
                Offset = 112,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "PRIORI",
                NombreBaseDeDatos = "Priori",
                Descripcion = "Prioridad para la asignación",
                Longitud = 1,
                Offset = 119,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "ESTAP",
                NombreBaseDeDatos = "EstadoProducto",
                Descripcion = "Estado del producto",
                Longitud = 1,
                Offset = 120,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "PTOSREL",
                NombreBaseDeDatos = "Estado",
                Descripcion = "Puntos para la relación",
                Longitud = 5,
                Offset = 121,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "COD-LISTA",
                NombreBaseDeDatos = "CodigoLista",
                Descripcion = "Codigo de la lista",
                Longitud = 4,
                Offset = 126,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "COD-CRITE",
                NombreBaseDeDatos = "CodigoCriterio",
                Descripcion = "Codigo de criterio",
                Longitud = 4,
                Offset = 130,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "TIPO-TARJ",
                NombreBaseDeDatos = "TipoTarjeta",
                Descripcion = "Tipo de tarjeta",
                Longitud = 2,
                Offset = 134,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "LIM-DIA-MAX-PESO",
                NombreBaseDeDatos = "LimiteMaxPesos",
                Descripcion = "Limite diario en pesos",
                Longitud = 7,
                Offset = 136,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "LIM-DIA-MAX-LITRO",
                NombreBaseDeDatos = "LimiteMaxLitros",
                Descripcion = "Limite diario en litros",
                Longitud = 7,
                Offset = 143,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            return detalle;
        }
    }
}
