using System.ComponentModel;

namespace TeethInc.Chantry.ViewModels;

public interface IViewModel : INotifyPropertyChanged
{
    bool IsDirty { get; set; }
}