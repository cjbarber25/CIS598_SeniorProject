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
    public class DoorHelper
    {

        public List<DoorSprite> AddDoors(int[,] layout, Tuple<int,int> currentPosition,Vector2 _roomPosition, int _roomLength, int _roomWidth, List<DoorSprite> _sprites,Sprite[,] roomLayout)
        {
            
            if (currentPosition.Item2 > 0)
            {
                if (currentPosition.Item2 < 4)
                {
                    if (currentPosition.Item1 > 0)
                    {
                        if (currentPosition.Item1 < 4)
                        {
                            //MIDDLE ROOM CASE
                            if (layout[currentPosition.Item1 + 1, currentPosition.Item2] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + (_roomLength - 1) * 62) + 48), new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2)));
                            }
                            if (layout[currentPosition.Item1 - 1, currentPosition.Item2] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + 62) + 32), new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2)));
                            }
                            if (layout[currentPosition.Item1, currentPosition.Item2 + 1] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + (_roomWidth - 1) * 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1)));
                            }
                            if (layout[currentPosition.Item1, currentPosition.Item2 - 1] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1)));
                            }





                        }
                        else
                        {
                            //BOTTOM ROOM CASE
                            if (layout[currentPosition.Item1 + 1, currentPosition.Item2] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + (_roomLength - 1) * 62) + 48),new Tuple<int,int>(currentPosition.Item1+1,currentPosition.Item2)));
                            }
                            if (layout[currentPosition.Item1 - 1, currentPosition.Item2] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + 62) + 32), new Tuple<int, int>(currentPosition.Item1 -1, currentPosition.Item2)));
                            }
                            if (layout[currentPosition.Item1, currentPosition.Item2 + 1] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + (_roomWidth - 1) * 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1 , currentPosition.Item2+1)));
                            }
                        }
                    }
                    else
                    {
                        //TOP ROOM CASE
                        if (layout[currentPosition.Item1 + 1, currentPosition.Item2] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + (_roomLength - 1) * 62) + 48), new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2)));
                        }
                        if (layout[currentPosition.Item1, currentPosition.Item2 + 1] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + (_roomWidth - 1) * 62), (_roomPosition.Y + (_roomLength * 58) / 2)),new Tuple<int, int>(currentPosition.Item1 , currentPosition.Item2+1)));
                        }
                        if (layout[currentPosition.Item1, currentPosition.Item2 - 1] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1 , currentPosition.Item2-1)));
                        }
                    }
                }
                else
                {
                    if (currentPosition.Item1 > 0)
                    {
                        if (currentPosition.Item1 < 4)
                        {
                            //RIGHTSIDE MIDDLE ROOM


                            if (layout[currentPosition.Item1 - 1, currentPosition.Item2] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + 62) + 32), new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2)));
                            }
                            if (layout[currentPosition.Item1, currentPosition.Item2 + 1] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + (_roomWidth - 1) * 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1)));
                            }
                            if (layout[currentPosition.Item1, currentPosition.Item2 - 1] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1)));
                            }
                        }
                        else
                        {
                            //BOTTOMRIGHT ROOM CASE

                            if (layout[currentPosition.Item1 - 1, currentPosition.Item2] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + 62) + 32), new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2)));
                            }
                            if (layout[currentPosition.Item1, currentPosition.Item2 + 1] != 0)
                            {
                                _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + (_roomWidth - 1) * 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1)));
                            }
                        }
                    }
                    else
                    {
                        //TOPRIGHT ROOM CASE

                        if (layout[currentPosition.Item1 + 1, currentPosition.Item2] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + (_roomLength - 1) * 62) + 48), new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2)));
                        }
                        if (layout[currentPosition.Item1, currentPosition.Item2 - 1] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1)));
                        }
                    }
                }
            }
            else
            {

                if (currentPosition.Item1 > 0)
                {
                    if (currentPosition.Item1 < 4)
                    {
                        //LEFTSIDE MIDDLE ROOM CASE

                        if (layout[currentPosition.Item1 + 1, currentPosition.Item2] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + (_roomLength - 1) * 62) + 48), new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2)));
                        }
                        if (layout[currentPosition.Item1, currentPosition.Item2 + 1] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + (_roomWidth - 1) * 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1)));
                        }
                        if (layout[currentPosition.Item1 - 1, currentPosition.Item2] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + 62) + 32), new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2)));
                        }
                    }
                    else
                    {
                        //BOTTOMLEFT ROOM CASE
                        if (layout[currentPosition.Item1 + 1, currentPosition.Item2] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + (_roomLength - 1) * 62) + 48), new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2)));
                        }
                        if (layout[currentPosition.Item1, currentPosition.Item2 + 1] != 0)
                        {
                            _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + (_roomWidth - 1) * 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1)));
                        }
                    }
                }
                else
                {
                    //TOPLEFT ROOM CASE

                    if (layout[currentPosition.Item1 + 1, currentPosition.Item2] != 0)
                    {
                        _sprites.Add(new DoorSprite(new Vector2(_roomPosition.X + (_roomWidth * 58) / 2, (_roomPosition.Y + (_roomLength - 1) * 62) + 48), new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2)));
                    }
                    if (layout[currentPosition.Item1, currentPosition.Item2 + 1] != 0)
                    {
                        _sprites.Add(new DoorSprite(new Vector2((_roomPosition.X + (_roomWidth - 1) * 62), (_roomPosition.Y + (_roomLength * 58) / 2)), new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1)));
                    }
                }
                
            }
            return _sprites;
        }

       
    }
}
