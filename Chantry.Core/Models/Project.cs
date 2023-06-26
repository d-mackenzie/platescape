using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TeethInc.Chantry.Core.Converters;
using TeethInc.Chantry.Core.Exporters;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core.Models
{
    public class Project
    {
        private ISource _source;
        private FilterService _filterService;
        private MosaicService _mosaicService;
        private LdrawService _ldrawService;

        private ExportLdrawSettings _exportLdrawSettings;

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
        public SKBitmap UnfilteredImage => FilterService.GetUnfilteredImage();

        [JsonIgnore]
        public SKBitmap FilteredImage => FilterService.GetFilteredImage();

        // mosaic properties.

        public string BaseplatePartNumber
        {
            get { return MosaicService.Baseplate; }
            set { MosaicService.Baseplate = value; }
        }

        public string ElementPartNumber
        {
            get { return MosaicService.Element; }
            set { MosaicService.Element = value; }
        }

        [JsonConverter(typeof(JsonSizeConverter))]
        public SKSizeI BaseplateExtent
        {
            get { return MosaicService.BaseplateExtent; }
            set { MosaicService.BaseplateExtent = value; }
        }

        [JsonIgnore]
        public SKSizeI ElementExtent => MosaicService.ElementExtent;

        public int[] AllowedColors
        {
            get { return MosaicService.AllowedColors; }
            set { MosaicService.AllowedColors = value; }
        }

        public MosaicAlgorithm MosaicAlgorithm { get; set; } = new FloydSteinberg();

        [JsonIgnore]
        public Mosaic Mosaic => MosaicService.GetMosaic(FilteredImage, MosaicAlgorithm);

        // export properties.

        public ExportLdrawSettings ExportLdrawSettings
        {
            get
            {
                _exportLdrawSettings = _exportLdrawSettings ?? GetDefaultExportFileSettings<ExportLdrawSettings>();
                return _exportLdrawSettings;
            }
        }

        public Project()
        {
            _ldrawService = new LdrawService();
        }

        public static Project CreateSimpleProject(string filename)
        {
            var project = new Project()
            {
                Name = Path.GetFileNameWithoutExtension(filename),
                Source = new ImageSource()
                {
                    Image = SKBitmap.Decode(filename)
                }
            };

            project.Filters.Add(new BrightnessContrastFilter());

            return project;
        }

        public void Export(IExportSettings exportSettings)
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
                    _mosaicService = new MosaicService(_ldrawService);

                return _mosaicService;
            }
        }

        private T GetDefaultExportFileSettings<T>() where T : IExportSettings
        {
            T ret = Activator.CreateInstance<T>();
            
            switch (Source)
            {
                case FileSource fileSource:

                    ret.Filename = Path.ChangeExtension(fileSource.Filename, "ldr");
                    ret.ExportFolder = Path.GetDirectoryName(fileSource.Filename);
                    ret.FilenamePattern = Path.GetFileNameWithoutExtension(fileSource.Filename) + "_row{row}_col{col}.ldr";
                    break;
            }

            return ret;
        }
    }
}
