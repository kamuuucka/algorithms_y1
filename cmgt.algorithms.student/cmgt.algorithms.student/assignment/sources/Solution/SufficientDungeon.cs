using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GXPEngine;
using GXPEngine.OpenGL;
using System.Threading;

class SufficientDungeon : Dungeon
{
	List<Room> roomList = new List<Room>();
    Random random = new Random();
	
    public SufficientDungeon(Size pSize) : base(pSize) { }

	//why this
	protected override void generate(int pMinimumRoomSize)
	{
        ////left room from 0 to half of screen + 1 (so that the walls overlap with the right room)
        ////(TODO: experiment with removing the +1 below to see what happens with the walls)

        //rooms.Add(new Room(new Rectangle(0, 0, size.Width / 2+1, size.Height)));
        ////right room from half of screen to the end
        //rooms.Add(new Room(new Rectangle(size.Width / 2, 0, size.Width / 2, size.Height)));
        ////and a door in the middle wall with a random y position
        ////TODO:experiment with changing the location and the Pens.White below
        //doors.Add(new Door(new Point(size.Width / 2, size.Height / 2 + Utils.Random(-5, 5))));

        //rooms.Add(new Room(new Rectangle(0, 0, size.Width / 2 + 1, size.Height)));			//1st room
        //rooms.Add(new Room(new Rectangle(size.Width / 2, 0, size.Width / 2, size.Height)));	//2nd room

        //rooms.Add(new Room(new Rectangle(0,0, size.Width/4+1, size.Height)));
        //rooms.Add(new Room(new Rectangle(size.Width/3, 0, size.Width/3+1, size.Height)));
        //rooms.Add(new Room(new Rectangle(size.Width / 3 + size.Width/3, 0, size.Width / 3 + 1, size.Height)));
        int cutVertical = 0;
        int cutHorizontal = 0;
        roomList.Add(new Room(new Rectangle(0, 0, size.Width, size.Height)));

        while (roomList.Count != 0)
        {
            
            bool horizontal = false;
            bool vertical = false;
            Room uncutRoom = roomList[roomList.Count - 1];
            //Console.WriteLine("X: " + uncutRoom.area.X);
            //Console.WriteLine("Y: " + uncutRoom.area.Y);
            roomList.Remove(uncutRoom);

            //Console.WriteLine("WIDTH: " + (uncutRoom.area.Width - cutVertical + pMinimumRoomSize));
            //Console.WriteLine("HEIGHT: " + (uncutRoom.area.Height - cutHorizontal + pMinimumRoomSize));

            //cutHorizontal = random.Next(uncutRoom.area.Y + pMinimumRoomSize, uncutRoom.area.Height);
            //cutVertical = random.Next(uncutRoom.area.X + pMinimumRoomSize, uncutRoom.area.Width);

            //if (uncutRoom.area.Width - pMinimumRoomSize > pMinimumRoomSize)
            //{

            //    if (uncutRoom.area.Height - pMinimumRoomSize > pMinimumRoomSize)
            //    {
            //        horizontal = uncutRoom.area.Width < uncutRoom.area.Height;
            //        //cutHorizontal = random.Next(uncutRoom.area.Y + pMinimumRoomSize, uncutRoom.area.Height + uncutRoom.area.Y - pMinimumRoomSize);
            //    }
            //    //cutVertical = random.Next(uncutRoom.area.X + pMinimumRoomSize, uncutRoom.area.Width + uncutRoom.area.X - pMinimumRoomSize);
               
            //    //Console.WriteLine(uncutRoom.area.X + pMinimumRoomSize + " : " + (uncutRoom.area.Width + uncutRoom.area.X - pMinimumRoomSize));
            //    //int cut = random;
            //    Console.WriteLine("RANDOM V: " + cutVertical);
            //    Console.WriteLine("RANDOM H: " + cutHorizontal);

            //    if (horizontal)
            //    {
                    
            //        Console.WriteLine("HORIZONTAL");
            //        //toproom
            //        //roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, uncutRoom.area.Width, uncutRoom.area.Height / 2+1)));
            //        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, uncutRoom.area.Width, cutHorizontal + 1)));
            //        //bottomroom
            //        //roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y + uncutRoom.area.Height / 2, uncutRoom.area.Width, uncutRoom.area.Height / 2)));
            //        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, cutHorizontal, uncutRoom.area.Width, uncutRoom.area.Height - cutHorizontal)));
            //    }
            //    else
            //    {
                    
            //        Console.WriteLine("VERTICAL");
            //        //leftroom
            //        //roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, uncutRoom.area.Width / 2, uncutRoom.area.Height)));
            //        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, cutVertical+1, uncutRoom.area.Height)));
            //        //right one
            //        //roomList.Add(new Room(new Rectangle(uncutRoom.area.X + uncutRoom.area.Width / 2, uncutRoom.area.Y, uncutRoom.area.Width / 2, uncutRoom.area.Height)));
            //        roomList.Add(new Room(new Rectangle(cutVertical, uncutRoom.area.Y, uncutRoom.area.Width-cutVertical, uncutRoom.area.Height)));
            //    }

            //}
            //else
            //{
            //    Console.WriteLine("Final room X: " + uncutRoom.area.X + " Y: " + uncutRoom.area.Y + " W: " + uncutRoom.area.Width + " H: " + uncutRoom.area.Height);
            //    Console.WriteLine("The room can't be cut anymore");
            //    rooms.Add(uncutRoom);
            //    //Room uncutRoom1 = roomList[roomList.Count];
            //    //cutHorizontal = random.Next(uncutRoom1.area.Y + pMinimumRoomSize, uncutRoom1.area.Height + uncutRoom1.area.Y - pMinimumRoomSize);
            //    //cutVertical = random.Next(uncutRoom1.area.X + pMinimumRoomSize, uncutRoom1.area.Width + uncutRoom1.area.X - pMinimumRoomSize);
            //    //roomList.Clear();
            //}

            if (uncutRoom.area.Width >= pMinimumRoomSize && uncutRoom.area.Height >= pMinimumRoomSize)
            {
                if (uncutRoom.area.Height >= pMinimumRoomSize * 2)
                {
                    horizontal = true;
                }
                else if (uncutRoom.area.Width >= pMinimumRoomSize * 2)
                {
                    vertical = true;
                }

                if (horizontal)
                {
                    SplitHorizontally(uncutRoom, roomList, pMinimumRoomSize);
                }
                else if (vertical)
                {
                    SplitVertically(uncutRoom, roomList, pMinimumRoomSize);
                }
                //if (vertical)
                //{
                //    SplitVertically(uncutRoom, roomList, pMinimumRoomSize);
                //}
                //else if (horizontal)
                //{
                //    SplitHorizontally(uncutRoom, roomList, pMinimumRoomSize);
                //}
                else
                {
                    rooms.Add(uncutRoom);
                }
            }


            Thread.Sleep(1000);

            drawRooms(roomList, Pens.Blue);
            //roomList.Clear();
        }

        //drawRooms(rooms, Pens.Green);

	}

    private void SplitVertically(Room uncutRoom, List<Room> roomList, int pMinimumRoomSize)
    {
        int cut = random.Next(pMinimumRoomSize, uncutRoom.area.Width - pMinimumRoomSize);
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, cut+1, uncutRoom.area.Height)));
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X + cut, uncutRoom.area.Y, uncutRoom.area.Width - cut, uncutRoom.area.Height)));
    }
    private void SplitHorizontally(Room uncutRoom, List<Room> roomList, int pMinimumRoomSize)
    {
        int cut = random.Next(pMinimumRoomSize, uncutRoom.area.Height - pMinimumRoomSize);
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, uncutRoom.area.Width, cut+1)));
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y + cut, uncutRoom.area.Width, uncutRoom.area.Height - cut)));
    }

}

