using System;
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
using Agent = Unity.MLAgents.Agent;

namespace Assets.Siege.View.Test.MoveController
{
    public class MoveToFlagAgent: Agent
    {
        [SerializeField] private BlockType _flagBlockType;
        [SerializeField] private bool _isMovable;
        [SerializeField] private MonoAgent _agent;

        [SerializeField] private BlockSpaceGrid _blockSpaceGrid;
        [SerializeField] private AgentSpaceGrid _agentSpaceGrid;

        private IFrameSpace<FrameBlock, BlockInfo, MonoBlock> _blockSpace;
        private IFrameSpace<FrameAgent, AgentInfo, MonoAgent> _agentSpace;

        private FrameBlock _flagBlock;
        private FrameAgent _frameAgent;
        private int _steps;
        private float _res;
        private readonly Direction[] _directions = {Direction.North, Direction.West, Direction.South, Direction.East};
        private Vector3Int _basePos;

        private void Start()
        {
            _blockSpaceGrid.Init();
            _agentSpaceGrid.Init();
            _blockSpace = _blockSpaceGrid.BlockSpace;
            _agentSpace = _agentSpaceGrid.AgentSpace;
            Debug.Log(_blockSpace.FormingPoints);
        }

        public override void OnEpisodeBegin()
        {
            _flagBlock ??= _blockSpace.GetFrames().FirstOrDefault(x => x.Features.BlockType == _flagBlockType);
            if (_frameAgent == null)
            {
                Debug.Log(_agentSpace.GetFrame(_agent.Id, out var tempAgent));
                _frameAgent = tempAgent;
                if (_isMovable)
                    _basePos = _frameAgent.Coords;
            }
            if (_blockSpace == null)
                throw new NullReferenceException("Block space null reference");
            if (_flagBlock == null)
                throw new Exception("Block space doesn't contain flag block");
            if (!_blockSpace.GetFrame(_flagBlock.Id, out _))
                throw new Exception("Something wrong in Block space id system");
            if (!_isMovable) return;
            Debug.Log("New episode");
            _steps = 0;
            _res = 0;
            _agentSpace.MoveTo(_basePos, _frameAgent.Id);
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(_frameAgent.Coords);
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            if (!_isMovable) return;
            var direction = _directions[actions.DiscreteActions[0]];
            var moved = _frameAgent.Move(direction, _blockSpace);

            //var distance = (Vector3.Normalize(_flagBlock.Coords) - Vector3.Normalize(_frameAgent.Coords)).magnitude;
            var distance = (_flagBlock.Coords - _frameAgent.Coords).magnitude;
            _res += 0.0001f;
            if (!moved) _res += 0.0002f;
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

        private Direction _direction = Direction.Stop;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W)) _direction = Direction.North;
            else if (Input.GetKeyDown(KeyCode.A)) _direction = Direction.West;
            else if (Input.GetKeyDown(KeyCode.S)) _direction = Direction.South;
            else if (Input.GetKeyDown(KeyCode.D)) _direction = Direction.East;
            if (_direction == Direction.Stop) return;

            Debug.Log(_direction);
            Debug.Log(_frameAgent.Move(_direction, _blockSpace));
            _direction = Direction.Stop;
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            return;
        }
    }
}