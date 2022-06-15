using GXPEngine;
using System;
using System.Collections.Generic;

/**
 * An example of a PathFinder implementation which completes the process by rolling a die 
 * and just returning the straight-as-the-crow-flies path if you roll a 6 ;). 
 */
class RecursivePathFinder : PathFinder
{

	public RecursivePathFinder(NodeGraph pGraph) : base(pGraph) { }

	protected override List<Node> generate(Node pFrom, Node pTo)
	{
		
			return new List<Node>() { pFrom, pTo };
		
		
	}

}

