namespace Assets.Siege.Model.General.Interfaces
{
    public interface IIdRepository<in TKey>
    {
        public int this[TKey key] { get; set; }
        public bool ContainsKey(TKey key);
        public void Remove(TKey key);
        public void Clear();
    }
}