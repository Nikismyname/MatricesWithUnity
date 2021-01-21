namespace Assets
{
    using System.Text;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        void Start()
        {
            float[][] matId = new float[][] {
                new float [] {1,0,0},
                new float [] {0,1,0},
                new float [] {0,0,1}};

            float[][] mat2 = new float[][] {
                new float [] {1,2,3},
                new float [] {4,5,6},
                new float [] {7,8,9}};

            float[][] vecDown = new float[][]
            {
                new float[] { 2 },
                new float[] { 3 },
                new float[] { 4 },
            };

            float[][] vecRight = new float[][] { new float[] { 2, 3, 4 } };

            // adding and subtracting matrices only defined for matrices of the same demantions
            // 1) You can only multiply two matrices if the number of columns on the left-hand side matrix is equal to the number of rows on the right - hand side matrix.
            // 2) Matrix multiplication is not commutative that is A⋅B≠B⋅A.

            //MutiplyMatrices(matId, vecDown);
            //MutiplyMatrices(vecRight, matId);

            float[][] vec2d = new float[][]
            {
                new float[] { 2 },
                new float[] { 2 },
            };

            float[][] rotation = new float[][]
            {
                new float[] {  0 , 1 },
                new float[] { -1 , 0 },
            };

            float[][] shear = new float[][]
            {
                new float[] { 1 , 0 },
                new float[] { 0 , 2 },
            };

            float[][] shear2 = new float[][] // the order of the unit vectors does not matter in 2d
            {
                new float[] { 0, 1 },
                new float[] { 2, 0 },
            };

            ShearRotation(rotation, vec2d, shear);
        }

        public void RotationShear(float[][] rotation, float[][] vec2d, float[][]  shear)
        {
            var rotv = MultiplyMatrices(rotation, vec2d);
            var shearVect = MultiplyMatrices(shear, rotv);
            Draw(vec2d, Color.green);
            Draw(shearVect, Color.red);
        }

        public void ShearRotation(float[][] rotation, float[][] vec2d, float[][] shear)
        {
            var shearVect = MultiplyMatrices(shear, vec2d);
            var rotv = MultiplyMatrices(rotation, shearVect);
            Draw(vec2d, Color.green);
            Draw(rotv, Color.red);
        }

        public void Draw(float[][] vector, Color color)
        {
            Debug.DrawLine(Vector3.zero, new Vector3(vector[1][0], vector[0][0], 0), color, 30000);
        }

        public float[][] MultiplyMatrices(float[][] left, float[][] right)
        {
            if (left[0].Length != right.Length)
            {
                Debug.LogError("the number of columns on the left-hand side matrix should equal to the number of rows on the right");
                return null;
            }

            int multLength = right.Length;
            int resultXlength = right[0].Length;
            int resultYLength = left.Length;

            // initing the result
            var result = new float[resultYLength][];
            for (int i = 0; i < resultYLength; i++)
            {
                result[i] = new float[resultXlength];
            }

            for (int y = 0; y < resultYLength; y++)
            {
                for (int x = 0; x < resultXlength; x++)
                {
                    float res = 0;

                    for (int sss = 0; sss < multLength; sss++)
                    {
                        res += left[y][sss] * right[sss][x];
                    }

                    result[y][x] = res;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Click to view result!");

            for (int y = 0; y < resultYLength; y++)
            {
                for (int x = 0; x < resultXlength; x++)
                {
                    sb.Append(" " + result[y][x]);
                }
                sb.AppendLine();
            }

            Debug.Log(sb.ToString());
            return result;
        }
    }
}
