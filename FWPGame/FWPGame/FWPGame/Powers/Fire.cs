using System;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FWPGame.Engine;
using FWPGame.Items;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace FWPGame.Powers
{
    class Fire : Power
    {

        public Fire(Texture2D icon, FWPGame aGame, Vector2 position, Vector2 mapPosition) :
            base(icon, aGame, position, mapPosition)
        {
            game = aGame;
            myPosition = position;
            myMapPosition = mapPosition;
            myXP = 15;
            name = "Fire";
        }

        public override void Interact(MapTile tile)
        {
            bool isBurnable = false;
            Sprite spriteToBurn = null;
            if (tile.mySprites.Count > 0)
            {
                foreach (Sprite s in tile.mySprites)
                {
                    if (s != null)
                    {
                        if (s.name.Equals("House") || s.name.Equals("Tree") ||
                            s.name.Equals("Road") || s.name.Equals("Wood"))
                        {
                            isBurnable = true;
                            spriteToBurn = s;
                            break;
                        }
                    }
                }
            }
            if (isBurnable)
            {
                MethodInfo myMethod = spriteToBurn.GetType().GetMethod("burn");
                myMethod.Invoke(spriteToBurn, null);
                if (tile.mySprites.ElementAt(0).name.Equals("Grass"))
                {
                    tile.mySprites.RemoveAt(0);
                }
            }
                 
        }


        public override void PowerCombo(List<MapTile> tiles, Power power2)
        {
            MapTile tile = tiles[0];
            tiles.RemoveAt(0);
            if (power2.name.Equals("Wind"))
            {
                Interact(tile);
                MapTile burnTile1 = pickATile(tiles);
                Interact(burnTile1);
                tiles.Remove(burnTile1);
                MapTile burnTile2 = pickATile(tiles);
                Interact(burnTile2);
            }
        }

        public MapTile pickATile(List<MapTile> tiles)
        {
            Random rand = new Random();
            int index = rand.Next(0, tiles.Count);
            return tiles[index];
        }
    }
}
