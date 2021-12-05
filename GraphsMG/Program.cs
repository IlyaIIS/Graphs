using System;
using System.IO;
using GraphsLogic;

namespace GraphsMG
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
/*            string path = Directory.GetCurrentDirectory() + @"\matrix.csv";
            double[,] matrix = { { 1, 2, 3, 4, 5 }, { 1, 2, 0, 0, 5 }, { 0, 0, 3, 4, 0 }, { 1, 2, 0, 4, 5 }, { 0, 0, 0, 4, 5 } };
            CsvMatrix.WriteMatrix(path, matrix);
            GraphsLogic.Graph res = CsvMatrix.GetGraph(path);
            CsvMatrix.SaveGraph(res, path);*/
        }
    }
}
