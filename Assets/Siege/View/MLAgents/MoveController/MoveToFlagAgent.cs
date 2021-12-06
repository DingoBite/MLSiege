using System;
using System.Linq;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Converters.Interfaces;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Zenject;

namespace Assets.Siege.View.MLAgents.MoveController
{
    public class MoveToFlagAgent: Agent
    {
        [SerializeField] private BlockType _flagBlockType;

        [Range(0, 10)]
        [SerializeField] private float _moveSpeed;

        [Inject] private IBlockSpace _blockSpace;
        [Inject] private IGridCoordsConverter _gridCoordsConverter;

        private PackBlock _flagBlock;
        private Vector3 _basePosition;
        private int _steps;
        private float _prevDistance;

        private void Awake()
        {
            _basePosition = this.transform.position;
        }


        public override void OnEpisodeBegin()
        {
            Debug.Log("New episode");
            _steps = 0;
            this.transform.position = _basePosition;
            var block = _blockSpace.GetPackBlocks().FirstOrDefault(x => x.Block.Features.BlockType == _flagBlockType);
            if (block == null)
                throw new Exception("Block space doesn't contain flag block");
            if (!_blockSpace.GetPackBlock(block.Id, out var flagBlock))
                throw new Exception("Something wrong in Block space id system");
            _flagBlock = flagBlock;
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(_gridCoordsConverter.Convert(this.transform.position));
            //sensor.AddObservation(_flagBlock.Coords);
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            var moveCoords = new Vector3Int(actions.DiscreteActions[0] - 1, 0, actions.DiscreteActions[1] - 1);
            this.transform.position += moveCoords;

            var agentCoords = _gridCoordsConverter.Convert(this.transform.position);
            var distance = (_flagBlock.Coords - agentCoords).magnitude;
            var coef = distance < _prevDistance ? -0.1f : 2f;
            _prevDistance = distance;

            if (distance <= 5)
            {
                AddReward(100);
                Debug.Log("SiegeAgent is near to flag");
                EndEpisode();
                return;
            }
            AddReward(-coef);

            _steps += 1;
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            return;
        }
    }
}