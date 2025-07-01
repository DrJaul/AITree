using System.Numerics;

namespace AITree.Core
{
    public interface ITreeAI
    {
        /// <summary>
        /// Attempts to grow the tree and returns the position of the new node, or null if no growth is possible.
        /// </summary>
        Vector3? Grow();

        /// <summary>
        /// Width of the tree grid.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Height of the tree grid.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Depth of the tree grid.
        /// </summary>
        int Depth { get; }
    }
}
