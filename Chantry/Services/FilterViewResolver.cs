using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeethInc.Chantry.Core.Filters;
using TeethInc.Chantry.Filters.ViewModels;
using TeethInc.Chantry.ViewModels;

namespace TeethInc.Chantry.Services
{
    public static class FilterViewResolver
    {
        private static List<Registration> _registrations = new List<Registration>();

        public static void Register<TFilter, TViewModel, TView>()
            where TFilter : Filter
            where TViewModel : IFilterViewModel
            where TView : UserControl
        {
            _registrations.Add(new Registration(typeof(TFilter), typeof(TViewModel), typeof(TView)));
        }

        public static IFilterViewModel ConstructFilterViewModel(Filter filter)
        {
            Type? viewModelType = _registrations.FirstOrDefault(x => x.FilterType == filter.GetType())?.ViewModelType;

            if (viewModelType is null)
                throw new ArgumentException($"{filter.GetType()} not registered.");

            IFilterViewModel? viewModel = Activator.CreateInstance(viewModelType, filter) as IFilterViewModel;

            if (viewModel is null)
                throw new Exception("Could not create filter ViewModel.");

            return viewModel;
        }

        private class Registration
        {
            public Type FilterType { get; }
            public Type ViewModelType { get; }
            public Type ViewType { get; }

            public Registration(Type filterType, Type viewModelType, Type viewType)
            {
                FilterType = filterType;
                ViewModelType = viewModelType;
                ViewType = viewType;
            }
        }

    }
}
