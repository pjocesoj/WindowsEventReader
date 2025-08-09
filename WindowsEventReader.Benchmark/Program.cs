// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using WindowsEventReader.Benchmark;

var summary = BenchmarkRunner.Run<QueryCreatorBenchmark>();
