using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace Project.Gameplay
{
    public class Nodes : GameObject
    {
        private Square _square;

        public List<NodePosition> NodesPosition;
        public NodePosition CurrentNode;
        public NodePosition Target;
        public NodePosition StartNode;
        public List<List<NodePosition>> Paths = new List<List<NodePosition>>();
        public List<NodePosition> Path;

        public override void Start()
        {
            _square = new Square();
            _square.SquareColor = Color.White;
            _square.Scene = Scene;
            _square.size = new Point(8, 8);
            _square.Origin = new Vector2(4, 4);
            _square.Start();

            SetupNodes();
            SetPath();
            CurrentNode = NodesPosition[0];
        }

        public void SetupNodes()
        {
            NodesPosition = new List<NodePosition>();

            var lastNode = new NodePosition();
            lastNode.Position = Position;
            NodesPosition.Add(lastNode);

            foreach (var nodePosition in Nodes)
            {
                Vector2 dotVector = GetDot(lastNode, nodePosition);

                NodePosition currentNode = new NodePosition();
                foreach (var createdNode in NodesPosition)
                    if (createdNode.Position == nodePosition)
                        currentNode = createdNode;

                currentNode.Position = nodePosition;
                SetNodeSettings(lastNode, dotVector, currentNode);

                NodesPosition.Add(currentNode);
                lastNode = currentNode;
            }
        }

        public void SetPath()
        {
            Target = NodesPosition[3];
            StartNode = NodesPosition[0];

            var firstPath = new List<NodePosition>();
            Paths.Add(firstPath);

            GetPosition(StartNode, null, firstPath);
            var allValidPaths = new List<List<NodePosition>>();

            foreach (var path in Paths)
            {
                if (path.Count > 1 && path[path.Count - 1] == Target)
                    allValidPaths.Add(path);
                else if (path.Count > 0 && path[0] == Target)
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
                    Path = allValidPaths[i];
                }

            }
        }

        public void GetPosition(NodePosition node, NodePosition lastNode, List<NodePosition> path)
        {
            if (node.LeftPosition != null && lastNode != node.LeftPosition)
                SetNewPath(node.LeftPosition, node, path);

            if (node.RightPosition != null && lastNode != node.RightPosition)
                SetNewPath(node.RightPosition, node, path);

            if (node.UpPosition != null && lastNode != node.UpPosition)
                SetNewPath(node.UpPosition, node, path);

            if (node.DownPosition != null && lastNode != node.DownPosition)
                SetNewPath(node.DownPosition, node, path);
        }

        private void SetNewPath(NodePosition node, NodePosition lastNode, List<NodePosition> path)
        {
            if (path.Contains(node)) return;

            var newPath = new List<NodePosition>();
            newPath.AddRange(path);
            Paths.Add(newPath);

            newPath.Add(node);
            if (node.Position != Target.Position && node.Position != StartNode.Position)
                GetPosition(node, lastNode, newPath);

        }

        private static void SetNodeSettings(NodePosition lastNode, Vector2 dotVector, NodePosition currentNode)
        {
            if (dotVector.X < 0)
            {
                currentNode.LeftPosition = lastNode;
                lastNode.RightPosition = currentNode;
            }
            if (dotVector.X > 0)
            {
                currentNode.RightPosition = lastNode;
                lastNode.LeftPosition = currentNode;
            }

            if (dotVector.Y < 0)
            {
                currentNode.UpPosition = lastNode;
                lastNode.DownPosition = currentNode;
            }
            if (dotVector.Y > 0)
            {
                currentNode.DownPosition = lastNode;
                lastNode.UpPosition = currentNode;
            }
        }

        private static Vector2 GetDot(NodePosition lastNode, Vector2 nodePosition)
        {
            float horizontal, vertical;

            Vector2 toPositionHorizontal = lastNode.Position * Vector2.UnitX - nodePosition * Vector2.UnitX;
            Vector2 toPositionVertical = lastNode.Position * Vector2.UnitY - nodePosition * Vector2.UnitY;

            horizontal = Vector2.Dot(Vector2.UnitX, toPositionHorizontal);
            horizontal = MathF.Sign(horizontal);
            vertical = Vector2.Dot(Vector2.UnitY, toPositionVertical);
            vertical = MathF.Sign(vertical);

            return new Vector2(horizontal, vertical);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _square.BeginDraw(spriteBatch);
            foreach (var node in Path)
            {
                _square.SpriteColor = Color.Aqua;
                _square.Position = node.Position;
                _square.DrawSprite(spriteBatch);
            }

            _square.SpriteColor = Color.White;
            _square.Position = Target.Position;
            _square.DrawSprite(spriteBatch);

            _square.EndDraw(spriteBatch);
        }
    }

    public class NodePosition
    {
        public Vector2 Position;

        public NodePosition UpPosition;
        public NodePosition DownPosition;

        public NodePosition LeftPosition;
        public NodePosition RightPosition;
    }
}