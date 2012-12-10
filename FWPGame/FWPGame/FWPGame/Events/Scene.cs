using System;
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

namespace FWPGame.Events
{
    public class Scene
    {
        protected internal Texture2D myTexture;
        protected internal Vector2 myPosition;
        protected internal Vector2 myMapPosition = new Vector2(0, 0);
        protected internal State myState;

     // The base constructor.
        public Scene(Texture2D texture, Vector2 position)
        {
            myTexture = texture;
            myPosition = position;
        }
    }
}
