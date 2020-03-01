﻿/// The MIT License (MIT)
/// Copyright (c) 2016 Bazinga Technologies Inc

[<AutoOpen>]
module Pure.GraphQL.BenchmarkProlog

open BenchmarkDotNet.Configs
open BenchmarkDotNet.Columns
open BenchmarkDotNet.Diagnosers
open BenchmarkDotNet.Exporters

type GraphQLBenchConfig() as this= 
    inherit ManualConfig()
    do
        this.Add(MemoryDiagnoser.Default)
        this.Add(StatisticColumn.Mean, StatisticColumn.Min, StatisticColumn.Max, StatisticColumn.OperationsPerSecond)
        this.Add(MarkdownExporter.GitHub)
        this.Add(Csv.CsvExporter(Csv.CsvSeparator.Comma))
