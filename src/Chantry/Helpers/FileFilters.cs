using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.Skia.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.Helpers
{
	internal static class FileFilters
	{
		public static IReadOnlyList<FilePickerFileType> Images =>
			new List<FilePickerFileType>
			{
				new FilePickerFileType("Images") { Patterns = [ "*.jpg", "*.jpeg", "*.png" ] },
				AllFilesFilter()
			};

		public static IReadOnlyList<FilePickerFileType> Projects =>
			new List<FilePickerFileType>
			{
				new FilePickerFileType("Chantry Project") { Patterns = [ "*.json" ] },
				AllFilesFilter()
			};

		public static IReadOnlyList<FilePickerFileType> Ldraw =>
			new List<FilePickerFileType>
			{
				new FilePickerFileType("LDRAW") { Patterns = [ "*.ldr" ] },
				AllFilesFilter()
			};

		public static IReadOnlyList<FilePickerFileType> Png =>
			new List<FilePickerFileType>
			{
				new FilePickerFileType("PNG") { Patterns = [ "*.png" ] }
			};

		private static FilePickerFileType AllFilesFilter() => new FilePickerFileType("All Files") { Patterns = ["*.*"] };
	}
}
