using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using TeethInc.Chantry.Core.Converters;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.Algorithms;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Models
{
	public class Project
	{
		private const int LDRAW_BLACK = 0;
		private const int LDRAW_BLUE = 1;
		private const int LDRAW_GREEN = 2;
		private const int LDRAW_RED = 4;
		private const int LDRAW_YELLOW = 14;
		private const int LDRAW_WHITE = 15;
		private const int LDRAW_TAN = 19;
		private const int LDRAW_DARK_TAN = 28;
		private const int LDRAW_LIGHT_BLUISH_GREY = 71;
		private const int LDRAW_DARK_BLUISH_GREY = 72;

		private ISource _source;
		private FilterService _filterService;
		private MosaicService _mosaicService;

		private static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings()
		{
			TypeNameHandling = TypeNameHandling.Auto
		};

		public int Version => 1;

		public string Name { get; set; }

		// source properties.

		public ISource Source
		{
			get { return _source; }
			set { _source = value; }
		}

		// filter properties.

		public List<Filter> Filters => FilterService.Filters;

		[JsonIgnore]
		public SKBitmap UnfilteredImage => Source.Image;

		[JsonIgnore]
		public SKBitmap FilteredImage => FilterService.GetFilteredImage();

		// mosaic properties.

		public ExtentSettings ExtentSettings { get; set; }

		public AlgorithmSettings AlgorithmSettings { get; set; }

		[JsonIgnore]
		public Mosaic Mosaic => MosaicService.GetMosaic(FilteredImage, ExtentSettings, AlgorithmSettings);

		// export settings.

		public LdrawExportSettings LdrawExportSettings { get; set; }

		public PngExportSettings PngExportSettings { get; set; }

		public Project()
		{
			ExtentSettings = new ExtentSettings();
			AlgorithmSettings = new AlgorithmSettings();

			ExtentSettings.BaseplateSize = new SKSizeI(32, 32);
			ExtentSettings.ElementSize = new SKSizeI(1, 1);
			ExtentSettings.BaseplateExtent = new SKSizeI(4, 4);

			AlgorithmSettings.AllowedColorNumbers = new int[]
			{
				LDRAW_BLACK,
				LDRAW_BLUE,
				LDRAW_GREEN,
				LDRAW_RED,
				LDRAW_YELLOW,
				LDRAW_WHITE,
				LDRAW_TAN,
				LDRAW_DARK_TAN,
				LDRAW_LIGHT_BLUISH_GREY,
				LDRAW_DARK_BLUISH_GREY
			};

			AlgorithmSettings.Algorithm = new FloydSteinberg();

			LdrawExportSettings = GetDefaultExportFileSettings<LdrawExportSettings>();
			PngExportSettings = GetDefaultExportFileSettings<PngExportSettings>();
		}

		public string Serialize()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented, JsonSerializerSettings);
		}

		public static Project Deserialize(string json)
		{
			return JsonConvert.DeserializeObject<Project>(json, JsonSerializerSettings);
		}

		public void Export(ExportSettings exportSettings)
		{
			new ExportService().Export(Mosaic, exportSettings);
		}

		// private properties.

		private FilterService FilterService
		{
			get
			{
				if (_filterService is null)
					_filterService = new FilterService(Source);

				return _filterService;
			}
		}

		private MosaicService MosaicService
		{
			get
			{
				if (_mosaicService is null)
					_mosaicService = new MosaicService();

				return _mosaicService;
			}
		}

		private T GetDefaultExportFileSettings<T>() where T : ExportSettings
		{
			T ret = Activator.CreateInstance<T>();

			switch (Source)
			{
				case FileSource fileSource:

					ret.Filename = Path.ChangeExtension(fileSource.Filename, ret.DefaultExtension);
					ret.ExportFolder = Path.GetDirectoryName(fileSource.Filename);
					ret.FilenamePattern = Path.GetFileNameWithoutExtension(fileSource.Filename) + $"row_{{row}}_col_{{col}}.{ret.DefaultExtension}";
					break;

				default:
					ret.Filename = $"mosaic.{ret.DefaultExtension}";
					ret.ExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					ret.FilenamePattern = $"mosaic_row_{{row}}_col_{{col}}.{ret.DefaultExtension}";
					break;
			}

			return ret;
		}
	}
}
