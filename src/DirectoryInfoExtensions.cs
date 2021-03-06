﻿using System.IO;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// <see cref="DirectoryInfo"/> extension methods.
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Deletes the specified directory recursively,
        /// including all of its sub-directories and files.
        /// </summary>
        /// <param name="dir">The directory to delete.</param>
        public static void DeleteRecursively(this DirectoryInfo dir)
        {
            if (dir is null || !dir.Exists)
            {
                return;
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                DeleteRecursively(subDir);
                subDir.Delete();
            }
        }
    }
}