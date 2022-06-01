using System.Collections.Generic;
using System.Drawing;

/**
 * This class represents (the data for) a Room, at this moment only a rectangle in the dungeon.
 */
class Room
{
	public Rectangle area;
	public List<Door> doors = new List<Door>();

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

}
