using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineZone
{
    /**
     * Cette classe sert à définir une zone où sont contenu des cases ou des points qui satisfont des contraintes précises
     * La contrainte de base est la suivante:
     * - Chaque points sont relié entre eux directement ou par des points voisins
     * Cette classe est une classe abstraite, pour qu'elle marche il faut utilisé ses classes filles
    **/
    abstract class Zone
    {
        protected ZEcart _ecart;//contient l'écart entre deux points adjacent
        protected List<ZPoint> _points;//Contient les points de la zone
        protected ZTaille _taille;//Contient la taille maximale de la zone( en prenant en compte l'écart)

        protected List<ZPoint> _listVoisin;//liste utilisé pour la recherche des voisins d'un point(expliqué plus tard)

        public Zone(ZEcart ecart,ZTaille taille, ZPoint depart)
        {
            _ecart = ecart;
            _taille = taille;

            Console.WriteLine("ecart:("+_ecart._x+";"+_ecart._y+")");
            Console.WriteLine("taille:(" + _taille._height + ";" + _taille._width + ")");

            _points = new List<ZPoint>();
        }

        public ZTaille GetTaille()
        {
            return _taille;
        }
    
        public ZEcart GetEcart()
        {
            return _ecart;
        }

        public virtual void AddVoisin(ZPoint point)
        {
            _listVoisin.Add(point);
        }

        public virtual void AddVoisin(int x,int y)
        {
            _listVoisin.Add(new ZPoint(x, y));
        }

        public virtual void Add(ZPoint point)
        {
            _points.Add(point);
        }

        public virtual void Add(int x,int y)
        {
            _points.Add(new ZPoint(x, y));
        }

        //Méthode voisin: détermine le voisin d'un point
        void Voisin(ZPoint p)
        {
            _listVoisin = new List<ZPoint>();//liste qui va contenir les voisins (4 voisins)

            bool inf_max_x = false, inf_max_y = false;

            if (p._x < _taille._width - _ecart._x)//on teste si le voisin du point sort de la zone maximale vers la droite
            {
                inf_max_x = true;//x pourra avoir un voisin à droite
            }
            if (p._y < _taille._height - _ecart._y)//on teste si le voisin de la case  sort de la zone maximale vers le bas
            {
                inf_max_y = true;//y pourra avoir un voisin en bas
            }

            //recherche des voisins dans les 4 directions
            if (p._x > 0 && Contrainte(p._x - _ecart._x, p._y))//recherche du voisin de gauche
            {
                AddVoisin(p._x - _ecart._x, p._y);//mise du voisin de gauche dans la liste 
            }
            if (inf_max_x == true && p._x >= 0 && Contrainte(p._x + _ecart._x, p._y))//recherche du voisin de  droite 
            {
                AddVoisin(p._x + _ecart._x, p._y);//mise du voisin de droite dans la liste
            }
            if (p._y > 0 && Contrainte(p._x, p._y - _ecart._y))//recherche du voisin du haut
            {
                AddVoisin(p._x, p._y-_ecart._y);//mise du voisin du haut dans la liste
            }
            if (inf_max_y == true && p._y >= 0 && Contrainte(p._x, p._y + _ecart._y))//recherche du voisin du bas
            {
                AddVoisin(p._x, p._y + _ecart._y);//mise du voisin du bas dans la liste
            }
        }


        //Méthode getGroupe: la fonction sert à définir la zone
        public void GetGroupe(ZPoint p)
        {
            int i, j;//compteurs

            i = p._x;
            j = p._y;

            Voisin(p);//fonction qui récupère la liste des voisins de (x,y)

            Add(p);//on ajoute le point dans la zone 
            foreach (ZPoint curr in _listVoisin)//on parcourt la liste des voisins
            {
                i = (int)curr._x;
                j = (int)curr._y;

                if (_points.Contains(new ZPoint(i,j)) == false)//si le voisin n'est pas encore dans le groupe
                {
                    GetGroupe(new ZPoint(i,j));/*on relance la fonction afin de voir les voisins du point courant
                    vont dans _move*/
                }
            }
        }

        //Méthodes gérant la contrainte, ici la contrainte est que le point n'est pas déja dans la zone
        public virtual bool Contrainte(ZPoint p)
        {
            return !_listVoisin.Contains(p);
        }

        public virtual bool Contrainte(int x,int y)
        {
            ZPoint p = new ZPoint(x, y);

            return !_listVoisin.Contains(p);
        }
    }
}
