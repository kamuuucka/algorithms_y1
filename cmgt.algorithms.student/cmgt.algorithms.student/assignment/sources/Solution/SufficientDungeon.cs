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
			Room uncutRoom = roomList[roomList.Count - 1];
			roomList.Remove(uncutRoom);
			
			if (uncutRoom.area.Width/2 > pMinimumRoomSize)
            {
				if (uncutRoom.area.Height/2 > pMinimumRoomSize)
                {
					bool horiznotal = uncutRoom.area.Width < uncutRoom.area.Height;
					
					if (horiznotal)
                    {
						//toproom
						roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, uncutRoom.area.Width, uncutRoom.area.Height/2)));
						//bottomroom
						roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y + uncutRoom.area.Height / 2, uncutRoom.area.Width, uncutRoom.area.Height/2)));
					}
                    else
                    {
						//leftroom
						roomList.Add(new Room(new Rectangle(uncutRoom.area.X, uncutRoom.area.Y, uncutRoom.area.Width / 2, uncutRoom.area.Height)));
						//right one
						roomList.Add(new Room(new Rectangle(uncutRoom.area.X+uncutRoom.area.Width/2, uncutRoom.area.Y, uncutRoom.area.Width / 2, uncutRoom.area.Height)));
					}
				}
				
			}
			else
            {
				//rooms.Add(uncutRoom);
				//roomList.Clear();
            }
            

			Thread.Sleep(1000);

			drawRooms(roomList,Pens.Blue);
			//roomList.Clear();
		}

		//drawRooms(rooms, Pens.Green);

	}
}

