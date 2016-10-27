namespace PacManKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze(6, 6);
            maze.Print();
            System.Console.ReadLine();

            maze.PacManRight();
            maze.Tick();
            maze.Print();
            System.Console.ReadLine();

            maze.PacManRight();
            maze.Tick();
            maze.Print();
            System.Console.ReadLine();

            maze.PacManUp();
            maze.Tick();
            maze.Print();
            System.Console.ReadLine();

            maze.PacManUp();
            maze.Tick();
            maze.Print();
            System.Console.ReadLine();

            maze.PacManLeft();
            maze.Tick();
            maze.Print();
            System.Console.ReadLine();

            maze.PacManLeft();
            maze.Tick();
            maze.Print();
            System.Console.ReadLine();

            maze.PacManLeft();
            maze.Tick();
            maze.Print();
            System.Console.ReadLine();

            maze.PacManLeft();
            maze.Tick();
            maze.Print();
            System.Console.ReadLine();
        }
    }
}
