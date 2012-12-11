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
    public class Tree : Sprite
    {
        private Texture2D[] myBurningSequence;
        private Animate myBurning;
        private Texture2D myBurnt;
        private Texture2D[] myMultiplySequence;
        private Animate myMultiply;


        public Tree(Texture2D texture, Vector2 position, Vector2 mapPosition,
            Texture2D[] burningSequence, Texture2D burnt, Texture2D[] multiplyTree) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            name = "Tree";
            myBurningSequence = burningSequence;
            myBurning = new Animate(burningSequence);
            SetUpBurning();
            myBurnt = burnt;
            myMultiplySequence = multiplyTree;
            myMultiply = new Animate(multiplyTree);
            SetUpMultiply();
            myState = new RegularState(this);
        }


        public Tree Clone()
        {
            return new Tree(this.myTexture, new Vector2(0, 0), new Vector2(0, 0),
                myBurningSequence, myBurnt, myMultiplySequence);
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


        public void SetUpBurning()
        {
            // Prepare the flip book sequence for expected Animate
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(1, 300);
            myBurning.AddFrame(2, 200);
            myBurning.AddFrame(3, 250);
            myBurning.AddFrame(4, 150);
            myBurning.AddFrame(5, 90);
            myBurning.AddFrame(6, 310);
            myBurning.AddFrame(7, 200);
            myBurning.AddFrame(8, 110);
            myBurning.AddFrame(9, 350);
            myBurning.AddFrame(0, 100);
            myBurning.AddFrame(1, 300);
            myBurning.AddFrame(2, 200);
            myBurning.AddFrame(3, 250);
            myBurning.AddFrame(4, 150);
            myBurning.AddFrame(5, 90);
            myBurning.AddFrame(6, 310);
            myBurning.AddFrame(7, 200);
            myBurning.AddFrame(8, 110);
            myBurning.AddFrame(9, 350);
        }

        public void SetUpMultiply()
        {
            // Prepare the flip book sequence for multiply Animate
            for (int i = 0; i < 68; ++i)
            {
                myMultiply.AddFrame(i, 100);
            }
        }



        // The Regular State
        class RegularState : State
        {
            private Tree tree;

            public RegularState(Tree sprite)
            {
                tree = sprite;
            }


            public Sprite Spread()
            {
                Tree newTree = tree.Clone();
                newTree.myState = new MultiplyState(newTree);
                return newTree;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
              
            }

            public void Draw(SpriteBatch batch)
            {              
                batch.Draw(tree.myTexture, tree.myPosition,
                        null, Color.White,
                        tree.myAngle, tree.myOrigin, tree.myScale,
                        SpriteEffects.None, 0f);
            }
        }



        // The Burning State
        class BurningState : State
        {
            private Tree tree;

            public BurningState(Tree sprite)
            {
                tree = sprite;
            }


            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                Tree newTree = tree.Clone();
                newTree.myState = new BurningState(newTree);
                return newTree;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                tree.myBurning.Update(elapsedTime, ref seqDone);
                
                if (seqDone)
                {
                    tree.myState = new BurntState(tree);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(tree.myBurning.GetImage(), tree.myPosition, null, Color.White, tree.myAngle,
                        tree.myOrigin, tree.myScale,
                        SpriteEffects.None, 0f); 
            }
        }



        // The Burnt State
        class BurntState : State
        {
            private Tree tree;

            public BurntState(Tree sprite)
            {
                tree = sprite;
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
                batch.Draw(tree.myBurnt, tree.myPosition,
                    null, Color.White,
                    tree.myAngle, tree.myOrigin, tree.myScale,
                    SpriteEffects.None, 0f);
            }

        }



        // The Multiplying State
        class MultiplyState : State
        {
            private Tree tree;

            public MultiplyState(Tree sprite)
            {
                tree = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                return null;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                bool seqDone = false;
                
                tree.myMultiply.Update(elapsedTime, ref seqDone);
                if (seqDone)
                {
                   tree.myState = new RegularState(tree);
                }
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(tree.myMultiply.GetImage(), tree.myPosition, null, Color.White, tree.myAngle,
                        tree.myOrigin, tree.myScale,
                        SpriteEffects.None, 0f);
            }
        }
    }
}
