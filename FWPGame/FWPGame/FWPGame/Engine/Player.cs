using System;
using System.Collections;
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
using FWPGame.Events;

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
        

        private float myAngle = 0f;
        private Vector2 myOrigin = new Vector2(0, 0);
        private Vector2 myScale = new Vector2(1, 1);

        protected internal Cursor myCursor;
        protected internal Vector2 myMapSize;
        protected internal Map myMap;
        private ArrayList myPowers;
        protected internal List<Power> availablePowers;
        private Power mySelectedPower;
        private Power myPreviousPower;
        private const int MAX_POWER_HOTKEYS = 10;

        protected internal int myLevel;
        protected internal int myXP;
        protected internal int myXPtoNext;
        protected internal int levelProgress;

        private Texture2D myXPbar;
        private Texture2D myXPblock;
        private SpriteFont levelFont;
        private Texture2D myIcon;
        private Texture2D myIconBG;
        private SpriteFont myFont;

        private Event scene;
        private Texture2D dadGod;
        private Texture2D broGod;
        private Texture2D sisGod;
        private SpriteFont dadFont;
        private SpriteFont broFont;
        private SpriteFont sisFont;

        public Player(ContentManager content, Map map, Vector2 screenSize, Cursor cursor, ArrayList powers)
        {
            mySelectedPower = (Power)powers[0];
            myPreviousPower = (Power)powers[0];
            myPowers = powers;
            myMap = map;
            myCursor = cursor;
            myScreenSize = screenSize;
            myVelocity = new Vector2(0, 0);
            myMapPosition = new Vector2(0, 0);
            myMapSize = myMap.mySize;

            myIcon = content.Load<Texture2D>("UI/icon");
            myIconBG = content.Load<Texture2D>("UI/iconBG");
            myFont = content.Load<SpriteFont>("ChillerFont");
            myXPbar = content.Load<Texture2D>("UI/xpbar");
            myXPblock = content.Load<Texture2D>("UI/xp");
            levelFont = content.Load<SpriteFont>("UI/LevelFont");
            myLevel = 0;
            myXP = 0;
            myXPtoNext = 100;

            dadGod = content.Load<Texture2D>("gods/DaddyGod");
            broGod = content.Load<Texture2D>("gods/BrotherGod");
            sisGod = content.Load<Texture2D>("gods/SisterGod");

            dadFont = content.Load<SpriteFont>("gods/DaddyFont");
            broFont = content.Load<SpriteFont>("gods/BrotherFont");
            sisFont = content.Load<SpriteFont>("gods/SisterFont");

            SetupInput();
            scene = new Introduction(content.Load<Texture2D>("gods/DaddyGod"), new Vector2(0, 0), new Vector2(0, 0), dadFont);
        }

        /// <summary>
        /// First update the map position with the velocity. Then set velocity to 0 and get new velocity.
        ///  
        /// Check for cursor location and decide whether to move the player via mouse scrolling.  
        /// </summary>
        /// <param name="elapsedTime">game time elapsed</param>
        public void Update(GameTime gameTime)
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
            myMap.Update(gameTime, myMapPosition);
            myCursor.Update(gameTime, myMapPosition);

            if (scene != null)
            {
                scene.myEventState.Update(gameTime, myMapPosition);
            }
        }

        /// <summary>
        /// Draw the pseudo-UI - the power hotkeys - with a white fill-in for the selected one.
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch)
        {

            myMap.Draw(batch, myMapPosition);
            myCursor.Draw(batch);

            #region Power Hotkeys
            Vector2 textPos = new Vector2(0, 0);
            textPos.Y = myScreenSize.Y - 25;
            Vector2 iconPos = new Vector2(0, 0);
            iconPos.Y = myScreenSize.Y - 129;


            int f = myPowers.Count;
            iconPos.X = (myScreenSize.X / 2) - (129 * (f / 2));
            textPos.X = iconPos.X + 58;
            for (int i = 0; i < f; i++)
            {
                Power p = (Power)myPowers[i];
                if (p.Equals(mySelectedPower))
                {
                    batch.Draw(myIconBG, new Vector2(iconPos.X + 1, iconPos.Y - 1), null, Color.White, myAngle, myOrigin, myScale, SpriteEffects.None, 0f);
                    batch.Draw(p.myIcon, iconPos, null, Color.White, myAngle, myOrigin, myScale,
    SpriteEffects.None, 0f);
                    batch.Draw(myIcon, iconPos, null, Color.White, myAngle, myOrigin, myScale, SpriteEffects.None, 0f);
                }
                else
                {
                    batch.Draw(myIconBG, new Vector2(iconPos.X + 1, iconPos.Y - 1), null, Color.Gray, myAngle, myOrigin, myScale,
                    SpriteEffects.None, 0f);
                    batch.Draw(p.myIcon, iconPos, null, Color.White, myAngle, myOrigin, myScale,
                        SpriteEffects.None, 0f);
                    batch.Draw(myIcon, iconPos, null, Color.Gray, myAngle, myOrigin, myScale, SpriteEffects.None, 0f);
                }
                batch.DrawString(myFont, "" + (i + 1), textPos, Color.Black);
                iconPos.X += 129;
                textPos.X += 129;
            }
            #endregion

            #region Exp Bar

            Vector2 barLoc = new Vector2((myScreenSize.X / 2) - (myXPbar.Width / 2), (iconPos.Y - myXPbar.Height - 10));
            batch.Draw(myXPbar, barLoc, null, Color.White, myAngle, myOrigin, myScale, SpriteEffects.None, 0f);

            Vector2 blockLoc = new Vector2(barLoc.X + 3, barLoc.Y + 3);

            for (int t = 0; t < levelProgress; t++)
            {
                float xOffset = blockLoc.X + (8 * t);
                batch.Draw(myXPblock, new Vector2(xOffset, blockLoc.Y), null, Color.White, myAngle, myOrigin, myScale, SpriteEffects.None, 0f);
            }

            batch.Draw(myXPbar, barLoc, null, Color.White, myAngle, myOrigin, myScale, SpriteEffects.None, 0f);

            Vector2 levelLoc = new Vector2(barLoc.X + (myXPbar.Width / 2), barLoc.Y - 40);
            batch.DrawString(levelFont, "" + myLevel, levelLoc, Color.Black);

            if (scene != null)
            {
                scene.myEventState.Draw(batch);
            }
            #endregion


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

            Keys[] possibleHotkeys = { Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0 };
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
                this.GetType().GetMethod("clearTile"),
                new object[0]);

            GameAction shift = new GameAction(
                this, this.GetType().GetMethod("useComboPower"),
                new object[0]);

            InputManager.AddToMouseMap(InputManager.LEFT_BUTTON, powerLMBClick);
            InputManager.AddToMouseMap(InputManager.RIGHT_BUTTON, powerRMBClick);
            InputManager.AddToKeyboardMap(Keys.LeftShift, shift);
            InputManager.AddToKeyboardMap(Keys.RightShift, shift);
            #endregion

        }

        /// <summary>
        /// Set the hotkey to the power.
        /// </summary>
        /// <param name="num"></param>
        public void powerHotkey(int num)
        {
            if (myPowers.Count > num)
            {
                myPreviousPower = mySelectedPower;
                mySelectedPower = (Power)myPowers[num];
            }
            else
            {
                Debug.WriteLine("That power does not exist.");
            }
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
                MapTile tile = myMap.GetTile(myCursor);
                mySelectedPower.Interact(tile);
                myXP += mySelectedPower.myXP;
                levelProgress = getLevelProgress();
            }
        }

        public void clearTile(Vector2 mouseClickPosition)
        {
            if (mouseClickPosition.X >= 0 && mouseClickPosition.X < myScreenSize.X
    && mouseClickPosition.Y > 0 && mouseClickPosition.Y < myScreenSize.Y)
            {
                MapTile tile = myMap.GetTile(myCursor);
                tile.ClearTile();
            }
        }

        public void useComboPower()
        {
            MapTile tile = myMap.GetTile(myCursor);
            mySelectedPower.PowerCombo(tile, myPreviousPower);
        }

        public void altUsePower(Vector2 mouseClickPosition)
        {
            // empty for now
        }



        private int getLevelProgress()
        {
            double levelPercent = ((double)myXP / (double)myXPtoNext);
            if (levelPercent >= 1.0)
            {
                myLevel += 1;
                myXPtoNext = myXPtoNext*2;
                myXP = 0;
                levelPercent = 0;
                if (myLevel % 2 == 0)
                {
                    switch (myLevel)
                    {
                        case 2:
                            {
                                break;
                            }
                        case 4:
                            {
                                break;
                            }
                        case 6:
                            {
                                break;
                            }
                        case 8:
                            {
                                break;
                            }
                        case 10:
                            {
                                break;
                            }
                    }
                }
            }

            return (int)(levelPercent * 100);
        }

        #region Smelly properties...
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
        #endregion
    }
}
