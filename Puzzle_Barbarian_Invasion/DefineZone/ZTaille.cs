using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineZone
{
    /**
     * La classe ZTaille contient la taille maximale d'une zone en x et en y, dans le cas d'un quadrillage il s'agit de la taille
     * du quadrillage et pour un écran la taille de l'écran
     */
    class ZTaille
    {
        public int _width { get; set; }
        public int _height { get; set; }

        public ZTaille(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public ZTaille(ZTaille taille)
        {
            _width = taille._width;
            _height = taille._height;
        }

        public override String ToString()
        {
            return "(" + _width + ";" + _height + ")";
        }
    }
}
