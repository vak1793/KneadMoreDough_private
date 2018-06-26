using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastSliceUWP.Models
{
    class Solver
    {
        public string[] Ingredients = {
            "ANCHOVY",
            "BACON",
            "CHEESE",
            "GARLIC",
            "GREENPEPPER",
            "HABANERO",
            "JALAPENO",
            "MUSHROOM",
            "OLIVE",
            "ONION",
            "PINEAPPLE",
            "PEPPERONI",
            "SAUSAGE",
            "TOMATOES"
        };

        bool isValidSubstring(string str)
        {
            // Linearly search all words
            for (int i = 0; i < Ingredients.Length; i++)
            {
                if (str.Length > Ingredients[i].Length)
                    continue;
                if (String.Compare(str, this.Ingredients[i].Substring(0, str.Length)) == 0)
                    return true;
            }
            return false;
        }

        bool isValidString(string str)
        {
            // Linearly search all words
            for (int i = 0; i < Ingredients.Length; i++)
                if (String.Compare(str, this.Ingredients[i]) == 0)
                    return true;
            return false;
        }

        string getDirection(int row, int col, int i, int j)
        {
            int rowDiff = row - i + 2,
                colDiff = col - j + 6;
            string direction = "";

            switch (rowDiff * colDiff)
            {
                case 5:
                    direction = "NW";
                    break;
                case 6:
                    direction = "N";
                    break;
                case 7:
                    direction = "NE";
                    break;
                case 10:
                    direction = "W";
                    break;
                case 14:
                    direction = "E";
                    break;
                case 15:
                    direction = "SW";
                    break;
                case 18:
                    direction = "S";
                    break;
                case 21:
                    direction = "SE";
                    break;
            }

            return direction;
        }

        void findWordsUtil(char[][] boggle, bool[][] visited, int M, int N, int i, int j, string str)
        {
            // Mark current cell as visited and append current character
            // to str
            // Console.WriteLine("(" + i + ", " + j + ")");

            visited[i][j] = true;
            str = str + boggle[i][j];
            //Console.WriteLine("(" + i + ", " + j + ") = " + str);

            // If str is present in dictionary, then print it
            if (isValidString(str))
                Console.WriteLine(str);

            // Traverse 8 adjacent cells of boggle[i][j]
            for (int row = i - 1; row <= i + 1 && row < M; row++)
                for (int col = j - 1; col <= j + 1 && col < N; col++)
                    if (row >= 0 && col >= 0 && !visited[row][col] && isValidSubstring(str + boggle[row][col]))
                        {
                            findWordsUtil(
                                boggle,
                                visited,
                                M,
                                N,
                                row,
                                col,
                                str,
                                dir + getDirection(row, col, i, j)
                            );
                        }

            // Erase current character from string and mark visited
            // of current cell as false
            str.Remove(str.Length - 1);
            visited[i][j] = false;
        }

        public void findWords()
        {
            int M = 15, N = 20;
            // Mark all characters as not visited
            bool[][] visited = {
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true }
            };

            char[][] boggle = {
            new char[] { 'L', 'A', 'P', 'E', 'N', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            new char[] { 'V', 'B', 'T', 'J', 'E', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            new char[] { 'R', 'H', 'S', 'U', 'M', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            new char[] { 'S', 'G', 'E', 'I', 'G', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            new char[] { 'T', 'T', 'U', 'B', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            new char[] { 'M', 'N', 'H', 'H', 'P', 'U', 'G', 'I', 'O', 'O', 'P', 'I', 'S', 'G', 'J', 'A', 'H', 'O', 'N', 'E' },
            new char[] { 'M', 'L', 'S', 'M', 'I', 'N', 'M', 'A', 'N', 'S', 'U', 'M', 'O', 'M', 'H', 'J', 'J', 'O', 'I', 'R' },
            new char[] { 'O', 'P', 'A', 'P', 'O', 'A', 'S', 'L', 'L', 'O', 'O', 'E', 'N', 'H', 'I', 'O', 'S', 'M', 'G', 'A' },
            new char[] { 'R', 'V', 'U', 'B', 'P', 'E', 'J', 'R', 'M', 'E', 'R', 'B', 'H', 'M', 'O', 'O', 'G', 'S', 'I', 'E' },
            new char[] { 'A', 'P', 'S', 'I', 'U', 'P', 'V', 'I', 'I', 'E', 'N', 'E', 'B', 'T', 'T', 'P', 'O', 'R', 'P', 'O' },
            new char[] { 'B', 'B', 'A', 'R', 'M', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            new char[] { 'V', 'B', 'G', 'O', 'M', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            new char[] { 'E', 'O', 'E', 'L', 'S', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            new char[] { 'L', 'P', 'L', 'H', 'S', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            new char[] { 'M', 'L', 'P', 'O', 'O', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
        };


            // for (int i = 0; i < 15; i++)
            // {
            //    for (int j = 0; j < 15; j++)
            //        Console.Write(boggle[i][j] + " ");
            //    Console.WriteLine("");
            // }

            Initialize current string
            string str = "";

            // Consider every character and look for all words
            // starting with this character
            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    findWordsUtil(boggle, visited, M, N, i, j, str);
        }
    }
}
