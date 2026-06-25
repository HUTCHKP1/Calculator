using System;
namespace Calculator
{
    public struct MatrixData
    {
        public string name;
        public double a, b, c, d;
        // lab 21
    }
    public class Matrix
    {
        static MatrixData[] matrices = new MatrixData[26]; // matrices are named with single letters (mat a, mat b...) so 26 covers a–z. It's a reasonable max.
        static int maxCount = 0;

        /*
         * Difficulty: the matrix store is a fixed array so you can't use built-in search — and searching beyond maxCount would hit uninitialised null name fields and crash
         * Solution: loop only up to maxCount, return -1 if not found
         * Why: -1 as sentinel is a standard C# convention (same as string.IndexOf) — callers can check for it before calling GetMatrix
         */
        public static int FindMatrix(string name)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (matrices[i].name == name)
                {
                    return i;
                }
            }
            return -1;
        }
        // Returns the MatrixData at the given name
        // Used by Program.cs to retrieve stored matrices for operations
        public static MatrixData GetMatrix(string name)
        {
            int index = FindMatrix(name);
            return matrices[index]; // caller must check FindMatrix != -1 first
        }
        /*
         * Difficulty: re-defining a matrix with the same name should overwrite it, not create a duplicate
         * Solution: call FindMatrix first — if found, reuse that index; if not, use maxCount and increment it
         * Why: this keeps the array compact and means FindMatrix always returns the most recent definition
         */
        public static void StoreMatrix(string name, double a, double b, double c, double d)
        {
            // Check if this name already exists — overwrite if so
            int index = FindMatrix(name);
            if (index == -1)
            {
                index = maxCount;
                maxCount++;
            }
            matrices[index].name = name;
            matrices[index].a = a;
            matrices[index].b = b;
            matrices[index].c = c;
            matrices[index].d = d;
        }
        //Format method

        /*
         * Difficulty: matrices have a specific visual layout that plain Console.WriteLine doesn't produce
         * Solution: two WriteLine calls with interpolated strings showing the 2x2 grid format
         * Why: consistent display across all operations makes output readable and matches mathematical convention
         */
        public static void DisplayMatrix(MatrixData m)
        {
            Console.WriteLine($"[ {m.a}  {m.b} ]");
            Console.WriteLine($"[ {m.c}  {m.d} ]");
        }
        // addMat: element-wise addition
        // [ a+e  b+f ]
        // [ c+g  d+h ]

        /*
         * Difficulty: needing to return a new matrix without modifying the inputs
         * Solution: declare a new MatrixData result struct, assign each element independently
         * Why: structs in C# are value types — assigning directly to a new struct is the clean way to build a result without affecting the originals (Lab 21)
         */
        public static MatrixData AddMatrix(MatrixData m1, MatrixData m2)
        {
            MatrixData result = new MatrixData(); // declare local struct first
            result.a = m1.a + m2.a;
            result.b = m1.b + m2.b;
            result.c = m1.c + m2.c;
            result.d = m1.d + m2.d;
            return result;
        }
        // scalMat: multiply every element by scalar s

        /*
         * Difficulty: none significant — direct formula
         * Solution: multiply each element by s independently
         * Why: scalar multiplication is defined as applying the scalar to every element — doing each independently is the only correct approach for a 2x2 matrix
         */
        public static MatrixData ScalarMultiply(double s, MatrixData m)
        {
            MatrixData result = new MatrixData(); // declare local struct first
            result.a = s * m.a;
            result.b = s * m.b;
            result.c = s * m.c;
            result.d = s * m.d;
            return result;
        }
        // dotMat: matrix multiplication (NOT element-wise)
        // row × column:
        // [a b]   [e f]   [ae+bg  af+bh]
        // [c d] × [g h] = [ce+dg  cf+dh]

        /*
         * Difficulty: matrix multiplication is not element-wise — each result element is a dot product of a row and a column, which is easy to confuse with scalar multiplication
         * Solution: implement the row-times-column formula explicitly for all four elements
         * Why: for a fixed 2x2 matrix the four formulas are always the same — no loop needed, which is simpler and less error-prone than trying to generalise
         */
        public static MatrixData DotProduct(MatrixData m1, MatrixData m2)
        {
            MatrixData result = new MatrixData(); // declare local struct first
            result.a = m1.a * m2.a + m1.b * m2.c;
            result.b = m1.a * m2.b + m1.b * m2.d;
            result.c = m1.c * m2.a + m1.d * m2.c;
            result.d = m1.c * m2.b + m1.d * m2.d;
            return result;
        }
        // detMat: ad - bc

        /*
         * Difficulty: none significant — direct formula
         * Solution: (a×d) - (b×c)
         * Why: this is the definition of a 2x2 determinant 
         */
        public static double Determinant(MatrixData m)
        {
            return (m.a * m.d) - (m.b * m.c);
        }
        // invMat: (1/det) * [ d  -b ]
        //                    [ -c   a ]
        // Only valid if determinant != 0

        /*
         * Difficulty: the inverse is undefined when the determinant is 0 (singular matrix), and returning a meaningful error from a method that returns MatrixData is awkward
         * Solution: set result.name to "ERROR" and return early — Program.cs checks for this string before displaying
         * Why: throwing an exception would be the cleaner approach in production C#, but using a sentinel value keeps the error handling visible and within concepts covered in the lab book
         */
        public static MatrixData Inverse(MatrixData m)
        {
            MatrixData result = new MatrixData(); // declare local struct first
            double det = Determinant(m);
            if (det == 0)
            {
                // cannot invert — signal this somehow
                // simplest: set name to "ERROR" and check in Program.cs
                result.name = "ERROR";
                return result;
            }
            result.a = m.d / det;
            result.b = -m.b / det;
            result.c = -m.c / det;
            result.d = m.a / det;
            return result;
        }
    }
}
