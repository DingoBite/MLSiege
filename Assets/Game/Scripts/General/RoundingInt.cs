namespace Game.Scripts.General
{
    public class RoundingInt
    {
        private readonly int _maxInt;
        public int CurrentIndex { get; private set; }

        public RoundingInt(int maxInt)
        {
            _maxInt = maxInt;
        }

        public void MoveNext()
        {
            CurrentIndex++;
            if (CurrentIndex >= _maxInt || CurrentIndex < 0)
            {
                CurrentIndex = 0;
            }
        }
    }
}