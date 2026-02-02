using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab3;

public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
    where T : notnull
{
    private readonly Dictionary<T, ExtendedDictionaryElement<T, U, V>> _items = new();

    public int Count => _items.Count;

    public void Add(T key, U value1, V value2)
    {
        ArgumentNullException.ThrowIfNull(key);
        
        if (ContainsKey(key))
            throw new ArgumentException("Key already exists", nameof(key));

        var element = new ExtendedDictionaryElement<T, U, V>(key, value1, value2);
        _items.Add(key, element);
    }

    public bool Remove(T key)
    {
        ArgumentNullException.ThrowIfNull(key);
        return _items.Remove(key);
    }

    public bool ContainsKey(T key)
    {
        ArgumentNullException.ThrowIfNull(key);
        return _items.ContainsKey(key);
    }

    public bool ContainsValue1(U value)
    {
        return _items.Values.Any(x => x.Value1!.Equals(value));
    }

    public bool ContainsValue2(V value)
    {
        return _items.Values.Any(x => x.Value2!.Equals(value));
    }

    // Індексатор
    public ExtendedDictionaryElement<T, U, V> this[T key]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(key);
            if (!_items.TryGetValue(key, out var item))
                throw new KeyNotFoundException($"Key '{key}' not found.");
            return item;
        }
    }

    // foreach
    public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
    {
        return _items.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}