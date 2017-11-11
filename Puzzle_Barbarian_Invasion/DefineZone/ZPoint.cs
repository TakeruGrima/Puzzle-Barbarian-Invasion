using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineZone
{
    partial class ZPoint
    {
        public int _x;
        public int _y;

        public ZPoint(int x,int y)
        {
            _x = x;
            _y = y;
        }

        public ZPoint(ZPoint p)
        {
            _x = p._x;
            _y = p._y;
        }

        public double Distance(ZPoint p,ZEcart ecart)
        {
            int x1 = _x / ecart._x;
            int x2 = p._x / ecart._x;

            int y1 = _y / ecart._y;
            int y2 = p._y / ecart._y;

            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public double Distance(int x,int y,ZEcart ecart)
        {
            return Distance(new ZPoint(x, y), ecart);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            ZPoint p = obj as ZPoint;
            if (!_x.Equals(p._x))
                return false;
            if (!_y.Equals(p._y))
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            // Some comment to explain if there is a real problem with providing GetHashCode() 
            // or if I just don't see a need for it for the given class
            return 13 * _x.GetHashCode() + _y.GetHashCode();
        }

        public override String ToString()
        {
            return "(" + _x + ";" + _y + ")";
        }
    }
}
