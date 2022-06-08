using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GXPEngine;
using GXPEngine.OpenGL;
using System.Threading;

class GoodDungeon : Dungeon
{
    List<Room> roomList = new List<Room>();
    List<Door> doorList = new List<Door>();
    List<Room> roomListDoor = new List<Room>();
    List<Point> corners = new List<Point>();
    Random random = new Random();
    static int seed = 6789;
    Random rand = new Random(seed);
    List<int> areas = new List<int>();

    public GoodDungeon(Size pSize) : base(pSize) { }

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
                    corners.Add(new Point(uncutRoom.area.X, uncutRoom.area.Y));
                    corners.Add(new Point(uncutRoom.area.X + uncutRoom.area.Width, uncutRoom.area.Y));
                    corners.Add(new Point(uncutRoom.area.X, uncutRoom.area.Y + uncutRoom.area.Height));
                    corners.Add(new Point(uncutRoom.area.X + uncutRoom.area.Width, uncutRoom.area.Y + uncutRoom.area.Height));
                }
            }


            //Thread.Sleep(1000);

            //drawRooms(roomList, Pens.Blue);
            //roomList.Clear();
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            areas.Add(rooms[i].area.Size.Width * rooms[i].area.Size.Height);
        }

        int min = areas.AsQueryable().Min();
        int max = areas.AsQueryable().Max();

        Console.WriteLine(min + " " + max);

        List<Room> roomsToRemove = new List<Room>();
        for (int i = 0; i < areas.Count - 1; i++)
        {
            if (areas[i] == min || areas[i] == max)
            {

                roomsToRemove.Add(rooms[i]);
                Console.WriteLine("Removing");
            }
        }

        foreach (Room room in roomsToRemove)
        {
            rooms.Remove(room);
        }



        drawRooms(rooms, Pens.Black);
        for (int i = 0; i < rooms.Count; i++)
        {
            roomListDoor.Add(rooms[i]);
        }

        for (int i = 0; i < roomListDoor.Count - 1; i++)
        {
            int collidingWithRooms = 0;
            Room roomToCheck = roomListDoor[i];

            //for (int i = 0; i < rooms.Count; i++)
            //{
            //    roomListDoor2.Add(rooms[i]);
            //}


            for (int j = i + 1; j < roomListDoor.Count; j++)
            {
                Room roomToCompare = roomListDoor[j];

                
                Rectangle rect = new Rectangle(new Point(roomToCheck.area.X, roomToCheck.area.Y), roomToCheck.area.Size);
                if (roomToCheck.area.IntersectsWith(roomToCompare.area))
                {
                    rect.Intersect(roomToCompare.area);

                    //Console.WriteLine(roomToCheck.toString());

                   // Console.WriteLine(roomToCompare.area.Height);

                    if (rect.Height == 1)        //horizontal
                    {
                        int start = rect.X;
                        int end = rect.X + rect.Width;
                        int randomPoint;

                        if (rect.Width > 3)
                        {
                            randomPoint = random.Next(start+1, end-1);
                            doors.Add(new Door(new Point(randomPoint, rect.Y), roomToCheck, roomToCompare));
                           // roomToCheck.doorsInRoom.Add(new Door(new Point(randomPoint, rect.Y)));
                        }
                        //Console.WriteLine("Door spawned");
                    }
                    else if (rect.Width == 1)
                    {
                        int start = rect.Y;
                        int end = rect.Y + rect.Height;
                        int randomPoint;

                        if (rect.Height > 3)
                        {
                            randomPoint = random.Next(start + 1, end - 1);
                            doors.Add(new Door(new Point(rect.X, randomPoint), roomToCheck, roomToCompare));
                           // roomToCheck.doorsInRoom.Add(new Door(new Point(rect.X, randomPoint)));
                        }
                       // Console.WriteLine("Door spawned");
                    }
                }
            }


            //roomListDoor.Remove(roomToCheck);

        }
        drawDoors(doors, Pens.Lime);

        Console.WriteLine("DOOORS");
        foreach (Door door in doors)
        {
            
            Console.WriteLine(door.toString());
        }
        Console.WriteLine("NO DOOORS");
        foreach (Room room in rooms)
        {
            Console.WriteLine(room.toString());

            foreach (Door door in doors)
            {
                room.doorsInRoom.Add(door);
            }
            room.checkDoors();
            foreach (Door door in room.doorsInRoom)
            {
                door.toString();
            }

            drawRoom(room, Pens.Black, room.colourDoors());
        }

    }

    private void SplitVertically(Room uncutRoom, List<Room> roomList, int pMinimumRoomSize)
    {
        //int cut = random.Next(pMinimumRoomSize, uncutRoom.area.Width - pMinimumRoomSize);
        int cut = rand.Next(pMinimumRoomSize, uncutRoom.area.Width - pMinimumRoomSize);
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, cut + 1, uncutRoom.area.Height)));//, cut, false));
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X + cut, uncutRoom.area.Y, uncutRoom.area.Width - cut, uncutRoom.area.Height)));//, cut, false)) ;
    }
    private void SplitHorizontally(Room uncutRoom, List<Room> roomList, int pMinimumRoomSize)
    {
        //int cut = random.Next(pMinimumRoomSize, uncutRoom.area.Height - pMinimumRoomSize);
        int cut = rand.Next(pMinimumRoomSize, uncutRoom.area.Height - pMinimumRoomSize);
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, uncutRoom.area.Width, cut + 1)));//, cut));
        roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y + cut, uncutRoom.area.Width, uncutRoom.area.Height - cut)));//,cut));
    }



}

