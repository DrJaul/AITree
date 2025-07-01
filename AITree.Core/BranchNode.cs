using System.Numerics;

namespace AITree.Core
{
    public class BranchNode : BaseGrowable
    {
        public BranchNode(Vector3 position)
            : base(position, NodeType.Branch)
        {
            GrowthCost = 1;
            EnergyIn = 0;
            EnergyOut = 2;
        }
    }
}
