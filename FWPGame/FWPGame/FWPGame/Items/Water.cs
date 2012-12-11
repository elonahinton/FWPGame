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
    public class Water : Sprite
    {
        private bool mother = true;
        private Texture2D[] myRainingSequence;
        private Animate myRaining;
        private SoundEffectInstance myPlay;

        public Water(Texture2D texture, Vector2 position, Vector2
            mapPosition, Texture2D[] rainingSequence, SoundEffectInstance rainPlay) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            name = "Water";
            myRainingSequence = rainingSequence;
            myRaining = new Animate(rainingSequence);
            SetUpRaining();
            myPlay = rainPlay;
            myState = new RainingState(this);
        }


        public Water Clone()
        {
            Water newWater = new Water(this.myTexture, new Vector2(0, 0),
                new Vector2(0, 0), myRainingSequence, myPlay);
            mother = false;
            return newWater;
        }


        public void rain()
        {
            myState = new RainingState(this);
        }


        public void setMyPosition(Vector2 pos)
        {
            myPosition = pos;
        }


        public void setMyMapPosition(Vector2 pos)
        {
            myMapPosition = pos;
        }


        public void SetUpRaining()
        {
            // Prepare the flip book sequence for expected Animate
            for (int j = 0; j < 3; ++j)
            {
                for (int i = 0; i < 22; ++i)
                {
                    myRaining.AddFrame(i, 2000);
                }
            }
        }



        // The Raining State
        class RainingState : State
        {
            private Water water;

            public RainingState(Water sprite)
            {
                water = sprite;
                if (!water.mother)
                {
                    water.myPlay.Play();
                }
            }


            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Water newWater = water.Clone();
                newWater.myState = new RainingState(newWater);
                return newWater;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                water.myRaining.Update(elapsedTime, ref seqDone);

                if (seqDone)
                {
                    water.myState = new SeaState(water);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(water.myRaining.GetImage(), water.myPosition, null, Color.White, water.myAngle,
                        water.myOrigin, water.myScale,
                        SpriteEffects.None, 0f);
            }
        }



        // The Sea State
        class SeaState : State
        {
            private Water water;

            public SeaState(Water sprite)
            {
                water = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Water newWater = water.Clone();
                newWater.myState = new RainingState(newWater);
                return newWater;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(water.myTexture, water.myPosition,
                    null, Color.White,
                    water.myAngle, water.myOrigin, water.myScale,
                    SpriteEffects.None, 0f);
            }
        }
    }
}
