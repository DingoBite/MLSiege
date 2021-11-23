namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IIdRepository<in TKey>
    {
        public int this[TKey coords] { get; }
        public void Add(TKey coords, int id);
        public void Remove(TKey coords);
        public void Clear();
    }
}