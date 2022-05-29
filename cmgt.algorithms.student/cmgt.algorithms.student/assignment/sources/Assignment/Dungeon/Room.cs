using System.Drawing;

/**
 * This class represents (the data for) a Room, at this moment only a rectangle in the dungeon.
 */
class Room
{
	public Rectangle area;
	public int direction;

	public Room (Rectangle pArea, int pDirection = 0)
	{
		area = pArea;
		direction = pDirection;

	}

	//TODO: Implement a toString method for debugging?
	//Return information about the type of object and it's data
	//eg Room: (x, y, width, height)

	public string toString()
    {
		return "X: " + area.X + " Y: " + area.Y + " W: " + area.Width + " H: " + area.Height + " D:" + direction;
    }

}
