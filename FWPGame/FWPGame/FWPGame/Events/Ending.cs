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
    public class Ending : Sprite
    {
        private FWPGame game;
        private bool nextState;

        public Ending(FWPGame game, Texture2D texture, Vector2 position, Vector2 mapPosition, SpriteFont font) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            myTexture = texture;
            myPosition = position;
            this.game = game;
            this.myState = new CongratsState(this, null, font); //second argument is sound effect, if wanted
        }

        class CongratsState : State
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Ending ending;

            public CongratsState(Ending ending, SoundEffect effect, SpriteFont openingText)
            {
                this.ending = ending;
                this.ending.nextState = false;
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
                GameAction skip = new GameAction(
                  this,
                  this.GetType().GetMethod("skipEvent"),
                  new object[0]);
                InputManager.AddToKeyboardMap(Keys.Space, next);
                InputManager.AddToKeyboardMap(Keys.Tab, skip);
            }

            public void setNextState()
            {
                ending.nextState = true;
            }

            public void skipEvent()
            {
                ending.myState = new EndingStateOver(this.ending, effect, openingTxt);
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                if (this.ending.nextState)
                {
                    ending.myState = new EndingStateOver(this.ending, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Congratulations!\n" +
                                      "You have learned so much and have really made this world your own.\n" +
                                      "I now announce you, a fully-fledged god! Congratulations, my child!\n" +
                                      "Please, feel free to keep this world to continue to build and mesh to your desires\n" +
                                      "It is yours now";
                //tried to draw father god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }

            public Sprite Spread()
            {
                return null;
            }
        }

        class EndingStateOver : State
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Ending ending;

            public EndingStateOver(Ending ending, SoundEffect effect, SpriteFont openingText)
            {
                this.ending = ending;
                ending.nextState = false;
                this.effect = effect;
                this.openingTxt = openingText;
            }

            public void Update(double GameTime, Vector2 playerMapPos)
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
