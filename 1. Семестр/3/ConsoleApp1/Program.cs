using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding encoding = Encoding.UTF8;

            string str = " ";
            using (StreamReader sr = new StreamReader(@"text.txt"))
            {
                str = sr.ReadToEnd();
            }

            var bytestr = Encoding.UTF8.GetBytes(str);
            string basestring = Convert.ToBase64String(bytestr);

            //Console.WriteLine(basestring);
            var bstring = ZBase64Encoder.Encode(bytestr);
            Console.WriteLine(bstring);

            var s = ZBase64Encoder.Decode(ZBase64Encoder.Encode(bytestr));
            Console.WriteLine();

            string sss = ZBase64Encoder.Enc("208 147 208 190 208 178 208 190 209 128 208 190 208 189 208 190 208 186", 
                "208 146 208 176 208 180 208 184 208 188");
            Console.WriteLine(sss);
            Console.WriteLine();
            Console.WriteLine(ZBase64Encoder.ShannonEntropy(str));
            Console.WriteLine(ZBase64Encoder.ShannonEntropy(bstring));
            Console.ReadKey();
        }



        public static class ZBase64Encoder
        {
            private const string EncodingTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            private static readonly byte[] DecodingTable = new byte[128];
            static ZBase64Encoder()
            {
                for (var i = 0; i < DecodingTable.Length; ++i)
                {
                    DecodingTable[i] = byte.MaxValue;
                }
                for (var i = 0; i < EncodingTable.Length; ++i)
                {
                    DecodingTable[EncodingTable[i]] = (byte)i;
                }
            }

            public static string Encode(byte[] data)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                var encodedResult = new StringBuilder((int)Math.Ceiling(data.Length * 8.0 / 6.0));

                for (var i = 0; i < data.Length; i += 6)
                {
                    var byteCount = Math.Min(6, data.Length - i);

                    ulong buffer = 0;
                    for (var j = 0; j < byteCount; ++j)
                    {
                        buffer = (buffer << 8) | data[i + j];
                    }

                    var bitCount = byteCount * 8;
                    while (bitCount > 0)
                    {
                        var index = bitCount >= 6
                                    ? (int)(buffer >> (bitCount - 6)) & 0x3f
                                    : (int)(buffer & (ulong)(0x3f >> (6 - bitCount))) << (6 - bitCount);

                        encodedResult.Append(EncodingTable[index]);
                        bitCount -= 6;
                    }
                }

                return encodedResult.ToString();
            }

            public static byte[] Decode(string data)
            {
                if (data == string.Empty)
                {
                    return new byte[0];
                }

                var result = new List<byte>((int)Math.Ceiling(data.Length * 6.0 / 8.0));

                var index = new int[8];
                for (var i = 0; i < data.Length;)
                {
                    i = CreateIndexByOctetAndMovePosition(ref data, i, ref index);

                    var shortByteCount = 0;
                    ulong buffer = 0;
                    for (var j = 0; j < 8 && index[j] != -1; ++j)
                    {
                        buffer = (buffer << 5) | (ulong)(DecodingTable[index[j]] & 0x1f);
                        shortByteCount++;
                    }

                    var bitCount = shortByteCount * 6;
                    while (bitCount >= 8)
                    {
                        result.Add((byte)((buffer >> (bitCount - 8)) & 0xff));
                        bitCount -= 8;
                    }
                }

                return result.ToArray();
            }

            private static int CreateIndexByOctetAndMovePosition(ref string data, int currentPosition, ref int[] index)
            {
                var j = 0;
                while (j < 8)
                {
                    if (currentPosition >= data.Length)
                    {
                        index[j++] = -1;
                        continue;
                    }

                    if (IgnoredSymbol(data[currentPosition]))
                    {
                        currentPosition++;
                        continue;
                    }

                    index[j] = data[currentPosition];
                    j++;
                    currentPosition++;
                }

                return currentPosition;
            }

            private static bool IgnoredSymbol(char checkedSymbol)
            {
                return checkedSymbol >= DecodingTable.Length || DecodingTable[checkedSymbol] == byte.MaxValue;
            }




            public static string Enc(string plaintext, string pad)
            {
                var data = Encoding.UTF8.GetBytes(plaintext);
                var key = Encoding.UTF8.GetBytes(pad);

                return Convert.ToBase64String(data.Select((b, i) => (byte)(b ^ key[i % key.Length])).ToArray());
            }

            public static string Dec(string enctext, string pad)
            {
                var data = Convert.FromBase64String(enctext);
                var key = Encoding.UTF8.GetBytes(pad);

                return Encoding.UTF8.GetString(data.Select((b, i) => (byte)(b ^ key[i % key.Length])).ToArray());
            }

            public static double ShannonEntropy(string s)
            {
                var map = new Dictionary<char, int>();

                foreach (char c in s)
                {
                    if (!map.ContainsKey(c))
                        map.Add(c, 1);
                    else
                        map[c] += 1;
                }

                double result = 0.0;
                int len = s.Length;
                foreach (var item in map)
                {
                    var frequency = (double)item.Value / len;
                    result -= frequency * (Math.Log(frequency) / Math.Log(2));
                }

                Console.WriteLine($"Enthropy: {Math.Round(result, 3)}");

                return result;
            }

            public static double BinEntropy(string s)
            {
                var map = new Dictionary<char, int>();

                foreach (char c in s)
                {
                    if (!map.ContainsKey(c))
                        map.Add(c, 1);
                    else
                        map[c] += 1;
                }

                double result = 0.0;
                int len = s.Length;
                foreach (var item in map)
                {
                    result = (Math.Log(map.Count, 2));
                }

                Console.WriteLine($"Binary Enthropy: {result}");

                return result;
            }
        }

    }
}
