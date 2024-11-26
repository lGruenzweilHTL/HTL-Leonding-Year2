using System;


namespace FractionCalculator
{
    /// <summary>
    /// Eine einfache Realisierung der rationalen Zahlen ohne spätere Themen:
    ///   *  Fehlerbehandlung über Exceptions
    ///   *  Überladen von Operatoren
    ///   *  Statische Methoden
    ///   *  Konstruktor
    /// </summary>
    public class Fraction
    {
        #region fields
        private int _numerator = 1;     // Zaehler
        private int _denominator = 1;   // Nenner
        #endregion

        #region properties
        /// <summary>
        /// Property für numerator
        /// </summary>
        public int Numerator
        {
            get
            {
                return _numerator;
            }
            set
            {
                _numerator = value;
            }
        }

        /// <summary>
        /// Property für denumerator
        /// </summary>
        public int Denominator
        {
            get
            {
                return _denominator;
            }
            set
            {
                _denominator = value;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Liefert den Zahlenwert des Bruchs. Ist der Nenner gleich 0
        /// wird als Zahlenwert double.MaxValue mit dem richtigen Vorzeichen 
        /// zurückgegeben
        /// </summary>
        public double GetValue()
        {
            if (Denominator == 0)
            {
                return double.MaxValue * Math.Sign(Numerator);
            }
            
            return (double)Numerator / Denominator;
        }

        /// <summary>
        /// Wandelt den Bruch in einen String um
        /// </summary>
        /// <returns></returns>
        public string ConvertToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        /// <summary>
        /// Bruch so weit es geht kuerzen
        /// </summary>
        public void Normalize()
        {
            var divisor = Gcd(Numerator, Denominator);
            Numerator /= divisor;
            Denominator /= divisor;
            
            // Some trickery for unit tests
            if (Denominator < 0)
            {
                Numerator *= -1;
                Denominator *= -1;
            }
        }

        /// <summary>
        /// Gekürzte Brüche stimmen bei Zähler und Nenner überein oder
        /// haben bei Nenner gleich 0 gleiches Vorzeichen (4/0  == 3/0)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsEqual(Fraction other)
        {
            if (Denominator == 0 && other.Denominator == 0)
            {
                return Math.Sign(Numerator) == Math.Sign(other.Numerator);
            }
            
            Normalize();
            other.Normalize();
            return Numerator == other.Numerator && Denominator == other.Denominator;
        }
        #endregion

        /// <summary>
        /// Gcd...greatest common divisor (GGT)
        /// Gcd von a und b nicht rekursiv gelöst.
        /// </summary>
        /// <param name="a">Erste Zahl</param>
        /// <param name="b">Zweite Zahl</param>
        /// <returns>gcd(a,b)</returns>
        public static int Gcd(int a, int b)
        {
            // Calculate the greatest common divisor of a and b
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            
            return a;
        }
    }
}