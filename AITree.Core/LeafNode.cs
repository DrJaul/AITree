using System.Numerics;

namespace AITree.Core
{
    public class LeafNode : BaseGrowable
    {
        public LeafNode(Vector3 position)
            : base(position, NodeType.Leaf)
        {
            GrowthCost = 1;
            EnergyIn = 3;
            EnergyOut = 0;
        }
    }
}
