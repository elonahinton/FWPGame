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
    public class GrassSprite : Sprite
    {
        public GrassSprite(Texture2D texture, Vector2 position, Vector2 mapPosition) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            
			name = "GrassSprite";
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

        public GrassSprite Clone()
        {
            return new GrassSprite(this.myTexture, new Vector2(0,0), new Vector2(0,0));
        }


        
        // The Regular State
        class RegularState : State
        {
            private GrassSprite grass;

            public RegularState(GrassSprite sprite)
            {
                grass = sprite;
            }

            // Determine whether this is a spreading conditition
            public Sprite Spread()
            {
                //GrassSprite newGrass = grass.Clone();
                //newGrass.myState = new RegularState(newGrass);
                //return newGrass;
                return null;
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
    }
}
