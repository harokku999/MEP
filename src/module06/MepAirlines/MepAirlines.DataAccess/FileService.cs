using System.Collections.Generic;
using System.IO;
using MepAirlines.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace MepAirlines.DataAccess
{
    public interface IFileService
    {
        bool Exists(string fileName);
        string ReadAllText(string fileName);
        IEnumerable<string> ReadAllLines(string fileName);
        void WriteAllText(string fileName, string content);
    }

    public sealed class FileService : IFileService
    {
        private readonly IOptions<FileOption> _fileOption;

        public FileService(IOptions<FileOption> fileOption)
        {
            _fileOption = fileOption;
        }

        internal string GetFullPath(string fileName) => $"{_fileOption.Value.DataFolder}\\{fileName}";
        public bool Exists(string fileName) => File.Exists(GetFullPath(fileName));
        public string ReadAllText(string fileName) => File.ReadAllText(GetFullPath(fileName));
        public IEnumerable<string> ReadAllLines(string fileName) => File.ReadAllLines(GetFullPath(fileName));
        public void WriteAllText(string fileName, string content) => File.WriteAllText(GetFullPath(fileName), content);
    }
}