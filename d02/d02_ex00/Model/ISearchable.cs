namespace d02_ex00.Model
{
    public enum Media
    {
        Book,
        Movie
    }

    public interface ISearchable
    {
        public Media MediaType { get; }
    }
}
