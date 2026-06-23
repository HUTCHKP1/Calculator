using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public struct MatrixData
    {
        public string name;
        public double a, b, c, d;
        // lab 21
    }
    internal class Matrix
    {
        static MatrixData[] matrices = new MatrixData[26];
        static int maxCount = 0;
    public static int FindMatrix(string name)
        {
            for (int i = 0; i < matrices.Length - 1; i++)
            {
                if (matrices[i].name == name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
