using Microsoft.CodeAnalysis.CSharp;

namespace Brimborium.Matschburg.Library;

public class Utiltity
{
    public static async Task GetWorkspaceAsync(string solutionFQN, CancellationToken cancellation)
    {
        System.Console.Out.WriteLine($"solutionFQN: {solutionFQN}");
        var workspace = MSBuildWorkspace.Create();
        var solution = await workspace.OpenSolutionAsync(solutionFQN);
        foreach (var projectId in solution.ProjectIds)
        {
            var project = solution.GetProject(projectId);
            if (project is null) { continue; }
            System.Console.Out.WriteLine($"project projectId: {projectId}");
            System.Console.Out.WriteLine($"project.FilePath: {project.FilePath}");
            var compilation = await project.GetCompilationAsync(cancellation);
            if (compilation is null) { continue; }
            foreach (var documentId in project.DocumentIds)
            {
                var document = project.GetDocument(documentId);
                if (document is null) { continue; }
                System.Console.Out.WriteLine($"document.FilePath: {document.FilePath}");

                var syntaxTree = await document.GetSyntaxTreeAsync(cancellation);
                if (syntaxTree is null) { continue; }
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                if (semanticModel is null) { continue; }
                /*
                var controlFlow = semanticModel.AnalyzeControlFlow(default!, default!);
                CSharpSyntaxVisitor
                TODO invoke graph
                TODO invest in existing call graph algorythm
                */
            }
        }
    }
}
