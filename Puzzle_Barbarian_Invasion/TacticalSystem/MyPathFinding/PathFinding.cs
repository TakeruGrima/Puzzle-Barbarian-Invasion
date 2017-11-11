using AStarPathFinding;
using static Microsoft.Xna.Framework.Vector2;
using Puzzle_Barbarian_Invasion.TacticalSystem.Deplacement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using DefineZone;
using Microsoft.Xna.Framework;

namespace Puzzle_Barbarian_Invasion.TacticalSystem.MyPathFinding
{
    class PathFinding
    {
        private bool[,] _map;
        private SearchParameters _searchParameters;

        private Trajet _trajet;
        private System.Drawing.Point _startLocation;
        private System.Drawing.Point _endLocation;

        private int _mapPosStart;
        private ZTaille _tMax;
        private ZEcart _ecart;

        public PathFinding(Trajet trajet, ZoneDeplacement zone)
        {
            _mapPosStart = zone._posStart;

            _tMax = new ZTaille(zone.GetTaille());
            _ecart = zone.GetEcart();

            _trajet = trajet;

            InitializeMap(zone);

            PathFinder pathFinder = new PathFinder(_searchParameters, 4);
            List<System.Drawing.Point> path = pathFinder.FindPath();
            ApplyPath(path);
            //ShowRoute("The algorithm should find a direct path without obstacles:", path);
        }

        public void InitializeMap(ZoneDeplacement zone)
        {
            _tMax._width = _tMax._width / _ecart._x;
            _tMax._height = _tMax._height / _ecart._y;

            this._map = new bool[_tMax._width, _tMax._height];

            for (int y = 0; y < _tMax._height; y++)
            {
                for (int x = 0; x < _tMax._width; x++)
                {
                    if (zone.Contains(new Vector2(x * _ecart._x + _mapPosStart, y * _ecart._y)))
                    {
                        _map[x, y] = true;
                    }
                    else
                    {
                        _map[x, y] = false;
                    }
                }
            }

            //Position final sur l'écran ( x32 ) -posStart pour x
            int startX = (int)(_trajet.GetFirstPosition().X - _mapPosStart);
            int startY = (int)(_trajet.GetFirstPosition().Y);

            int endX = (int)(_trajet.GetLastPosition().X - _mapPosStart);
            int endY = (int)(_trajet.GetLastPosition().Y);

            _endLocation = new System.Drawing.Point(endX / _ecart._x, endY / _ecart._y);
            _startLocation = new System.Drawing.Point(startX / _ecart._x, startY / _ecart._y);

            _searchParameters = new SearchParameters(_startLocation, _endLocation, _map);
        }

        public void ApplyPath(List<System.Drawing.Point> path)
        {
            Console.WriteLine(path.Count);
            if(path.Count >0 && path.Count<_trajet._count-1)
            {
                _trajet._vide = true;
                _trajet._trajet.Clear();

                _trajet.AddChemin(new Vector2(path[0].X * _ecart._x + _mapPosStart, path[0].Y * _ecart._y));
                foreach (System.Drawing.Point  curr in path)
                {
                    _trajet.AddChemin(new Vector2(curr.X * _ecart._x + _mapPosStart, curr.Y*_ecart._y));
                }
            }
            else if(path.Count == 0)
            {
                _trajet.Clear();
            }
        }

        /// <summary>
        /// Displays the map and path as a simple grid to the console
        /// </summary>
        /// <param name="title">A descriptive title</param>
        /// <param name="path">The points that comprise the path</param>
        private void ShowRoute(string title, IEnumerable<System.Drawing.Point> path)
        {
            Console.WriteLine("{0}\r\n", title);
            for (int y = 0; y < this._map.GetLength(1); y++) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < this._map.GetLength(0); x++)
                {
                    if (this._searchParameters.EqualStart(x,y))
                        // Show the start position
                        Console.Write('S');
                    else if (this._searchParameters.EqualEnd(x,y))
                        // Show the end position
                        Console.Write('F');
                    else if (this._map[x, y] == false)
                        // Show any barriers
                        Console.Write('░');
                    else if (path.Where(p => p.X == x && p.Y == y).Any())
                        // Show the path in between
                        Console.Write('*');
                    else
                        // Show nodes that aren't part of the path
                        Console.Write('·');
                }

                Console.WriteLine();
            }
        }
    }
}
