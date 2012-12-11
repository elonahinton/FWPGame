using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace FWPGame.Engine
{
    public class MapTile
    {

        protected internal Vector2 myMapPosition;
        protected internal Vector2 myPosition;
        protected internal List<Sprite> mySprites = new List<Sprite>();
        protected internal Vector2 mySize;
        protected internal Vector2 myScale = new Vector2(1f, 1f);

        public MapTile(Vector2 pos, Vector2 size)
        {
            myMapPosition = pos;
            mySize = size;
        }

        public void Clear()
        {
            mySprites.Clear();
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            myPosition = myMapPosition - playerPosition;
            foreach (Sprite s in mySprites)
            {
                s.Update(gameTime.ElapsedGameTime.Milliseconds);
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
    }
}
