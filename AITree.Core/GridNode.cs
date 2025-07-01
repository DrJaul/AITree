using System.Numerics;

namespace AITree.Core
{
    public struct GridNode
    {
        public Vector3 Position;
        public bool IsOccupied;
        public IGrowable? Growable;
    }
    
}
