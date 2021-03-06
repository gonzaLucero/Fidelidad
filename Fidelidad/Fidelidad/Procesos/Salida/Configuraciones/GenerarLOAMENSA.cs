﻿using Hexacta.YPF.Fidelizacion.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexacta.YPF.Fidelizacion.Core.Procesos
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

        private static Detalle GenerarRegistro()
        {
            Detalle detalle = new Detalle();
            detalle.NombreTabla = "registro";
            detalle.Campos = new List<CampoDetalle>();

            CampoDetalle campoDetalle = new CampoDetalle()
            {
                NombreCampo = "COD-MENSA",
                NombreBaseDeDatos = "CodigoMensaje",
                Descripcion = "Código del mensaje",
                Longitud = 4,
                Offset = 0,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "DESCR",
                NombreBaseDeDatos = "DescripcionMensaje",
                Descripcion = "Descripción del mensaje",
                Longitud = 35,
                Offset = 4,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "FEDESDE",
                NombreBaseDeDatos = "FechaDesde",
                Descripcion = "Fecha de inicio de vigencia",
                Longitud = 8,
                Offset = 39,
                PadCaracter = ' ',
                IsPadLeft = false
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "FEHASTA",
                NombreBaseDeDatos = "FechaHasta",
                Descripcion = "Fecha de fin de vigencia",
                Longitud = 8,
                Offset = 47,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "COD-LISTA",
                NombreBaseDeDatos = "CodigoLista",
                Descripcion = "Código de la lista",
                Longitud = 4,
                Offset = 55,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "COD-TEXTO",
                NombreBaseDeDatos = "CodigoTexto",
                Descripcion = "Código de texto",
                Longitud = 4,
                Offset = 59,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
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
            detalle.Campos.Add(campoDetalle);

            campoDetalle = new CampoDetalle()
            {
                NombreCampo = "COD-CRITE",
                NombreBaseDeDatos = "CodigoCriterio",
                Descripcion = "Código del criterio",
                Longitud = 4,
                Offset = 64,
                PadCaracter = '0',
                IsPadLeft = true
            };
            detalle.Campos.Add(campoDetalle);

            return detalle;
        }
    }
}
