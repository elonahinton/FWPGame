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
                        if (s.name.Equals("House") || s.name.Equals("Tree") || s.name.Equals("Road") || s.name.Equals("Wood"))
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
                if (tile.mySprites.ElementAt(0).name.Equals("GrassSprite"))
                {
                    tile.mySprites.RemoveAt(0);
                }
            }
                 
        }


        public override void PowerCombo(MapTile tile, Power power2)
        {
            if (power2.GetType().Name.Equals("Wind"))
            {
                power2.PowerCombo(tile, this);
            }
        }
    }
}
