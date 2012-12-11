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
    public class Halo : Sprite
    {
        protected internal bool isProtecting = true;

        public Halo(Texture2D texture, Vector2 position, Vector2 mapPosition) :
            base(texture, position)
        {
            myMapPosition = mapPosition;
            myTexture = texture;
            myPosition = position;
            myState = new ProtectState(this);

            name = "Halo";
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

        public void Protect()
        {
            myState = new ProtectState(this);
        }

        public Halo Clone()
        {
            return new Halo(this.myTexture, new Vector2(0, 0), new Vector2(0, 0));
        }

        public override void Update(GameTime gameTime, Vector2 playerMapPos)
        {
            if (myState != null)
            {
                myState.Update(gameTime.TotalGameTime.Seconds, playerMapPos);
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            if (myState != null)
            {
                myState.Draw(batch);
            }
        }

        class ProtectState : State
        {
            private Halo halo;
            private double timer = 0.0;
            private double protectionLength = 500.0;
            public ProtectState(Halo h)
            {
                halo = h;
                halo.isProtecting = true;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {
                if (timer == 0.0)
                    timer = elapsedTime;
                if (elapsedTime - timer > protectionLength)
                {
                    halo.myState = new HideState(halo);
                }
                halo.myPosition = halo.myMapPosition - playerMapPos;
            }

            public void Draw(SpriteBatch batch)
            {
                batch.Draw(halo.myTexture, halo.myPosition,
                        null, Color.White,
                        halo.myAngle, halo.myOrigin, halo.myScale,
                        SpriteEffects.None, 0f);
            }

            public Sprite Spread()
            {
                return null;
            }

        }

        class HideState : State
        {
            private Halo halo;

            public HideState(Halo h)
            {
                halo = h;
                halo.isProtecting = false;
            }

            public void Update(double elapsedTime, Vector2 playerMapPos)
            {

            }

            public void Draw(SpriteBatch batch)
            {

            }

            public Sprite Spread()
            {
                return null;
            }

        }

    }
}
