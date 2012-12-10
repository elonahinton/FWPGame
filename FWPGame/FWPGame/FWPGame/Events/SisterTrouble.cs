using System;
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
    class SisterTrouble : Sprite
    {
        private FWPGame game;
        private bool nextState;

        public SisterTrouble(FWPGame game, Texture2D texture, Vector2 position, Vector2 mapPosition, SpriteFont font) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            myTexture = texture;
            myPosition = position;
            this.game = game;
            this.myState = new SisterVisit(this, null, font); //second argument is sound effect, if wanted
        }

        class SisterVisit : State
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private SisterTrouble angrySis;

            public SisterVisit(SisterTrouble angrySisEvent, SoundEffect effect, SpriteFont openingText)
            {
                this.angrySis = angrySisEvent;
                this.angrySis.nextState = false;
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
                this.angrySis.nextState = true;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                if (this.angrySis.nextState)
                {
                    this.angrySis.myState = new TroubleState(this.angrySis, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Hi there! I was looking in Daddy’s room and found\n" +
                                      "all of this cool stuff! This only continues to prove\n" +
                                      "that I should should be allowed to have my own world!\n" +
                                      "I mean, look at the cool stuff I can do!\n";
                //tried to draw sister god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }

            public Sprite Spread()
            {
                return null;
            }
        }

        class TroubleState : State
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private SisterTrouble angrySis;

            public TroubleState(SisterTrouble angrySisterEvent, SoundEffect soundEffect, SpriteFont openingText)
            {
                this.angrySis = angrySisterEvent;
                this.angrySis.nextState = false;
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
                InputManager.AddToKeyboardMap(Keys.Space, next);
            }

            public void setNextState()
            {
                angrySis.nextState = true;
            }

            //don't let them skip event?
            //public void skipEvent()
            //{
            //    angryBro.myState = new EndingStateOver(this.angryBro, effect, openingTxt);
            //}

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                if (this.angrySis.nextState)
                {
                    angrySis.myState = new LeaveState(this.angrySis, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "";
                //draw something small happen
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }

            public Sprite Spread()
            {
                return null;
            }
        }

        class LeaveState : State
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private SisterTrouble angrySis;

            public LeaveState(SisterTrouble angrySisEvent, SoundEffect soundEffect, SpriteFont openingText)
            {
                this.angrySis = angrySisEvent;
                this.angrySis.nextState = false;
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
                InputManager.AddToKeyboardMap(Keys.Space, next);
            }

            public void setNextState()
            {
                angrySis.nextState = true;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                if (this.angrySis.nextState)
                {
                    angrySis.myState = new EndState(this.angrySis, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "There you two are! It is great to see you are helping\n" +
                                      "your sister learn how to make a INSERT POWER NAME!\n" +
                                      "You will be a good fledgling when your time comes!\n" +
                                      "Hmmm... It looks as though this one was not done too well...\n" +
                                      "Here, you should probably take these insructions with you!\n\n" +
                                      "Not right!?! And I do not get to keep the instructions? HMPH!!";
                //draw sister and father
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }

            public Sprite Spread()
            {
                return null;
            }
        }

        class EndState : State
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private SisterTrouble angrySis;

            public EndState(SisterTrouble angrySisEvent, SoundEffect soundEffect, SpriteFont openingText)
            {
                this.angrySis = angrySisEvent;
                this.angrySis.nextState = false;
                this.effect = soundEffect;
                this.openingTxt = openingText;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
            }

            public Sprite Spread()
            {
                return null;
            }
        }
    }
}
