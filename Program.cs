using MazeSolverProject;

string? inputfilePath, outputfilePath;
Console.WriteLine("Please enter the txt file path: ");
inputfilePath = Console.ReadLine();

Console.WriteLine("Where you want to save the file into? Please enter the full path: ");
outputfilePath = Console.ReadLine();

try
{
    MazeSolver solver = new MazeSolver(inputfilePath, outputfilePath);
    solver.SolveIt();
}
catch (Exception ex)
{
    Console.WriteLine("Something Went Wrong!: {0}", ex.Message);
}