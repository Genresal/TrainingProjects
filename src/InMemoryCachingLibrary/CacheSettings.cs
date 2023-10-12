namespace InMemoryCachingLibrary
{
    /// <summary>
    /// Memory cache setting.
    /// </summary>
    public class CacheSettings
    {
        /// <summary>
        /// Memory cache expiration time, relative to now.
        /// </summary>
        public IDictionary<string, double> MemoryCacheDuration { get; set; }
    }
}
