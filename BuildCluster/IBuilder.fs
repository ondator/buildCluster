namespace BuildCluster

open System.Threading.Tasks
open Microsoft.CodeAnalysis.CSharp
open System.IO
open System.Linq
open Microsoft.CodeAnalysis.Emit
open Microsoft.CodeAnalysis
open System.Collections.Immutable
open System

type BuildResult = Success | Error of string[]

type IBuilder = 
    abstract Build : string -> BuildResult

type Builder() = 
    
    let DefaultAssemblies = [|typeof<Object>.Assembly|]

    let ReturnError(errors: ImmutableArray<Diagnostic>) : BuildResult  = 
        match errors.Any() with
            | false -> Error [|"unknown error"|]
            | _ -> Error (errors.Where(fun d -> 
            d.IsWarningAsError || 
            d.Severity = DiagnosticSeverity.Error).Select(fun d -> d.GetMessage()).ToArray())

    let RunTests(emitResult: EmitResult) : BuildResult  = 
        Success

    let ProcessResult(emitResult: EmitResult) : BuildResult = 
        match emitResult.Success with
            | false -> ReturnError emitResult.Diagnostics
            | _ -> RunTests emitResult

    let Compile(text:string):BuildResult = 
        let syntaxTree = CSharpSyntaxTree.ParseText text;
        let assemblies = DefaultAssemblies
                                .Select(fun a -> a.Location)
                                .Select(fun a-> MetadataReference.CreateFromFile a)
                                .Cast<MetadataReference>()
        let compilation = CSharpCompilation.Create (Path.GetRandomFileName(), [|syntaxTree|], assemblies)
        use stream = new MemoryStream()
        let emitResult = compilation.Emit stream
        ProcessResult emitResult

    interface IBuilder with
        member this.Build text = 
            match System.String.IsNullOrEmpty text with
            | true -> Error [|"can't compile nothing"|]
            | _ -> Compile text
