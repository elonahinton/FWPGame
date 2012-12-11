using System;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using FWPGame.Powers;

namespace FWPGame.Engine
{
    public class Player
    {
        //screen position (relative)
        public Vector2 myPosition;
        //map position (absolute)
        public Vector2 myMapPosition;
        private Vector2 myVelocity;
        private Vector2 myScreenSize;
        private bool canGoUp
        {
            get
            {
                return myMapPosition.Y <= 0 ? false : true;
            }
        }
        private bool canGoLeft
        {
            get
            {
                return myMapPosition.X <= 0 ? false : true;
            }
        }
        private bool canGoRight
        {
            get
            {
                return myMapPosition.X + myScreenSize.X + 20 >= myMapSize.X ? false : true;
            }
        }
        private bool canGoDown
        {
            get
            {
                return myMapPosition.Y + myScreenSize.Y + 20 >= myMapSize.Y ? false : true;
            }
        }

        private float myAngle = 0f;
        private Vector2 myOrigin = new Vector2(0, 0);
        private Vector2 myScale = new Vector2(1, 1);

        protected internal Cursor myCursor;
        protected internal Vector2 myMapSize;
        private ArrayList myPowers;
        private Power mySelectedPower;
        private const int MAX_POWER_HOTKEYS = 5;

        private Texture2D myIcon;
        private SpriteFont myFont;

        public Player(Texture2D icon, SpriteFont font, Vector2 mapPos, Vector2 screenSize,
            Vector2 mapSize, Cursor cursor, ArrayList powers)
        {
            mySelectedPower = (Power)powers[0];
            myIcon = icon;
            myFont = font;
            myPowers = powers;
            myMapPosition = mapPos;
            myMapSize = mapSize;
            myCursor = cursor;
            myScreenSize = screenSize;
            myVelocity = new Vector2(0, 0);
            SetupInput();
        }

        /// <summary>
        /// First update the map position with the velocity. Then set velocity to 0 and get new velocity.
        ///  
        /// Check for cursor location and decide whether to move the player via mouse scrolling.  
        /// </summary>
        /// <param name="elapsedTime">game time elapsed</param>
        public void Update(GameTime gameTime, Vector2 worldScale)
        {
            myMapPosition += myVelocity;
            myVelocity = new Vector2(0, 0);

            if ((myCursor.myPosition.Y <= 20))
            {
                MoveUp();
            }
            else if (myCursor.myPosition.Y >= (myScreenSize.Y - 40))
            {
                MoveDown();
            }
            if (myCursor.myPosition.X <= 20)
            {
                MoveLeft();
            }
            else if (myCursor.myPosition.X >= (myScreenSize.X - 20))
            {
                MoveRight();
            }

        }

        public void MoveLeft()
        {
            myVelocity.X = 0;
            if (canGoLeft)
                myVelocity.X = -20;
        }

        public void MoveRight()
        {
            myVelocity.X = 0;
            if (canGoRight)
                myVelocity.X = 20;
        }

        public void MoveUp()
        {
            myVelocity.Y = 0;
            if (canGoUp)
                myVelocity.Y = -20;
        }

        public void MoveDown()
        {
            myVelocity.Y = 0;
            if (canGoDown)
                myVelocity.Y = 20;
        }

        /// <summary>
        /// Three separate regions:
        /// 1. Movement: W,A,S,D keys
        /// 2. Power selection: 1,2,3,4,5 for now
        /// 3. Power usage: LMB for main use, RMB for alt-fire
        /// </summary>
        public void SetupInput()
        {
            #region Movement
            GameAction moveLeft = new GameAction(
              this,
              this.GetType().GetMethod("MoveLeft"),
              new object[0]);
            GameAction moveRight = new GameAction(
              this,
              this.GetType().GetMethod("MoveRight"),
              new object[0]);
            GameAction moveUp = new GameAction(
              this,
              this.GetType().GetMethod("MoveUp"),
              new object[0]);
            GameAction moveDown = new GameAction(
              this,
              this.GetType().GetMethod("MoveDown"),
              new object[0]);

            InputManager.AddToKeyboardMap(Keys.W, moveUp);
            InputManager.AddToKeyboardMap(Keys.A, moveLeft);
            InputManager.AddToKeyboardMap(Keys.S, moveDown);
            InputManager.AddToKeyboardMap(Keys.D, moveRight);

            #endregion

            #region Power Selection Hotkeys

            Keys[] possibleHotkeys = { Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5 };
            for (int i = 0; i < MAX_POWER_HOTKEYS; i++)
            {
                object[] param = new object[1];
                param[0] = i;
                GameAction hotkey = new GameAction(
                    this,
                    this.GetType().GetMethod("powerHotkey"),
                    param);
                InputManager.AddToKeyboardMap(possibleHotkeys[i], hotkey);
            }
            #endregion

            #region Power Usage Hotkeys
            GameAction powerLMBClick = new GameAction(
                this,
                this.GetType().GetMethod("usePower"),
                new object[0]);

            GameAction powerRMBClick = new GameAction(
                this,
                this.GetType().GetMethod("altUsePower"),
                new object[0]);

            InputManager.AddToMouseMap(InputManager.LEFT_BUTTON, powerLMBClick);
            InputManager.AddToMouseMap(InputManager.RIGHT_BUTTON, powerRMBClick);
            #endregion

        }

        /// <summary>
        /// Set the hotkey to the power.
        /// </summary>
        /// <param name="num"></param>
        public void powerHotkey(int num)
        {
            if (myPowers.Count > num)
                mySelectedPower = (Power)myPowers[num];
            else
                Debug.WriteLine("That power does not exist.");
        }

        /// <summary>
        /// If we aren't offscreen, use the power.
        /// </summary>
        /// <param name="mouseClickPosition"></param>
        public void usePower(Vector2 mouseClickPosition)
        {
            if (mouseClickPosition.X >= 0 && mouseClickPosition.X < myScreenSize.X
                && mouseClickPosition.Y > 0 && mouseClickPosition.Y < myScreenSize.Y)
            {
                MapTile tile = myCursor.getTile();
                mySelectedPower.Interact(tile);
            }
        }

        public void altUsePower(Vector2 mouseClickPosition)
        {
            // empty for now
        }

        /// <summary>
        /// Draw the pseudo-UI - the power hotkeys - with a gray fill-in for the selected one.
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch)
        {
            Vector2 textPos = new Vector2(0, 0);
            textPos.Y = myScreenSize.Y - 25;
            Vector2 iconPos = new Vector2(0,0);
            iconPos.Y = myScreenSize.Y - 129;
            

            int f = myPowers.Count;
            iconPos.X = (myScreenSize.X / 2) - (129*(f/2));
            textPos.X = iconPos.X + 58;
            for (int i = 0; i < f; i++)
            {
                Power p = (Power)myPowers[i];
                batch.Draw(p.myIcon, iconPos, null, Color.White, myAngle, myOrigin, myScale,
                    SpriteEffects.None, 0f);
                if (p.Equals(mySelectedPower))
                {
                    batch.Draw(myIcon, iconPos, null, Color.Gray, myAngle, myOrigin, myScale, SpriteEffects.None, 0f);
                }
                else
                {
                batch.Draw(myIcon, iconPos, null, Color.White, myAngle, myOrigin, myScale,
                    SpriteEffects.None, 0f);
                }
                batch.DrawString(myFont, "" + (i+1), textPos, Color.Black);
                iconPos.X += 129;
                textPos.X += 129;
            }
        }
        
    }
}
