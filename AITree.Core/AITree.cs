using System;
using System.Numerics;
using AITree.Core;
using System.Collections.Generic;

namespace AITree.AI
{
    public class TreeAI
    {
        private readonly Tree _tree;
        private readonly Random _random = new();

        public TreeAI(Tree tree)
        {
            _tree = tree;
        }

        public bool TryGrow()
        {
            if (_tree.CurrentEnergy <= 1)
                return false;

            List<(IGrowable node, int cost)> candidates = new();

            foreach (var pos in _tree.ActiveBranches)
            {
                var branch = new BranchNode(pos);
                if (branch.GrowthCost <= _tree.CurrentEnergy)
                    candidates.Add((branch, branch.GrowthCost));
            }

            foreach (var pos in _tree.ActiveStems)
            {
                var stem = new StemNode(pos);
                if (stem.GrowthCost <= _tree.CurrentEnergy)
                    candidates.Add((stem, stem.GrowthCost));
            }

            foreach (var pos in _tree.ActiveLeaves)
            {
                var leaf = new LeafNode(pos);
                if (leaf.GrowthCost <= _tree.CurrentEnergy)
                    candidates.Add((leaf, leaf.GrowthCost));
            }

            if (candidates.Count == 0)
                return false;

            var selected = candidates[_random.Next(candidates.Count)];
            var node = selected.node;

            _tree.AddNode(node);

            return true;
        }
    }
}
