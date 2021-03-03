using System.IO;
using System.Reflection;

namespace SqlHelper.Code
{
    class Utility
    {
        /// <summary>
        /// Get or set current directory for executing assembly
        /// </summary>
        public static string CurrentDirectory
        {
            get
            {
                var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return currentDirectory ?? string.Empty;
            }
        }

        /// <summary>
        /// Get path for data directory
        /// </summary>
        public static string DataDirectory
        {
            get
            {
                var path = Path.Combine(CurrentDirectory, Properties.Settings.Default.DbDirPath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
    }
}
