using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include
{
    public class DisplayNameDynamicAttribute : DisplayNameAttribute
    {
        public DisplayNameDynamicAttribute(string translateFilePath, string key, string defaultValue) : base(GetMessageFromResource(translateFilePath, key, defaultValue))
        {
        }

        private static string GetMessageFromResource(string translateFilePath, string key, string defaultValue)
        {
            string pathFullName = Path.Combine(translateFilePath, AionGame.Game.GetCurrentAionbotnetUILanguage() + ".txt");
            var result = ReadResourceFile(pathFullName, key);

            if (string.IsNullOrWhiteSpace(result) == false)
            {
                return result;
            }
            return defaultValue;
        }

        private static string ReadResourceFile(string fileName, string keyToSeach)
        {
            if (File.Exists(fileName))
            {
                var translateFile = File.ReadAllLines(fileName);

                foreach (var line in translateFile)
                {
                    if (string.IsNullOrWhiteSpace(line) == false)
                    {
                        string key = line.Substring(0, line.IndexOf("="));

                        if (string.Compare(key, keyToSeach, true) == 0)
                        {
                            string finalValue = line.Substring(line.IndexOf("=") + 1);
                            return finalValue;
                        }
                    }
                }
            }

            return "";
        }
    }

    public class CategoryDynamicAttribute : CategoryAttribute
    {
        public CategoryDynamicAttribute(string translateFilePath, string key, string defaultValue) : base(GetMessageFromResource(translateFilePath, key, defaultValue))
        {
        }

        private static string GetMessageFromResource(string translateFilePath, string key, string defaultValue)
        {
            string pathFullName = Path.Combine(translateFilePath, AionGame.Game.GetCurrentAionbotnetUILanguage() + ".txt");
            var result = ReadResourceFile(pathFullName, key);

            if (string.IsNullOrWhiteSpace(result) == false)
            {
                return result;
            }
            return defaultValue;
        }

        private static string ReadResourceFile(string fileName, string keyToSeach)
        {
            if (File.Exists(fileName))
            {
                var translateFile = File.ReadAllLines(fileName);

                foreach (var line in translateFile)
                {
                    if (string.IsNullOrWhiteSpace(line) == false)
                    {
                        string key = line.Substring(0, line.IndexOf("="));

                        if (string.Compare(key, keyToSeach, true) == 0)
                        {
                            string finalValue = line.Substring(line.IndexOf("=") + 1);
                            return finalValue;
                        }
                    }
                }
            }

            return "";
        }
    }
}
