using System;
using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories;
using Assets.Siege.View.Agents;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.Agents.Repositories
{
    public class FrameAgentSpace : AbstractFrameSpace<FrameAgent, AgentInfo, MonoAgent>
    {
        [Inject]
        public FrameAgentSpace(
            IFrameFabric<FrameAgent, AgentInfo, MonoAgent> frameFabric,
            IRepository<FrameAgent> frameRepositoryRepository,
            IIdRepository<Vector3Int> idRepository,
            IGridShaper<AgentInfo, MonoAgent> gridShaper,
            IGridCoordsConverter gridCoordsConverter)
            : base(frameFabric, frameRepositoryRepository, idRepository, gridShaper, gridCoordsConverter)
        {
        }

        public override bool GetFrame(int id, out FrameAgent frame)
        {
            var successfulGet = _frameRepository.TryGetCustomerById(id, out var frameAgent);
            frame = !successfulGet ? null : frameAgent;
            return successfulGet;
        }

        public override bool GetFrame(Vector3Int coords, out FrameAgent frame)
        {
            if (!_idByCoords.ContainsKey(coords))
            {
                frame = null;
                return false;
            }
            var successfulGet = _frameRepository.TryGetCustomerById(_idByCoords[coords], out var frameAgent);
            frame = !successfulGet ? null : frameAgent;
            return successfulGet;
        }

        public override bool InsertBlock(Vector3Int coords, AgentInfo info, out int id)
        {
            if (_idByCoords.ContainsKey(coords))
            {
                id = -1;
                return false;
            }
            id = _frameRepository.InsertCustomer(_frameFabric.Make(coords, info, this));
            _idByCoords[coords] = id;
            return true;
        }

        public override bool InsertBlock(Vector3Int coords, AgentInfo info)
        {
            if (_idByCoords.ContainsKey(coords))
                return false;

            var id = _frameRepository.InsertCustomer(_frameFabric.Make(coords, info, this));
            _idByCoords[coords] = id;
            return true;
        }

        public override bool InsertBlock(MonoAgent mono, out int id)
        {
            var frameAgent = _frameFabric.Make(mono, this);
            if (_idByCoords.ContainsKey(frameAgent.Coords))
            {
                id = -1;
                return false;
            }
            id = _frameRepository.InsertCustomer(frameAgent);
            _idByCoords[frameAgent.Coords] = id;
            return true;
        }

        public override bool InsertBlock(MonoAgent mono)
        {
            var frameAgent = _frameFabric.Make(mono, this);
            if (_idByCoords.ContainsKey(frameAgent.Coords))
                return false;
            var id = _frameRepository.InsertCustomer(frameAgent);
            _idByCoords[frameAgent.Coords] = id;
            return true;
        }

        public override void SwapBlock(int id1, int id2)
        {
            var successfulGet1 = _frameRepository.TryGetCustomerById(id1, out var agent1);
            var successfulGet2 = _frameRepository.TryGetCustomerById(id2, out var agent2);
            if (successfulGet1 && successfulGet2)
                agent1.SwapPosition(agent2);
            else
                throw new NullReferenceException("Swap frame by id null reference exception");
        }

        public override void SwapBlock(Vector3Int cords1, Vector3Int cords2)
        {
            var successfulGet1 = _frameRepository.TryGetCustomerById(_idByCoords[cords1], out var agent1);
            var successfulGet2 = _frameRepository.TryGetCustomerById(_idByCoords[cords2], out var agent2);
            if (successfulGet1 && successfulGet2)
                agent1.SwapPosition(agent2);
            else
                throw new NullReferenceException("Swap frame by coords null reference exception");
        }

        public override void MoveBlock(int id, Vector3Int newCoords)
        {
            if (_idByCoords.ContainsKey(newCoords))
                throw new Exception($"Agent space is already have frame on new Coords = {newCoords}");

            var successfulGet = _frameRepository.TryGetCustomerById(id, out var agent);
            if (!successfulGet)
                throw new KeyNotFoundException($"Agent space doesn't contains id = {id}");

            _idByCoords[newCoords] = id;
            agent.Coords = newCoords;
        }

        public override void MoveBlock(Vector3Int coords, Vector3Int newCoords)
        {
            if (!_idByCoords.ContainsKey(coords))
                throw new KeyNotFoundException($"Agent space doesn't contains frame on {coords}");
            MoveBlock(_idByCoords[coords], newCoords);
        }

        public override void DeleteBlock(Vector3Int coords)
        {
            if (!_idByCoords.ContainsKey(coords))
                return;
            _frameRepository.DeleteCustomer(_idByCoords[coords]);
            _idByCoords.Remove(coords);
        }

        public override void DeleteBlock(int id)
        {
            if (!_frameRepository.TryGetCustomerById(id, out var agent))
                return;
            _frameRepository.DeleteCustomer(id);
            _idByCoords.Remove(agent.Coords);
        }
    }
}