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
    class MakePerson : Power
    {
        private People person;

        public MakePerson(Texture2D icon, FWPGame aGame, Vector2 position, Vector2 mapPosition) :
            base(icon, aGame, position, mapPosition)
        {
            game = aGame;
            myPosition = position;
            myMapPosition = mapPosition;
            myXP = 30;
            
        }

        public override void Interact(MapTile tile)
        {
            //bool grassFound = false;
            //if (tile.mySprites.Count > 0)
            //{
            //    foreach (Sprite s in tile.mySprites)
             //   {
             //       if (s != null)
              //      {
              //          if (s.name.Equals("GrassSprite"))
              //          {
              //              grassFound = true;
              //              break;
              //          }
              //      }
               // }
            //}
            //if (grassFound)
                //tile.Clear();
                person = game.person.Clone();
                tile.Add(person);
            }

        public override void PowerCombo(MapTile tile, Power power2)
        {
            //not currently implemented
        }
        }

    }

