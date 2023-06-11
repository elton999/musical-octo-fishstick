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
                float distance = Vector2.Distance(playerPosition, Nodes.NodesPosition[i].Position);

                float currentDistance = Vector2.Distance(currentPosition, Nodes.NodesPosition[i].Position);

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

            if (Path.Count > 0)
                _movementComponent.AddDirection(Vector2.Normalize(Path[0].Position - GameObject.Position));
            else
                _movementComponent.AddDirection(Vector2.Normalize(playerPosition - GameObject.Position));

            base.UpdateData(gameTime);
        }

    }
}
