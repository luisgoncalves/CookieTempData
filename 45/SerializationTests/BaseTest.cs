using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationTests
{
    public abstract class BaseTest
    {
        private const int NRuns = 10000;
        private readonly Fixture fixture = new Fixture();

        [Serializable]
        public class Data1
        {
            public string Prop1 { get; set; }
            public int[] Prop2 { get; set; }
        }

        [Serializable]
        public class Data2
        {
            public string Some1 { get; set; }
            public int[] Some2 { get; set; }
        }

        protected void Run(Func<IDictionary<string, object>, byte[]> serialize, bool compress)
        {
            var sw = Stopwatch.StartNew();

            // Try to simulate some variety of data
            var data = new Dictionary<string, object>
            {
                { "one",  fixture.Create<int>() },
                { "two",  fixture.Create<long>() },
                { "three", fixture.Create<string>() },
                { "four", fixture.Create<Data1>() },
                { "five", fixture.Create<Data2>() },
            };

            byte[] serializedData = null;

            for (int i = 0; i < NRuns; i++)
            {
                serializedData = serialize(data);
                if (compress)
                {
                    serializedData = Compress(serializedData);
                }
            }

            sw.Stop();
            Console.WriteLine("Individual size: {0}, Total time: {1}", serializedData.Length, sw.Elapsed);
        }

        /// <summary>
        /// From CookieTempDataProvider
        /// </summary>
        private byte[] Compress(byte[] data)
        {
            if (data == null || data.Length == 0) return null;

            using (var input = new MemoryStream(data))
            {
                using (var output = new MemoryStream())
                {
                    using (Stream cs = new DeflateStream(output, CompressionMode.Compress))
                    {
                        input.CopyTo(cs);
                    }

                    return output.ToArray();
                }
            }
        }
    }
}
