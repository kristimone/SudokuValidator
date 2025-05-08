namespace SudokuValidator
{
    public class Program
    {
        public static void Main()
        {
            //given sudoku data structures

            int[][] goodSudoku1 =
            [
                [7, 8, 4, 1, 5, 9, 3, 2, 6],
                [5, 3, 9, 6, 7, 2, 8, 4, 1],
                [6, 1, 2, 4, 3, 8, 7, 5, 9],
                [9, 2, 8, 7, 1, 5, 4, 6, 3],
                [3, 5, 7, 8, 4, 6, 1, 9, 2],
                [4, 6, 1, 9, 2, 3, 5, 8, 7],
                [8, 7, 6, 3, 9, 4, 2, 1, 5],
                [2, 4, 3, 5, 6, 1, 9, 7, 8],
                [1, 9, 5, 2, 8, 7, 6, 3, 4]
            ];

            int[][] goodSudoku2 =
            [
                [1, 4, 2, 3],
                [3, 2, 4, 1],

                [4, 1, 3, 2],
                [2, 3, 1, 4]
            ];

            int[][] badSudoku1 =
            [
                [1, 2, 3, 4, 5, 6, 7, 8, 9],
                [1, 2, 3, 4, 5, 6, 7, 8, 9],
                [1, 2, 3, 4, 5, 6, 7, 8, 9],

                [1, 2, 3, 4, 5, 6, 7, 8, 9],
                [1, 2, 3, 4, 5, 6, 7, 8, 9],
                [1, 2, 3, 4, 5, 6, 7, 8, 9],

                [1, 2, 3, 4, 5, 6, 7, 8, 9],
                [1, 2, 3, 4, 5, 6, 7, 8, 9],
                [1, 2, 3, 4, 5, 6, 7, 8, 9]
            ];

            int[][] badSudoku2 =
            [
                [1, 2, 3, 4, 5],
                [1, 2, 3, 4],
                [1, 2, 3, 4],
                [1]
            ];

            try
            {
                ValidateSudoku(goodSudoku1);
                ValidateSudoku(goodSudoku2);
                ValidateSudoku(badSudoku1);
                ValidateSudoku(badSudoku2);
            }

            catch (Exception ex)
            {
                Console.Write("Error during Sudoku validation: " + ex);
            }
        }
        public static bool ValidateSudoku(int[][] board)
        {
            try
            {
                var n = CalculateDimension(board);

                if (HasInvalidRow(board, n)) 
                    return false;
                if (HasInvalidColumn(board, n)) 
                    return false;
                if (HasInvalidSquare(board, n)) 
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static int CalculateDimension(int[][] board)
        {
            if (board == null || board.Length == 0)
                throw new ArgumentException("Board cannot be null or empty.");

            var n = board.Length;

            if (board.Any(row => row.Length != n))
                throw new ArgumentException("Board must be NxN.");

            if (Math.Sqrt(n) % 1 != 0)
                throw new ArgumentException("Board size must have an integer square root.");

            return n;
        }

        private static bool HasInvalidRow(int[][] board, int n)
        {
            return board.Any(row => !HasUniqueNumbers(row, n));
        }

        private static bool HasInvalidColumn(int[][] board, int n)
        {
            return Enumerable.Range(0, n)
                .Any(col => !HasUniqueNumbers(board.Select(row => row[col]), n));
        }

        private static bool HasInvalidSquare(int[][] board, int n)
        {
            var blockSize = (int)Math.Sqrt(n);

            for (var row = 0; row < n; row += blockSize)
            {
                for (var col = 0; col < n; col += blockSize)
                {
                    var square = new List<int>();

                    for (var r = 0; r < blockSize; r++)
                    {
                        for (var c = 0; c < blockSize; c++)
                        {
                            square.Add(board[row + r][col + c]);
                        }
                    }

                    if (!HasUniqueNumbers(square, n))
                        return true;
                }
            }

            return false;
        }
        private static bool HasUniqueNumbers(IEnumerable<int> values, int n)
        {
            return values.ToHashSet().SetEquals(Enumerable.Range(1, n));
        }
    }
}