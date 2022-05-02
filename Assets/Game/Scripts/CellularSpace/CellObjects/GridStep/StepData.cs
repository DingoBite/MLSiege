using System;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;

namespace Game.Scripts.CellularSpace.GridStep
{
    public class StepData : IComparable
    {
        public readonly ICell StartCell;
        public readonly ICell EndCell;
        public readonly int Cost;
        
        public StepData(ICell startCell, ICell endCell, int cost)
        {
            StartCell = startCell;
            EndCell = endCell;
            Cost = cost;
        }
        
        public StepData(ICell startCell, ICell endCell)
        {
            StartCell = startCell;
            EndCell = endCell;
            Cost = Int32.MaxValue;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is StepData stepData))
                throw new NullReferenceException();
            return Cost.CompareTo(stepData.Cost);
        }

        public override string ToString()
        {
            return $"{StartCell.Coords} -> {EndCell.Coords} : {Cost}";
        }

        public static StepData operator +(StepData g1, StepData g2) => 
            new StepData(g1.EndCell, g2.EndCell, g1.Cost + g2.Cost);
    }
}