using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //class for simple dysplaying saved games in SavedGames.xml
    class GameInfo
    {
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

        private string difficulty;
        public string _difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }

        private int timePassed;
        public int _timePassed
        {
            get { return timePassed; }
            set { timePassed = value; }
        }

        public GameInfo(string name, string lastAlteration, string difficulty, int timePassed)
        {
            this.name = name;
            this.lastAlteration = lastAlteration;
            this.difficulty = difficulty;
            this.timePassed = timePassed;
        }
        
        
    }
}
