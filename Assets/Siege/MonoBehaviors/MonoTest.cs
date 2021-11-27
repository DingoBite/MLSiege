using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.MonoBehaviors.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.MonoBehaviors
{
    public class MonoTest : MonoBehaviour
    {
        [SerializeField] private GameObject _pointer;

        [Inject] private IBlockSpace _blockSpace;
        [Inject] private IGridCoordsConverter _gridCoordsConverter;

        public Vector3 position;
        public BlockType blockType = BlockType.Null;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                position.x--;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                position.z--;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                position.x++;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                position.z++;
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
                _blockSpace.InsertBlock(_gridCoordsConverter.Convert(position), new BlockInfo(blockType, BlockSolidity.Solid), out var id);
                print(id);
                print(position);
                print(_gridCoordsConverter.Convert(position));
            }

            if (Input.anyKeyDown) MovePointer();
        }

        public void MovePointer()
        {
            _pointer.gameObject.transform.position = position;
        }
    }
}
