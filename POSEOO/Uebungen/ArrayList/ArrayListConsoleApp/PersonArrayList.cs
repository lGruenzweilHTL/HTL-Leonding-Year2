using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ArrayListConsoleApp;

public class PersonArrayList
{
    private Person?[] _storage = [];

    public PersonArrayList(long initialSize)
    {
        
    }

    public PersonArrayList()
    {
        
    }

    public void Add(Person? p)
    {
        Array.Resize(ref _storage, _storage.Length + 1);
        _storage[^1] = p;
    }

    public void Insert(int index, Person? p)
    {
        Array.Resize(ref _storage, _storage.Length + 1);
        for (int i = _storage.Length - 1; i > index; i--)
        {
            _storage[i] = _storage[i - 1];
        }
        _storage[index] = p;
    }

    public void Remove(Person? p)
    {
        int idx = IndexOf(p);
        if (idx != -1)
        {
            RemoveAt(idx);
        }
    }

    public void RemoveAt(int index)
    {
        // Validate idx
        if (index < 0 || index >= _storage.Length) return;
        
        // Shift left
        for (int i = index + 1; i < _storage.Length; i++)
        {
            _storage[i - 1] = _storage[i];
        }
        
        // Resize
        Array.Resize(ref _storage, _storage.Length - 1);
    }

    public void Clear()
    {
        _storage = [];
    }

    public int IndexOf(Person? person)
    {
        for (var i = 0; i < _storage.Length; i++)
        {
            if (_storage[i] == person)
            {
                return i;
            }
        }

        return -1;
    }

    public bool Contains(Person? person)
    {
        return IndexOf(person) != -1;
    }

    public Person? this[int index]
    {
        get => _storage[index];
        set => _storage[index] = value;
    }
    
    public int Count => _storage.Length;
}