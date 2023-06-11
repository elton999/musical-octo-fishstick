using Project.Gameplay;
using System.Collections.Generic;

namespace Project.Pathing
{
    public class NodePathing
    {
        public NodePosition Node;
        public NodePosition LastNode;
        public List<NodePosition> Path;

        public IPathble Pathble { get; set; }

        public bool IsTargetNode() => Node.Position == Pathble.Target.Position;
        public bool IsStartNode() => Node.Position == Pathble.StartNode.Position;

        public bool IsTheLastNode(NodePosition node) => node != null && LastNode != node;
    }
}

