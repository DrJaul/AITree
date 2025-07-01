using System.Numerics;

namespace AITree.Core
{
    public enum NodeType
    {
        Branch,
        Stem,
        Leaf
    }

    public interface IGrowable
    {
        Vector3 Position { get; }
        NodeType Type { get; }

        int GrowthCost { get; }
        int EnergyIn { get; }
        int EnergyOut { get; }
    }
}
