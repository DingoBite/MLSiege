using Assets.Siege.Model.CellularSpace.Interfaces;
using UnityEngine;
using Zenject;

public class MonoTest : MonoBehaviour
{
    [Inject] private IBlockSpace _blockSpace;

    void Start()
    {
        print(_blockSpace.GetCustomers());
    }
}
