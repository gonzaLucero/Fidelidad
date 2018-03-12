using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fidelidad.Config
{
    public class Configuracion
    {
        public string NombreActual { get; set; }

        public string NombreCLM { get; set; }

        public int Tamanio { get; set; }

        public int Posicion { get; set; }

        public Configuracion(){ }

        public Configuracion(string nombreActual, string nombreCLM, int tamanio, int posicion)
        {
            NombreActual = nombreActual;
            NombreCLM = nombreCLM;
            Tamanio = tamanio;
            Posicion = posicion;
        }
    }
}
