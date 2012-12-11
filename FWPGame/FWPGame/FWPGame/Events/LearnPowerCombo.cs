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
    class LearnPowerCombo : Event
    {
        private bool nextState;

        public LearnPowerCombo(Texture2D texture, Vector2 position, Vector2 mapPosition, SpriteFont font) :
            base(texture, position, mapPosition, font)
        {
            myMapPosition = mapPosition;
            myTexture = texture;
            myPosition = position;
            this.myEventState = new Tutorial1(this, null, font); //second argument is sound effect, if wanted
        }

        class Tutorial1 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private LearnPowerCombo pcEvent;

            public Tutorial1(LearnPowerCombo aEvent, SoundEffect effect, SpriteFont openingText)
            {
                this.pcEvent = aEvent;
                this.pcEvent.nextState = false;
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
                pcEvent.nextState = true;
            }

            public void skipEvent()
            {
                pcEvent.myEventState = new EndTutorial(this.pcEvent, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.pcEvent.nextState)
                {
                    pcEvent.myEventState = new Tutorial2(this.pcEvent, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Ah! There you are, my child! I have some exciting news for you!\n" +
                                      "I am going to show you how to make a Power Combo!\n" +
                                      "A Power Combo in a combination of two powers.\n" +
                                      "\n\n\nPress space to continue." +
                                      "\nPress tab to skip";
                //tried to draw father god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class Tutorial2 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private LearnPowerCombo pcEvent;

            public Tutorial2(LearnPowerCombo aEvent, SoundEffect effect, SpriteFont openingText)
            {
                this.pcEvent = aEvent;
                this.pcEvent.nextState = false;
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
                pcEvent.nextState = true;
            }

            public void skipEvent()
            {
                pcEvent.myEventState = new EndTutorial(this.pcEvent, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.pcEvent.nextState)
                {
                    pcEvent.myEventState = new EndTutorial(this.pcEvent, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "The combination uses both, your currently selected\n" +
                                      "power, and the power you selected before it.\n" +
                                      "Press shift with your mouse on a tile to activate.\n" +
                                      "Take this Wind Power, and try it with fire! Have fun!\n" +
                                      "\n\n\nPress space to continue." +
                                      "\nPress tab to skip";
                //tried to draw father god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class EndTutorial : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private LearnPowerCombo pcEvent;

            public EndTutorial(LearnPowerCombo aEvent, SoundEffect effect, SpriteFont openingText)
            {
                this.pcEvent = aEvent;
                pcEvent.nextState = false;
                this.effect = effect;
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
