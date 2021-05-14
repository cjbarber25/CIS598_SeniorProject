using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ForeignSubstance.Sprites;
namespace ForeignSubstance.Rooms
{
    public class BossRoom : Room
    {
        private Sprite[,] _sprites;

        public override void Build(int length, int width, Vector2 position)
        {
            _sprites = new Sprite[length, width];
            Vector2 tempPosition = position;
            float currentX = position.X;
            float currentY = position.Y;
            int xAdjust = 64;
            int yAdjust = 128;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("TOP", "LEFT", "CORNER"));
                            currentX += xAdjust;
                        }
                        else if (j == width - 1)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("TOP", "RIGHT", "CORNER"));
                            currentX = position.X;
                            currentY += yAdjust;
                        }
                        else
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("TOP", "CENTER", "WALL"));
                            currentX += xAdjust;
                        }
                    }
                    else if (i == length - 1)
                    {
                        if (j == 0)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("BOTTOM", "LEFT", "CORNER"));
                            currentX += xAdjust;
                        }
                        else if (j == width - 1)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("BOTTOM", "RIGHT", "CORNER"));
                        }
                        else
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("BOTTOM", "CENTER", "WALL"));
                            currentX += xAdjust;
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("NORMAL", "LEFT", "WALL"));
                            currentX += xAdjust;
                        }
                        else if (j == width - 1)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("NORMAL", "RIGHT", "WALL"));
                            currentX = position.X;
                            currentY += 64;
                        }
                        else
                        {
                            _sprites[i, j] = new FloorSprite(new Vector2(currentX, currentY));
                            currentX += xAdjust;
                        }
                    }
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    _sprites[i, j].LoadContent(content);
                }
            }
        }

        public override bool CheckForOutOfBounds(BoundingRectangle playerBounds)
        {
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    if (_sprites[i, j].CheckCollision(playerBounds))
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public override void Update(GameTime gametime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    _sprites[i, j].Draw(gameTime, spriteBatch);
                }
            }
        }
    }
}

