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

        public override void Start()
        {
            _square = new Square();
            _square.SquareColor = Color.Blue;
            _square.Scene = Scene;
            _square.size = new Point(8, 8);
            _square.Origin = new Vector2(4, 4);
            _square.Start();

            SetNodePositions();
        }

        public void SetNodePositions()
        {
            NodesPosition = new List<NodePosition>();

            var lastPosition = Position;
            foreach (var nodePosition in Nodes)
            {
                Vector2 toPositionHorizontal = lastPosition * Vector2.UnitX - nodePosition * Vector2.UnitX;
                Vector2 toPositionVertical = lastPosition * Vector2.UnitY - nodePosition * Vector2.UnitY;

                float horizontal = Vector2.Dot(Vector2.UnitX, toPositionHorizontal);
                horizontal = MathF.Sign(horizontal);
                float vertical = Vector2.Dot(Vector2.UnitY, toPositionVertical);
                vertical = MathF.Sign(vertical);

                lastPosition = nodePosition;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _square.BeginDraw(spriteBatch);
            foreach (var nodePosition in Nodes)
            {
                _square.Position = nodePosition;
                _square.DrawSprite(spriteBatch);
            }
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