using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class BreadthFirstPathFinder : PathFinder
{
    List<Node> listNode;
    List<Node> visited;
    List<Node> path;
    NodeGraph graph;
    Dictionary<Node, Node> visitedMap;
    public BreadthFirstPathFinder(NodeGraph pGraph) : base(pGraph)
    {
        graph = pGraph;
        
    }

    protected override List<Node> generate(Node pFrom, Node pTo)
    {
        visitedMap = new Dictionary<Node, Node>();
        path = new List<Node>();
        visited = new List<Node>();
        listNode = new List<Node>();
        path.Add(pFrom);
        visitedMap.Add(pFrom, null);
        while (path.Count > 0)
        {
            foreach(Node n in path)
            {
                Console.WriteLine(n);
            }
            Node node = path[0];
            path.Remove(node);

            Console.WriteLine("Working on node: " + node);

            if (node.Equals(pTo))
            {
                Console.WriteLine("End node found");
                while (visitedMap[node] != null)
                {
                    listNode.Add(node);

                    node = visitedMap[node];
                }

                listNode.Add(node);
                listNode.Reverse();
                return listNode;
            }

            visited.Add(node);

            for (int i = 0; i < node.connections.Count; i++) {
                Console.WriteLine(node.connections);
                if (!visited.Contains(node.connections[i]) && !path.Contains(node.connections[i]))
                {
                    path.Add(node.connections[i]);
                    visitedMap.Add(node.connections[i], node);
                    Console.WriteLine("Adding the neighbour node");
                    Console.WriteLine(node.connections[i]);
                }
                
            }


        }
    

        return listNode;
    }
}

