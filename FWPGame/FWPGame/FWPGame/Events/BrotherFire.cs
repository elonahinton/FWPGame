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
using FWPGame.Powers;

namespace FWPGame.Events
{
    public class BrotherFire : Event
    {
        private bool nextState;
        private Map map;

        public BrotherFire(Texture2D texture, Vector2 position, Vector2 mapPosition, SpriteFont font, Map aMap) :
            base(texture, position, mapPosition, font)
        {
            myMapPosition = mapPosition;
            myTexture = texture;
            myPosition = position;
            this.map = aMap;
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
            private MapTile burnTile;

            public FireballState(BrotherFire angryBroEvent, SoundEffect soundEffect, SpriteFont openingText)
            {
                this.angryBro = angryBroEvent;
                burnTile = BurnTiles(angryBro.map);
                this.angryBro.nextState = false;
                this.effect = soundEffect;
                this.openingTxt = openingText;
                BurnTile(burnTile);
            }

            public void setNextState()
            {
                angryBro.nextState = true;
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (burnTile == null)
                {
                    angryBro.myEventState = new NoBurnState(this.angryBro, effect, openingTxt);
                }
                else if (this.angryBro.nextState)
                {
                    angryBro.myEventState = new LeaveState(this.angryBro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "";                
                batch.DrawString(openingTxt, instructions, new Vector2(0, 0), Color.White);
            }

            public void BurnTile(MapTile tile)
            {
                Fire fire = new Fire(angryBro.myTexture, null, new Vector2(0, 0), new Vector2(0, 0));
                fire.Interact(tile);
                setNextState();
            }

            public MapTile BurnTiles(Map map)
            {
                List<MapTile> tiles = new List<MapTile>();
                int tilesX = map.MapTiles.GetLength(0);
                int tilesY = map.MapTiles.GetLength(1);
                for (int i = 0; i < tilesX; i++)
                {
                    for (int j = 0; j < tilesY; j++)
                    {
                        MapTile tile = map.MapTiles[i, j];
                        if (isBurnable(tile))
                        {
                            tiles.Add(tile);
                        }
                    }
                }
                if (tiles.Count == 0)
                {
                    return null;
                }
                else
                {
                    Random rand = new Random();
                    int tileNum = rand.Next(0, tiles.Count);
                    return tiles[tileNum];
                }
            }

            public bool isBurnable(MapTile tile)
            {
                foreach (Sprite s in tile.mySprites)
                {
                    if (s.name.Equals("House") || s.name.Equals("Tree") || s.name.Equals("Road") || s.name.Equals("Wood"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        class NoBurnState : EventState
        {
            private SoundEffect effect;
            private SpriteFont openingTxt;
            private BrotherFire angryBro;

            public NoBurnState(BrotherFire angryBroEvent, SoundEffect soundEffect, SpriteFont openingText)
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
                InputManager.AddToKeyboardMap(Keys.Space, next);
            }

            public void setNextState()
            {
                angryBro.nextState = true;
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.angryBro.nextState)
                {
                    angryBro.myEventState = new EndState(this.angryBro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Awww! You have nothing to burn!\n" +
                                        "Well, have this anyway!\n\n" +
                                        "You have been given the fire power!";
                //draw brother
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
                InputManager.AddToKeyboardMap(Keys.Space, next);
            }

            public void setNextState()
            {
                angryBro.nextState = true;
            }

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.angryBro.nextState)
                {
                    angryBro.myEventState = new EndState(this.angryBro, effect, openingTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "It was fun, Kid! See ya!\n\n" +
                                        "You have learned the fire power!";
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
