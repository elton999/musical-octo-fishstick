using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Gameplay;

namespace Project.Pathing
{
    public class PathReader
    {
        public void PathUpdate(IPathble pathble)
        {
            pathble.Paths.Clear();

            var firstPath = new List<NodePosition>();
            pathble.Paths.Add(firstPath);

            var newNodePathing = new NodePathing()
            {
                Pathble = pathble,
                Path = firstPath,
                Node = pathble.StartNode,
                LastNode = null
            };

            GetPosition(newNodePathing);
            var allValidPaths = new List<List<NodePosition>>();

            foreach (var path in pathble.Paths)
            {
                if (path.Count > 1 && path[path.Count - 1] == pathble.Target)
                    allValidPaths.Add(path);
                else if (path.Count > 0 && path[0] == pathble.Target)
                    allValidPaths.Add(path);
            }

            List<float> distances = new List<float>();
            for (int i = 0; i < allValidPaths.Count; i++)
            {
                distances.Add(0);
                for (int j = 0; j < allValidPaths[i].Count - 1; j++)
                    distances[i] += Vector2.Distance(allValidPaths[i][j + 1].Position, allValidPaths[i][j].Position);
            }

            float distance = float.PositiveInfinity;
            for (int i = 0; i < distances.Count; i++)
            {
                if (distance > distances[i])
                {
                    distance = distances[i];
                    pathble.Path = allValidPaths[i];
                }
            }
        }

        public void GetPosition(NodePathing nodePathing)
        {
            if (nodePathing.IsTheLastNode(nodePathing.Node.LeftPosition))
                SetNewPath(nodePathing, nodePathing.Node.LeftPosition);

            if (nodePathing.IsTheLastNode(nodePathing.Node.RightPosition))
                SetNewPath(nodePathing, nodePathing.Node.RightPosition);

            if (nodePathing.IsTheLastNode(nodePathing.Node.UpPosition))
                SetNewPath(nodePathing, nodePathing.Node.UpPosition);

            if (nodePathing.IsTheLastNode(nodePathing.Node.DownPosition))
                SetNewPath(nodePathing, nodePathing.Node.DownPosition);
        }

        public void SetNewPath(NodePathing nodePathing, NodePosition nodePosition)
        {
            var newNodePathing = new NodePathing()
            {
                Pathble = nodePathing.Pathble,
                Path = nodePathing.Path,
                Node = nodePosition,
                LastNode = nodePathing.Node
            };
            SetNewPath(newNodePathing);
        }

        public void SetNewPath(NodePathing nodePathing)
        {
            if (nodePathing.Path.Contains(nodePathing.Node)) return;

            var newPath = new List<NodePosition>();
            newPath.AddRange(nodePathing.Path);
            nodePathing.Pathble.Paths.Add(newPath);

            newPath.Add(nodePathing.Node);
            if (nodePathing.IsTargetNode() || nodePathing.IsStartNode()) return;

            var newNodePathing = new NodePathing()
            {
                Pathble = nodePathing.Pathble,
                Node = nodePathing.Node,
                LastNode = nodePathing.LastNode,
                Path = newPath
            };

            GetPosition(newNodePathing);
        }
    }
}