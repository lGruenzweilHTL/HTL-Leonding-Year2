using System.Collections;
using System.Collections.Generic;

namespace NumberFactory;

public abstract class AbstractNumbers : INumbers
{
    protected AbstractNumbers(long lowerBound, long upperBound)
    {
        LowerBound = lowerBound;
        UpperBound = upperBound;
        _list = GenerateNumbers(lowerBound, upperBound);
    }
    
    public long this[int index] => _list[index];

    public long LowerBound { get; }
    public long UpperBound { get; }
    public int Length => _list.Count;
    
    protected readonly List<long> _list;
    
    protected abstract List<long> GenerateNumbers(long lowerBound, long upperBound);
    
    public IEnumerator<long> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}