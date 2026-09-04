using System.Runtime.CompilerServices;

namespace Brimborium.Matschburg;

public class Program
{
    public static async Task Main(string[] args)
    {
        global::Microsoft.Build.Locator.MSBuildLocator.RegisterDefaults();
        // TODO use args
        var solutionFQN = GetSolutionPath();
        await Utiltity.GetWorkspaceAsync(solutionFQN, CancellationToken.None);

        static string GetSolutionPath([CallerFilePath] string callerFilePath="")
        {
            var solutionPath = System.IO.Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                    callerFilePath))!;
            return System.IO.Path.Combine(solutionPath, @"Brimborium.Matschburg.slnx");
        }
    }
}