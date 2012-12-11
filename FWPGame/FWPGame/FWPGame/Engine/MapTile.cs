using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using FWPGame.Powers;
using FWPGame.Items;

namespace FWPGame.Engine
{
    public class MapTile
    {

        protected internal Vector2 myMapPosition;
        protected internal Vector2 myPosition;
        protected internal List<People> myPeople = new List<People>();
        protected internal List<Sprite> mySprites = new List<Sprite>();
        protected internal Vector2 mySize;
        protected internal Vector2 myScale = new Vector2(1f, 1f);
        protected internal bool hasGrass = false;
        protected internal bool hasFeature = false;
        protected internal bool isProtected = false;

        public MapTile(Vector2 pos, Vector2 size)
        {
            myMapPosition = pos;
            mySize = size;
        }

        public void Clear()
        {
            mySprites.Clear();
        }

        public void Destroy()
        {
            if (isProtected == false)
                Clear();
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            hasGrass = false;
            hasFeature = false;
            myPosition = myMapPosition - playerPosition;
            foreach (Sprite s in mySprites)
            {
                if (s.name.Equals("grass"))
                {
                    hasGrass = true;
                }
                if (s.name.Equals("tree") || s.name.Equals("house"))
                {
                    hasFeature = true;
                }
                if (s.name.Equals("Halo"))
                {
                    Halo h = (Halo)s;
                    if (h.isProtecting == true)
                        isProtected = true;
                    else
                    {
                        isProtected = false;
                    }
                }
                s.Update(gameTime.TotalGameTime.Milliseconds);
                s.myPosition = myPosition;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Sprite s in mySprites)
            {
                s.Draw(batch);
            }
        }

        public void Add(Sprite s)
        {
            s.myMapPosition = myMapPosition;
            s.myPosition = myPosition;
            s.myScale = myScale;
            mySprites.Add(s);
        }

        public void ClearTile()
        {
            mySprites = new List<Sprite>();
        }
    }
}