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

namespace FWPGame
{
    public class House : Sprite
    {
        private Texture2D[] myAnimateSequence;
        private Animate myAnimate;
        private Texture2D myBurnt;

        public House(Texture2D texture, Vector2 position, Vector2 mapPosition, Texture2D[] animateSequence, Texture2D burnt) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            name = "House";
            myAnimateSequence = animateSequence;
            myAnimate = new Animate(animateSequence);
            SetUpAnimate();
            myBurnt = burnt;
            myState = new RegularState(this);
        }

        public House Clone()
        {
            return new House(this.myTexture, new Vector2(0, 0), new Vector2(0, 0), myAnimateSequence, myBurnt);
        }


        public void burn()
        {
            myState = new BurningState(this);
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
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(1, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(2, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(3, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(4, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(5, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(6, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(7, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(8, 50);
            myAnimate.AddFrame(0, 100);
            myAnimate.AddFrame(9, 50);
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
                return null;
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
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
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
                return null;
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
    }
}
