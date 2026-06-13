using System.Windows;
using StoreApp.Models;

namespace StoreApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ⚠️ ВАРИАНТ В4 - ИГРУШКИ
            VariantSettings.CurrentVariant = ExamVariant.V4_Toys;
        }
    }
}