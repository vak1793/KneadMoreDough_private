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

        bool isValid(string str) {
            // Linearly search all words
            for (int i = 0; i < Ingredients.Length; i++)
                if (String.Compare(str, this.Ingredients[i]) == 0)
                    return true;
            return false;
        }

        void findWordsUtil(char[,] boggle, bool[,] visited, int M, int N, int i, int j, string str) {
            // Mark current cell as visited and append current character
            // to str
            Debug.WriteLine("(" + i + ", " + j + ")");

            visited[i, j] = true;
            str = str + boggle[i, j];

            // If str is present in dictionary, then print it
            if (isValid(str))
            {
                Debug.WriteLine(str);
            }

            // Traverse 8 adjacent cells of boggle[i][j]
            for (int row = i - 1; row <= i + 1 && row < M; row++)
                for (int col = j - 1; col <= j + 1 && col < N; col++)
                    if (row >= 0 && col >= 0 && !visited[row, col])
                        findWordsUtil(boggle, visited, M,N, row, col, str);

            // Erase current character from string and mark visited
            // of current cell as false
            str.Remove(str.Length - 1);
            visited[i, j] = false;
        }

        public void findWords()
        {
            int M = 15, N = 20;
            // Mark all characters as not visited
            bool[,] visited = {
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
                { false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true }
            };

            char[,] boggle = {
                { 'L', 'A', 'P', 'E', 'N', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { 'V', 'B', 'T', 'J', 'E', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { 'R', 'H', 'S', 'U', 'M', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { 'S', 'G', 'E', 'I', 'G', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { 'T', 'T', 'U', 'B', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { 'M', 'N', 'H', 'H', 'P', 'U', 'G', 'I', 'O', 'O', 'P', 'I', 'S', 'G', 'J', 'A', 'H', 'O', 'N', 'E' },
                { 'M', 'L', 'S', 'M', 'I', 'N', 'M', 'A', 'N', 'S', 'U', 'M', 'O', 'M', 'H', 'J', 'J', 'O', 'I', 'R' },
                { 'O', 'P', 'A', 'P', 'O', 'A', 'S', 'L', 'L', 'O', 'O', 'E', 'N', 'H', 'I', 'O', 'S', 'M', 'G', 'A' },
                { 'R', 'V', 'U', 'B', 'P', 'E', 'J', 'R', 'M', 'E', 'R', 'B', 'H', 'M', 'O', 'O', 'G', 'S', 'I', 'E' },
                { 'A', 'P', 'S', 'I', 'U', 'P', 'V', 'I', 'I', 'E', 'N', 'E', 'B', 'T', 'T', 'P', 'O', 'R', 'P', 'O' },
                { 'B', 'B', 'A', 'R', 'M', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { 'V', 'B', 'G', 'O', 'M', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { 'E', 'O', 'E', 'L', 'S', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { 'L', 'P', 'L', 'H', 'S', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                { 'M', 'L', 'P', 'O', 'O', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
            };

            // Initialize current string
            string str = "";
 
            // Consider every character and look for all words
            // starting with this character
            for (int i = 0; i < M; i++)
               for (int j = 0; j < N; j++)
                     findWordsUtil(boggle, visited, M, N, i, j, str);
        }
}
}
