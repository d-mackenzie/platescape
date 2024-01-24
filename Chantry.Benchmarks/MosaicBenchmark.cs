using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Models;
using TeethInc.Chantry.Core.Services;

namespace TeethInc.Chantry.Benchmarks
{
	[SimpleJob(RuntimeMoniker.Net80)]
	public class MosaicBenchmark
	{
		private Project _project;

		[GlobalSetup]
		public void Setup()
		{
			_project = ProjectService.Load("c:\\temp\\eric-avatar.jpg");
			_project.ExtentSettings.BaseplateExtent = new SkiaSharp.SKSizeI(8, 8);
			_project.ExtentSettings.Element = LdrawService.GetPart("3024");
		}

		// baseline:								50ms.
		// crop image and don't copy to 2d array:	47ms.
		// ldcolorcache:							22ms.

		[Benchmark]
		public void GetMosaic()
		{
			var mosaic = _project.Mosaic;
		}
	}
}
