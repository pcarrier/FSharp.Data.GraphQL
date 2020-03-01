module Program

open BenchmarkDotNet.Running
open Pure.GraphQL.AsyncValBenchmark
open Pure.GraphQL.ParsingBenchmark
open Pure.GraphQL.ExecutionBenchmark
open Pure.GraphQL.MiddlewareBenchmark

let defaultSwitch () = 
    BenchmarkSwitcher [| 
        typeof<AsyncValBenchmark>
        typeof<SimpleExecutionBenchmark>
        typeof<ParsingBenchmark>
        typeof<SimpleExecutionWithMiddlewareBenchmark>
    |]

[<EntryPoint>]
let Main args =
    defaultSwitch().Run args |> ignore
    0
