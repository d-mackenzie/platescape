using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Models;

namespace TeethInc.Chantry.Core.Services
{
    public static class ProjectService
    {
        public static Project Load(string filename)
        {
            switch (Path.GetExtension(filename))
            {
                case "json":

                    string json = File.ReadAllText(filename);
                    return Project.Deserialize(json);

                default:

                    Project project = new ProjectBuilder()
                        .WithFilename(filename)
                        .WithMaximumSize(320)
                        .WithDefaultFilters()
                        .Build();

                    return project;
            }
        }
    }
}
