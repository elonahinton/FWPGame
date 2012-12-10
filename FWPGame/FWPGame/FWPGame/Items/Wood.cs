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
    public class Wood : Sprite
    {
        private Texture2D[] myAnimateSequence;
        private Texture2D[] myBurningSequence;
        private Animate myAnimate;
        private Animate myBurning;
        private Texture2D myBurnt;

        public Wood(Texture2D texture, Vector2 position,
        Vector2 mapPosition, Texture2D[] animateSequence,
        Texture2D[] burningSequence, Texture2D burnt) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            name = "Wood";

            myAnimateSequence = animateSequence;
            myAnimate = new Animate(animateSequence);
            SetUpAnimate();

            myBurningSequence = burningSequence;
            myBurning = new Animate(burningSequence);
            SetUpBurning();
            myBurnt = burnt;

            myState = new CuttingState(this);
        }

        public Wood Clone()
        {
            return new Wood(this.myTexture, new Vector2(0, 0), new Vector2(0, 0), 
                myAnimateSequence, myBurningSequence, myBurnt);
        }


        public override void burn()
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
            for (int j = 0; j < 7; ++j)
            {
                for (int i = 0; i < 5; ++i)
                {
                    myAnimate.AddFrame(i, 5000);
                }
            }
        }


        public void SetUpBurning()
        {
            // Prepare the flip book sequence for expected Animate
            myBurning.AddFrame(0, 1000);
            myBurning.AddFrame(1, 3000);
            myBurning.AddFrame(2, 2000);
            myBurning.AddFrame(3, 2500);
            myBurning.AddFrame(4, 1500);
            myBurning.AddFrame(5, 900);
            myBurning.AddFrame(8, 1100);
            myBurning.AddFrame(9, 3500);
            myBurning.AddFrame(6, 3100);
            myBurning.AddFrame(7, 2000);
            myBurning.AddFrame(8, 1100);
            myBurning.AddFrame(3, 2500);
            myBurning.AddFrame(4, 1500);
            myBurning.AddFrame(5, 900);
            myBurning.AddFrame(6, 3100);
            myBurning.AddFrame(7, 2000);  
            myBurning.AddFrame(9, 3500);
            myBurning.AddFrame(0, 1000);
            myBurning.AddFrame(1, 3000);
            myBurning.AddFrame(2, 2000);
        }


        // The Regular State
        class RegularState : State
        {
            private Wood wood;

            public RegularState(Wood sprite)
            {
                wood = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Wood newWood = wood.Clone();
                newWood.myState = new RegularState(newWood);
                return newWood;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {

                wood.myPosition = wood.myMapPosition - playerMapPos;
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(wood.myTexture, wood.myPosition,
                    null, Color.White,
                    wood.myAngle, wood.myOrigin, wood.myScale,
                    SpriteEffects.None, 0f);
            }

        }


        // The Cutting State
        class CuttingState : State
        {
            private Wood wood;

            public CuttingState(Wood sprite)
            {
                wood = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Wood newWood = wood.Clone();
                newWood.myState = new CuttingState(newWood);
                return newWood;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                wood.myAnimate.Update(elapsedTime, ref seqDone);
                if (seqDone)
                {
                    wood.myState = new RegularState(wood);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(wood.myAnimate.GetImage(), wood.myPosition,
                    null, Color.White, wood.myAngle,
                    wood.myOrigin, wood.myScale,
                    SpriteEffects.None, 0f);
            }
        }


        // The Burning State
        class BurningState : State
        {
            private Wood wood;

            public BurningState(Wood sprite)
            {
                wood = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Wood newWood = wood.Clone();
                newWood.myState = new BurningState(newWood);
                return newWood;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                wood.myBurning.Update(elapsedTime, ref seqDone);
                if (seqDone)
                {
                    wood.myState = new BurntState(wood);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(wood.myBurning.GetImage(), wood.myPosition,
                    null, Color.White, wood.myAngle,
                    wood.myOrigin, wood.myScale,
                    SpriteEffects.None, 0f);
            }
        }


        // The Burnt State
        class BurntState : State
        {
            private Wood wood;

            public BurntState(Wood sprite)
            {
                wood = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Wood newWood = wood.Clone();
                newWood.myState = new BurningState(newWood);
                return newWood;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(wood.myBurnt, wood.myPosition,
                    null, Color.White,
                    wood.myAngle, wood.myOrigin, wood.myScale,
                    SpriteEffects.None, 0f);
            }
        }
    }
}
