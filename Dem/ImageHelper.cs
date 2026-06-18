using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public static class ImageHelper
    {
        public static Image LoadProductImage(string fileName)
        {
            // Если имя файла не задано или пустое — сразу загружаем картинку по умолчанию
            if (string.IsNullOrWhiteSpace(fileName))
                return LoadDefaultImage();

            string imagePath = Path.Combine(Application.StartupPath, "Images", fileName);
            if (File.Exists(imagePath))
            {
                try
                {
                    // Создаём независимую копию, чтобы не блокировать файл
                    using (var original = Image.FromFile(imagePath))
                    {
                        return new Bitmap(original);
                    }
                }
                catch
                {
                    // Если произошла ошибка при загрузке (например, повреждённый файл)
                    return LoadDefaultImage();
                }
            }
            // Файл не найден — загружаем картинку по умолчанию
            return LoadDefaultImage();
        }

        private static Image LoadDefaultImage()
        {
            string defaultPath = Path.Combine(Application.StartupPath, "Images", "picture.png");
            if (File.Exists(defaultPath))
            {
                using (var original = Image.FromFile(defaultPath))
                {
                    return new Bitmap(original);
                }
            }
            // Если даже picture.png нет, создаём пустое изображение, чтобы не было null
            return new Bitmap(100, 100);
        }

    }
}
