using Assets.Siege.Model.CellularSpace.Blocks.Realizations;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks.Realizations;
using UnityEngine;
using Zenject;

public class MonoTest : MonoBehaviour
{
    [Inject] private IBlockSpace _blockSpace;

    public int i = 0;
    public int j = 0;

    void Start()
    {
        print(_blockSpace.GetCustomers());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _blockSpace.InsertBlock(new Vector3Int(i, 5, 1), new StoneBlock(0, new NullBlockFeatures()));
            i++;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _blockSpace.DeleteBlock(new Vector3Int(j, 5, 1));
            j++;
        }
    }
}
