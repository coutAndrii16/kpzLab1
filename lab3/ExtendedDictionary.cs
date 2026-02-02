using System.Collections;
using System.Collections.Generic;

namespace Lab3;

public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
{
    private readonly List<ExtendedDictionaryElement<T, U, V>> _items = new();

    public int Count => _items.Count;

    public void Add(T key, U value1, V value2)
    {   
        if (ContainsKey(key))
            throw new ArgumentException("Key already exists");

        _items.Add(new ExtendedDictionaryElement<T, U, V>(key, value1, value2));
    }

    public bool Remove(T key)
    {
        var item = _items.Find(x => x.Key!.Equals(key));
        return item != null && _items.Remove(item);
    }

    public bool ContainsKey(T key)
    {
        return _items.Exists(x => x.Key!.Equals(key));
    }

    public bool ContainsValue1(U value)
    {
        return _items.Exists(x => x.Value1!.Equals(value));
    }

    public bool ContainsValue2(V value)
    {
        return _items.Exists(x => x.Value2!.Equals(value));
    }

    // Індексатор
    public ExtendedDictionaryElement<T, U, V> this[T key]
    {
        get
        {
            var item = _items.Find(x => x.Key!.Equals(key));
            if (item == null)
                throw new KeyNotFoundException();
            return item;
        }
    }

    // foreach
    public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}