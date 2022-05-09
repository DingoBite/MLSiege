namespace Game.Scripts.PathFind
{
    public interface ILimitedValue<out TValue>
    {
        TValue MaxValue { get; }
        TValue MinValue { get; }
    }
}