using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Filters.ViewModels
{
    public interface IFilterViewModel
    {
        event PropertyChangedEventHandler? PropertyChanged;

        string DisplayName { get; }

        bool Enabled { get; set; }
    }
}
