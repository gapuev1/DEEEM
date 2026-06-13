using System.Windows;
using StoreApp.Models;

namespace StoreApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ⚠️ ВЫБЕРИТЕ ВАРИАНТ ЗДЕСЬ!
            VariantSettings.CurrentVariant = ExamVariant.V2_Bicycles;
        }
    }
}
// Для В2 (Велосипеды)
VariantSettings.CurrentVariant = ExamVariant.V2_Bicycles;

// Для В3 (Стройматериалы)
VariantSettings.CurrentVariant = ExamVariant.V3_Construction;

// Для В4 (Игрушки)
VariantSettings.CurrentVariant = ExamVariant.V4_Toys;

// Для В5 (Мебель)
VariantSettings.CurrentVariant = ExamVariant.V5_Furniture;