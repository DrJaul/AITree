using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AITree.Core
{
    public class Tree
    {
        public Dictionary<Vector3, GridNode> Grid { get; } = new();
        public List<Vector3> ActiveBranches { get; private set; } = new();
        public List<Vector3> ActiveStems { get; private set; } = new();
        public List<Vector3> ActiveLeaves { get; private set; } = new();
        public SunNode Sun { get; private set; }

        public int Width { get; }
        public int Height { get; }
        public int Depth { get; }

        public float TreeHeight { get; private set; } = 0f;

        public int CurrentEnergy { get; set; }
        public int EnergyIn { get; set; }
        public int EnergyOut { get; set; }

        private readonly int _initialEnergy;
        private readonly Random _random = new();

        public Tree(int width, int height, int depth, int initialCurrentEnergy)
        {
            Width = width;
            Height = height;
            Depth = depth;
            CurrentEnergy = initialCurrentEnergy;
            _initialEnergy = initialCurrentEnergy;

            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            for (int z = 0; z < depth; z++)
            {
                var pos = new Vector3(x, y, z);
                Grid[pos] = new GridNode { Position = pos };
            }

            CreateSunNode();
        }

        private void CreateSunNode()
        {
            int x = _random.Next(Width);
            int y = _random.Next(Height);
            int z = _random.Next(1, Depth); // z=0 is not allowed

            if (_random.Next(3) == 0) x = (x < Width / 2) ? 0 : Width - 1;
            else if (_random.Next(2) == 0) y = (y < Height / 2) ? 0 : Height - 1;
            else z = (z < Depth / 2) ? 0 : Depth - 1;

            Sun = new SunNode(new Vector3(x, y, z), intensity: 100);
        }

        public void AddNode(IGrowable node)
        {
            if (Grid.TryGetValue(node.Position, out var gridNode))
            {
                gridNode.IsOccupied = true;
                gridNode.Growable = node;
                Grid[node.Position] = gridNode;

                EnergyIn += node.EnergyIn;
                EnergyOut += node.EnergyOut;
                CurrentEnergy += node.EnergyIn - node.EnergyOut;
                UpdateGrowthTargets(node);

                if (node.Position.Z > TreeHeight)
                    TreeHeight = node.Position.Z;
            }
        }

        public void ResetEnergy()
        {
            EnergyIn = 0;
            EnergyOut = 0;
            CurrentEnergy = _initialEnergy;
        }

        public IEnumerable<Vector3> GetAllAdjacent(Vector3 pos)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                if (dx == 0 ) continue;
                var neighbor = new Vector3(pos.X + dx, pos.Y , pos.Z );
                if (Grid.ContainsKey(neighbor))
                    yield return neighbor;
            }
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dy == 0) continue;
                var neighbor = new Vector3(pos.X, pos.Y + dy, pos.Z );
                if (Grid.ContainsKey(neighbor))
                    yield return neighbor;
            }
            for (int dz = -1; dz <= 1; dz++)
                {
                    if (dz == 0) continue;
                    var neighbor = new Vector3(pos.X , pos.Y, pos.Z + dz);
                    if (Grid.ContainsKey(neighbor))
                        yield return neighbor;
                }
        }

        private void UpdateGrowthTargets(IGrowable justGrown)
        {
            var candidates = GetAllAdjacent(justGrown.Position)
                .Where(p => !Grid[p].IsOccupied)
                .ToList();

            foreach (var pos in candidates)
            {
                if (IsValidBranchGrowth(pos)) ActiveBranches.Add(pos);
                if (IsValidStemGrowth(pos)) ActiveStems.Add(pos);
                if (IsValidLeafGrowth(pos)) ActiveLeaves.Add(pos);
            }

            ActiveBranches = ActiveBranches.Distinct().ToList();
            ActiveStems = ActiveStems.Distinct().ToList();
            ActiveLeaves = ActiveLeaves.Distinct().ToList();
        }

        public bool IsValidBranchGrowth(Vector3 candidate)
        {
            if (Grid[candidate].IsOccupied) return false;

            var below = candidate.Y < Height - 1 && Grid.TryGetValue(new Vector3(candidate.X, candidate.Y - 1, candidate.Z), out var testBelow)
                ? testBelow.Growable as BranchNode
                : null;

            if (below != null) return false;

            return GetAllAdjacent(candidate).Any(p => Grid[p].Growable is BranchNode);
        }

        public bool IsValidStemGrowth(Vector3 candidate)
        {
            if (Grid[candidate].IsOccupied) return false;

            var adj = GetAllAdjacent(candidate).ToList();
            bool hasParent = adj.Any(p => Grid[p].Growable is BranchNode or StemNode);
            if (!hasParent) return false;

            foreach (var p in adj)
            {
                if (Grid[p].Growable is BranchNode)
                {
                    var siblings = GetAllAdjacent(p)
                        .Where(q => Grid[q].Growable is StemNode && q != candidate);

                    if (siblings.Any()) return false;
                }
            }

            return true;
        }

        public bool IsValidLeafGrowth(Vector3 candidate)
        {
            if (Grid[candidate].IsOccupied) return false;

            var adj = GetAllAdjacent(candidate);
            return adj.Any(p => Grid[p].Growable is StemNode) || adj.Count(p => Grid[p].Growable is LeafNode) >= 2;
        }

        public Vector3 GetSunDirectionFrom(Vector3 origin)
        {
            var dir = Sun.Position - origin;
            return Vector3.Normalize(dir);
        }
    }
}
