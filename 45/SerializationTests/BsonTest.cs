using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationTests
{
    [TestClass]
    public class BsonTest : BaseTest
    {
        private readonly JsonSerializer jsonSerializer;

        public BsonTest()
        {
            this.jsonSerializer = new JsonSerializer();
            this.jsonSerializer.Formatting = Formatting.None;
            this.jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            this.jsonSerializer.TypeNameHandling = TypeNameHandling.Objects;
        }

        [TestMethod]
        public void BsonSerializeWithCompression()
        {
            Run(SerializeWithJsonNetBson, compress: true);
        }

        [TestMethod]
        public void BsonSerializeWithoutCompression()
        {
            Run(SerializeWithJsonNetBson, compress: false);
        }

        private byte[] SerializeWithJsonNetBson(IDictionary<string, object> data)
        {
            if (data == null || data.Keys.Count == 0) return null;

            using (var ms = new MemoryStream())
            {
                using (var writer = new BsonWriter(ms))
                {
                    this.jsonSerializer.Serialize(writer, data);
                }
                return ms.ToArray();
            }
        }
    }
}
