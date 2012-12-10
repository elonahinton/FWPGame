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
namespace FWPGame.Powers
{
    class Wind : Power
    {
    public Wind(Texture2D icon, FWPGame aGame, Vector2 position, Vector2 mapPosition) :
         base(icon, aGame, position, mapPosition)
        {
            game = aGame;
            myPosition = position;
            myMapPosition = mapPosition;
            myXP = 15;
        }

        public override void Interact(MapTile tile)
        {                 
        }


        public override void PowerCombo(MapTile tile, Power power2)
        {
            if (power2.GetType().Name.Equals("Fire"))
            {
                Debug.WriteLine("power combo reached");
            }
        }
    }
}