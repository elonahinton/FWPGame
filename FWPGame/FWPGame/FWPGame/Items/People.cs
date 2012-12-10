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
using FWPGame.Engine;
using FWPGame.Powers;
using System.Collections;
using System.Diagnostics;

namespace FWPGame.Items
{
    public class People : Sprite
    {
        private Texture2D[] myBurningSequence;
        private Animate myBurning;
        private Texture2D myBurnt;
        private Texture2D myElectrocuted;

        public People(Texture2D texture, Vector2 position, Vector2 mapPosition, Texture2D[] burningSequence, Texture2D burnt, Texture2D electrocuted) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            name = "People";
            myBurningSequence = burningSequence;
            myBurning = new Animate(burningSequence);
            SetUpBurning();
            myBurnt = burnt;
            myElectrocuted = electrocuted;
            myState = new RegularState(this);
        }

        public People Clone()
        {
            return new People(this.myTexture, new Vector2(0, 0), new Vector2(0, 0), myBurningSequence, myBurnt, myElectrocuted);
        }


        public void burn()
        {
            myState = new BurningState(this);
        }

        public void electrocute()
        {
            myState = new ElectricState(this);
        }


        public void SetUpBurning()
        {
            // Prepare the flip book sequence for expected Animate
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(1, 50);
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(2, 50);
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(3, 50);
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(4, 50);
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(5, 50);
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(6, 50);
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(7, 50);
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(8, 50);
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(9, 50);
        }

        public void SetUpElectric()
        {
            //should prepare flip book sequence for expected animation
        }



        // The Regular State
        class RegularState : State
        {
            private People people;
            private bool goLeft;
            private bool goRight;
            private bool goUp;
            private bool goDown;
            private int count;

            public RegularState(People sprite)
            {
                people = sprite;
                count = 50;
                goLeft = true;
                goRight = false;
                goUp = false;
                goDown = false;
            }



            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
            }
            
            public void changeDirection()
            {
                count = 50;
                if (goLeft)
                {
                    goLeft = false;
                    goUp = true;
                }
                else if (goUp)
                {
                    goUp = false;
                    goRight = true;
                }
                else if (goRight)
                {
                    goRight = false;
                    goDown = true;
                }
                else if (goDown)
                {
                    goDown = false;
                    goLeft = true;
                }
            }


            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                if (count <= 0)
                {
                    changeDirection();
                }

                if (goLeft)
                {
                    if (people.myPosition.X > 0)
                    {
                        people.myPosition.X = people.myPosition.X-50;
                    }
                }
                else if (goUp)
                {
                    if (people.myPosition.Y > 0)
                    {
                        people.myPosition.Y--;
                    }
                }
                else if (goRight)
                {
                    people.myPosition.X++;
                    if (people.OutOfBounds())
                    {
                        people.myPosition.X--;
                    }
                }
                else
                {
                    people.myPosition.Y++;
                    if (people.OutOfBounds())
                    {
                        people.myPosition.Y--;
                    }
                }
                count--;

                
            }


            public void Draw(SpriteBatch batch)
            {
                batch.Draw(people.myTexture, people.myPosition,
                        null, Color.White,
                        people.myAngle, people.myOrigin, people.myScale,
                        SpriteEffects.None, 0f);
            }
        }



        // The Burning State
        class BurningState : State
        {
            private People people;


            public BurningState(People sprite)
            {
                people = sprite;
            }


            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
            }


            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                people.myBurning.Update(elapsedTime, ref seqDone);
                if (seqDone)
                {
                    people.myState = new BurntState(people);
                }
            }


            public void Draw(SpriteBatch batch)
            {
                batch.Draw(people.myBurning.GetImage(), people.myPosition, null, Color.White, people.myAngle,
                        people.myOrigin, people.myScale,
                        SpriteEffects.None, 0f);
            }
        }



        // The Burnt State
        class BurntState : State
        {
            private People people;


            public BurntState(People sprite)
            {
                people = sprite;
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
                batch.Draw(people.myBurnt, people.myPosition,
                    null, Color.White,
                    people.myAngle, people.myOrigin, people.myScale,
                    SpriteEffects.None, 0f);
            }
        }

        // The Electric State
        class ElectricState : State
        {
            private People people;

            public ElectricState(People sprite)
            {
                people = sprite;
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
                //batch.Draw(people.myElectrocuted, people.myPosition,
                //    null, Color.White,
                //    people.myAngle, people.myOrigin, people.myScale,
                //   SpriteEffects.None, 0f);
            }
        }
    }
}
