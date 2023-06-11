using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit;

namespace Project.Gameplay
{
    public class Nodes : GameObject
    {
        public static List<NodePosition> NodesPosition;

        public override void Start() => SetupNodes();

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

        public static void SetNodeSettings(NodePosition lastNode, Vector2 dotVector, NodePosition currentNode)
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

        public static Vector2 GetDot(NodePosition lastNode, Vector2 nodePosition)
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
    }
}