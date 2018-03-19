using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hexacta.Core.Tools.Utilities
{

    public static class GenericUtilities
    {
        public static string Now()
        {
            return DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss.ffffff");
        }

        public static string EncodeTo64(string toEncode) {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            return System.Convert.ToBase64String(toEncodeAsBytes);
        }

        public static string DecodeFrom64(string encodedData) {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            return System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        }

        public static string ByteArrayToString(byte[] contenido)
        {
            return ByteArrayToString(contenido, new ASCIIEncoding());
        }

        public static string ByteArrayToString(byte[] contenido, Encoding encoding)
        {
            return encoding.GetString(contenido);
        }

        /// <summary>
        /// Gets the array hexa representation
        /// </summary>
        /// <param name="bytes">array of bytes</param>
        /// <returns>hexa-string that represents the array</returns>
        public static string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder result = new StringBuilder();
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in bytes)
            {
                result.Append(HexAlphabet[(int)(B >> 4)]);
                result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return (result.ToString());
        }

        /// <summary>
        /// Gets the array from the hexa representation
        /// </summary>
        /// <param name="hex">hexa-string to convert</param>
        /// <returns>array that represents the hexa-string</returns>
        public static byte[] HexStringToByteArray(string hex)
        {
            int[] _hexValues = new int[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,
                                          0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x0B, 0x0C, 0x0D,
                                          0x0E, 0x0F };
            byte[] result = new byte[hex.Length / 2];

            for (int x = 0, i = 0; i < hex.Length; i += 2, x += 1)
            {
                result[x] = (byte)(_hexValues[Char.ToUpper(hex[i + 0]) - '0'] << 4 |
                                   _hexValues[Char.ToUpper(hex[i + 1]) - '0']);
            }

            return (result);
        }

        public static void CastBaseClassToDerivedClass(object baseClass, object derivedObject)
        {
            Type baseType = baseClass.GetType();
            Type derivedType = derivedObject.GetType();
            if (!derivedType.IsSubclassOf(baseType))
            {
                throw new Exception("derivedObject is not a baseClass");
            }
            Type oType = baseClass.GetType();
            object[] oValue = new object[0];
            foreach (PropertyInfo oPinfo in oType.GetProperties())
            {
                MethodInfo oMinfo = oPinfo.GetGetMethod();
                MethodInfo mInfo = derivedType.GetProperty(oPinfo.Name).GetSetMethod();
                oValue[0] = oPinfo.GetValue(baseClass, null);
                mInfo.Invoke(derivedObject, oValue);
            }
        }

        public static void DeleteFiles(string[] filePaths)
        {
            foreach (string oneFile in filePaths)
            {
                try
                {
                    File.Delete(oneFile);
                }
                catch
                {
                }
            }
        }

        public static void DeleteFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                string[] innerFolders = Directory.GetDirectories(folderPath);
                foreach (string oneFolder in innerFolders)
                {
                    DeleteFolder(oneFolder);
                }
                DeleteFiles(Directory.GetFiles(folderPath));
                Directory.Delete(folderPath);
            }
        }

        public static byte[] ReadBinaryFile(string filePath)
        {
            using (Stream auxiliarFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return ReadFully(auxiliarFile, 0);
            }
        }

        public static string ReadFile(string filePath)
        {
            using (StreamReader auxiliarFile = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                return auxiliarFile.ReadToEnd();
            }
        }

        public static byte[] ReadFileFromEmbebdResource(Assembly Asm, string fileName)
        {
            using (Stream RSstream = Asm.GetManifestResourceStream(fileName))
            {
                return ReadFully(RSstream, 0);
            }
        }

        public static string ReadFromLocalMachineRegistryKey(string key, string path)
        {
            string value;
            try
            {
                value = Registry.LocalMachine.OpenSubKey(path).GetValue(key).ToString();
            }
            catch (NullReferenceException ex)
            {
                throw new ApplicationException("Registry Key not Found", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("GetRegistryConfiguration", ex);
            }
            return value;
        }

        public static byte[] ReadFully(Stream stream, int initialLength)
        {
            int chunk;
            if (initialLength < 1)
            {
                initialLength = 256;
            }
            byte[] buffer = new byte[initialLength];
            int read = 0;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte == -1)
                    {
                        return buffer;
                    }
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }

        public static string SaveFile(string filePath, byte[] data)
        {
            using (FileStream objfilestream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                objfilestream.Write(data, 0, data.Length);
                objfilestream.Close();
                return filePath;
            }
        }

        public static string SaveFile(string filePath, string fileContent)
        {
           return SaveFile(filePath, fileContent, Encoding.GetEncoding("ISO-8859-1"), true);
        }

        public static string SaveFile(string filePath, string fileContent, bool append)
        {
            SaveFile(filePath, fileContent, Encoding.GetEncoding("ISO-8859-1"), append);
            return filePath;
        }

        public static string SaveFile(string filePath, string fileContent, Encoding encoding, bool append) {
            if (append)
                return AppendTextFile(filePath, fileContent, encoding);
            else
                return OverwriteFile(filePath, fileContent, encoding);
        }

        public static string SaveTempFile(byte[] data)
        {
            string temporayFileName = Path.GetTempFileName();
            using (FileStream tmpFileStream = File.OpenWrite(temporayFileName)) //, FileMode.Create))
            {
                tmpFileStream.Write(data, 0, data.Length);
                tmpFileStream.Close();
            }
            return temporayFileName;
        }

        private static string AppendTextFile(string filePath, string fileContent, Encoding encoding)
        {
            string returnValue;
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter w = new StreamWriter(fs, encoding))
                {
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    w.Write(fileContent);
                    w.Flush();
                    w.Close();
                    returnValue = filePath;
                }
            }
            return returnValue;
        }

        private static string OverwriteFile(string filePath, string fileContent, Encoding encoding)
        {
            using (StreamWriter FileWriter = new StreamWriter(filePath, false, encoding))
            {
                FileWriter.Write(fileContent);
                FileWriter.Close();
                return filePath;
            }
        }


        public static void SaveSerializedXML(object theObject, string path)
        {
            try
            {
                using (StringWriter Output = new StringWriter(new StringBuilder()))
                {
                    new XmlSerializer(theObject.GetType()).Serialize((TextWriter)Output, theObject);
                    using (StreamWriter file = new StreamWriter(path, false, Output.Encoding))
                    {
                        file.AutoFlush = true;
                        file.Write(Output);
                        file.Flush();
                        file.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static T Deserialize<T>(string input)
           where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
                return (T)ser.Deserialize(sr);
        }

        public static string Serialize<T>(T theObject)
        {
            try
            {
                string returnValue;
                using (MemoryStream stream = new MemoryStream())
                {
                    using (TextWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        new XmlSerializer(typeof(T)).Serialize(writer, theObject);
                        int count = (int)stream.Length;
                        byte[] arr = new byte[count];
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.Read(arr, 0, count);
                        UnicodeEncoding utf = new UnicodeEncoding();
                        returnValue = utf.GetString(arr).Trim();
                    }
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                return string.Format("[Serialing Error: {0}]", ex.Message);
            }
        }

        public static XDocument SerializeToXML<T>(T input)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            XDocument xd = null;
            using (MemoryStream memStm = new MemoryStream())
            {
                ser.Serialize(memStm, input);
                memStm.Position = 0;
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;

                using (var xtr = XmlReader.Create(memStm, settings))
                {
                    xd = XDocument.Load(xtr);
                }
            }

            return xd;
        }

        public static XDocument SerializeToXML(object input)
        {
            XmlSerializer ser = new XmlSerializer(input.GetType());
            XDocument xd = null;
            using (MemoryStream memStm = new MemoryStream())
            {
                ser.Serialize(memStm, input);
                memStm.Position = 0;
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;

                using (var xtr = XmlReader.Create(memStm, settings))
                {
                    xd = XDocument.Load(xtr);
                }
            }

            return xd;
        }


        public static byte[] SerializarBinario(object theObject)
        {
            byte[] bytes;
            using (MemoryStream stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, theObject);
                int count = (int)stream.Length;
                bytes = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(bytes, 0, count);
                stream.Close();
            }

            return bytes;
        }

        public static void WriteToEventLog(string input, string logSourceName, EventLogEntryType eventLogEntryType)
        {
            using (EventLog log = new EventLog())
            {

                if (!EventLog.SourceExists(logSourceName))
                {
                    EventLog.CreateEventSource(logSourceName, logSourceName);
                }
                log.Source = logSourceName;
                log.WriteEntry(input, eventLogEntryType);
                log.Close();
            }
        }

        public static void WriteToEventLog(string input, string logSource, string logName, EventLogEntryType eventLogEntryType)
        {
            using (EventLog log = new EventLog())
            {
                if (!EventLog.SourceExists(logSource))
                {
                    EventLog.CreateEventSource(logSource, logName);
                }
                log.Source = logSource;
                log.WriteEntry(input, eventLogEntryType);
                log.Close();
            }
        }

        public static string GenerateRandomText(int maxlength, bool ultraStrong) {
            char[] ultraChars = "!@#$%^&*~|=?{}[]qwertyuiopasdfghjklzxcvbnm1234567890!@#$%^&*~|=?{}[]".ToCharArray();
            char[] normalChars = "qwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();

            string password = string.Empty;
            for (int i = 0; i < maxlength; i++) {
                char n = ' ';
                if (ultraStrong) n = ultraChars[GetRandomNumber(ultraChars.Length - 1)];
                if (!ultraStrong) n = normalChars[GetRandomNumber(normalChars.Length - 1)];
                if (i % 2 == 0)
                    password += n.ToString().ToUpper();
                else
                    password += n;
            }
            return password;
        }

        public static int GetRandomNumber(int maxvalue) {
            System.Security.Cryptography.RandomNumberGenerator random =
                System.Security.Cryptography.RandomNumberGenerator.Create();

            byte[] r = new byte[1];
            random.GetBytes(r);
            double val = (double)r[0] / byte.MaxValue;
            int i = (int)Math.Round(val * maxvalue, 0);
            return i;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GenerateUserName(string name, string surename)
        {
            string firstname = name.Trim().Substring(0, 1);
            if (surename.Trim().Length > 5)
            {
                return surename.Trim() + firstname;
            }
            else {
                return (surename.Trim() + name.Trim()).PadRight(6, ' ').Trim();
            }
        }
        public static string GenerateUserNameForExistUser(string userName, int count)
        {
            return userName.Trim() + count.ToString();
        }
    }
}
