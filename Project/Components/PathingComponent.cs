using System;
using System.Collections.Generic;
using Project.Pathing;
using Project.Gameplay;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework;

namespace Project.Components
{
    public class PathingComponent : Component, IPathble
    {
        private MovementComponent _movementComponent;
        private LadderComponent _ladderComponent;
        private JumpAnimation _jumpAnimation;

        public NodePosition Target { get; set; }
        public NodePosition StartNode { get; set; }
        public List<List<NodePosition>> Paths { get; set; }
        public List<NodePosition> Path { get; set; }

        public PathReader PathReader;

        public override void Start()
        {
            _movementComponent = GameObject.GetComponent<MovementComponent>();
            _ladderComponent = GameObject.GetComponent<LadderComponent>();
            _jumpAnimation = GameObject.GetComponent<JumpAnimation>();
            PathReader = new PathReader();
            Paths = new List<List<NodePosition>>();
        }

        public override void UpdateData(GameTime gameTime)
        {
            float targetDistance = float.PositiveInfinity;
            float startDistance = float.PositiveInfinity;

            var playerPosition = GameObject.Scene.AllActors[0].Position;
            var currentPosition = GameObject.Position;

            for (int i = 0; i < Nodes.NodesPosition.Count; i++)
            {
                Vector2 nodePosition = Nodes.NodesPosition[i].Position;
                float distance = Vector2.Distance(playerPosition, nodePosition);
                float currentDistance = Vector2.Distance(currentPosition, nodePosition);

                if (targetDistance > distance)
                {
                    Target = Nodes.NodesPosition[i];
                    targetDistance = distance;
                }

                if (startDistance > currentDistance)
                {
                    StartNode = Nodes.NodesPosition[i];
                    startDistance = currentDistance;
                }
            }

            PathReader.PathUpdate(this);
            SetDirectionByPath();

            base.UpdateData(gameTime);
        }

        private void SetDirectionByPath()
        {
            if (Path.Count <= 1 || Path[0].Position.Length() <= 0) return;

            var direction = Path[0].Position - GameObject.Position;

            if (Path.Count > 2)
            {
                float characterFirstNodeDotX = Vector2.Dot((GameObject.Position - Path[0].Position) * Vector2.UnitX, Vector2.UnitX);
                characterFirstNodeDotX = MathF.Sign(characterFirstNodeDotX);

                float firstNodeSecondNodeDotX = Vector2.Dot((Path[0].Position - Path[1].Position) * Vector2.UnitX, Vector2.UnitX);
                firstNodeSecondNodeDotX = MathF.Sign(firstNodeSecondNodeDotX);

                float characterFirstNodeDotY = Vector2.Dot((GameObject.Position - Path[0].Position) * Vector2.UnitY, Vector2.UnitY);
                characterFirstNodeDotY = MathF.Sign(characterFirstNodeDotY);

                float firstNodeSecondNodeDotY = Vector2.Dot((Path[0].Position - Path[1].Position) * Vector2.UnitY, Vector2.UnitY);
                firstNodeSecondNodeDotY = MathF.Sign(firstNodeSecondNodeDotY);

                if (characterFirstNodeDotX == 0 || firstNodeSecondNodeDotX != characterFirstNodeDotX)
                    if (characterFirstNodeDotY == 1 || characterFirstNodeDotY == 0)
                        direction = Path[1].Position - GameObject.Position;

                if (firstNodeSecondNodeDotY != characterFirstNodeDotY)
                    if (characterFirstNodeDotX == 1 || characterFirstNodeDotX == 0)
                        direction = Path[1].Position - GameObject.Position;
            }

            direction.Normalize();
            _movementComponent.AddDirection(direction);
        }
    }
}
