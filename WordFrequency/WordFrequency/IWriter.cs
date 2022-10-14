namespace WordFrequency
{
    public interface IWriter
    {
        void Write(IReadOnlyDictionary<string, int> frequency);
    }
}
