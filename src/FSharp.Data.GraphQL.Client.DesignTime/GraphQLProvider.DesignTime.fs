/// The MIT License (MIT)
/// Copyright (c) 2016 Bazinga Technologies Inc

namespace Pure.GraphQL.Client

open System.Reflection
open FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open Pure.GraphQL

[<TypeProvider>]
type GraphQLTypeProvider (config) as this =
    inherit TypeProviderForNamespaces(config, 
                                      assemblyReplacementMap = ["Pure.GraphQL.Client.DesignTime", "Pure.GraphQL.Client"],
                                      addDefaultProbingLocation = true)

    let ns = "Pure.GraphQL"
    let asm = Assembly.GetExecutingAssembly()
    
    do this.AddNamespace(ns, [Provider.makeProvidedType(asm, ns, config.ResolutionFolder)])

[<assembly:TypeProviderAssembly>] 
do()