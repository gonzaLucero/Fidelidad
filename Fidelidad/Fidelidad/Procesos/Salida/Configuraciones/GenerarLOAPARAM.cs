using Hexacta.YPF.Fidelizacion.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexacta.YPF.Fidelizacion.Core.Procesos
{
    public static class GenerarLOAPARAM
    {

        public static Archivo Generar()
        {
            Archivo archivo = new Archivo()
            {
                Nombre = "LOAPARAM",
                OrigenDatos = "mockLOAPARAM",
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

            return cabecera;
        }

        private static Detalle GenerarRegistro()
        {
            Detalle detalle = new Detalle();
            detalle.NombreTabla = "registro";
            detalle.Campos = new List<CampoDetalle>();

            CampoDetalle campoDetalle = new CampoDetalle()
            {
                NombreCampo = "SECCION",
                NombreBaseDeDatos = "NombreSeccion",
                Descripcion = "Nombre de la sección a modificar",
                Longitud = 30,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "KEY",
                NombreBaseDeDatos = "NombreElemento",
                Descripcion = "Nombre del elemento a modificar",
                Longitud = 40,
                Offset = 30,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "VALOR",
                NombreBaseDeDatos = "Valor",
                Descripcion = "Valor para la actualización",
                Longitud = 100,
                Offset = 70,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "TAM",
                NombreBaseDeDatos = "Tamanio",
                Descripcion = "Tamaño del nuevo valor",
                Longitud = 3,
                Offset = 170,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            return detalle;
        }
    }
}
