using System;
using System.Collections.ObjectModel;
using System.Linq;
using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.General.Enums;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Agents;
using Assets.Siege.View.Blocks;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Zenject;
using Agent = Unity.MLAgents.Agent;

namespace Assets.Siege.View.Test.MoveController
{
    public class MoveToFlagAgent: Agent
    {
        [SerializeField] private BlockType _flagBlockType;

        [Inject] private IBlockSpace<FrameBlock, BlockInfo, MonoBlock> _blockSpace;
        [Inject] private IBlockSpace<FrameAgent, AgentInfo, MonoAgent> _agentSpace;

        private FrameBlock _flagBlock;
        private FrameAgent _frameAgent;
        private int _steps;
        private float _res;
        private float _prevDistance;
        private readonly Direction[] _directions = {Direction.North, Direction.West, Direction.South, Direction.East};
        private Vector3Int _basePos;
        private ReadOnlyCollection<float> _obs;

        private void Start()
        {
            _basePos = _agentSpace.Convert(this.transform.position);
            Debug.Log(_blockSpace.FormingPoints);
        }

        public override void OnEpisodeBegin()
        {
            _flagBlock ??= _blockSpace.GetBlocks().FirstOrDefault(x => x.Features.BlockType == _flagBlockType);
            _frameAgent ??= _agentSpace.GetBlocks().First();

            if (_flagBlock == null)
                throw new Exception("Block space doesn't contain flag block");
            if (!_blockSpace.GetFrame(_flagBlock.Id, out _))
                throw new Exception("Something wrong in Block space id system");
            Debug.Log("New episode");
            _steps = 0;
            _res = 0;
            _frameAgent.Coords = _basePos;
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(_frameAgent.Coords);
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            var direction = _directions[actions.DiscreteActions[0]];
            var moved = _frameAgent.Move(direction, _blockSpace, _agentSpace);

            //var distance = (Vector3.Normalize(_flagBlock.Coords) - Vector3.Normalize(_frameAgent.Coords)).magnitude;
            var distance = (_flagBlock.Coords - _frameAgent.Coords).magnitude;
            _res += 0.0001f;
            if (!moved) _res += 0.0002f;

            _prevDistance = distance;
            _steps += 1;


            if (distance <= 2)
            {
                SetReward(1 - _res);
                Debug.Log($"SiegeAgent is near to flag. Steps = {_steps}");
                EndEpisode();
                return;
            }

            SetReward(-1 + _res);
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            return;
        }
    }
}