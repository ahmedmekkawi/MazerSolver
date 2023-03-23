using System.Diagnostics;

namespace MazeSolverProject
{
    public class MazeSolver
    {
        private readonly char[,] grid;
        private readonly int height;
        private readonly int width;
        private readonly char wall;
        private readonly char startPoint;
        private readonly char endPoint;
        private readonly char pathChar;
        private (int, int) start;
        private (int, int) end;
        private List<(int, int)>? path;
        private readonly string? inputfilePath;
        private readonly string? outputfilePath;

        public MazeSolver(string? ifilePath, string? ofilePath)
        {
            inputfilePath = ifilePath;
            outputfilePath = ofilePath;

            // check if the file exists
            if (File.Exists(inputfilePath))
            {
                grid = LoadMaze(inputfilePath);
                if (grid.Length > 0)
                {
                    height = grid.GetLength(0);
                    width = grid.GetLength(1);
                    start = (-1, -1);
                    end = (-1, -1);
                    path = null;
                    wall = 'X';
                    startPoint = 'S';
                    endPoint = 'E';
                    pathChar = '.';
                }
                else
                    Console.WriteLine("Cannot Proceed: Width and height should be between 3 and 255 spaces");
            }
            else
                Console.WriteLine("Cannot Proceed: File not found!");
        }


        public void SolveIt()
        {
            FindStartAndEndPoints();
            var visited = new HashSet<(int, int)>();
            var stack = new Stack<(int, int)>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var (x, y) = stack.Pop();
                if ((x, y) == end)
                {
                    this.path = new List<(int, int)>(visited);
                    this.path.Add(end);
                    break;
                }

                if (visited.Contains((x, y)))
                {
                    continue;
                }

                visited.Add((x, y));

                // Check neighbors
                int[] dxValues = { 0, 0, 1, -1 };
                int[] dyValues = { 1, -1, 0, 0 };

                for (int i = 0; i < dxValues.Length; i++)
                {
                    var new_x = x + dxValues[i];
                    var new_y = y + dyValues[i];
                    if (new_x >= 0 && new_x < height && new_y >= 0 && new_y < width && grid[new_x, new_y] != wall)
                    {
                        stack.Push((new_x, new_y));
                    }
                }
            }

            WriteToFile(outputfilePath);
        }

        private static char[,] LoadMaze(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int height = lines.Length;
            int width = lines[0].Length;
            if (height >= 3 && height <= 255 && width >= 3 && width <= 255)
            {
                char[,] maze = new char[height, width];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        maze[i, j] = lines[i][j];
                    }
                }
                return maze;
            }
            else
                return new char[0, 0];
        }

        private void WriteToFile(string outputFilePath)
        {
            if (path == null)
            {
                Console.WriteLine("No solution found.");
                return;
            }

            using (var writer = new StreamWriter(outputFilePath + @"\solution.txt"))
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (path.Contains((i, j)))
                        {
                            writer.Write(pathChar);
                        }
                        else
                        {
                            writer.Write(grid[i, j]);
                        }
                    }
                    writer.Write("\n");
                }
            }

            Console.WriteLine("Done.. You can find the solution file in : {0}", outputFilePath);

            //open the directory
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = outputFilePath,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }

        private void FindStartAndEndPoints()
        {
            bool startFound = false, endFound = false;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[i, j] == startPoint)
                    {
                        if (startFound)
                        {
                            Console.WriteLine("Cannot Proceed: The maze has more than one start, please keep only one");
                            return;
                        }

                        startFound = true;
                        start = (i, j + 1);
                    }
                    else if (grid[i, j] == endPoint)
                    {
                        if (endFound)
                        {
                            Console.WriteLine("Cannot Proceed: The maze has more than one end, please keep only one");
                            return;
                        }

                        endFound = true;
                        end = (i, j + 1);
                    }
                }
            }
        }

    }
}
