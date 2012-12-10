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
    class BuildRoad : Power
{
        public BuildRoad(Texture2D icon, FWPGame aGame, Vector2 position, Vector2 mapPosition) :
            base(icon, aGame, position, mapPosition)
        {
            game = aGame;
            myPosition = position;
            myMapPosition = mapPosition;
            myXP = 10;
        }

        public override void Interact(MapTile tile)
        {
            tile.Clear();
            tile.Add(game.myRoad.Clone());
        }


        public override void PowerCombo(Power power2, MouseState mState)
        {
            //not currently implemented
        }
    }
}
