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
        public Cursor(Texture2D texture, Vector2 position) :
            base(texture, position)
        {
            myTexture = texture;
            myPosition = position;
            SetupInput();
        }

        /// <summary>
        /// Update the map position.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime, Vector2 playerPos)
        {
            myMapPosition = playerPos + myPosition;
            
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

    }
}
