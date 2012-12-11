using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using FWPGame.Powers;
using System.Collections;

namespace FWPGame.Engine
{
    public class Cursor : Sprite
    {
        private FWPGame myGame;
        private ArrayList myPowers;
        private List<Sprite> sprites;
        public Cursor(Texture2D texture, Vector2 position, FWPGame game, ArrayList powers) :
            base(texture, position)
        {
            myGame = game;
            myTexture = texture;
            myPosition = position;
            myPowers = powers;
            SetupInput();
            sprites = new List<Sprite>();
        }


        /// <summary>
        /// Might be  obsolete now?  Not sure...
        /// </summary>
        /// <param name="mState"></param>
        /// <returns></returns>
        public List<Sprite> getTileSprites(MouseState mState)
        {
            List<Sprite> tileSprites = new List<Sprite>();
            MapTile tile = myGame.map.GetTile(this);
            if (tile != null)
            {
                tileSprites = tile.mySprites;
            }
            return tileSprites;
        }

        /// <summary>
        /// Update the map position.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime, Vector2 worldScale)
        {
            myMapPosition = myGame.player.myMapPosition + myPosition;
            
        }

        /// <summary>
        /// Get the map to get the tile using itself as a parameter.  Pretty roundabout and silly, but it works..
        /// </summary>
        /// <returns></returns>
        public MapTile getTile()
        {
            return myGame.map.GetTile(this);
        }

        /// <summary>
        /// Draw the little cursor hand.
        /// </summary>
        /// <param name="batch"></param>
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(myTexture, myPosition,
                   null, Color.White,
                   myAngle, myOrigin, myScale,
                   SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Set up the "following"
        /// </summary>
        public void SetupInput()
        {
            // This action lets the cursor follow the mouse
            GameAction follow = new GameAction(
                this,
                this.GetType().GetMethod("Follow"),
                new object[0]);
            InputManager.AddToMouseMap(InputManager.POSITION, follow);


            object[] param = new object[1];
            object[] paramZ = new object[1];
            param[0] = -1;
            GameAction zoomOut = new GameAction(
                this,
                this.GetType().GetMethod("Zoom"),
                param);
            paramZ[0] = 1;
            GameAction zoomIn = new GameAction(
                this,
                this.GetType().GetMethod("Zoom"),
                paramZ);

            InputManager.AddToKeyboardMap(Keys.Add, zoomIn);
            InputManager.AddToKeyboardMap(Keys.Subtract, zoomOut);
        }

        /// <summary>
        /// The actual following.
        /// </summary>
        /// <param name="position"></param>
        public void Follow(Vector2 position)
        {
            myPosition.X = position.X - myTexture.Width / 2;
            myPosition.Y = position.Y - myTexture.Height / 2;
        }

        //1 for zoom in, -1 for zoom out, 0 for reset zoom
        public void Zoom(int dir)
        {
            Vector2 zoomVelocity = new Vector2(0.1f, 0.1f);
            if (dir == -1 && (myGame.worldScale.X > myGame.map.baseScale.X) && (myGame.worldScale.Y > myGame.map.baseScale.Y))
            {
                myGame.worldScale -= zoomVelocity;
                Debug.WriteLine(myGame.worldScale);
            }
            else if (dir == 1)
            {
                myGame.worldScale += zoomVelocity;
            }
            else if (dir == 0)
            {
                myGame.worldScale = myGame.map.baseScale;
            }
        }
    }
}
