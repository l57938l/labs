namespace Lab4
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    class FileAttributeChanger
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            // Перевірка аргументів командного рядка
            if (args.Length < 2)
            {
                ShowHelp();
                return;
            }

            // Отримання каталогу та шаблону файлу
            string directoryPath = args[0];
            string filePattern = args[1];

            // Отримання параметрів атрибутів
            bool setHidden = false;
            bool setReadOnly = false;
            bool setArchive = false;

            for (int i = 2; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-h":
                    case "--hidden":
                        setHidden = true;
                        break;
                    case "-r":
                    case "--readonly":
                        setReadOnly = true;
                        break;
                    case "-a":
                    case "--archive":
                        setArchive = true;
                        break;
                    default:
                        ShowHelp();
                        return;
                }
            }

            // Отримання списку файлів
            string[] files = Directory.GetFiles(directoryPath, filePattern);

            // Зміна атрибутів файлів
            foreach (string filePath in files)
            {
                FileAttributes attributes = File.GetAttributes(filePath);

                // Застосування нових атрибутів
                if (setHidden)                
                    attributes |= FileAttributes.Hidden;
                else                
                    attributes &= ~FileAttributes.Hidden;                

                if (setReadOnly)                
                    attributes |= FileAttributes.ReadOnly;               
                else                
                    attributes &= ~FileAttributes.ReadOnly;
                

                if (setArchive)               
                    attributes |= FileAttributes.Archive;                
                else                
                    attributes &= ~FileAttributes.Archive;                

                File.SetAttributes(filePath, attributes);
            }

            // Вивід повідомлення про успішне виконання
            Console.WriteLine("Атрибути файлів у каталозі успішно змінені.", directoryPath);
        }

        static void ShowHelp()
        {
            Console.WriteLine("Використання:");
            Console.WriteLine("FileAttributeChanger <каталог> <шаблон файлу> [-h|--hidden] [-r|--readonly] [-a|--archive]");
            Console.WriteLine("\nПараметри:");
            Console.WriteLine("<каталог> - шлях до каталогу, в якому потрібно змінити атрибути файлів.");
            Console.WriteLine("<шаблон файлу> - шаблон імені файлу (наприклад, *.exe).");
            Console.WriteLine("-h|--hidden - встановити атрибут 'прихований' для файлів.");
            Console.WriteLine("-r|--readonly - встановити атрибут 'тільки для читання' для файлів.");
            Console.WriteLine("-a|--archive - встановити атрибут 'архівний' для файлів.");
        }
    }
}
