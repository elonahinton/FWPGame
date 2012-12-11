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
    public class Tornado : Sprite
    {
        private Texture2D[] myAnimateSequence;
        private Animate myAnimate;
        public Tornado(Texture2D[] animSeq, Vector2 position, Vector2 velocity, Vector2 mapPosition) :
            base(animSeq[0], position)
        {
            myMapPosition = mapPosition;
            myTexture = animSeq[0];
            myPosition = position;
            myVelocity = velocity;
            myAnimateSequence = animSeq;
            myAnimate = new Animate(animSeq);
            SetUpAnimate();
            name = "Tornado";
        }

        public void SetUpAnimate()
        {
            // Prepare the flip book sequence for expected Animate
            for (int j = 0; j < 5; ++j)
            {
                for (int i = 0; i < 13; ++i)
                {
                    myAnimate.AddFrame(i, 100);
                }
            }
        }

        public void setMyPosition(Vector2 pos)
        {
            myPosition = pos;
        }

        public void setMyMapPosition(Vector2 pos)
        {
            myMapPosition = pos;
        }

        public Tornado Clone()
        {
            return new Tornado(this.myAnimateSequence, new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0));
        }

        public override void Update(double elapsedTime, Vector2 playerMapPos)
        {
            bool seqDone = false;
            myAnimate.Update(elapsedTime, ref seqDone);
            myMapPosition += myVelocity;
            myPosition += myVelocity;
            myPosition = myMapPosition - playerMapPos;
        }

        public override void Update(GameTime gameTime, Vector2 playerMapPos)
        {
            bool seqDone = false;
            myAnimate.Update(gameTime.TotalGameTime.Seconds, ref seqDone);
            myMapPosition += myVelocity;
            myPosition = myMapPosition - playerMapPos;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(myAnimate.GetImage(), myPosition,
                    null, Color.White,
                    myAngle, myOrigin, myScale,
                    SpriteEffects.None, 0f);
        }

        public void printDebug()
        {
            Debug.WriteLine("map position: " + myMapPosition);
            Debug.WriteLine("relative position: " + myPosition);
        }


        class GoneState : State
        {
            public GoneState(Sprite s)
            {
                s.myMapPosition = new Vector2(0, 0);
                s.myVelocity = new Vector2(0, 0);
                s.myPosition = new Vector2(0, 0);
            }

            public Sprite Spread()
            {
                return null;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
            }
        }
    }
}
