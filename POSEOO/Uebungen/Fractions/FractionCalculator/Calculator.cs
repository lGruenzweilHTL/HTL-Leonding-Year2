using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionCalculator
{
    public class Calculator
    {
        #region fields

        private Fraction _leftOperand = new Fraction(); // Linker Operand
        private Fraction _rightOperand = new Fraction(); // Rechter Operand

        #endregion

        #region properties

        /// <summary>
        /// Ruft den linken Openanden ab oder legt diesen fest. Ein 
        /// Operand kann nur dann zugewiesen werden, wenn dieser 
        /// ungleich null ist.
        /// </summary>
        public Fraction LeftOperand
        {
            get { return _leftOperand; }
            set { _leftOperand = value; }
        }

        /// <summary>
        /// Ruft den rechten Openanden ab oder legt diesen fest. Ein 
        /// Operand kann nur dann zugewiesen werden, wenn dieser 
        /// ungleich null ist.
        /// </summary>
        public Fraction RightOperand
        {
            get { return _rightOperand; }
            set { _rightOperand = value; }
        }

        #endregion

        #region methods

        /// <summary>
        /// Addiert die beiden Operanden 'LeftOperand und RightOperand' zur Summe
        /// und liefert das Ergenis an den Aufrufer. Das Ergebnis ist bereits 
        /// normalisiert.
        /// </summary>
        /// <returns>Ergebnis der Addition</returns>
        public Fraction Add()
        {
            int numerator = _leftOperand.Numerator * _rightOperand.Denominator +
                            _rightOperand.Numerator * _leftOperand.Denominator;
            int denominator = _leftOperand.Denominator * _rightOperand.Denominator;

            Fraction result = new Fraction();
            result.Numerator = numerator;
            result.Denominator = denominator;

            result.Normalize();
            return result;
        }

        /// <summary>
        /// Subtrahiert die beiden Operanden 'LeftOperand und RightOperand' zur Differenz
        /// und liefert das Ergenis an den Aufrufer. Das Ergebnis ist bereits 
        /// normalisiert.
        /// </summary>
        /// <returns>Ergebnis der Subtraktion</returns>
        public Fraction Sub()
        {
            RightOperand.Numerator *= -1;

            Fraction result = Add();

            return result;
        }

        /// <summary>
        /// Multipliziert die beiden Operanden 'LeftOperand und RightOperand' zum Produkt
        /// und liefert das Ergenis an den Aufrufer. Das Ergebnis ist bereits 
        /// normalisiert.
        /// </summary>
        /// <returns>Ergebnis der Multiplikation</returns>
        public Fraction Mul()
        {
            int numerator = _leftOperand.Numerator * _rightOperand.Numerator;
            int denominator = _leftOperand.Denominator * _rightOperand.Denominator;

            Fraction result = new Fraction();
            result.Numerator = numerator;
            result.Denominator = denominator;

            result.Normalize();
            return result;
        }

        /// <summary>
        /// Dividiert die beiden Operanden 'LeftOperand und RightOperand' zum Quotient
        /// und liefert das Ergenis an den Aufrufer. Das Ergebnis ist bereits 
        /// normalisiert.
        /// </summary>
        /// <returns>Ergebnis der Multiplikation</returns>
        public Fraction Div()
        {
            // Reverse right fraction
            (RightOperand.Numerator, RightOperand.Denominator) = (RightOperand.Denominator, RightOperand.Numerator);

            // Multiply left fraction with reversed right fraction
            return Mul();
        }

        #endregion
    }
}