using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathFinding
{
    /// <summary>
    /// Defines the parameters which will be used to find a path across a section of the map
    /// </summary>
    public class SearchParameters
    {
        public Point StartLocation { get; set; }

        public Point EndLocation { get; set; }
        
        public bool[,] Map { get; set; }

        public SearchParameters(Point startLocation, Point endLocation, bool[,] map)
        {
            this.StartLocation = startLocation;
            this.EndLocation = endLocation;
            this.Map = map;
        }

        public bool EqualStart(int x,int y)
        {
            if(StartLocation.X != x)
            {
                return false;
            }
            if (StartLocation.Y != y)
            {
                return false;
            }
            return true;
        }

        public bool EqualEnd(int x, int y)
        {
            if (EndLocation.X != x)
            {
                return false;
            }
            if (EndLocation.Y != y)
            {
                return false;
            }
            return true;
        }
    }
}
