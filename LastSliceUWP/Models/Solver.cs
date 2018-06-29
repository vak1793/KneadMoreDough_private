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
            "FROG",
            "GARLIC",
            "GREENPEPPERS",
            "HABENERO",
            "JALAPENO",
            "MUSHROOMS",
            "OLIVES",
            "ONIONS",
            "PINEAPPLE",
            "PEPPERONI",
            "SAUSAGE",
            "TOMATOES",
            // not sure
            //"MOZZARELLA",
            //"PROVOLONE",
            //"FETA",
            //"CHEDDAR",
            //"PARMESAN",
            //"ROMANO",
            //"ASIAGO",
            //"BEEF",
            //"STEAK",
            //"CHICKEN",
            //"HAM",
            //"PORK",
            //"SALAMI",
            //"SPINACH",
            //"TUNA",
            //"PEPPERONCINI",
            //"BITCOIN",
            //"CORN",
            //"CAYENNE",
            //"MEATBALLS",
            //"BROCCOLI",
            //"ARTICHOKES",
            //"SAGE",
            //"PROSCIUTTO",
            //"OREGANO",
            //"MARINARA",
            //"CRUST",
            //"TURKEY",
            //"CAJUN",
            //"SAUCE",
            //"SHRIMP",
            //"PRAWNS",
        };

        string[] shortIngredients = new string[] {
            "AN",
            "BA",
            "CH",
            "GA",
            "GB",
            "HB",
            "JP",
            "MR",
            "OL",
            "ON",
            "PA",
            "PP",
            "SA",
            "TM"
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

        bool singleDirection(char[][] grid, Word word)
        {

            string direction = word.direction;
            int i = word.y, j = word.x;
            string[] steps = direction.Split(',');
            string lastStep = steps[0];

            for(int n=0; n < steps.Length; n++)
            {
                bool sideChanged;
                var nextPos = moveInDirection(steps[n], j, i, out sideChanged);
                if (sideChanged && n < steps.Length - 1)
                {
                    // reverse steps[n + 1]
                    bool temp;
                    string reverseDir = reverse(steps[n + 1]);
                    var reversePos = moveInDirection(reverseDir, nextPos[0], nextPos[1], out temp);
                    if (reversePos[0] != j || reversePos[1] != i)
                        return false;
                    i = nextPos[1];
                    j = nextPos[0];
                    lastStep = steps[n + 1];
                }
                else
                {
                    if (steps[n] != lastStep)
                        return false;
                    i = nextPos[1];
                    j = nextPos[0];
                    lastStep = steps[n];
                }
            }
            
            return true;
        }

        string reverse(string dir)
        {
            char[] letters = dir.ToCharArray();

            for(int i=0;i< letters.Length; i++)
                switch(letters[i])
                {
                    case 'N':
                        letters[i] = 'S';
                        break;
                    case 'S':
                        letters[i] = 'N';
                        break;
                    case 'E':
                        letters[i] = 'W';
                        break;
                    case 'W':
                        letters[i] = 'E';
                        break;
                }
            
            return new string(letters);
        }

        int[] moveInDirection(string direction, int x, int y, out bool sideChanged)
        {
            int newX = x, newY = y;
            sideChanged = false;

            switch(direction)
            {
                case "N":
                    newY--;
                    break;
                case "S":
                    newY++;
                    break;
                case "E":
                    newX++;
                    break;
                case "W":
                    newX--;
                    break;
                case "NE":
                    newY--;
                    newX++;
                    break;
                case "NW":
                    newY--;
                    newX--;
                    break;
                case "SE":
                    newY++;
                    newX++;
                    break;
                case "SW":
                    newY++;
                    newX--;
                    break;
            }

            if(newX < 0 || newX > 19)
            {
                sideChanged = true;
            } else if(newY < 0 || newY > 14)
            {
                sideChanged = true;
            } else if(newX > 4 && newX < 20 && ((newY >= 0 && newY < 5) || (newY > 9 && newY < 15)))
            {
                sideChanged = true;
            }

            // if side changed, wrap x and y
            if(sideChanged)
            {
                var nextCell = wrapCells(newY, newX);
                newY = nextCell[0];
                newX = nextCell[1];
            }
            
            return new int[] { newX, newY };
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

            return direction + ",";
        }

        void findWordsUtil(ref List<Word> found, ref List<string> toppings, char[][] boggle, bool[][] visited, int M, int N, int i, int j, string str, string dir, int x, int y)
        {
            visited[i][j] = true;
            str = str + boggle[i][j];

            if (isValidString(str))
            {
                Word word = new Word();
                word.x = y;
                word.y = x;
                word.word = str;
                word.direction = dir.Substring(0, dir.Length - 1);
                found.Add(word);
                toppings.Add(str);
                //Debug.WriteLine(str);
                //Debug.WriteLine("({0},{1})", y, x);
                //Debug.WriteLine(word.direction);
            }

            // Traverse 8 adjacent cells of boggle[i][j]
            List<Dictionary<string, Dictionary<int, int>>> adjacentCells = getAdjacentCells(i, j);

            foreach(var cell in adjacentCells)
            {
                foreach(string nextDir in cell.Keys)
                {
                    foreach(KeyValuePair<int, int> pair in cell[nextDir])
                    {
                        if (!visited[pair.Key][pair.Value] && isValidSubstring(str + boggle[pair.Key][pair.Value]))
                            findWordsUtil(
                                ref found,
                                ref toppings,
                                boggle,
                                visited,
                                M,
                                N,
                                pair.Key,
                                pair.Value,
                                str,
                                dir + nextDir,
                                x,
                                y
                            );
                    }
                }
            }

            str.Remove(str.Length - 1);
            visited[i][j] = false;
        }

        List<Dictionary<string, Dictionary<int, int>>> getAdjacentCells(int i, int j) {
            List<Dictionary<string, Dictionary<int, int>>> cells = new List<Dictionary<string, Dictionary<int, int>>>();

            for (int row = i - 1; row <= i + 1; row++)
                for (int col = j - 1; col <= j + 1; col++)
                {
                    string dir = getDirection(row, col, i, j);
                    Dictionary<int, int> cell = new Dictionary<int, int>();

                    int rowToAdd, colToAdd;
                    var nextCell = wrapCells(row, col);
                    rowToAdd = nextCell[0];
                    colToAdd = nextCell[1];


                    if (rowToAdd >=0 && rowToAdd < 15 && colToAdd >=0 && colToAdd < 20)
                    {
                        cell.Add(rowToAdd, colToAdd);
                        var cellWithDir = new Dictionary<string, Dictionary<int, int>>();
                        cellWithDir.Add(dir, cell);
                        cells.Add(cellWithDir);
                    }
                }

            return cells;
        }

        public int[] wrapCells(int row, int col)
        {
            int rowToAdd = row, colToAdd = col;

            if (col == -1)
            {
                if (row >= 0 && row < 5)
                {
                    // r = 5, c = 15 + row
                    rowToAdd = 5;
                    colToAdd = row + 15;
                }
                else if (row >= 5 && row < 10)
                {
                    // r = row, c = 19
                    rowToAdd = row;
                    colToAdd = 19;
                }
                else if (row >= 10 && row < 15)
                {
                    // r = 9, c = 29 - row
                    rowToAdd = 9;
                    colToAdd = 29 - row;
                }
            }
            else if (col == 5)
            {
                if (row >= 0 && row < 5)
                {
                    // r = 5, c = 9 - row
                    rowToAdd = 5;
                    colToAdd = 9 - row;
                }
                else if (row >= 10 && row < 15)
                {
                    // r = 9, c = row - 5
                    rowToAdd = 9;
                    colToAdd = row - 5;
                }
            }
            else if (col == 20)
            {
                if (row >= 5 && row < 10)
                {
                    rowToAdd = row;
                    colToAdd = 0;
                }
            }

            if (row == -1)
            {
                if (col >= 0 && col < 5)
                {
                    // r = 5, c = 14 - col
                    rowToAdd = 5;
                    colToAdd = 14 - col;
                }
            }
            else if (row == 4)
            {
                if (col >= 5 && col < 10)
                {
                    //r = 9 - col, c = 4
                    rowToAdd = 9 - col;
                    colToAdd = 4;
                }
                else if (col >= 10 && col < 15)
                {
                    //r = 0, c = 14 - col
                    rowToAdd = 0;
                    colToAdd = 14 - col;
                }
                else if (col >= 15 && col < 20)
                {
                    //r = 15 - col, col = 0
                    rowToAdd = 15 - col;
                    colToAdd = 0;
                }
            }
            else if (row == 10)
            {
                if (col >= 5 && col < 10)
                {
                    //r = col + 5, c = 4
                    rowToAdd = col + 5;
                    colToAdd = 4;
                }
                else if (col >= 10 && col < 15)
                {
                    //r = 14, c = 14 - col
                    rowToAdd = 14;
                    colToAdd = 14 - col;
                }
                else if (col >= 15 && col < 20)
                {
                    //r = 29 - col, col = 0
                    rowToAdd = 29 - col;
                    colToAdd = 0;
                }
            }
            else if (row == 15)
            {
                if (col >= 0 && col < 5)
                {
                    //r = 0, c = col
                    rowToAdd = 0;
                    colToAdd = col;
                }
            }

            return new int[] { rowToAdd, colToAdd };
        }

        public Word[] findWords(char[][] grid, int numRows, int numCols)
        {
            bool[][] visited = new bool[numRows][];
            for(int i = 0; i < numRows; i++)
            {
                visited[i] = new bool[numCols];
                for(int j = 0; j < numCols; j++)
                {
                    if(grid[i][j] == ' ')
                    {
                        visited[i][j] = true;
                    }
                    else
                    {
                        visited[i][j] = false;
                    }
                }
            }

             for (int i = 0; i < numRows; i++)
             {
                for (int j = 0; j < numCols; j++)
                    Debug.Write(grid[i][j] + " ");
                Debug.WriteLine("");
             }

            // Initialize current string
            string str = "";
            List<Word> foundWords = new List<Word>();
            List<string> added = new List<string>();

            // Consider every character and look for all words
            // starting with this character
            for (int i = 0; i < numRows; i++)
                for (int j = 0; j < numCols; j++)
                {
                    if(grid[i][j] == ' ')
                    {
                        continue;
                    }
                    findWordsUtil(ref foundWords, ref added, grid, visited, numRows, numCols, i, j, str, "", i, j);
                }

            Debug.WriteLine("*****************");

            for(int i = foundWords.Count - 1; i >= 0; i--)
                if (!singleDirection(grid, foundWords[i]))
                    foundWords.RemoveAt(i);

            foreach(Word word in foundWords)
            {
                string dir = word.direction.Split(',')[0];
                word.direction = dir;
                Debug.WriteLine(word.word);
                Debug.WriteLine("({0},{1})", word.x, word.y);
                Debug.WriteLine(word.direction);
            }

            return foundWords.ToArray();
        }
    }
}
