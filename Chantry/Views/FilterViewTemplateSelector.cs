using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using System;
using System.Collections.Generic;
using TeethInc.Chantry.App.FilterViewModels;
using TeethInc.Chantry.App.ViewModels;

namespace TeethInc.Chantry.App.Views
{
    public class FilterViewTemplateSelector : IDataTemplate
    {
        public bool SupportsRecycling => false;

        [Content]
        public Dictionary<Type, IDataTemplate> Templates { get; } = new Dictionary<Type, IDataTemplate>();

        public IControl Build(object data)
        {
            return Templates[data.GetType()].Build(data);
        }

        public bool Match(object data)
        {
            return true;
        }
    }
}
