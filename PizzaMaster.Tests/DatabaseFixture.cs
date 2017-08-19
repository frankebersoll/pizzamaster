using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PizzaMaster.Tests
{
    public class DatabaseFixture : IDisposable
    {
        private const string DataDirectory = @".\TestDatabase\";

        public DatabaseFixture()
        {
            this.CreateDataDirectory();
        }

        void IDisposable.Dispose()
        {
            // this.RemoveDataDirectory();
        }

        private void CreateDataDirectory()
        {
            this.RemoveDataDirectory();
            Directory.CreateDirectory(DataDirectory);
        }

        public string GetRandomDataFileName()
        {
            var randomName = $"{Guid.NewGuid().ToString().Substring(0, 5)}.db";
            return Path.Combine(DataDirectory, randomName);
        }

        private void RemoveDataDirectory()
        {
            if (Directory.Exists(DataDirectory))
                Directory.Delete(DataDirectory, true);
        }
    }
}