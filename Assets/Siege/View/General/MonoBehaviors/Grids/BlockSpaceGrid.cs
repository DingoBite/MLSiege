using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;


[RequireComponent(typeof(Grid))]
public class BlockSpaceGrid : MonoBehaviour
{
    [Inject] public IFrameSpace<FrameBlock, BlockInfo, MonoBlock> BlockSpace { get; private set; }
    [Inject] public ITilemapLevelsGrid<MonoBlock> TilemapLevelsGrid { get; private set; }

    private Grid _selfGrid;

    public void Init()
    {
        _selfGrid = this.GetComponent<Grid>();
        TilemapLevelsGrid.Init(_selfGrid);
        BlockSpace.Init(TilemapLevelsGrid);
    }
}