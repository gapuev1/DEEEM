using System.Windows.Media;

namespace StoreApp.Models
{
    public enum ExamVariant
    {
        V2_Bicycles,      // Велосипеды
        V3_Construction,  // Стройматериалы
        V4_Toys,          // Игрушки
        V5_Furniture      // Мебель
    }

    public static class VariantSettings
    {
        // ⚠️ ВЫБЕРИТЕ НУЖНЫЙ ВАРИАНТ ЗДЕСЬ!
        public static ExamVariant CurrentVariant { get; set; } = ExamVariant.V2_Bicycles;

        // Цвет для подсветки скидки
        public static Color DiscountColor
        {
            get
            {
                return CurrentVariant switch
                {
                    ExamVariant.V2_Bicycles => (Color)ColorConverter.ConvertFromString("#483D8B"),
                    ExamVariant.V3_Construction => (Color)ColorConverter.ConvertFromString("#F4A460"),
                    ExamVariant.V4_Toys => (Color)ColorConverter.ConvertFromString("#FFDEAD"),
                    ExamVariant.V5_Furniture => (Color)ColorConverter.ConvertFromString("#008080"),
                    _ => (Color)ColorConverter.ConvertFromString("#F4A460")
                };
            }
        }

        // Цвет для подсветки "нет в наличии"
        public static Color OutOfStockColor
        {
            get
            {
                return CurrentVariant switch
                {
                    ExamVariant.V2_Bicycles => (Color)ColorConverter.ConvertFromString("#95A5A6"),
                    ExamVariant.V3_Construction => (Color)ColorConverter.ConvertFromString("#ADD8E6"),
                    ExamVariant.V4_Toys => (Color)ColorConverter.ConvertFromString("#ADD8E6"),
                    ExamVariant.V5_Furniture => (Color)ColorConverter.ConvertFromString("#95A5A6"),
                    _ => (Color)ColorConverter.ConvertFromString("#ADD8E6")
                };
            }
        }

        // Порог скидки для подсветки
        public static decimal DiscountThreshold
        {
            get
            {
                return CurrentVariant switch
                {
                    ExamVariant.V2_Bicycles => 15,
                    ExamVariant.V3_Construction => 12,
                    ExamVariant.V4_Toys => 17,
                    ExamVariant.V5_Furniture => 15,
                    _ => 12
                };
            }
        }

        // Название магазина
        public static string StoreName
        {
            get
            {
                return CurrentVariant switch
                {
                    ExamVariant.V2_Bicycles => "🚲 Магазин велосипедов",
                    ExamVariant.V3_Construction => "🔨 Склад стройматериалов",
                    ExamVariant.V4_Toys => "🧸 Магазин игрушек",
                    ExamVariant.V5_Furniture => "🛋️ Магазин мебели",
                    _ => "🏬 Управление складом"
                };
            }
        }
    }
}