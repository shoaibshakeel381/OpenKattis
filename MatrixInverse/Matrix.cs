namespace MatrixInverse
{
    public partial class Program
    {
        private class Matrix
        {
            private int _a;
            private int _b;
            private int _c;
            private int _d;

            public Matrix(int a, int b, int c, int d)
            {
                _a = a;
                _b = b;
                _c = c;
                _d = d;
            }

            public Matrix Inverse()
            {
                // Find Matrix Determinent
                var determinent = _a * _d - _b * _c;

                // Calculate Inverse
                var bA = _d / determinent;
                var bB = -_b / determinent;
                var bC = -_c / determinent;
                var bD = _a / determinent;

                return new Matrix(bA, bB, bC, bD);
            }

            public override string ToString()
            {
                return _a + " " + _b + "\n" + _c + " " + _d;
            }
        }
    }
}
