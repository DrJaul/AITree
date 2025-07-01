using System.Numerics;

namespace AITree.Core
{
    public class StemNode : BaseGrowable
    {
        public StemNode(Vector3 position)
            : base(position, NodeType.Stem)
        {
            GrowthCost = 2;
            EnergyIn = 1;
            EnergyOut = 0;
        }
    }
}
