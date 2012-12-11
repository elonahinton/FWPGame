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

namespace FWPGame
{
    public class People : Sprite
    {
        private Texture2D[] myBurningSequence;
        private Animate myBurning;
        private Texture2D myBurnt;

        public People(Texture2D texture, Vector2 position, Vector2 mapPosition, Texture2D[] burningSequence, Texture2D burnt) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            name = "People";
            myBurningSequence = burningSequence;
            myBurning = new Animate(burningSequence);
            SetUpBurning();
            myBurnt = burnt;
            myState = new RegularState(this);
        }

        public People Clone()
        {
            return new People(this.myTexture, new Vector2(0, 0), new Vector2(0, 0), myBurningSequence, myBurnt);
        }


        public void burn()
        {
            myState = new BurningState(this);
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



        // The Regular State
        class RegularState : State
        {
            private People people;

            public RegularState(People sprite)
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
    }
}
