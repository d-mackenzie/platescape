using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TeethInc.Chantry.App.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        public Bitmap BackgroundBitmap
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "TeethInc.Chantry.App.Assets.splash.jpg";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    return new Bitmap(stream);

                }
            }
        }
    }
}
