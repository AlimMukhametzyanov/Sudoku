﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //class for simple dysplaying saved games in SavedGames.xml
    class GameInfo
    {
        private int id;
        public int _id
        {
            get { return id; }
            set { id = value; }
        }
        
        private string name;
        public string _name
        {
            get { return name; }
            set { name = value; }
        }

        private string lastAlteration;
        public string _lastAlteration
        {
            get { return lastAlteration; }
            set { lastAlteration = value; }
        }

        private string timePassed;
        public string _timePassed
        {
            get { return timePassed; }
            set { timePassed = value; }
        }

        public GameInfo(int id, string name, string lastAlteration, string timePassed)
        {
            this.id = id;
            this.name = name;
            this.lastAlteration = lastAlteration;
            this.timePassed = timePassed;
        }
        
        
    }
}
