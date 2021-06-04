namespace d03.Nasa
{
    public interface INasaClient<in TIn, out TOut>
    {
        public TOut GetAsync(TIn input);
    }
}
