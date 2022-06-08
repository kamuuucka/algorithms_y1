using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

/**
 * An example of a dungeon nodegraph implementation.
 * 
 * This implementation places only three nodes and only works with the SampleDungeon.
 * Your implementation has to do better :).
 * 
 * It is recommended to subclass this class instead of NodeGraph so that you already 
 * have access to the helper methods such as getRoomCenter etc.
 * 
 * TODO:
 * - Create a subclass of this class, and override the generate method, see the generate method below for an example.
 */
class SufficientDungeonNodeGraph : NodeGraph
{
    protected Dungeon _dungeon;
    private int numberOfDoors = 0;
    private Dictionary<Room, Node> roomsNodes = new Dictionary<Room, Node>();

    public SufficientDungeonNodeGraph(Dungeon pDungeon) : base((int)(pDungeon.size.Width * pDungeon.scale), (int)(pDungeon.size.Height * pDungeon.scale), (int)pDungeon.scale / 3)
    {
        Debug.Assert(pDungeon != null, "Please pass in a dungeon.");

        _dungeon = pDungeon;
    }

    protected override void generate()
    {

        for (int i = 0; i < _dungeon.rooms.Count; i++)
        {
            Node roomNode = new Node(getRoomCenter(_dungeon.rooms[i]));
            nodes.Add(roomNode);
            roomsNodes.Add(_dungeon.rooms[i], roomNode);
        }

        for (int i = 0; i < _dungeon.doors.Count; i++)
        {
            Console.WriteLine("Door node");
            Node doorNode = new Node(getDoorCenter(_dungeon.doors[i]));
            nodes.Add(doorNode);
            
            if (roomsNodes.ContainsKey(_dungeon.doors[i].roomA) && _dungeon.doors[i].roomA != null)
            {
                AddConnection(doorNode, roomsNodes[_dungeon.doors[i].roomA]);
            }
            if (roomsNodes.ContainsKey(_dungeon.doors[i].roomB) && _dungeon.doors[i].roomB != null)
            {
                AddConnection(doorNode, roomsNodes[_dungeon.doors[i].roomB]);
            }
           
        }

        foreach (Node node in nodes)
        {

            Console.WriteLine(node);
            Console.WriteLine(node.location);
        }
    }

    /**
	 * A helper method for your convenience so you don't have to meddle with coordinate transformations.
	 * @return the location of the center of the given room you can use for your nodes in this class
	 */
    protected Point getRoomCenter(Room pRoom)
    {
        float centerX = ((pRoom.area.Left + pRoom.area.Right) / 2.0f) * _dungeon.scale;
        float centerY = ((pRoom.area.Top + pRoom.area.Bottom) / 2.0f) * _dungeon.scale;
        return new Point((int)centerX, (int)centerY);
    }

    /**
	 * A helper method for your convenience so you don't have to meddle with coordinate transformations.
	 * @return the location of the center of the given door you can use for your nodes in this class
	 */
    protected Point getDoorCenter(Door pDoor)
    {
        return getPointCenter(pDoor.location);
    }

    /**
	 * A helper method for your convenience so you don't have to meddle with coordinate transformations.
	 * @return the location of the center of the given point you can use for your nodes in this class
	 */
    protected Point getPointCenter(Point pLocation)
    {
        float centerX = (pLocation.X + 0.5f) * _dungeon.scale;
        float centerY = (pLocation.Y + 0.5f) * _dungeon.scale;
        return new Point((int)centerX, (int)centerY);
    }

}
