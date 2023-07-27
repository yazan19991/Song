namespace ServerSideProject.Model
{
    public class CachedData
    {
        public string Value { get; set; }
        public DateTime LastAccessed { get; set; }

        public CachedData(string value)
        {
            Value = value;
            LastAccessed = DateTime.Now;
        }
    }
}
