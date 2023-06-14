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

        public NodePosition Target { get; set; }
        public NodePosition StartNode { get; set; }
        public List<List<NodePosition>> Paths { get; set; }
        public List<NodePosition> Path { get; set; }

        public PathReader PathReader;

        public override void Start()
        {
            _movementComponent = GameObject.GetComponent<MovementComponent>();
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

            if (Path.Count > 0 && Path[0].Position.Length() > 0)
            {
                var direction = Path[0].Position - GameObject.Position;
                direction = direction / direction.Length();
                direction.X = float.IsNaN(direction.X) ? 0 : direction.X;
                direction.Y = float.IsNaN(direction.Y) ? 0 : direction.Y;
                _movementComponent.AddDirection(direction);
            }

            base.UpdateData(gameTime);
        }

    }
}
