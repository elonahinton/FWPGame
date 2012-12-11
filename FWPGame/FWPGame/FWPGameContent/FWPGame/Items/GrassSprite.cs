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

namespace FWPGame
{
    public class GrassSprite : Sprite
    {
        public GrassSprite(Texture2D texture, Vector2 position, Vector2 mapPosition) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            myTexture = texture;
            myPosition = position;
			name = "GrassSprite";
        }

        public void setMyPosition(Vector2 pos)
        {
            myPosition = pos;
            Debug.WriteLine("position is at: " + pos);
        }

        public void setMyMapPosition(Vector2 pos)
        {
            myMapPosition = pos;
            Debug.WriteLine("map position is at: " + pos);
        }

        public GrassSprite Clone()
        {
            return new GrassSprite(this.myTexture, new Vector2(0,0), new Vector2(0,0));
        }

        public override void Update(GameTime gameTime, Vector2 playerMapPos)
        {
           // myPosition = myMapPosition - playerMapPos;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(myTexture, myPosition,
                    null, Color.White,
                    myAngle, myOrigin, myScale,
                    SpriteEffects.None, 0f);
        }

    }
}
