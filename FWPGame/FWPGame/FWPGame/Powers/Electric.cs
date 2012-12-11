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
using System.Collections;
using System.Reflection;

namespace FWPGame.Powers
{
    class Electric : Power
    {
        public Electric(Texture2D icon, FWPGame aGame, Vector2 position, Vector2 mapPosition) :
            base(icon, aGame, position, mapPosition)
        {
            game = aGame;
            myPosition = position;
            myMapPosition = mapPosition;
            myXP = 15;
            name = "Electric";
        }

        public override void Interact(MapTile tile)
        {
            bool isElectrocutable = false;
            Sprite spriteToElectro = null;
            if (tile.mySprites.Count > 0)
            {
                foreach (Sprite s in tile.mySprites)
                {
                    if (s != null)
                    {
                        if ((s.name.Equals("House")) || (s.name.Equals("People")) || (s.name.Equals("Tree")))
                        {
                            isElectrocutable = true;
                            spriteToElectro = s;
                            break;
                        }
                    }
                }
            }
            if (isElectrocutable)
            {
                MethodInfo myMethod = spriteToElectro.GetType().GetMethod("electrocute");
                myMethod.Invoke(spriteToElectro, null);
            }
        }

        public override void PowerCombo(MapTile tile, Power power2)
        {
            //not currently implemented
        }

    }
}
