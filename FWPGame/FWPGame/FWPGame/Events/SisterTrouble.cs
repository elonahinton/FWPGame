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
using FWPGame.Items;
using System.Reflection;

namespace FWPGame.Events
{
    public class SisterTrouble : Event
    {
        private bool nextState;
        private Map myMap;

        public SisterTrouble(Texture2D texture, Vector2 position, Vector2 mapPosition, SpriteFont font, Map aMap) :
            base(texture, position, mapPosition, font)
        {
            myMapPosition = mapPosition;
            myTexture = texture;
            myPosition = position;
            myMap = aMap;
            this.myEventState = new SisterVisit(this, null, font); //second argument is sound effect, if wanted
        }

        class SisterVisit : EventState
        {
            private SoundEffect effect;
            private SpriteFont sisterTxt;
            private SisterTrouble angrySis;

            public SisterVisit(SisterTrouble angrySisEvent, SoundEffect effect, SpriteFont sisterText)
            {
                this.angrySis = angrySisEvent;
                this.angrySis.nextState = false;
                this.effect = effect;
                SetUpInput();
                this.sisterTxt = sisterText;
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

            public void Update(GameTime gameTime, Vector2 v)
            {
                if (this.angrySis.nextState)
                {
                    this.angrySis.myEventState = new TroubleState(this.angrySis, effect, sisterTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "Hi there! I was looking in Daddys room and found\n" +
                                      "all of this cool stuff! This only continues to prove\n" +
                                      "that I should should be allowed to have my own world!\n" +
                                      "I mean, look at the cool stuff I can do!\n";
                //tried to draw sister god
                batch.DrawString(sisterTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class TroubleState : EventState
        {
            private SoundEffect effect;
            private SpriteFont sisterTxt;
            private SisterTrouble angrySis;
            private MapTile tileToBuild;

            public TroubleState(SisterTrouble angrySisterEvent, SoundEffect soundEffect, SpriteFont sisterText)
            {
                this.angrySis = angrySisterEvent;
                this.angrySis.nextState = false;
                this.effect = soundEffect;
                SetUpInput();
                this.sisterTxt = sisterText;
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

            public void Update(GameTime gameTime, Vector2 playerMapPos)
            {
                if (this.angrySis.nextState)
                {
                    angrySis.myEventState = new LeaveState(this.angrySis, effect, sisterTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "";
                //draw something small happen
                batch.DrawString(sisterTxt, instructions, new Vector2(0, 0), Color.White);
            }

            public void BuildTile(MapTile tile)
            {
                GrowGrass grass = new GrowGrass(angrySis.myTexture, null, new Vector2(0, 0), new Vector2(0, 0));
                grass.Interact(tile);
                BuildHouse house = new BuildHouse(angrySis.myTexture, null, new Vector2(0, 0), new Vector2(0, 0));
                house.Interact(tile);
                foreach (Sprite s in tile.mySprites)
                {
                    if (s.name.Equals("House"))
                    {
                        MethodInfo myMethod = s.GetType().GetMethod("burnt");
                        myMethod.Invoke(s, null);
                    }
                }
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
                        if (tile.mySprites.Count == 0)
                        {
                            tiles.Add(tile);
                        }
                    }
                }
                if (tiles.Count == 0)
                {
                    map.MapTiles[0, 0].ClearTile();
                    return map.MapTiles[0, 0];
                }
                else
                {
                    Random rand = new Random();
                    int tileNum = rand.Next(0, tiles.Count);
                    return tiles[tileNum];
                }
            }
        }

        class LeaveState : EventState
        {
            private SoundEffect effect;
            private SpriteFont sisterTxt;
            private SisterTrouble angrySis;

            public LeaveState(SisterTrouble angrySisEvent, SoundEffect soundEffect, SpriteFont sisterText)
            {
                this.angrySis = angrySisEvent;
                this.angrySis.nextState = false;
                this.effect = soundEffect;
                SetUpInput();
                this.sisterTxt = sisterText;
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

            public void Update(GameTime gameTime, Vector2 playerMapPos)
            {
                if (this.angrySis.nextState)
                {
                    angrySis.myEventState = new EndState(this.angrySis, effect, sisterTxt);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                String instructions = "There you two are! It is great to see you are helping\n" +
                                      "your sister learn how to build a house!\n" +
                                      "You will be a good fledgling when your time comes!\n" +
                                      "Hmmm... It looks as though this one was not done too well...\n" +
                                      "Here, you should probably take these insructions with you!\n\n" +
                                      "Not right!?! And I do not get to keep the instructions? HMPH!!";
                //draw sister and father
                batch.DrawString(sisterTxt, instructions, new Vector2(0, 0), Color.White);
            }
        }

        class EndState : EventState
        {
            private SoundEffect effect;
            private SpriteFont sisterTxt;
            private SisterTrouble angrySis;

            public EndState(SisterTrouble angrySisEvent, SoundEffect soundEffect, SpriteFont sisterText)
            {
                this.angrySis = angrySisEvent;
                this.angrySis.nextState = false;
                this.effect = soundEffect;
                this.sisterTxt = sisterText;
            }

            public void Update(GameTime gameTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
            }
        }
    }
}
