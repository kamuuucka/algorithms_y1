using System;
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

    public SufficientDungeonNodeGraph(Dungeon pDungeon) : base((int)(pDungeon.size.Width * pDungeon.scale), (int)(pDungeon.size.Height * pDungeon.scale), (int)pDungeon.scale / 3)
    {
        Debug.Assert(pDungeon != null, "Please pass in a dungeon.");

        _dungeon = pDungeon;
    }

    protected override void generate()
    {
        //Generate nodes, in this sample node graph we just add to nodes manually
        //of course in a REAL nodegraph (read:yours), node placement should 
        //be based on the rooms in the dungeon

        //We assume (bad programming practice 1-o-1) there are two rooms in the given dungeon.
        //The getRoomCenter is a convenience method to calculate the screen space center of a room
        //nodes.Add(new Node(getRoomCenter(_dungeon.rooms[0])));
        //nodes.Add(new Node(getRoomCenter(_dungeon.rooms[1])));
        //The getDoorCenter is a convenience method to calculate the screen space center of a door
        //nodes.Add(new Node(getDoorCenter(_dungeon.doors[0])));

        //create a connection between the two rooms and the door...
        //AddConnection(nodes[0], nodes[2]);
        //AddConnection(nodes[1], nodes[2]);

        //for (int i = 0; i < _dungeon.rooms.Count; i++)
        //      {
        //	//Console.WriteLine("Node created");
        //	nodes.Add(new Node(getRoomCenter(_dungeon.rooms[i])));
        //}

        for (int i = 0; i < _dungeon.doors.Count; i++)
        {
            Console.WriteLine("Door node");
            nodes.Add(new Node(getDoorCenter(_dungeon.doors[i])));
            Console.WriteLine("Room nodes");
            nodes.Add(new Node(getRoomCenter(_dungeon.doors[i].roomA)));
            nodes.Add(new Node(getRoomCenter(_dungeon.doors[i].roomB)));

            numberOfDoors++;
        }

        //foreach (Door door in _dungeon.doors)
        //{
        //    nodes.Add(new Node(getRoomCenter(door.roomA)));
        //    nodes.Add(new Node(getRoomCenter(door.roomB)));
        //}

        foreach (Node node in nodes)
        {

            Console.WriteLine(node);
            Console.WriteLine(node.location);
        }

        //for (int i = 0; i < nodes.Count; i++)
        //{
        //    Node node1 = nodes[i];
        //    for (int j = i + 1; j < nodes.Count - 1; j++)
        //    {
        //        Node node2 = nodes[j];

        //        if (node1.location.X == node2.location.X && node1.location.Y == node2.location.Y)
        //        {
        //            nodes.Remove(node2);
        //        }
        //    }
        //}

        //Console.WriteLine("Cleaning the list");
        //foreach (Node node in nodes)
        //{
        //    Console.WriteLine(node);
        //    Console.WriteLine(node.location);
        //}

        for (int i = 0; i < _dungeon.doors.Count; i++)
        {
            //Console.WriteLine("door" + i);
            //Console.WriteLine(i * 3);
            //Console.WriteLine(i * 3+1);
            //Console.WriteLine(i * 3+2);
            AddConnection(nodes[i * 3], nodes[i * 3 + 1]);
            AddConnection(nodes[i * 3], nodes[i * 3 + 2]);
        }

        //AddConnection(nodes[0], nodes[1]);
        //AddConnection(nodes[0], nodes[2]);
        //AddConnection(nodes[3], nodes[4]);
        //AddConnection(nodes[3], nodes[5]);
        //AddConnection(nodes[6], nodes[7]);
        //AddConnection(nodes[6], nodes[8]);




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
