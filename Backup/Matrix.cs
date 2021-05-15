using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron
{
    public class Matrix
    {
        float[,] data;
        int width, height;
        bool identity = false;

        public static Matrix Identity = new Matrix(true);

        public float this[int x, int y]
        {
            get 
            {
                if (identity)
                {
                    if (x == y) return 1;
                    return 0;
                }

                return data[x , y];
            }
            set 
            {
                if (identity) return;
                data[x, y] = value;
            }
        }

        public int Width 
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Matrix(int width, int height)
        { 
            data = new float[width , height];
            this.width = width;
            this.height = height;
        }

        public Matrix(Matrix mat)
        { 
            data = new float[mat.width , mat.height];
            this.width = mat.width;
            this.height = mat.height;
        }

        public Matrix(bool Identity)
        {
            identity = Identity;
        }

        public static Matrix operator *(Matrix a, float b)
        {
            Matrix res = new Matrix(a);

            for (int i = 0; i < res.width; i++)
            {
                for (int j = 0; j < res.height; j++)
                {
                    res[i, j] = a[i, j] * b;
                }
            }

            return res;
        }

        public static Matrix operator -(Matrix a, float b)
        {
            Matrix res = new Matrix(a);

            for (int i = 0; i < res.width; i++)
            {
                for (int j = 0; j < res.height; j++)
                {
                    res[i, j] = a[i, j] - b;
                }
            }

            return res;
        }

        public static Matrix operator *(float b , Matrix a)
        {
            Matrix res = new Matrix(a);

            for (int i = 0; i < res.width; i++)
            {
                for (int j = 0; j < res.height; j++)
                {
                    res[i, j] = a[i, j] * b;
                }
            }

            return res;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.width != b.height) throw new Exception("Матрицы не умножаемы!");

            Matrix res = new Matrix(b.width , a.height);

            for (int i = 0; i < res.width; i++)
            {
                for (int j = 0; j < res.height; j++)
                {
                    res[i, j] = 0;

                    for (int col = 0; col < a.width; col++)
                    {                  
                      res[i, j] += a[col, j] * b[i, col];                        
                    }
                }
            }

            return res;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a);

            for (int i = 0; i < res.width; i++)
            {
                for (int j = 0; j < res.height; j++)
                {
                    res[i, j] = a[i, j] + b[i, j];
                }
            }

            return res;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a);

            for (int i = 0; i < res.width; i++)
            {
                for (int j = 0; j < res.height; j++)
                {
                    res[i, j] = a[i, j] - b[i, j];
                }
            }

            return res;
        }

        public Matrix Transponation()
        {
            Matrix res = new Matrix(height , width);

            for (int i = 0; i < res.width; i++)
            {
                for (int j = 0; j < res.height; j++)
                {
                    res[i, j] = data[j , i];
                }
            }

            return res;
        }
        public delegate float F(float x);

        public Matrix SetFunction(F func)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    data[i, j] = func(data[i, j]);
                }
            }
            
            return this;
        }

        public bool IsSimmetric()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (data[i, j] != data[j, i]) return false;
                }
            }

            return true;
        }

        public void ToZeroMainLine()
        {
            for (int i = 0; i < width; i++) data[i, i] = 0;
        }

        public static Matrix operator/(Matrix a , float b)
        {
            Matrix res = new Matrix(a.width, a.height);

            for (int i = 0; i < res.width; i++)
            {
                for (int j = 0; j < res.height; j++)
                {
                    res[i, j] = a.data[i, j] / b;
                }
            }

            return res;
        }        
    }
}
