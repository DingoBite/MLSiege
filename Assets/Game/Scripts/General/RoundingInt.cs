namespace Game.Scripts.General
{
    public class RoundingInt
    {
        private readonly int _maxInt;

        private int _currentIndex;

        public RoundingInt(int maxInt)
        {
            _maxInt = maxInt;
        }
        
        public RoundingInt(int startIndex, int maxInt)
        {
            _maxInt = maxInt;
            CurrentIndex = startIndex;
        }
        
        public void MoveNext() => CurrentIndex++;
        
        public int CurrentIndex
        {
            get => _currentIndex;
            private set
            {
                if (value >= _maxInt || value < 0)
                    _currentIndex = 0;
                else
                    _currentIndex = value;
            }
        }
    }
}