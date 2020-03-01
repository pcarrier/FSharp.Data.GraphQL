/// The MIT License (MIT)
/// Copyright (c) 2016 Bazinga Technologies Inc

module Pure.GraphQL.Tests.ResolveTests

open System
open Xunit
open Pure.GraphQL
open Pure.GraphQL.Types
open Pure.GraphQL.Parser
open Pure.GraphQL.Execution

type TestData = 
    { Test: string }
    member x.TestMethod (a: string) (b: int) = x.Test + a + b.ToString()
    member x.AsyncTestMethod (a: string) (b: int) = async { return x.Test + a + b.ToString() }
let testSchema testFields : Schema<TestData> = Schema(Define.Object("Query", fields = testFields))

[<Fact>]
let ``Execute uses default resolve to accesses properties`` () =
    let schema = testSchema [ Define.AutoField("test", String) ]
    let expected = NameValueLookup.ofList [ "test", "testValue" :> obj ]
    let actual = sync <| Executor(schema).AsyncExecute(parse "{ test }", { Test = "testValue" })
    match actual with
    | Direct(data, errors) ->
      empty errors
      data.["data"] |> equals (upcast expected)
    | _ -> fail "Expected Direct GQResponse"
            
[<Fact>]
let ``Execute uses provided resolve function to accesses properties`` () =
    let schema = testSchema [ Define.Field("test", String, "", [ Define.Input("a", String) ], resolve = fun ctx d -> d.Test + ctx.Arg("a")) ]
    let expected = NameValueLookup.ofList [ "test", "testValueString" :> obj ]
    let actual = sync <| Executor(schema).AsyncExecute(parse "{ test(a: \"String\") }", { Test = "testValue" })    
    match actual with
    | Direct(data, errors) ->
      empty errors
      data.["data"] |> equals (upcast expected)
    | _ -> fail "Expected Direct GQResponse"
