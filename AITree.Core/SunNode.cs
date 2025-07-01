using System.Numerics;

namespace AITree.Core
{
    public class SunNode
    {
        public Vector3 Position { get; }
        public int Intensity { get; }

        public SunNode(Vector3 position, int intensity)
        {
            Position = position;
            Intensity = intensity;
        }
    }
}
