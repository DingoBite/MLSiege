using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.View.Test
{
    public class MonoTest : MonoBehaviour
    {
        [SerializeField] private GameObject _pointer;

        [Inject] private IBlockSpace<FrameBlock, BlockInfo, MonoBlock> _blockSpace;
        [Inject] private IGridCoordsConverter _gridCoordsConverter;
        [Inject] private IDictionary<BlockType, MonoBlock> _blockPrefabs;

        public Vector3 _position;
        public BlockType _blockType = BlockType.Null;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _position.x--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _position.z--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _position.x++;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                _position.z++;
                MovePointer();
            }
            else if (Input.mouseScrollDelta.y > 0.2)
            {
                _position.y++;
                MovePointer();
            }
            else if (Input.mouseScrollDelta.y < -0.2)
            {
                _position.y--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.C))
                _blockSpace.Clear();
            else if (Input.GetKeyDown(KeyCode.Space))
                _blockSpace.DeleteBlock(_gridCoordsConverter.Convert(_position));
            else if (Input.GetKeyDown(KeyCode.F))
            {
                _blockSpace.InsertBlock(_gridCoordsConverter.Convert(_position), _blockPrefabs[_blockType].GetInfo(), out var id);
                print(id);
                print(_position);
                print(_gridCoordsConverter.Convert(_position));
            }
        }

        private void MovePointer() => _pointer.gameObject.transform.position = _position;
    }
}
