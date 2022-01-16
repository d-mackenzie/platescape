using Newtonsoft.Json;
using SkiaSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TeethInc.Chantry.Core.Converters;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Core.MosaicAlgorithms;
using TeethInc.Chantry.Core.Services;
using TeethInc.Chantry.Core.Sources;

namespace TeethInc.Chantry.Core
{
    public class Project
    {
        private ISource m_source;
        private FilterService m_filterService;
        private MosaicService m_mosaicService;
        private MosaicAlgorithm m_mosaicAlgorithm = new FloydSteinberg();
//        private MosaicAlgorithm m_mosaicAlgorithm = new BayerMatrix();
        private LdrawService m_ldrawService;

        public string Name { get; set; }

        // source properties.

        public ISource Source
        {
            get { return m_source; }
            set { m_source = value; }
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

        public MosaicAlgorithm MosaicAlgorithm => m_mosaicAlgorithm;

        [JsonIgnore]
        public Mosaic Mosaic => MosaicService.GetMosaic(FilteredImage, m_mosaicAlgorithm);

        public Project()
        {
            m_ldrawService = new LdrawService();
        }

        public Project(LdrawService ldrawService)
        {
            m_ldrawService = ldrawService;
        }

        public static Project CreateSimpleProject(string filename)
        {
            var project = new Project()
            {
                Name = Path.GetFileNameWithoutExtension(filename),
                Source = new FileSource()
                {
                    Filename = filename
                }
            };

            project.Filters.Add(
                new BrightnessContrastFilter()
                {
                    Brightness = 0,
                    Contrast = 0
                });

            return project;
        }

        public void ExportLdraw(string filename)
        {
            Mosaic mosaic = Mosaic;

            var sw = Stopwatch.StartNew();

            var ldFile = m_ldrawService.GetLdrawFile(mosaic);
            File.WriteAllLines(filename, ldFile.ToList());

            Debug.WriteLine($"Saved to {filename}: {sw.ElapsedMilliseconds}ms.");
        }

        // private properties.

        private FilterService FilterService
        {
            get
            {
                if (m_filterService is null)
                    m_filterService = new FilterService(Source);

                return m_filterService;
            }
        }

        private MosaicService MosaicService
        {
            get
            {
                if (m_mosaicService is null)
                    m_mosaicService = new MosaicService(m_ldrawService);

                return m_mosaicService;
            }
        }
    }
}
