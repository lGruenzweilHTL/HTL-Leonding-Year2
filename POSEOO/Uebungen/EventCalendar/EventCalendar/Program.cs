using System;
using System.Diagnostics.CodeAnalysis;

namespace EventCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isDemoMode = !CheckIfImportMode(args);
            if (isDemoMode)
            {
                RunDemo();
            }
            else
            {
                RunNormal();
            }
        }

        private static void RunNormal()
        {
            throw new NotImplementedException();
        }

        private static void RunDemo()
        {
            throw new NotImplementedException();
        }

        private static bool CheckIfImportMode(string[] args)
        {
            return false;
        }
    }
}
