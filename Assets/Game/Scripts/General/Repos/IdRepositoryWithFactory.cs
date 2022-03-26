using System;

namespace Game.Scripts.General.Repos
{
    public class IdRepositoryWithFactory<TValue> : IdRepository<TValue>
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
    }
}