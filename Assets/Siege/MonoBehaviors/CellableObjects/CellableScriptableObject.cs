using System;
using System.Collections.Generic;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.MonoBehaviors.CellableObjects
{
    [CreateAssetMenu(fileName = "CellableObject", menuName = "ScriptableObjects/CellableObjects", order = 1)]
    public class CellableScriptableObject : InfoScriptableObject<BlockType>
    {
        [SerializeField] private BlockType _blockType;

        public override Tuple<BlockType, IEnumerable<int>> GetInfo()
        {
            return new Tuple<BlockType, IEnumerable<int>>(_blockType, new List<int>());
        }
    }
}
