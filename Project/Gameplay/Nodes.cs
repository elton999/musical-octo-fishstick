using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using Project.Pathing;

namespace Project.Gameplay
{
    public class Nodes : GameObject, IPathble
    {
        private Square _square;

        public List<NodePosition> NodesPosition;
        public NodePosition Target { get; set; }
        public NodePosition StartNode { get; set; }
        public List<List<NodePosition>> Paths { get; set; }
        public List<NodePosition> Path { get; set; }

        public PathReader PathReader;

        public override void Start()
        {
            _square = new Square();
            _square.SquareColor = Color.White;
            _square.Scene = Scene;
            _square.size = new Point(8, 8);
            _square.Origin = new Vector2(4, 4);
            _square.Start();

            SetupNodes();
            Target = NodesPosition[5];
            StartNode = NodesPosition[0];
            PathReader = new PathReader();

            PathReader.PathUpdate(this);
        }

        public void SetupNodes()
        {
            Paths = new List<List<NodePosition>>();
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

        public override void UpdateData(GameTime gameTime)
        {
            float targetDistance = float.PositiveInfinity;

            for (int i = 0; i < NodesPosition.Count; i++)
            {
                var playerPosition = Scene.AllActors[0].Position;
                float distance = Vector2.Distance(playerPosition, NodesPosition[i].Position);
                if (targetDistance > distance)
                {
                    Target = NodesPosition[i];
                    targetDistance = distance;
                }
            }

            PathReader.PathUpdate(this);
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
}