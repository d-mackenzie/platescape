using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Filters.ViewModels
{
    public interface IFilterViewModel : IViewModel
    {
        event PropertyChangedEventHandler? PropertyChanged;

        string DisplayName { get; }

        bool Enabled { get; set; }
    }
}
