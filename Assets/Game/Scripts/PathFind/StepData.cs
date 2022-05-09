using System;
using UnityEngine;

namespace Game.Scripts.PathFind
{
    public class StepData : IComparable, ILimitedValue<StepData>
    {
        private static readonly StepData _maxValue = new StepData(int.MaxValue, Vector3Int.zero);
        private static readonly StepData _minValue = new StepData(int.MaxValue, Vector3Int.zero);
        
        public readonly int Cost;
        public readonly Vector3Int Direction;

        public StepData(int cost, Vector3Int direction)
        {
            Cost = cost;
            Direction = direction;
        }

        public StepData MaxValue => _maxValue;
        public StepData MinValue => _minValue;

        public int CompareTo(object obj)
        {
            if (!(obj is StepData stepData))
                throw new NullReferenceException();
            return Cost.CompareTo(stepData.Cost);
        }

        public static StepData operator +(StepData g1, StepData g2)
        {
            if (g1.Cost == int.MaxValue || g2.Cost == int.MaxValue)
                return new StepData(int.MaxValue, g1.Direction + g2.Direction);
            if (g1.Cost == int.MinValue || g2.Cost == int.MinValue)
                return new StepData(int.MinValue, g1.Direction + g2.Direction);
            return new StepData(g1.Cost + g2.Cost, g1.Direction + g2.Direction);
        }

        public override string ToString()
        {
            return $"{Direction} : {Cost}";
        }
    }
}