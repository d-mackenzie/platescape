using BenchmarkDotNet.Running;

namespace TeethInc.Chantry.Benchmarks
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var result = BenchmarkRunner.Run<MosaicBenchmark>();
		}
	}
}
