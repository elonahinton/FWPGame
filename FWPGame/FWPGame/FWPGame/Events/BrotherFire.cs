﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Collections;
using System.Diagnostics;
using FWPGame.Engine;

namespace FWPGame.Events
{
    public class BrotherFire : Event
    {
        private bool nextState;

        public BrotherFire(Texture2D texture, Vector2 position, Vector2 mapPosition, SpriteFont font) :
            base(texture, position, mapPosition, font)
        {
            myMapPosition = mapPosition;
            myTexture = texture;
            myPosition = position;
            this.myEventState = new BroVisit(this, null, font); //second argument is sound effect, if wanted
        }

        class BroVisit : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private BrotherFire angryBro;

            public BroVisit(BrotherFire angryBroEvent, SoundEffect effect, SpriteFont openingText)
            {
                this.angryBro = angryBroEvent;
                this.angryBro.nextState = false;
                this.effect = effect;
                SetUpInput();
                this.openingTxt = openingText;
            }

            public void SetUpInput()
            {
                GameAction next = new GameAction(
                  this,
                  this.GetType().GetMethod("setNextState"),
                  new object[0]);
                InputManager.AddToKeyboardMap(Keys.Space, next);
            }

            public void setNextState()
            {
                this.angryBro.nextState = true;
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.angryBro.nextState)
                {
                    this.angryBro.myEventState = new FireballState(this.angryBro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Hey there, Kid! What am I doing, you asked?\n" +
                                      " Well, I am just stopping by for a visit to see\n" +
                                      "how your wourld is coming along. It seems really\n" +
                                      "boring if you ask me...\n" +
                                      "I know! This will brighten things up!";
                //tried to draw brother god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class FireballState : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private BrotherFire angryBro;

            public FireballState(BrotherFire angryBroEvent, SoundEffect soundEffect, SpriteFont openingText)
            {
                this.angryBro = angryBroEvent;
                this.angryBro.nextState = false;
                this.effect = soundEffect;
                SetUpInput();
                this.openingTxt = openingText;
            }

            public void SetUpInput()
            {
                GameAction next = new GameAction(
                  this,
                  this.GetType().GetMethod("setNextState"),
                  new object[0]);
                //GameAction skip = new GameAction(
                //  this,
                //  this.GetType().GetMethod("skipEvent"),
                //  new object[0]);
                InputManager.AddToKeyboardMap(Keys.Space, next);
                //InputManager.AddToKeyboardMap(Keys.Tab, skip);
            }

            public void setNextState()
            {
                angryBro.nextState = true;
            }

            //don't let them skip event?
            //public void skipEvent()
            //{
            //    angryBro.myState = new EndingStateOver(this.angryBro, effect, openingTxt);
            //}

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.angryBro.nextState)
                {
                    angryBro.myEventState = new LeaveState(this.angryBro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "";
                //draw fireball going into area
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class LeaveState : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private BrotherFire angryBro;

            public LeaveState(BrotherFire angryBroEvent, SoundEffect soundEffect, SpriteFont openingText)
            {
                this.angryBro = angryBroEvent;
                this.angryBro.nextState = false;
                this.effect = soundEffect;
                SetUpInput();
                this.openingTxt = openingText;
            }

            public void SetUpInput()
            {
                GameAction next = new GameAction(
                  this,
                  this.GetType().GetMethod("setNextState"),
                  new object[0]);
                //GameAction skip = new GameAction(
                //  this,
                //  this.GetType().GetMethod("skipEvent"),
                //  new object[0]);
                InputManager.AddToKeyboardMap(Keys.Space, next);
                //InputManager.AddToKeyboardMap(Keys.Tab, skip);
            }

            public void setNextState()
            {
                angryBro.nextState = true;
            }

            //don't let them skip event?
            //public void skipEvent()
            //{
            //    angryBro.myState = new EndingStateOver(this.angryBro, effect, openingTxt);
            //}

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.angryBro.nextState)
                {
                    angryBro.myEventState = new EndState(this.angryBro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "It was fun, Kid! See ya!";
                //draw brother
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class EndState : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private BrotherFire angryBro;

            public EndState(BrotherFire angryBroEvent, SoundEffect soundEffect, SpriteFont openingText)
            {
                this.angryBro = angryBroEvent;
                this.angryBro.nextState = false;
                this.effect = soundEffect;
                this.openingTxt = openingText;
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
            }

            public void Draw(SpriteBatch batch)
            {
            }
        }
    }
}
