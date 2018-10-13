﻿using System;
using System.IO;
using System.Threading;

namespace Data_protection
{
    internal static class Utils
    {
        public const string EngAlphabet = "abcdefghijklmnopqrstuvwxyz1234567890.,\"()!?:-_$";
        public const string CyrAlphabet = "абвгдеєжзиіїйклмнопрстуфхцчшщюяь1234567890.,\"()!?:-'_";

        public static int[][] ReverseMatrix(int[][] matrix, int mod)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (matrix.Length == 0)
                throw new ArgumentException(@"Value cannot be an empty collection.", nameof(matrix));
            var result = new int[matrix.Length][];
            var determinant = Determinant(matrix);
            if (determinant.Equals(0))
            {
                throw new ArgumentException("Determinant is 0. Reverse matrix can not be found.");
            }

            determinant = ReverseElement((int) determinant, mod);

            for (var i = 0; i < matrix.Length; i++)
            {
                result[i] = new int[matrix.Length];
                for (var j = 0; j < matrix.Length; j++)
                {
                    result[i][j] = (int) ((Math.Pow(-1, j + i) + Minor(matrix, j, i)) * determinant) % mod;
                }
            }

            return result;
        }

        private static int ReverseElement(int element, int mod)
        {
            if (Gcd(element, mod) != 1)
            {
                throw new InvalidDataException("Can not calculate reverse element for " + element.ToString() + " and " +
                                               mod.ToString());
            }

            if (element < 0)
            {
                while (element < 0)
                {
                    element += mod;
                }
            }

            return (mod + 1) / element;
        }

        public static float Determinant(int[][] matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (matrix.Length == 0)
                throw new ArgumentException(@"Value cannot be an empty collection.", nameof(matrix));
            var m1 = new float[matrix.Length][];
            for (var i = 0; i < matrix.Length; i++)
            {
                m1[i] = new float[matrix.Length];
                for (var j = 0; j < matrix.Length; j++)
                {
                    m1[i][j] = matrix[i][j];
                }
            }

            var swapped = false;
            for (var i = 0; i < m1.Length; i++)
            {
                for (var j = i + 1; j < m1.Length; j++)
                {
                    if (m1[i][i].Equals(0))
                    {
                        swap_rows(m1, i);
                        swapped = !swapped;
                    }

                    var r = m1[j][i] / m1[i][i];
                    for (var k = i; k < m1.Length; k++)
                    {
                        var res = m1[i][k] * r;
                        m1[j][k] -= res;
                    }
                }
            }


            var determinant = 1.0;
            for (var i = 0; i < m1.Length; i++)
            {
                determinant *= m1[i][i];
            }

            if (swapped)
            {
                determinant *= -1;
            }

            return (float) determinant;
        }

        public static int[][] MultiplyMatrices(int[][] matrixA, int[][] matrixB, int mod)
        {
            if (matrixA == null) throw new ArgumentNullException(nameof(matrixA));
            if (matrixA.Length == 0)
                throw new ArgumentException(@"Value cannot be an empty collection.", nameof(matrixA));
            if (matrixB == null) throw new ArgumentNullException(nameof(matrixB));
            if (matrixB.Length == 0)
                throw new ArgumentException(@"Value cannot be an empty collection.", nameof(matrixB));
            if (matrixA[0].Length != matrixB.Length)
            {
                throw new InvalidDataException("Attempted multiplying incompatible matrices");
            }

            var resultMatrix = new int[matrixA.Length][];
            for (var i = 0; i < matrixA.Length; i++)
            {
                resultMatrix[i] = new int[matrixB[0].Length];
                for (var j = 0; j < matrixB[0].Length; j++)
                {
                    for (var k = 0; k < matrixB.Length; k++)
                    {
                        resultMatrix[i][j] += matrixA[i][k] * matrixB[k][j];
                    }

                    resultMatrix[i][j] = resultMatrix[i][j] % mod;
                }
            }

            return resultMatrix;
        }

        private static void swap_rows(float[][] matrixA, int position)
        {
            if (matrixA == null) throw new ArgumentNullException(nameof(matrixA));
            for (var i = position + 1; i < matrixA.Length; i++)
            {
                if (matrixA[i][position].Equals(0)) continue;
                var swapRow = matrixA[i];
                matrixA[i] = matrixA[position];
                matrixA[position] = swapRow;
                break;
            }
        }

        public static int Gcd(int m, int n)
        {
            while (n!=0)
            {
                var t = n;
                n = m % n;
                m = t;
            }

            return Math.Abs(m);
            /*while (true)
            {
                if (m == 0)
                {
                    return n;
                }

                if (n == 0)
                {
                    return m;
                }

                if (m == 1 || n == 1)
                {
                    return 1;
                }

                if (m % 2 == 0 && n % 2 == 0)
                {
                    return 2 * Gcd(m / 2, n / 2);
                }

                if (m % 2 == 0 && n % 2 == 1)
                {
                    m = m / 2;
                    continue;
                }

                if (m % 2 == 1 && n % 2 == 0)
                {
                    n = n / 2;
                    continue;
                }

                if (m % 2 == 1 && n % 2 == 1 && n > m)
                {
                    var m1 = m;
                    m = (n - m) / 2;
                    n = m1;
                    continue;
                }

                if (m % 2 == 1 && n % 2 == 1 && m > n)
                {
                    m = (m - n) / 2;
                    continue;
                }

                
            }*/
            throw new InvalidDataException("Error while calculating greatest comon divisor for m:" + m + " n: " + n);

        }

        private static float Minor(int[][] matrix, int i, int j)
        {
            var minorMatrix = new int[matrix.Length - 1][];
            for (var k = 0; k < matrix.Length - 1; k++)
            {
                minorMatrix[k] = new int[matrix.Length - 1];
            }

            for (var k = 0; k < matrix.Length; k++)
            {
                if (k == i) continue;
                var k1 = 0;
                if (k < i)
                {
                    k1 = k;
                }

                if (k > i)
                {
                    k1 = k - 1;
                }

                for (var l = 0; l < matrix.Length; l++)
                {
                    if (l == j) continue;
                    var l1 = 0;
                    if (l < j)
                    {
                        l1 = l;
                    }

                    if (l > j)
                    {
                        l1 = l - 1;
                    }

                    minorMatrix[k1][l1] = matrix[k][l];
                }
            }

            return Determinant(minorMatrix);
        }
    }
}