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
    public class Introduction : Event
    {
        private bool nextState;

        public Introduction(Texture2D texture, Vector2 position, Vector2 mapPosition, SpriteFont font) :
            base(texture, position, mapPosition, font)
        {
            myMapPosition = mapPosition;
            myTexture = texture;
            myPosition = position;
            this.myEventState = new IntroState1(this, null, font); //second argument is sound effect, if wanted
        }

        class IntroState1 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public IntroState1(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                this.intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new IntroState2(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Ah! There you are, my child! Come in, come in! \nI have some exciting news for you! \n" + 
                                      "As you know, you are now of age, \nand it is time for you to grow up. \n" + 
                                      "As a fledgling god, it is time for you to \nstart training to become a fully fledged god-" +
                                      "\n\n\n Press space to continue." +
                                      "\nPress tab to skip";
                //tried to draw father god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }

        }

        class IntroState2 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public IntroState2(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new IntroState3(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Like me!" +
                                      "\n\nYes, like your older brother, \nme, and all of our ancestors." +
                                      "\n\n\n Press space to continue." +
                                      "\nPress tab to skip";
                //want to draw dad god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }

        }

        class IntroState3 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public IntroState3(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new IntroState4(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Hey! What about me! I want to become a fully fledged god too!" +
                                      "\n\nYes, now.  You will be given this opportunity as well" +
                                      "\n once you are of age.  Your time will come." +
                                      "\n\nBut that is not fair! Just because I am the youngest fledgling-" +
                                      "\n\n\n Press space to continue." +
                                      "\nPress tab to skip";
                //want to draw sis God and dad god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }

        }

        class IntroState4 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public IntroState4(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new IntroState5(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "But I am not a fledgling anymore!" +
                                      "\n\nBut I got the best grades in school and actually started" + 
                                      "\nreading up on how to use some different powers! Just because"  + 
                                      "\nI do not have my own place and am a little bit younger does not" + 
                                      "\nmean that I am unqualified!" +
                                      "\n\n\n Press space to continue." +
                                      "\nPress tab to skip";
                //want to draw broGod and sis god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class IntroState5 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public IntroState5(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new IntroState6(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "My youngest, your day will come, but you are not\n" + 
                                      "ready yet. You still have much to learn before you can\n" +
                                      "start your training. In the meantime, please do not\n" +
                                      "ruin this special day.\n" +
                                      "Please, do remember that your older brother and younger\n" +
                                      "sister do care and are here to support you, even if\n" +
                                       "they do not show it.\n\n\n" +
                                      "Do not!" +
                                      "\n\n\n Press space to continue." +
                                      "\nPress tab to skip";

                //want to draw dad god, broGod and sis god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class IntroState6 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public IntroState6(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new Instructions1(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Children, please!  It is time, to bring out the new world.\n" +
                                      "As you take on this task, you will learn more powers and \n" + 
                                      "become more powerful. Try every combination you can think of.\n" +
                                      "Use your powers on various objects and and use various combinations\n" +
                                      "of your powers to truly explore and build your land.\n" +
                                      "Now, it is time for you to receive your First World!\n\n\n" +
                                      "           CAMCO's First World Problems";

                //want to draw dad god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class Instructions1 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public Instructions1(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new Instructions2(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Now, let me show you how to build you world!\n\n" +
                                        "Use your mouse to move around the map\n\n\n" +
                                        "Press spacebar to continue.";

                //want to draw dad god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class Instructions2 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public Instructions2(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new Instructions3(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "On the bottom of your screen are your powers.\n\n" +
                                        "To use a power, you must first select it.\n\n" +
                                        "To select a power, press the key to the\n"+
                                        "associated number on your keyboard.\n\n" +
                                        "Then click where you wish to use it!\n" + 
                                        "Some powers can only work on certain items,\n" + 
                                        "and others can only work on empty tiles.\n\n\n" +
                                        "Press spacebar to continue.";

                //want to draw dad god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class Instructions3 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public Instructions3(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new Instructions4(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Above your powers, you can see your current level,\n" +
                                        "and your experience.\n\n" +
                                        "Everyime you use an ability, you gain experience.\n" +
                                        "Everytime you grow a level, you gain an ability.\n\n\n" +
                                        "Press spacebar to continue.";

                //want to draw dad god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class Instructions4 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public Instructions4(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new Instructions5(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "It is okay if you make a mistake!\n" +
                                        "Just right click on the tile you don't like,\n" +
                                        "and it will be cleared!\n" +
                                        "Be careful, once it is erased, you cannot get it back!\n\n\n" +
                                        "Press spacebar to continue.";

                //want to draw dad god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class Instructions5 : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public Instructions5(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
                intro.nextState = true;
            }

            public void skipEvent()
            {
                intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.intro.nextState)
                {
                    intro.myEventState = new IntroStateOver(this.intro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "One last piece of advice: try everything!\n" +
                                        "You never know what may happen!\n\n" +
                                        "Good luck, and have fun, my child!\n\n\n\n\n" + 
                                        "If at any point you wish to exit the game, press the Escape Key.\n\n" +
                                        "Press spacebar to continue.";

                //want to draw dad god
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class IntroStateOver : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private Introduction intro;

            public IntroStateOver(Introduction intro, SoundEffect effect, SpriteFont openingText)
            {
                this.intro = intro;
                intro.nextState = false;
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
