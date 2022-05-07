using System;
using UnityEngine;

namespace Game.Scripts.CellularSpace.GridStep
{
    public class StepData : IComparable
    {
        public readonly int Cost;
        public readonly Vector3Int Direction;

        public StepData(int cost, Vector3Int direction)
        {
            Cost = cost;
            Direction = direction;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is StepData stepData))
                throw new NullReferenceException();
            return Cost.CompareTo(stepData.Cost);
        }

        public override string ToString()
        {
            return $"{Direction} : {Cost}";
        }

        public static StepData operator +(StepData g1, StepData g2) => 
            new StepData(g1.Cost + g2.Cost, g1.Direction + g2.Direction);
    }
}