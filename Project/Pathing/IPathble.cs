using Project.Gameplay;
using System.Collections.Generic;

namespace Project.Pathing
{
    public interface IPathble
    {
        NodePosition Target { get; set; }
        NodePosition StartNode { get; set; }
        List<List<NodePosition>> Paths { get; set; }
        List<NodePosition> Path { get; set; }
    }
}
