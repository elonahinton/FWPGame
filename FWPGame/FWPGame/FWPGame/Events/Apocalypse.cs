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
using FWPGame.Items;
using System.Diagnostics;

namespace FWPGame.Events
{
    class Apocalypse
    {
        public List<Sprite> mySprites;
        private FWPGame myGame;
        private Dictionary<string, Power> myPowers;
        private MapTile[,] myMap;
        public Vector2 villageOriginIndex;
        public Vector2 villageEndIndex;
        public Vector2 villageOrigin;
        public Vector2 villageEnd;
        private State myState;

        public Apocalypse(FWPGame game, Dictionary<string, Power> powers)
        {
            myPowers = powers;
            myGame = game;
            myMap = myGame.map.MapTiles;
            mySprites = new List<Sprite>();
        }

        public void BuildVillage()
        {
            Power makeGrass = myPowers["grass"];
            Power buildHouse = myPowers["house"];
            Power makeTree = myPowers["sprout"];
            // We want to go from 1/3rd of the map to 2/3rds of the map
            villageOriginIndex = new Vector2(myMap.GetLength(0) / 3, myMap.GetLength(1) / 3);
            villageOrigin = myMap[(int)villageOriginIndex.X, (int)villageOriginIndex.Y].myMapPosition;

            villageEndIndex = new Vector2(2 * (myMap.GetLength(0) / 3), 2 * (myMap.GetLength(1) / 3));
            villageEnd = myMap[(int)villageEndIndex.X, (int)villageEndIndex.Y].myMapPosition;

            // Within that "quadrant," let's make our village
            for (int i = (int)villageOriginIndex.X; i < (int)villageEndIndex.X; i++)
            {
                for (int j = (int)villageOriginIndex.Y; j < (int)villageEndIndex.Y; j++)
                {
                    makeGrass.Interact(myMap[i, j]);
                    Random r = new Random(DateTime.Now.Millisecond);
                    double rnd1 = r.Next(0, 100);
                    Debug.WriteLine(rnd1);
                    if (rnd1 <= 50)
                    {
                        buildHouse.Interact(myMap[i, j]);
                    }
                    double rnd2 = r.Next(0, 100);
                    Debug.WriteLine(rnd2);
                    if (rnd2 < 20)
                    {
                        makeTree.Interact(myMap[i, j]);
                    }

                }
            }

            myState = new PhaseOne(this);
        }

        // Spawn a random # of tornadoes at random areas and with random directions
        public void SpawnTornadoes()
        {
            Random r = new Random(System.DateTime.Now.Millisecond);
            for (int i = 0; i < r.Next(0, 2); i++)
            {
                Tornado t = myGame.motherTornado.Clone();
                Random randLoc = new Random(System.DateTime.Now.Millisecond);
                int rand = randLoc.Next(0, 100);
                Debug.WriteLine(rand);
                if (rand < 50)
                    t.myMapPosition = myMap[(int)villageOriginIndex.X + r.Next(-10, -2), (int)villageOriginIndex.Y + r.Next(-10, -3)].myMapPosition;
                else
                    t.myMapPosition = myMap[(int)villageEndIndex.X + r.Next(0, 10), (int)villageEndIndex.Y + r.Next(0, 10)].myMapPosition;

                if (rand >= 0 && rand < 20)
                    t.myVelocity = new Vector2(1, 1);
                else if (rand >= 20 && rand < 40)
                    t.myVelocity = new Vector2(1, 0);
                else if (rand >= 40 && rand < 60)
                    t.myVelocity = new Vector2(-1, 1);
                else if (rand >= 60 && rand < 80)
                    t.myVelocity = new Vector2(1, -1);
                else if (rand >= 80 && rand < 100)
                    t.myVelocity = new Vector2(0, 1);

                if (t.myMapPosition.X < villageOrigin.X)
                    t.myVelocity.X = 1;
                else if (t.myMapPosition.X > villageEnd.X)
                    t.myVelocity.X = -1;

                if (t.myMapPosition.Y < villageOrigin.Y)
                    t.myVelocity.Y = 1;
                else if (t.myMapPosition.Y > villageEnd.Y)
                    t.myVelocity.Y = -1;

                mySprites.Add(t);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Sprite s in mySprites)
            {
                s.Draw(batch);
            }
        }

        public void Update(double elapsedTime, Vector2 playerMapPos)
        {
            if (myState != null)
            {
                myState.Update(elapsedTime, playerMapPos);
            }
            else
            {
                foreach (Sprite s in mySprites)
                {
                    s.Update(elapsedTime, playerMapPos);
                }
            }
            //t2.printDebug();
        }


        class PhaseOne : State
        {

            private Apocalypse apoc;
            private double torInterval = 4.0;
            private double lastTor = 0.0;
            public PhaseOne(Apocalypse a)
            {
                apoc = a;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                if (elapsedTime - lastTor > torInterval)
                {
                    apoc.SpawnTornadoes();
                    lastTor = elapsedTime;
                }
                foreach (Sprite s in apoc.mySprites)
                {
                    s.Update(elapsedTime, playerMapPos);
                    Vector2 tornadoPos = new Vector2(s.myMapPosition.X + s.myTexture.Width / 2, s.myMapPosition.Y + s.myTexture.Height);
                    apoc.myGame.map.GetTile(tornadoPos).Destroy();
                }

            }

            public void Draw(SpriteBatch batch)
            {
                apoc.Draw(batch);
            }

            public Sprite Spread()
            {
                return null;
            }
        }
    }
}
