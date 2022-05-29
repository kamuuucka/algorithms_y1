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
    List<Door> doorList = new List<Door>();
    List<Room> roomListDoor = new List<Room>();
    List<Room> roomListDoor2 = new List<Room>();
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
        roomList.Add(new Room(new Rectangle(0, 0, size.Width, size.Height)));

        while (roomList.Count != 0)
        {

            bool horizontal = false;
            bool vertical = false;
            Room uncutRoom = roomList[roomList.Count - 1];
            roomList.Remove(uncutRoom);

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

                else
                {
                    rooms.Add(uncutRoom);
                }
            }


            //Thread.Sleep(1000);

            drawRooms(roomList, Pens.Blue);
            //roomList.Clear();
        }
        drawRooms(rooms, Pens.Black);
        for (int i = 0; i < rooms.Count; i++)
        {
            roomListDoor.Add(rooms[i]);
        }

        while (roomListDoor.Count != 0)
        {
            int collidingWithRooms = 0;
            Room roomToCheck = roomListDoor[roomListDoor.Count - 1];
            for (int i = 0; i < rooms.Count; i++)
            {
                roomListDoor2.Add(rooms[i]);
            }

            bool doorLeft = false;
            bool doorRight = false;
            bool doorTop = false;
            bool doorBottom = false;
            

            while (roomListDoor2.Count != 0)
            {
                Room roomToCompare = roomListDoor2[roomListDoor2.Count - 1];
                roomListDoor2.Remove(roomToCompare);


                if (roomToCheck.area.Top == roomToCompare.area.Bottom - 1 && !doorTop)
                {
                    //Console.WriteLine("There is room above me!");
                    //drawRoom(roomToCheck, new Pen(Color.FromArgb(128, Color.Red)));
                    //doors.Add(new Door(new Point(roomToCheck.area.X + roomToCheck.area.Width / 2, roomToCheck.area.Top)));
                    //doorList.Add(new Door(new Point(roomToCheck.area.X + roomToCheck.area.Width / 2, roomToCheck.area.Top)));

                    doorTop = true;
                    collidingWithRooms++;
                }
                else
                {
                    //Console.WriteLine("No room above me");
                }

                if (roomToCheck.area.Bottom == roomToCompare.area.Top + 1 && !doorBottom)
                {
                    //Console.WriteLine("There is room below me!");
                    //drawRoom(roomToCheck, new Pen(Color.FromArgb(128, Color.Yellow)));
                    //doors.Add(new Door(new Point(roomToCheck.area.X + roomToCheck.area.Width / 2, roomToCheck.area.Bottom-1)));
                    //doorList.Add(new Door(new Point(roomToCheck.area.X + roomToCheck.area.Width / 2, roomToCheck.area.Bottom - 1)));
                    doorBottom = true;
                    collidingWithRooms++;
                }
                else
                {
                    //Console.WriteLine("No room below me");
                }

                if (roomToCheck.area.Left == roomToCompare.area.Right - 1 && !doorLeft)
                {
                    //Console.WriteLine("There is room on the left!");
                    //drawRoom(roomToCheck, new Pen(Color.FromArgb(128, Color.Lime)));
                    //doors.Add(new Door(new Point(roomToCheck.area.Left, roomToCheck.area.Y + roomToCheck.area.Height/2)));
                    //doorList.Add(new Door(new Point(roomToCheck.area.Left, roomToCheck.area.Y + roomToCheck.area.Height / 2)));
                    doorLeft = true;
                    collidingWithRooms++;
                }
                else
                {
                    //Console.WriteLine("There are no rooms on the left :(");
                }

                if (roomToCheck.area.Right == roomToCompare.area.Left + 1 && !doorRight)
                {
                    //Console.WriteLine("There is room on the left!");
                    //drawRoom(roomToCheck, new Pen(Color.FromArgb(128, Color.Blue)));
                    //doors.Add(new Door(new Point(roomToCheck.area.Right-1, roomToCheck.area.Y + roomToCheck.area.Height / 2)));
                    //doorList.Add(new Door(new Point(roomToCheck.area.Right - 1, roomToCheck.area.Y + roomToCheck.area.Height / 2)));
                    doorRight = true;
                    collidingWithRooms++;
                }
                else
                {
                    //Console.WriteLine("There are no rooms on the left :(");
                }

                
                
            }

            Console.WriteLine("ROOM: " + roomToCheck.toString() + " Colliding rooms: " + collidingWithRooms);

            if (collidingWithRooms == 2)
            {
                drawRoom(roomToCheck, new Pen(Color.FromArgb(128, Color.Purple)));
            }

            roomListDoor.Remove(roomToCheck);

        }

        doorList = doorList.Distinct().ToList();
        foreach (Door door in doorList)
        {
            Console.WriteLine(door.toString());
        }
        //drawDoors(doorList, Pens.Lime);



    }

    private void SplitVertically(Room uncutRoom, List<Room> roomList, int pMinimumRoomSize)
    {
        int cut = random.Next(pMinimumRoomSize, uncutRoom.area.Width - pMinimumRoomSize);
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, cut + 1, uncutRoom.area.Height)));//, cut, false));
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X + cut, uncutRoom.area.Y, uncutRoom.area.Width - cut, uncutRoom.area.Height)));//, cut, false)) ;
    }
    private void SplitHorizontally(Room uncutRoom, List<Room> roomList, int pMinimumRoomSize)
    {
        int cut = random.Next(pMinimumRoomSize, uncutRoom.area.Height - pMinimumRoomSize);
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, uncutRoom.area.Width, cut + 1)));//, cut));
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y + cut, uncutRoom.area.Width, uncutRoom.area.Height - cut)));//,cut));
    }

}

