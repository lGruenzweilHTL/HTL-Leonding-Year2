using System.Numerics;

namespace Core;

public class ComplexNumber : 
    IEquatable<ComplexNumber>, 
    IComparisonOperators<ComplexNumber, ComplexNumber, bool>,
    IComparable<ComplexNumber>
{
    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imag = imaginary;
    }
    
    public double Real, Imag;
    
    public double Magnitude => Math.Sqrt(Real * Real + Imag * Imag);
    public static ComplexNumber Zero => new ComplexNumber(0, 0);
    public static ComplexNumber I => new ComplexNumber(0, 1);

    #region Arithmetic Operators  
    
    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real + b.Real, a.Imag + b.Imag);
    }
    
    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real - b.Real, a.Imag - b.Imag);
    }
    
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        // (a + bi) (c + id) = (ac – bd) + i(ad + bc)
        return new ComplexNumber(a.Real * b.Real - a.Imag * b.Imag, a.Real * b.Imag + a.Imag * b.Real);
    }
    
    public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
    {
        double denominator = b.Real * b.Real + b.Imag * b.Imag;
        return new ComplexNumber((a.Real * b.Real + a.Imag * b.Imag) / denominator, (a.Imag * b.Real - a.Real * b.Imag) / denominator);
    }

    public static ComplexNumber operator -(ComplexNumber n)
    {
        return Zero - n;
    }
    
    #endregion

    #region Comparison Operators
    
    public static bool operator ==(ComplexNumber a, ComplexNumber b)
    {
        return a.Equals(b);
    }
    
    public static bool operator !=(ComplexNumber a, ComplexNumber b)
    {
        return !(a == b);
    }
    
    public static bool operator >(ComplexNumber a, ComplexNumber b)
    {
        return a.Magnitude > b.Magnitude;
    }
    
    public static bool operator <(ComplexNumber a, ComplexNumber b)
    {
        return a.Magnitude < b.Magnitude;
    }

    public static bool operator >=(ComplexNumber a, ComplexNumber b)
    {
        return a.Magnitude >= b.Magnitude;
    }
    
    public static bool operator <=(ComplexNumber a, ComplexNumber b)
    {
        return a.Magnitude <= b.Magnitude;
    }
    
    #endregion

    #region Implicit Operators
    
    public static implicit operator ComplexNumber(double n)
    {
        return new ComplexNumber(n, 0);
    }
    
    public static implicit operator ComplexNumber(int n)
    {
        return new ComplexNumber(n, 0);
    }
    
    public static implicit operator ComplexNumber(BigInteger n)
    {
        return new ComplexNumber((double)n, 0);
    }
    
    public static implicit operator ComplexNumber((double real, double imag) n)
    {
        return new ComplexNumber(n.real, n.imag);
    }
    
    public static implicit operator double(ComplexNumber n)
    {
        return n.Real;
    }
    
    #endregion

    #region Overrides and Comparison
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Real, Imag);
    }

    public bool Equals(ComplexNumber? other)
    {
        if (other is null)
        {
            return false;
        }
        
        return Math.Abs(Real - other.Real) < double.Epsilon 
               && Math.Abs(Imag - other.Imag) < double.Epsilon;
    }

    public override string ToString()
    {
        return $"{Real} + {Imag}i";
    }

    public int CompareTo(ComplexNumber? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Magnitude.CompareTo(other.Magnitude);
    }
    
    #endregion
}