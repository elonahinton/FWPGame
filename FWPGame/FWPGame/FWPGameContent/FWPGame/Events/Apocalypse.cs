using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FWPGame.Engine;
using FWPGame.Powers;

namespace FWPGame.Events
{
    class Apocalypse
    {

        private FWPGame myGame;
        private MapTile[,] map;
        public Apocalypse(FWPGame game)
        {
            myGame = game;
        }

        public void BuildVillage()
        {
            map = myGame.map.MapTiles;
        }
    }
}
