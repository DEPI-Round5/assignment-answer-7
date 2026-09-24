using System;

namespace OOPAssignment04
{
    // =========================================================================
    // FIRST PROJECT: 3D Point Class & Interfaces
    // =========================================================================
    
    public class Point3D : IComparable<Point3D>, ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        // Constructors with chaining
        public Point3D() : this(0, 0, 0) { }

        public Point3D(int x) : this(x, 0, 0) { }

        public Point3D(int x, int y) : this(x, y, 0) { }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Question 2: Override ToString
        public override string ToString()
        {
            return $"Point Coordinates: ({X}, {Y}, {Z})";
        }

        // Question 4: Override Equals and GetHashCode so == works logically
        public override bool Equals(object obj)
        {
            if (obj is Point3D p)
            {
                return X == p.X && Y == p.Y && Z == p.Z;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Point3D p1, Point3D p2)
        {
            if (ReferenceEquals(p1, p2)) return true;
            if (p1 is null || p2 is null) return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Point3D p1, Point3D p2)
        {
            return !(p1 == p2);
        }

        // Question 5: Implement IComparable to sort by X then Y
        public int CompareTo(Point3D other)
        {
            if (other == null) return 1;

            if (X != other.X)
            {
                return X.CompareTo(other.X);
            }
            return Y.CompareTo(other.Y);
        }

        // Question 6: Implement ICloneable
        public object Clone()
        {
            return new Point3D(X, Y, Z);
        }
    }


    // =========================================================================
    // SECOND PROJECT: Maths Class (Static Methods)
    // =========================================================================

    public static class Maths
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("Cannot divide by zero!");
                return 0;
            }
            return a / b;
        }
    }


    // =========================================================================
    // THIRD PROJECT: Duration Class & Operator Overloading
    // =========================================================================

    public class Duration
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        // Helper property to convert total duration into seconds
        public int TotalSeconds => (Hours * 3600) + (Minutes * 60) + Seconds;

        // Constructors
        public Duration(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Normalize();
        }

        public Duration(int totalSeconds)
        {
            Hours = totalSeconds / 3600;
            totalSeconds %= 3600;
            Minutes = totalSeconds / 60;
            Seconds = totalSeconds % 60;
        }

        private void Normalize()
        {
            int total = TotalSeconds;
            if (total < 0) total = 0;

            Hours = total / 3600;
            total %= 3600;
            Minutes = total / 60;
            Seconds = total % 60;
        }

        // Override System.Object members
        public override string ToString()
        {
            if (Hours > 0)
            {
                return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";
            }
            return $"Minutes :{Minutes}, Seconds :{Seconds}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Duration d)
            {
                return TotalSeconds == d.TotalSeconds;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return TotalSeconds.GetHashCode();
        }

        // Operator Overloading
        public static Duration operator +(Duration d1, Duration d2)
        {
            int s1 = d1?.TotalSeconds ?? 0;
            int s2 = d2?.TotalSeconds ?? 0;
            return new Duration(s1 + s2);
        }

        public static Duration operator +(Duration d, int seconds)
        {
            int s = d?.TotalSeconds ?? 0;
            return new Duration(s + seconds);
        }

        public static Duration operator +(int seconds, Duration d)
        {
            return d + seconds;
        }

        public static Duration operator -(Duration d1, Duration d2)
        {
            int s1 = d1?.TotalSeconds ?? 0;
            int s2 = d2?.TotalSeconds ?? 0;
            return new Duration(Math.Max(0, s1 - s2));
        }

        public static Duration operator ++(Duration d)
        {
            // Increase One Minute (60 seconds)
            int s = (d?.TotalSeconds ?? 0) + 60;
            return new Duration(s);
        }

        public static Duration operator --(Duration d)
        {
            // Decrease One Minute (60 seconds)
            int s = Math.Max(0, (d?.TotalSeconds ?? 0) - 60);
            return new Duration(s);
        }

        public static bool operator >(Duration d1, Duration d2)
        {
            int s1 = d1?.TotalSeconds ?? 0;
            int s2 = d2?.TotalSeconds ?? 0;
            return s1 > s2;
        }

        public static bool operator <(Duration d1, Duration d2)
        {
            int s1 = d1?.TotalSeconds ?? 0;
            int s2 = d2?.TotalSeconds ?? 0;
            return s1 < s2;
        }

        public static bool operator >=(Duration d1, Duration d2)
        {
            return !(d1 < d2);
        }

        public static bool operator <=(Duration d1, Duration d2)
        {
            return !(d1 > d2);
        }

        public static implicit operator bool(Duration d)
        {
            return d != null && d.TotalSeconds > 0;
        }

        public static explicit operator DateTime(Duration d)
        {
            DateTime now = DateTime.Now;
            if (d == null) return now.Date;
            return new DateTime(now.Year, now.Month, now.Day, Math.Min(d.Hours, 23), d.Minutes, d.Seconds);
        }
    }


    // =========================================================================
    // MAIN APPLICATION PROGRAM
    // =========================================================================

    internal class Program
    {
        static void Main(string[] args)
        {
            // =========================================================================
            // FIRST PROJECT DEMO
            // =========================================================================
            Console.WriteLine("=========================================================================");
            Console.WriteLine("                           FIRST PROJECT: Point3D                        ");
            Console.WriteLine("=========================================================================");

            // Question 2
            Point3D P = new Point3D(10, 10, 10);
            Console.WriteLine(P.ToString());

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // Question 3: Read 2 points from user
            Point3D P1 = ReadPointFromUser("P1");
            Point3D P2 = ReadPointFromUser("P2");

            Console.WriteLine($"P1 = {P1}");
            Console.WriteLine($"P2 = {P2}");

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // Question 4: Check if P1 == P2
            Console.WriteLine("Testing (P1 == P2):");
            if (P1 == P2)
            {
                Console.WriteLine("P1 and P2 are EQUAL.");
            }
            else
            {
                Console.WriteLine("P1 and P2 are NOT EQUAL.");
            }
            Console.WriteLine("Explanation: By default, == compares memory references for classes. We overloaded == and Equals so it compares coordinates.");

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // Question 5: Sorting Array of Points
            Point3D[] pointArray = new Point3D[]
            {
                new Point3D(5, 10, 2),
                new Point3D(2, 20, 1),
                new Point3D(5, 3, 8),
                new Point3D(1, 15, 0)
            };

            Console.WriteLine("Array Before Sorting:");
            foreach (var pt in pointArray) Console.WriteLine(pt);

            Array.Sort(pointArray);

            Console.WriteLine("\nArray After Sorting (by X then Y):");
            foreach (var pt in pointArray) Console.WriteLine(pt);

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // Question 6: Cloning Point
            Point3D P_Cloned = (Point3D)P1.Clone();
            Console.WriteLine($"Original P1: {P1}");
            Console.WriteLine($"Cloned Point: {P_Cloned}");


            // =========================================================================
            // SECOND PROJECT DEMO
            // =========================================================================
            Console.WriteLine("\n\n=========================================================================");
            Console.WriteLine("                           SECOND PROJECT: Maths                         ");
            Console.WriteLine("=========================================================================");

            double num1 = 20, num2 = 5;
            Console.WriteLine($"Maths.Add({num1}, {num2})      = " + Maths.Add(num1, num2));
            Console.WriteLine($"Maths.Subtract({num1}, {num2}) = " + Maths.Subtract(num1, num2));
            Console.WriteLine($"Maths.Multiply({num1}, {num2}) = " + Maths.Multiply(num1, num2));
            Console.WriteLine($"Maths.Divide({num1}, {num2})   = " + Maths.Divide(num1, num2));


            // =========================================================================
            // THIRD PROJECT DEMO
            // =========================================================================
            Console.WriteLine("\n\n=========================================================================");
            Console.WriteLine("                           THIRD PROJECT: Duration                      ");
            Console.WriteLine("=========================================================================");

            Duration D1 = new Duration(1, 10, 15);
            Console.WriteLine("D1: " + D1.ToString());

            Duration D1_sec = new Duration(3600);
            Console.WriteLine("D1_sec: " + D1_sec.ToString());

            Duration D2 = new Duration(7800);
            Console.WriteLine("D2: " + D2.ToString());

            Duration D3 = new Duration(666);
            Console.WriteLine("D3: " + D3.ToString());

            Console.WriteLine("\n-------------------------------------------------------------------------\n");
            Console.WriteLine("Testing Operators Overloading:");

            D3 = D1 + D2;
            Console.WriteLine("D3 = D1 + D2  => " + D3);

            D3 = D1 + 7800;
            Console.WriteLine("D3 = D1 + 7800 => " + D3);

            D3 = 666 + D3;
            Console.WriteLine("D3 = 666 + D3 => " + D3);

            D3 = ++D1;
            Console.WriteLine("D3 = ++D1     => " + D3);

            D3 = --D2;
            Console.WriteLine("D3 = --D2     => " + D3);

            D1 = D1 - D2;
            Console.WriteLine("D1 = D1 - D2  => " + D1);

            Console.WriteLine("If (D1 > D2)  => " + (D1 > D2));
            Console.WriteLine("If (D1 <= D2) => " + (D1 <= D2));

            if (D1)
            {
                Console.WriteLine("D1 evaluates to True (has valid time > 0)");
            }
            else
            {
                Console.WriteLine("D1 evaluates to False (0 seconds)");
            }

            DateTime dt = (DateTime)D1;
            Console.WriteLine("DateTime Obj  => " + dt.ToString("yyyy-MM-dd HH:mm:ss"));

            Console.WriteLine("\n=========================================================================");
            Console.WriteLine("End of Assignment 04 Solution");
            Console.WriteLine("=========================================================================");

            Console.ReadLine(); // Keeps console window open
        }

        // Helper Method for Question 3 in First Project
        static Point3D ReadPointFromUser(string pointName)
        {
            Console.WriteLine($"Enter coordinates for {pointName}:");
            int x = ReadIntWithTryGet("X");
            int y = ReadIntWithParse("Y");
            int z = ReadIntWithConvert("Z");

            return new Point3D(x, y, z);
        }

        static int ReadIntWithTryGet(string coordName)
        {
            int val;
            while (true)
            {
                Console.Write($"  Enter {coordName} (using tryParse): ");
                if (int.TryParse(Console.ReadLine(), out val))
                    return val;
                Console.WriteLine("  Invalid integer. Try again.");
            }
        }

        static int ReadIntWithParse(string coordName)
        {
            while (true)
            {
                try
                {
                    Console.Write($"  Enter {coordName} (using Parse): ");
                    return int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("  Invalid integer. Try again.");
                }
            }
        }

        static int ReadIntWithConvert(string coordName)
        {
            while (true)
            {
                try
                {
                    Console.Write($"  Enter {coordName} (using Convert): ");
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("  Invalid integer. Try again.");
                }
            }
        }
    }
}