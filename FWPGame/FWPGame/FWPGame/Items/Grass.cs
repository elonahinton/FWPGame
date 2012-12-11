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
    public class Grass : Sprite
    {
        private Texture2D[] myAnimateSequence;
        private Animate myAnimate;
        private Texture2D myBurnt;
        private SoundEffectInstance myPlay;

        public Grass(Texture2D texture, Vector2 position, Vector2 mapPosition,
            Texture2D[] burningSequence, Texture2D burnt, SoundEffectInstance firePlay) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            
			name = "Grass";
            myAnimateSequence = burningSequence;
            myAnimate = new Animate(burningSequence);
            SetUpAnimate();
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

        public Grass Clone()
        {
            return new Grass(this.myTexture, new Vector2(0,0), new Vector2(0,0),
                myAnimateSequence, myBurnt, myPlay);
        }


        public override void burn()
        {
            myState = new BurningState(this);
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
            Grass grass = Clone();
            grass.myState = new BurningState(grass);
            return grass;
        }
        


        // The Regular State
        class RegularState : State
        {
            private Grass grass;

            public RegularState(Grass sprite)
            {
                grass = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Grass newGrass = grass.Clone();
                newGrass.myState = new RegularState(newGrass);
                return newGrass;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(grass.myTexture, grass.myPosition,
                        null, Color.White,
                        grass.myAngle, grass.myOrigin, grass.myScale,
                        SpriteEffects.None, 0f);
            }
        }



        // The Burning State
        class BurningState : State
        {
            private Grass grass;

            public BurningState(Grass sprite)
            {
                grass = sprite;
                grass.myPlay.Play();
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Grass newGrass = grass.Clone();
                newGrass.myState = new BurningState(newGrass);
                return newGrass;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                grass.myAnimate.Update(elapsedTime, ref seqDone);
                if (seqDone)
                {
                    grass.myState = new BurntState(grass);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(grass.myAnimate.GetImage(), grass.myPosition, null, Color.White, grass.myAngle,
                        grass.myOrigin, grass.myScale,
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
            private Grass grass;

            public BurntState(Grass sprite)
            {
                grass = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Grass newGrass = grass.Clone();
                newGrass.myState = new RegularState(newGrass);
                return newGrass;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(grass.myBurnt, grass.myPosition,
                    null, Color.White,
                    grass.myAngle, grass.myOrigin, grass.myScale,
                    SpriteEffects.None, 0f);
            }
        }
    }
}
