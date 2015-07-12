using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SerializationTests
{
    [TestClass]
    public class JavaScriptSerializerTest : BaseTest
    {
        [TestMethod]
        public void JsonSerializeWithCompression()
        {
            Run(SerializeWithJsonFormatter, compress: true);
        }

        [TestMethod]
        public void JsonSerializeWithoutCompression()
        {
            Run(SerializeWithJsonFormatter, compress: false);
        }

        byte[] SerializeWithJsonFormatter(IDictionary<string, object> data)
        {
            if (data == null || data.Keys.Count == 0) return null;

            var s = new JavaScriptSerializer();
            return Encoding.UTF8.GetBytes(s.Serialize(data));
        }
    }
}
