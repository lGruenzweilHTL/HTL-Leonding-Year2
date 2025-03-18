using EnumeratorWorkshop;

var a = new MyClass();

int x = ((IComparable<int>)a).CompareTo(1);
int y = a.CompareTo(a);
    
class MyClass : IComparable<int>, IComparable<MyClass>
{ 
    int IComparable<int>.CompareTo(int other)
    {
        return 1;
    }

    public int CompareTo(MyClass? other)
    {
        return 0;
    }
}
