using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FWPGame.Engine;
using FWPGame.Items;
using System.Collections;
using System.Diagnostics;

namespace FWPGame.Powers
{
    class GrowGrass : Power
    {
        public GrowGrass(Texture2D icon, FWPGame aGame, Vector2 position, Vector2 mapPosition) :
            base(icon, aGame, position, mapPosition)
        {
            game = aGame;
            myPosition = position;
            myMapPosition = mapPosition;
            myXP = 3;
            name = "GrowGrass";
        }

        //only grow grass on empty tiles
        public override void Interact(MapTile tile)
        {
            if (tile.mySprites.Count == 0)
            {
                myXP = 3;
                tile.Add(game.myGrass.Clone());
            }
            else
            {
                myXP = 0;
            }
        }

        public override void PowerCombo(List<MapTile> tiles, Power power2)
        {
            //not currently implemented
        }
    }

}
