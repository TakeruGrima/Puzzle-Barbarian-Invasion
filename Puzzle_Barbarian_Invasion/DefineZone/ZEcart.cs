using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineZone
{
    /**
     * La classe ZEcart défini l'écart en abscisse et en ordonnée entre deux points adjacents, par exemple si les deux points
     * ne sont pas des simples point sur un repère mais la position d'une image sur un écran, l'écart en X et en Y seront égal
     * à la taille en X et en Y d'une image
     */
    class ZEcart
    {
        public int _x { get; set; }
        public int _y { get; set; }

        public ZEcart(int x,int y)
        {
            _x = x;
            _y = y;
        }
    }
}
