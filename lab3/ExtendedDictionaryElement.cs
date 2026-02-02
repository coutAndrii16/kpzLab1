namespace Lab3;

public class ExtendedDictionaryElement<T, U, V>
{
    public T Key { get; }
    public U Value1 { get; }
    public V Value2 { get; }

    public ExtendedDictionaryElement(T key, U value1, V value2)
    {
        Key = key;
        Value1 = value1;
        Value2 = value2;
    }
}