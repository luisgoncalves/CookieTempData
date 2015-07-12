using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationTests
{
    [TestClass]
    public class BinaryFormatterTest : BaseTest
    {
        [TestMethod]
        public void BinarySerializeWithCompression()
        {
            Run(SerializeWithBinaryFormatter, compress: true);
        }

       /// <summary>
       /// From CookieTempDataProvider
       /// </summary>
        byte[] SerializeWithBinaryFormatter(IDictionary<string, object> data)
        {
            if (data == null || data.Keys.Count == 0) return null;

            var f = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                f.Serialize(ms, data);
                ms.Seek(0, SeekOrigin.Begin);
                return ms.ToArray();
            }
        }
    }
}
