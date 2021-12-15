using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.CoordsConverters;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.General.CellObjects;
using Assets.Siege.Model.BlockSpace.General.Enums;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Agents;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Agents
{
    public class FrameAgent : CellObjectFrame<FrameAgent, Agent, MonoAgent, AgentFeatures>
    {
        public FrameAgent(IFrameSpaceContext<FrameAgent> context, Agent cellObj, MonoAgent mono, Vector3Int coords) : base(context, cellObj, mono, coords)
        {
        }

        public bool Move(Direction direction, IFrameSpaceInfo<FrameBlock> blockSpace)
        {
            var coords = Coords + VectorIntFromDirection.GetVector(direction);
            if (CoordsAvailableCheck(coords, blockSpace))
            {
                _context.MoveTo(coords, Id);
                coords.y -= 1;
                while (CoordsAvailableCheck(coords, blockSpace))
                {
                    _context.MoveTo(coords, Id);
                    coords.y -= 1;
                }
                return true;
            }
            coords.y += 1;
            if (!CoordsAvailableCheck(coords, blockSpace)) return false;

            _context.MoveTo(coords, Id);
            return true;
        }

        private bool CoordsAvailableCheck(Vector3Int coords, IFrameSpaceInfo<FrameBlock> blockSpace)
            => !blockSpace.GetFrame(coords, out _) && !_context.GetFrame(coords, out _) &&
               coords.x >= blockSpace.FormingPoints.Item1.x &&
               coords.y >= blockSpace.FormingPoints.Item1.y &&
               coords.z >= blockSpace.FormingPoints.Item1.z &&
               coords.x <= blockSpace.FormingPoints.Item2.x &&
               coords.y <= blockSpace.FormingPoints.Item2.y &&
               coords.z <= blockSpace.FormingPoints.Item2.z;

        public void CommitAction(FrameAgent sender, AgentAction action, IFrameSpaceInfo<FrameAgent> senderSpace)
            => PostAnimationCommit(action, () => _cellObj.CommitAction(sender, this, senderSpace, _context, action));

        public void CommitAction(FrameBlock sender, BlockAction action, IFrameSpaceInfo<FrameBlock> senderSpace)
            => PostAnimationCommit(action, () => _cellObj.CommitAction(sender, this, senderSpace, _context, action));

        public void CommitAction(FrameAgent sender, AgentAction action)
            => PostAnimationCommit(action, () => _cellObj.CommitAction(sender, this, _context, action));
    }
}