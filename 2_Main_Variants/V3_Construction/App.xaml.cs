using System.Windows;
using StoreApp.Models;

namespace StoreApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ⚠️ ВАРИАНТ В3 - СТРОЙМАТЕРИАЛЫ
            VariantSettings.CurrentVariant = ExamVariant.V3_Construction;
        }
    }
}