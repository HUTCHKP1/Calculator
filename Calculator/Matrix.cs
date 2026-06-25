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
        public static void DisplayMatrix(MatrixData m)
        {
            Console.WriteLine($"[ {m.a}  {m.b} ]");
            Console.WriteLine($"[ {m.c}  {m.d} ]");
        }
        // addMat: element-wise addition
        // [ a+e  b+f ]
        // [ c+g  d+h ]
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
        public static double Determinant(MatrixData m)
        {
            return (m.a * m.d) - (m.b * m.c);
        }
        // invMat: (1/det) * [ d  -b ]
        //                    [ -c   a ]
        // Only valid if determinant != 0
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
