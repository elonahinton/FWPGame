using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using FWPGame.Engine;

namespace FWPGame.Engine
{
    public class Map
    {
        public const int MAX_TILE_SIZE = 64;
        private Vector2 myScreenSize;
        private Player myPlayer;
        private MapTile[,] mapTiles;
        public MapTile[,] MapTiles { get { return mapTiles; } }
        private Vector2 myPosition;
        private Texture2D myTexture;
        private float myAngle = 0f;
        private Vector2 myOrigin = new Vector2(0, 0);
        private Vector2 myScale = new Vector2(1, 1);
        private Vector2 mySize = new Vector2(0, 0);

        public Vector2 baseScale = new Vector2(1, 1);

        public int getMaxTileSize()
        {
            return MAX_TILE_SIZE;
        }

        public Map(Texture2D texture, Vector2 screenSize, Vector2 position, Player player)
        {
            /*if (screenSize.X > texture.Width)
            {
                baseScale = new Vector2(screenSize.X / texture.Width, screenSize.X / texture.Width);
            }
            else if (screenSize.Y > texture.Height)
            {
                baseScale = new Vector2(screenSize.Y / texture.Height, screenSize.Y / texture.Height);
            }*/
            baseScale = new Vector2(2, 2);
            myScale = baseScale;
            myTexture = texture;
            myPosition = position;
            myScreenSize = screenSize;
            myPlayer = player;
            mySize.X = myTexture.Bounds.Width * myScale.X;
            mySize.Y = myTexture.Bounds.Height * myScale.Y;
            player.myMapSize = mySize;
            CreateTileGrid(mySize);
        }

        /// <summary>
        /// Update the map's relative positions with the player's position
        /// </summary>
        public void Update(GameTime gameTime, Vector2 worldScale)
        {
            myPlayer.Update(gameTime, worldScale);
            myScale = worldScale;
            myPosition = -(myPlayer.myMapPosition);
            foreach (MapTile m in mapTiles)
            {
                m.Update(gameTime, myPlayer.myMapPosition);
            }
        }

        /// <summary>
        /// Get the MapTile the cursor is on - built-in safeguards to disallow index out of bounds exceptions.
        /// </summary>
        /// <returns></returns>
        public MapTile GetTile(Cursor cursor)
        {
            int tilesX = (int) mySize.X / MAX_TILE_SIZE;
            int tilesY = (int) mySize.Y / MAX_TILE_SIZE;
            int x = (int) cursor.myMapPosition.X / MAX_TILE_SIZE;
            int y = (int) cursor.myMapPosition.Y / MAX_TILE_SIZE;
            if (x >= tilesX)
                x = tilesX - 1;
            if (x < 0)
                x = 0;
            if (y >= tilesY)
                y = tilesY - 1;
            if (y < 0)
                y = 0;
            MapTile tile = mapTiles[x, y];
            return tile;
        }

        /// <summary>
        /// Get coordinates of MapTile to spread to - built-in safeguards to disallow index out of bounds exceptions.
        /// </summary>
        /// <returns></returns>
        public void SpreadTile(ref int x, ref int y)
        {
            int tilesX = (int)mySize.X / MAX_TILE_SIZE;
            int tilesY = (int)mySize.Y / MAX_TILE_SIZE;
            
            if (x >= tilesX)
                x = tilesX - 1;
            if (x < 0)
                x = 0;
            if (y >= tilesY)
                y = tilesY - 1;
            if (y < 0)
                y = 0;
        }

        /// <summary>
        /// Create the tile grid given the size of the map.
        /// </summary>
        /// <param name="textureSize"></param>
        public void CreateTileGrid(Vector2 textureSize)
        {

            int tilesX = (int) textureSize.X / MAX_TILE_SIZE;
            int tilesY = (int) textureSize.Y / MAX_TILE_SIZE;
            mapTiles = new MapTile[tilesX, tilesY];
            for (int i = 0; i < tilesX; i++)
            {
                for (int j = 0; j < tilesY; j++)
                {
                    MapTile tile = new MapTile(new Vector2(i * MAX_TILE_SIZE, j * MAX_TILE_SIZE),
                        new Vector2(MAX_TILE_SIZE, MAX_TILE_SIZE));
                    mapTiles[i, j] = tile;
                }
            }
        }


        /// <summary>
        /// Draw the map, then draw the MapTiles that are visible to the player.
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(myTexture, myPosition,
                   null, Color.White,
                   myAngle, myOrigin, myScale,
                   SpriteEffects.None, 0f);

            // We only want to render the map tiles that are visible, so we need to find which ones the player can see
            var x = (int)Math.Floor((myPlayer.myMapPosition.X / (MAX_TILE_SIZE)));
            var xmax = (int)Math.Floor((myPlayer.myMapPosition.X + myScreenSize.X) / (MAX_TILE_SIZE));
            var y = (int)Math.Floor((myPlayer.myMapPosition.Y) / (MAX_TILE_SIZE));
            var ymax = (int)Math.Floor((myPlayer.myMapPosition.Y + myScreenSize.Y) / (MAX_TILE_SIZE));

            if (xmax >= mapTiles.GetLength(0))
                xmax = mapTiles.GetLength(0) - 1;
            if (ymax >= mapTiles.GetLength(1))
                ymax = mapTiles.GetLength(1) - 1;

            int xToRender = (int)Math.Floor(myScreenSize.X / MAX_TILE_SIZE);
            int yToRender = (int)Math.Floor(myScreenSize.Y / MAX_TILE_SIZE);

            for (int i = x; i < xmax; i++)
            {
                for (int j = y; j < ymax; j++)
                {
                    mapTiles[i, j].Draw(batch);
                }
            }
        }

    }
}
