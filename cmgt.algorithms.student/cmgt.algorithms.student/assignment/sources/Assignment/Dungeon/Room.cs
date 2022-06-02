using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

/**
 * This class represents (the data for) a Room, at this moment only a rectangle in the dungeon.
 */
class Room
{
	public Rectangle area;
	public List<Door> doorsInRoom = new List<Door>();
	private int doorsConnected = 0;
    

	public Room (Rectangle pArea)
	{
		area = pArea;
	}

	//TODO: Implement a toString method for debugging?
	//Return information about the type of object and it's data
	//eg Room: (x, y, width, height)

	public string toString()
    {
		return "X: " + area.X + " Y: " + area.Y + " W: " + area.Width + " H: " + area.Height;
    }

	public void checkDoors()
    {
		Console.WriteLine("ROOM : " + toString());

        doorsInRoom = doorsInRoom.Distinct().ToList();

        while (doorsInRoom.Count != 0)
        {
			Door doorToCheck = doorsInRoom[doorsInRoom.Count - 1];
			doorsInRoom.Remove(doorToCheck);

			Console.WriteLine(doorToCheck.toString());

            

            if (doorToCheck.location.X >= area.X
                && doorToCheck.location.X <= area.X + area.Width -1
                && doorToCheck.location.Y == area.Y)
            {
                Console.WriteLine("Door is on the top");
                doorsConnected++;
            }

            if (doorToCheck.location.X >= area.X
                && doorToCheck.location.X <= area.X + area.Width -1
                && doorToCheck.location.Y == area.Y + area.Height - 1)
            {
                Console.WriteLine("Door is in the bottom");
                doorsConnected++;
            }

            if (doorToCheck.location.Y >= area.Y
                && doorToCheck.location.Y <= area.Y + area.Height - 1
                && doorToCheck.location.X == area.X)
            {
                Console.WriteLine("Door is on the left");
                doorsConnected++;
            }

            if (doorToCheck.location.Y >= area.Y
                && doorToCheck.location.Y <= area.Y + area.Height - 1
                && doorToCheck.location.X == area.X + area.Width -1)
            {
             Console.WriteLine("Door is on the right");
                doorsConnected++;
            }
        }

		Console.WriteLine("Total number of doors: " + doorsConnected);
    }

    public Brush colourDoors()
    {
        Brush brush = null;
        switch (doorsConnected)
        {
            case 0:
                //red
                brush = new SolidBrush(Color.Red);
                break;
            case 1:
                //orange
                brush = new SolidBrush(Color.Orange);
                break;
            case 2:
                //yellow
                brush = new SolidBrush(Color.Yellow);
                break;
            //case 3:
            //    //green
            //    brush = new SolidBrush(Color.Green);
            //    break;
            default:
                //green;
                brush = new SolidBrush(Color.Green);
                break;
        }

        return brush;
    }

	

}
