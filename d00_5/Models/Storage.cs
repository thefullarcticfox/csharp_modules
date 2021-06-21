namespace d00_5.Models
{
    public class Storage
    {
        public int ProductCount { get; set; }

        public Storage(int productCount) => ProductCount = productCount;

        public bool IsEmpty() => ProductCount == 0;
    }
}
