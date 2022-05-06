using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellularSpace.GridStep
{
    public class StepData : IComparable
    {
        public readonly int Cost;
        public readonly IList<PerformanceParam> OrderedPerformanceParam;

        public StepData(int cost, IList<PerformanceParam> orderedPerformanceParam)
        {
            Cost = cost;
            OrderedPerformanceParam = orderedPerformanceParam;
        }
        
        public StepData()
        {
            Cost = -1;
            OrderedPerformanceParam = null;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is StepData stepData))
                throw new NullReferenceException();
            return Cost.CompareTo(stepData.Cost);
        }

        public override string ToString()
        {
            if (OrderedPerformanceParam != null && OrderedPerformanceParam.Count == 1)
                return $"{OrderedPerformanceParam[0].EnumActionType} : {Cost}";
            return $"{Cost}";
        }

        public static StepData operator +(StepData g1, StepData g2) => 
            new StepData(g1.Cost + g2.Cost, g2.OrderedPerformanceParam);
    }
}