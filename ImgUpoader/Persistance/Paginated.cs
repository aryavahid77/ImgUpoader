namespace ImgUpoader.Persistance
{
    public class Paginated<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }


        public Paginated(int count, List<T> items)
        {
            TotalCount = count;
            Items = items;
        }

    }
}
