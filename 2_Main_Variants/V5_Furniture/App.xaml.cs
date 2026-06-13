using System.Windows;
using StoreApp.Models;

namespace StoreApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ⚠️ ВАРИАНТ В5 - МЕБЕЛЬ
            VariantSettings.CurrentVariant = ExamVariant.V5_Furniture;
        }
    }
}