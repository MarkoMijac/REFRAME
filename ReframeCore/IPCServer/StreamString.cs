using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCServer
{
    internal class StreamString
    {
        private Stream ioStream;
        private UnicodeEncoding streamEncoding;

        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            int len;
            len = ioStream.ReadByte() * 16777216;
            len += ioStream.ReadByte() * 65536;
            len += ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            return streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > int.MaxValue)
            {
                len = int.MaxValue;
            }

            ioStream.WriteByte((byte)(len / 16777216)); //1st byte
            ioStream.WriteByte((byte)(len / 65536)); //2nd byte
            ioStream.WriteByte((byte)(len / 256)); //3rd byte
            ioStream.WriteByte((byte)(len & 255)); //4th byte
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }
}
