using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace EDS_V4.Code
{
    public class FileAlreadyExistingException : Exception
    {
        public FileAlreadyExistingException() { }

        public FileAlreadyExistingException(string message)
            : base(message) { }

        public FileAlreadyExistingException(string message, Exception inner)
            : base(message, inner) { }
    }

    // The object with type T must contain only properties and no fields!!
    // All properties of object with type T have to be public for this class to work!!
    public class XmlHandler<T>
    {
        private readonly object fileLocker = new();

        public XmlSerializer Serializer { get; private set; }

        public XmlHandler()
        {
            Serializer = new XmlSerializer(typeof(T));
        }

        public XmlHandler(Type[] extratypes)
        {
            Serializer = new XmlSerializer(typeof(T), extratypes);
        }

        public IList<T> ReadFromDirectory(string pathToDirectory)
        {
            // Correct path for OS specific syntax '\' or '/'
            string path = Path.GetFullPath(pathToDirectory);

            //only works when files in directory have the same datatype!
            //will throw invalid operation exception when this is not the case!
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException();
            var files = Directory.GetFiles(path);
            var objects = new List<T>();
            foreach (var file in files)
                objects.Add(ReadFromXmlFile(file));

            return objects;
        }

        public T ReadFromXmlFile(string pathToFile)
        {
            if (string.IsNullOrEmpty(pathToFile))
                throw new ArgumentNullException(nameof(pathToFile));

            // Correct path for OS specific syntax '\' or '/'
            string path = Path.GetFullPath(pathToFile);

            //path must include the name of the file!
            if (!File.Exists(path))
                throw new FileNotFoundException();

            T? xmlobject = default;

            lock (fileLocker)
            {
                try
                {
                    using StreamReader file = new StreamReader(path);
                    xmlobject = (T?)Serializer.Deserialize(file);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException($"Cannot read XML content of {pathToFile}, is the XML file valid?", ex);
                }
            }
            return xmlobject;
        }

        public void WriteToXml(string pathToFile, dynamic obj, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(pathToFile))
                throw new ArgumentNullException(nameof(pathToFile));

            if (obj is null)
                throw new ArgumentNullException(nameof(obj));

            // Correct path for OS specific syntax '\' or '/'
            string path = Path.GetFullPath(pathToFile);

            if (!path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("wrong or missing file extension");

            if (File.Exists(path) && !overwrite)
                throw new FileAlreadyExistingException();

            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                _ = Directory.CreateDirectory(dir);

            lock (fileLocker)
            {
                using FileStream file = File.Create(path);
                Serializer.Serialize(file, obj);
            }
        }
    }
}
