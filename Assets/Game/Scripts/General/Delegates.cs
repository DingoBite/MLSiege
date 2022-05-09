namespace Game.Scripts.General
{
    public static class Delegates
    {
        public delegate bool TryGet<in TParam, TResult>(TParam param1, out TResult result);
        public delegate bool TryGet<in TParam1, in TParam2, TResult>(TParam1 param1, TParam2 param2, out TResult result);
    }
}