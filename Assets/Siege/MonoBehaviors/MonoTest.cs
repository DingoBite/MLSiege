using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.Model.ObjectFeatures.Blocks.Realizations;
using UnityEngine;
using Zenject;

namespace Assets.Siege.MonoBehaviors
{
    public class MonoTest : MonoBehaviour
    {
        [SerializeField] private GameObject _pointer;

        [Inject] private IBlockSpace _blockSpace;
        [Inject] private IGridCoordsConverter _gridCoordsConverter;

        public Vector3 vector3;
        public BlockType blockType = BlockType.Null;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                vector3.z++;
            else if (Input.GetKeyDown(KeyCode.S))
                vector3.x--;
            else if (Input.GetKeyDown(KeyCode.D))
                vector3.z--;
            else if (Input.GetKeyDown(KeyCode.W))
                vector3.x++;
            else if (Input.mouseScrollDelta.y > 0.2)
            {
                vector3.y++;
                MovePointer();
            }
            else if (Input.mouseScrollDelta.y < -0.2)
            {
                vector3.y--;
                MovePointer();
            }
            else if (Input.GetKeyDown(KeyCode.C))
                _blockSpace.Clear();
            else if (Input.GetKeyDown(KeyCode.Space))
                _blockSpace.DeleteBlock(_gridCoordsConverter.Convert(vector3));
            else if (Input.GetKeyDown(KeyCode.F))
            {
                _blockSpace.InsertBlock(_gridCoordsConverter.Convert(vector3), GetBlock(), out var id);
                print(id);
                print(vector3);
                print(_gridCoordsConverter.Convert(vector3));
            }

            if (Input.anyKeyDown)
                MovePointer();
        }

        public void MovePointer()
        {
            _pointer.gameObject.transform.position = vector3;
        }

        public BlockFeatures GetBlock()
        {
            return blockType switch
            {
                BlockType.Dirt => new CommonBlockFeatures(BlockType.Dirt),
                BlockType.Stone => new CommonBlockFeatures(BlockType.Stone),
                _ => new NullBlockFeatures()
            };
        }
    }
}
