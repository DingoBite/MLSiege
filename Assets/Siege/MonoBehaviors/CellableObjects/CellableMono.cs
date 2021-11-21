using System;
using System.Collections.Generic;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.General.Repositories.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Siege.MonoBehaviors.CellableObjects
{
    public class CellableMono : MonoBehaviour
    {
        [SerializeField] private CellableScriptableObject _scriptableObject;

        public readonly int Id;
        public Vector3Int PositionInt => Vector3Int.FloorToInt(transform.position);

        public Tuple<int, BlockType, IEnumerable<int>> ScriptableObjectInfo()
        {
            var (type, featureParams) = _scriptableObject.GetInfo();
            return new Tuple<int, BlockType, IEnumerable<int>>(Id, type, featureParams);
        }
    }
}
