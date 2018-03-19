using Hexacta.YPF.Fidelizacion.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexacta.YPF.Fidelizacion.Core.Procesos
{
    public static class GenerarLOACRITE
    {

        public static Archivo Generar()
        {
            Archivo archivo = new Archivo()
            {
                Nombre = "LOACRITE",
                OrigenDatos = "mockLOACRITE",
                IsUnixSaltoLinea = true
            };
            archivo.Cabecera = GenerarCabecera();
            archivo.Detalle = GenerarDetalle();
            archivo.Detalle.SubDetalle = GenerarSubDetalle();

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
                NombreCampo = "VERSIONACES",
                NombreBaseDeDatos = "version",
                Descripcion = "Versión de los datos de Serviclub",
                Longitud = 5,
                Offset = 18,
                PadCaracter = '0',
                IsPadLeft = true
            };
            cabecera.Campos.Add(campoCabecera);

            return cabecera;
        }

        private static Detalle GenerarDetalle()
        {
            Detalle detalle = new Detalle();
            detalle.NombreTabla = "registro";
            detalle.Campos = new List<CampoDetalle>();
            
            CampoDetalle campoDetalle = new CampoDetalle()
            {
                NombreCampo = "COD-CRITE",
                NombreBaseDeDatos = "CodigoCriterio",
                Descripcion = "Código de criterio",
                Longitud = 4,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "DESCR",
                NombreBaseDeDatos = "CodigoTexto",
                Descripcion = "Código de texto",
                Longitud = 35,
                Offset = 4,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            detalle.Campos.Add(campoDetalle);

            return detalle;
        }

        private static Detalle GenerarSubDetalle()
        {
            Detalle subDetalle = new Detalle();
            subDetalle.NombreTabla = "subRegistro";
            subDetalle.Campos = new List<CampoDetalle>();

            CampoDetalle registro = new CampoDetalle()
            {
                NombreCampo = "ITEM",
                NombreBaseDeDatos = "Item",
                Descripcion = "Items para utilizar en los criterios",
                Longitud = 5,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            subDetalle.Campos.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "SIMBOLO",
                NombreBaseDeDatos = "Simbolo",
                Descripcion = "Símbolo operador",
                Longitud = 2,
                Offset = 5,
                PadCaracter = ' ',
                IsPadLeft = true
            };
            subDetalle.Campos.Add(registro);

            registro = new CampoDetalle()
            {
                NombreCampo = "VALOR",
                NombreBaseDeDatos = "Valores",
                Descripcion = "Valores de los criterios separados por “;” para en lista y entre",
                Longitud = 20,
                Offset = 7,
                PadCaracter = ' ',
                IsPadLeft = true
            };
            subDetalle.Campos.Add(registro);

            return subDetalle;
        }
    }
}
