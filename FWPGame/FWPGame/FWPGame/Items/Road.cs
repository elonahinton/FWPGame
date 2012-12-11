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

namespace FWPGame.Items
{
    public class Road : Sprite
    {
        private Texture2D myHighway;
        private Texture2D myVillage;
        private Texture2D myCity;
        private Texture2D[] myBurningSequence;
        private Animate myBurning;
        private Texture2D myBurnt;
        private SoundEffectInstance myPlay;

        public Road(Texture2D texture, Vector2 position, Vector2 mapPosition, 
            Texture2D[] burningSequence, Texture2D burnt, SoundEffectInstance firePlay,
            Texture2D highway,  Texture2D village, Texture2D city) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            
            name = "Road";
            myHighway = highway;
            myVillage = village;
            myCity = city;
            myBurningSequence = burningSequence;
            myBurning = new Animate(burningSequence);
            SetUpBurning();
            myBurnt = burnt;
            myPlay = firePlay;
            myState = new RegularState(this);
        }


        public void setMyPosition(Vector2 pos)
        {
            myPosition = pos;
        }
        

        public void setMyMapPosition(Vector2 pos)
        {
            myMapPosition = pos;
        }
        

        public Road Clone()
        {
            return new Road(this.myTexture, new Vector2(0, 0), new Vector2(0, 0),
                myBurningSequence, myBurnt, myPlay, myHighway, myVillage, myCity);
        }


        public override void burn()
        {
            myState = new BurningState(this);
        }


        public void SetUpBurning()
        {
            // Prepare the flip book sequence for expected Animate
            myBurning.AddFrame(1, 1000);
            myBurning.AddFrame(3, 3000);
            myBurning.AddFrame(2, 2000);
            myBurning.AddFrame(4, 2500);
            myBurning.AddFrame(0, 1500);
            myBurning.AddFrame(5, 900);
            myBurning.AddFrame(6, 3100);
            myBurning.AddFrame(9, 3500);
            myBurning.AddFrame(0, 1000);
            myBurning.AddFrame(1, 3000);
            myBurning.AddFrame(2, 2000);
            myBurning.AddFrame(3, 2500);
            myBurning.AddFrame(8, 1500);
            myBurning.AddFrame(1, 1100);
            myBurning.AddFrame(7, 3500);
        }



        // The Regular State
        class RegularState : State
        {
            private Road road;

            public RegularState(Road sprite)
            {
                road = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Road newRoad = road.Clone();
                newRoad.myState = new VillageState(newRoad);
                return newRoad;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(road.myTexture, road.myPosition,
                    null, Color.White,
                    road.myAngle, road.myOrigin, road.myScale,
                    SpriteEffects.None, 0f);
            }
        }



        // The Highway State
        class HighwayState : State
        {
            private Road road;

            public HighwayState(Road sprite)
            {
                road = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Road newRoad = road.Clone();
                newRoad.myState = new CityState(newRoad);
                return newRoad;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(road.myHighway, road.myPosition,
                    null, Color.White,
                    road.myAngle, road.myOrigin, road.myScale,
                    SpriteEffects.None, 0f);
            }
        }



        // The Village State
        public class VillageState : State
        {
            private Road road;

            public VillageState(Road sprite)
            {
                road = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Road newRoad = road.Clone();
                newRoad.myState = new HighwayState(newRoad);
                return newRoad;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(road.myVillage, road.myPosition,
                        null, Color.White,
                        road.myAngle, road.myOrigin, road.myScale,
                        SpriteEffects.None, 0f);
            }
        }



        // The City State
        class CityState : State
        {
            private Road road;

            public CityState(Road sprite)
            {
                road = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Road newRoad = road.Clone();
                newRoad.myState = new BurningState(newRoad);
                return newRoad;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(road.myCity, road.myPosition,
                        null, Color.White,
                        road.myAngle, road.myOrigin, road.myScale,
                        SpriteEffects.None, 0f);
            }
        }



        // The Burning State
        class BurningState : State
        {
            private Road road;

            public BurningState(Road sprite)
            {
                road = sprite;
                road.myPlay.Play();
            }


            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Road newRoad = road.Clone();
                newRoad.myState = new BurningState(newRoad);
                return newRoad;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                road.myBurning.Update(elapsedTime, ref seqDone);

                if (seqDone)
                {
                    road.myState = new BurntState(road);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(road.myBurning.GetImage(), road.myPosition, null, Color.White, road.myAngle,
                        road.myOrigin, road.myScale,
                        SpriteEffects.None, 0f);
            }
        }



        // The Burnt State
        class BurntState : State
        {
            private Road road;

            public BurntState(Road sprite)
            {
                road = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Road newRoad = road.Clone();
                newRoad.myState = new RegularState(newRoad);
                return newRoad;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(road.myBurnt, road.myPosition,
                    null, Color.White,
                    road.myAngle, road.myOrigin, road.myScale,
                    SpriteEffects.None, 0f);
            }
        }
    }
}
