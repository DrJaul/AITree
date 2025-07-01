using System.Numerics;

namespace AITree.Core
{
    public abstract class BaseGrowable : IGrowable
    {
        public Vector3 Position { get; protected set; }
        public NodeType Type { get; protected set; }

        public virtual int GrowthCost { get; protected set; }
        public virtual int EnergyIn { get; protected set; }
        public virtual int EnergyOut { get; protected set; }

        protected BaseGrowable(Vector3 position, NodeType type)
        {
            Position = position;
            Type = type;
        }
    }
}
