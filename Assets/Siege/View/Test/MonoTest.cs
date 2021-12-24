using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Blocks.Enums;
using Assets.Siege.CellularSpace.General.Interfaces;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.View.Test
{
    public class MonoTest : MonoBehaviour
    {
        [SerializeField] private GameObject _pointer;

        [Inject] private IFrameSpace<FrameBlock, BlockInfo, MonoBlock> _frameSpace;
        [Inject] private IPrefabsByType<BlockType, MonoBlock> _blockPrefabs;

        private Vector3Int _coords;
        public BlockType _blockType = BlockType.Null;

        private void Start()
        {
            _coords = _frameSpace.Convert(this.transform.position);
            Debug.Log(_coords);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _coords.x--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _coords.z--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _coords.x++;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                _coords.z++;
                MovePointer();
            }
            else if (Input.mouseScrollDelta.y > 0.2)
            {
                _coords.y++;
                MovePointer();
            }
            else if (Input.mouseScrollDelta.y < -0.2)
            {
                _coords.y--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.C))
                _frameSpace.Clear();
            else if (Input.GetKeyDown(KeyCode.Space))
                _frameSpace.Delete(_coords);
            else if (Input.GetKeyDown(KeyCode.F))
            {
                _frameSpace.Insert(_coords, _blockPrefabs[_blockType].GetInfo(), out var id);
                print(id);
                print(_coords);
            }
        }

        private void MovePointer() => _pointer.gameObject.transform.position = _frameSpace.Convert(_coords);
    }
}
