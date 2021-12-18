using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
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