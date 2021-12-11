using System;
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
        public FrameAgent(IFrameSpaceContext<FrameAgent> frameSpace, Agent data, MonoAgent mono, Vector3Int coords) : base(frameSpace, data, mono, coords)
        {
        }

        public override AgentFeatures Features => _data.Features;

        private void PostAnimationCommit(Action commitAction)
        {
            // TODO Animation, postAnimation commit
            _mono.transform.localScale *= 0.8f;
            commitAction?.Invoke();
        }

        public bool Move(Direction direction, IFrameSpaceContext<FrameBlock> blockSpace, IFrameSpaceContext<FrameAgent> agentSpace)
        {
            var coords = Coords + VectorIntFromDirection.GetVector(direction);
            if (CoordsAvailableCheck(coords, blockSpace, agentSpace))
            {
                Coords = coords;
                coords.y -= 1;
                while (CoordsAvailableCheck(coords, blockSpace, agentSpace))
                {
                    Coords = coords;
                    coords.y -= 1;
                }
                return true;
            }
            coords.y += 1;
            if (!CoordsAvailableCheck(coords, blockSpace, agentSpace)) return false;

            Coords = coords;
            return true;
        }

        private bool CoordsAvailableCheck(Vector3Int coords,
            IFrameSpaceContext<FrameBlock> blockSpace, IFrameSpaceContext<FrameAgent> agentSpace)
            => !blockSpace.GetFrame(coords, out _) && !agentSpace.GetFrame(coords, out _) &&
               coords.x >= blockSpace.FormingPoints.Item1.x &&
               coords.y >= blockSpace.FormingPoints.Item1.y &&
               coords.z >= blockSpace.FormingPoints.Item1.z &&
               coords.x <= blockSpace.FormingPoints.Item2.x &&
               coords.y <= blockSpace.FormingPoints.Item2.y &&
               coords.z <= blockSpace.FormingPoints.Item2.z;

        public void CommitAction(FrameAgent sender, AgentAction action, IFrameSpaceContext<FrameAgent> senderSpace)
            => PostAnimationCommit(() => _data.CommitAction(sender, this, senderSpace, _frameSpace, action));

        public void CommitAction(FrameBlock sender, BlockAction action, IFrameSpaceContext<FrameBlock> senderSpace)
            => PostAnimationCommit(() => _data.CommitAction(sender, this, senderSpace, _frameSpace, action));

        public void CommitAction(FrameAgent sender, AgentAction action)
            => PostAnimationCommit(() => _data.CommitAction(sender, this, _frameSpace, action));
    }
}