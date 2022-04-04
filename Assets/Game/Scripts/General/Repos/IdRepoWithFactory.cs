using System;
using System.Collections.Generic;

namespace Game.Scripts.General.Repos
{
    public class IdRepoWithFactory<TValue> : IdRepository<TValue>
    {
        public TConcreteValue MakeAndAdd<TConcreteValue>(Func<int, TConcreteValue> constructFunc) where TConcreteValue : TValue
        {
            if (constructFunc == null)
                throw new ArgumentNullException();
            var id = PopId();
            var value = constructFunc.Invoke(id);
            _values.Add(id, value);
            return value;
        }
        
        public (TConcreteValue1, TConcreteValue2) MakeAndAdd<TConcreteValue1, TConcreteValue2>(
            Func<int, int,
            (TConcreteValue1, TConcreteValue2)> constructFunc)
            where TConcreteValue1 : TValue where TConcreteValue2 : TValue
        {
            if (constructFunc == null)
                throw new ArgumentNullException();
            var id1 = PopId();
            var id2 = PopId();
            var value = constructFunc.Invoke(id1, id2);
            _values.Add(id1, value.Item1);
            _values.Add(id2, value.Item2);
            return value;
        }
        
        public IEnumerable<TConcreteValue> MakeAndAdd<TConcreteValue>(IEnumerable<Func<int, TConcreteValue>> constructFunctions) where TConcreteValue : TValue
        {
            if (constructFunctions == null)
                throw new ArgumentNullException();
            var result = new List<TConcreteValue>();
            foreach (var constructFunc in constructFunctions){
                var id = PopId();
                var value = constructFunc.Invoke(id);
                result.Add(value);
                _values.Add(id, value);
            }
            return result;
        }
    }
}