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
                        if (s.name.Equals("House") || s.name.Equals("Tree"))
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
            }
                 
        }


        public override void PowerCombo(Power power2, MouseState mState)
        {
            //not currently implemented
        }
    }
}
