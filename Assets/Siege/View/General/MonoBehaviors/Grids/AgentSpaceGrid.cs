using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.Agents;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Grid))]
public class AgentSpaceGrid : MonoBehaviour
{
    [Inject] public IFrameSpace<FrameAgent, AgentInfo, MonoAgent> AgentSpace { get; set; }
    [Inject] public ITilemapLevelsGrid<MonoAgent> TilemapLevelsGrid { get; set; }

    private Grid _selfGrid;

    public void Init()
    {
        _selfGrid = this.GetComponent<Grid>();
        TilemapLevelsGrid.Init(_selfGrid);
        AgentSpace.Init(TilemapLevelsGrid);
    }
}