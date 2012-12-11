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

namespace FWPGame.Powers
{
    class BuildHouse : Power
    {
        public BuildHouse(Texture2D icon, FWPGame aGame, Vector2 position, Vector2 mapPosition) :
            base(icon, aGame, position, mapPosition)
        {
            game = aGame;
            myPosition = position;
            myMapPosition = mapPosition;
            myXP = 20;
            name = "BuildHouse";
        }

        public override void Interact(MapTile tile)
        {
            bool grassFound = false;
            bool hasFeature = false;
            if (tile.mySprites.Count > 0)
            {
                foreach (Sprite s in tile.mySprites)
                {
                    if (s != null)
                    {
                        if (s.name.Equals("Grass"))
                        {
                            grassFound = true;
                        }
                        if (s.name.Equals("House") || s.name.Equals("Tree"))
                        {
                            hasFeature = true;
                        }
                    }
                }
            }
            if (grassFound && hasFeature == false)
            {
                tile.Add(game.motherHouse.Clone());
            }
        }

        public override void PowerCombo(List<MapTile> tiles, Power power2)
        {
            //not currently implemented
        }

    }
}
