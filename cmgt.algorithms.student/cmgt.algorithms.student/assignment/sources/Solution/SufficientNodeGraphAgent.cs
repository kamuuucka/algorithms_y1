using GXPEngine;
using System.Collections.Generic;

/**
 * Very simple example of a nodegraphagent that walks directly to the node you clicked on,
 * ignoring walls, connections etc.
 */
class OnGraphWayPointAgent : NodeGraphAgent
{
	//Current target to move towards
	private Node _target = null;
	private List<Node> targetList = new List<Node>();
	int i = 0;

	public OnGraphWayPointAgent(NodeGraph pNodeGraph) : base(pNodeGraph)
	{
		SetOrigin(width / 2, height / 2);

		//position ourselves on a random node
		if (pNodeGraph.nodes.Count > 0)
		{
			jumpToNode(pNodeGraph.nodes[Utils.Random(0, pNodeGraph.nodes.Count)]);
		}

		//listen to nodeclicks
		pNodeGraph.OnNodeLeftClicked += onNodeClickHandler;
	}

	protected virtual void onNodeClickHandler(Node pNode)
	{
		//_target = pNode;
		targetList.Add(pNode);
	}

	protected override void Update()
	{
		
		if (targetList.Count > 0)
        {
			MoveToTarget(targetList[i]);
		}
		

        ////no target? Don't walk
        //if (_target == null) return;

        ////Move towards the target node, if we reached it, clear the target
        //if (moveTowardsNode(_target))
        //{
        //	_target = null;
        //}
    }

	private void MoveToTarget(Node target)
    {

		if (target == null)
		{
			return;
			
		}


        if (moveTowardsNode(target))
        {
			i++;
        }
    }
}
