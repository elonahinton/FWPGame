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
    public class House : Sprite
    {
        private Texture2D[] myAnimateSequence;
        private Animate myAnimate;
        private Texture2D myBurnt;
        private SoundEffectInstance myPlay;
        private Texture2D myLit;
        private Road myRoad;

        public House(Texture2D texture, Vector2 position, Vector2 mapPosition, Texture2D[] animateSequence, Texture2D burnt,
            SoundEffectInstance firePlay, Texture2D lit, Road road) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            name = "House";
            myAnimateSequence = animateSequence;
            myAnimate = new Animate(animateSequence);
            SetUpAnimate();
            myBurnt = burnt;
            myPlay = firePlay;
            myLit = lit;
            myRoad = road;
            myState = new RegularState(this);
        }

        public House Clone()
        {
            return new House(this.myTexture, new Vector2(0, 0), new Vector2(0, 0),
                myAnimateSequence, myBurnt, myPlay, myLit, myRoad);
        }


        public override void burn()
        {
            myState = new BurningState(this);
        }

        public void burnt()
        {
            myState = new BurntState(this);
        }

        public void electrocute()
        {
            myState = new ElectricState(this);
        }

        public void setMyPosition(Vector2 pos)
        {
            myPosition = pos;
        }

        public void setMyMapPosition(Vector2 pos)
        {
            myMapPosition = pos;
        }


        public void SetUpAnimate()
        {
            // Prepare the flip book sequence for expected Animate
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(1, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(2, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(3, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(4, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(5, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(6, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(7, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(8, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(9, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(1, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(2, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(3, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(4, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(5, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(6, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(7, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(8, 1000);
            myAnimate.AddFrame(0, 2900);
            myAnimate.AddFrame(9, 1000);
        }


        public override Sprite Transform()
        {
            Road newVillage = myRoad.Clone();
            newVillage.myState = new Road.VillageState(newVillage);
            return newVillage;
        }



        // The Regular State
        class RegularState : State
        {
            private House house;

            public RegularState(House sprite)
            {
                house = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                House newHouse = house.Clone();
                newHouse.myState = new RegularState(newHouse);
                return newHouse;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {

                house.myPosition = house.myMapPosition - playerMapPos;
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(house.myTexture, house.myPosition,
                        null, Color.White,
                        house.myAngle, house.myOrigin, house.myScale,
                        SpriteEffects.None, 0f);
            }

        }

        // The Burning State
        class BurningState : State
        {
            private House house;

            public BurningState(House sprite)
            {
                house = sprite;
                house.myPlay.Play();
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                House newHouse = house.Clone();
                newHouse.myState = new BurningState(newHouse);
                return newHouse;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                house.myAnimate.Update(elapsedTime, ref seqDone);
                if (seqDone)
                {
                    house.myState = new BurntState(house);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(house.myAnimate.GetImage(), house.myPosition, null, Color.White, house.myAngle,
                        house.myOrigin, house.myScale,
                        SpriteEffects.None, 0f);
            }

            public override string ToString()
            {
                return "BurnState";
            }
        }

        // The Burnt State
        class BurntState : State
        {
            private House house;

            public BurntState(House sprite)
            {
                house = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                House newHouse = house.Clone();
                newHouse.myState = new RegularState(newHouse);
                return newHouse;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(house.myBurnt, house.myPosition,
                    null, Color.White,
                    house.myAngle, house.myOrigin, house.myScale,
                    SpriteEffects.None, 0f);
            }
        }

        //electric state
        class ElectricState : State
        {
            private House house;

            public ElectricState(House sprite)
            {
                house = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(house.myLit, house.myPosition,
                    null, Color.White,
                    house.myAngle, house.myOrigin, house.myScale,
                    SpriteEffects.None, 0f);
            }
        }
    }
}
