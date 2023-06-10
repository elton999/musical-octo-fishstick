using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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

        public override void Start()
        {
            _square = new Square();
            _square.SquareColor = Color.Blue;
            _square.Scene = Scene;
            _square.size = new Point(8, 8);
            _square.Origin = new Vector2(4, 4);
            _square.Start();

            SetNodePositions();
            CurrentNode = NodesPosition[0];
        }

        public void SetNodePositions()
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

                NodesPosition.Add(currentNode);
                lastNode = currentNode;
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

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                if (CurrentNode.UpPosition != null)
                    CurrentNode = CurrentNode.UpPosition;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                if (CurrentNode.DownPosition != null)
                    CurrentNode = CurrentNode.DownPosition;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                if (CurrentNode.LeftPosition != null)
                    CurrentNode = CurrentNode.LeftPosition;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                if (CurrentNode.RightPosition != null)
                    CurrentNode = CurrentNode.RightPosition;


            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _square.BeginDraw(spriteBatch);
            _square.Position = CurrentNode.Position;
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