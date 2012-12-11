using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace FWPGame.Engine
{
    /// <summary>
    /// Written with help from David Amador's online tutorial:
    /// http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
    /// This class enables zooming within the game through the use of Matrix transformation
    /// and spriteBatch.Begin.
    /// </summary>
    public class Camera
    {
        protected float          zoom; // Camera Zoom
        public Matrix             transform; // Matrix Transform
        public Vector2          pos; // Camera Position
        protected float         rotation; // Camera Rotation
 
        public Camera()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            pos = Vector2.Zero;
        }

        // Sets and gets zoom
        public float Zoom
                {
                    get { return zoom; }
                    set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } // Negative zoom will flip image
                }
 
         public float Rotation
                {
                    get {return rotation; }
                    set { rotation = value; }
                }
 
        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
           pos += amount;
        }
       // Get set position
        public Vector2 Pos
        {
             get{ return  pos; }
             set{ pos = value; }
        }
 
        public Matrix getTransformation(GraphicsDevice graphicsDevice)
        {
            transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width* 0.5f, 
                                             graphicsDevice.Viewport.Height * 0.5f, 0));
            return transform;
        }

    }
}
