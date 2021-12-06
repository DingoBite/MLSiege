using System.Collections.Generic;
using Assets.Siege.Model.CellularSpace.Converters.Interfaces;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.View
{
    public class MonoTest : MonoBehaviour
    {
        [SerializeField] private GameObject _pointer;

        [Inject] private IBlockSpace _blockSpace;
        [Inject] private IGridCoordsConverter _gridCoordsConverter;
        [Inject] private IDictionary<BlockType, MonoBlock> _blockPrefabs;

        public Vector3 position;
        public BlockType blockType = BlockType.Null;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                position.x--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                position.z--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                position.x++;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                position.z++;
                MovePointer();
            }
            else if (Input.mouseScrollDelta.y > 0.2)
            {
                position.y++;
                MovePointer();
            }
            else if (Input.mouseScrollDelta.y < -0.2)
            {
                position.y--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.C))
                _blockSpace.Clear();
            else if (Input.GetKeyDown(KeyCode.Space))
                _blockSpace.DeleteBlock(_gridCoordsConverter.Convert(position));
            else if (Input.GetKeyDown(KeyCode.F))
            {
                _blockSpace.InsertBlock(_gridCoordsConverter.Convert(position), _blockPrefabs[blockType].GetInfo(), out var id);
                print(id);
                print(position);
                print(_gridCoordsConverter.Convert(position));
            }
        }

        private void MovePointer() => _pointer.gameObject.transform.position = position;
    }
}
