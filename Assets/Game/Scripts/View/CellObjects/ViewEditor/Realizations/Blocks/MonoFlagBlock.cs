using System;
using Game.Scripts.Agents.Learning;
using Game.Scripts.CellularSpace.CellObjects;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellObjects.Realizations;
using Game.Scripts.General.FlexibleDataApi;
using Unity.MLAgents;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Realizations
{
    public class MonoFlagBlock : MonoCellObject
    {
        [SerializeField] private AgentWithEndGame _agentWithEndGame;
        protected override bool IsModifiable => true;
        protected override CellObjectType CellObjectType => CellObjectType.Flag;
        private FlagCharacteristic Characteristics => new FlagCharacteristic();
        protected override AbstractChildCellObject MakeCellObject(int id, 
            Action<object, PerformanceParam> commitReaction, bool isExternallyModifiable)
        {
            return new CellFlagBlock(id, Characteristics, commitReaction, _agentWithEndGame.Win, isExternallyModifiable);
        }
    }
}