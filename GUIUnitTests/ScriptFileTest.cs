using System.Text;

namespace ScriptUnitTests
{
    /// <summary>
    /// Tests <see cref="ScriptFile"/> class read and writes.
    /// </summary>
    [TestClass]
    public class ScriptFileTest
    {


        /// <summary>
        /// Tests write with a mock SteamWriter
        /// Input: An SteamWriter which throws IOException when written
        /// Expects: IOException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void Write_StreamWriterError_ThrowsIOException()
        {
            // arrange
            StreamWriterMock mock = new StreamWriterMock();
            string[] script = new string[1]
            {
                "circle 10"
            };
            // act
            using (mock)
            {
                ScriptFile.Write(mock, script);
            }
        }

        /// <summary>
        /// Tests read with a mock SteamReader
        /// Input: An SteamReader which throws IOException when read
        /// Expects: IOException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void Read_StreamReadError_ThrowsIOException()
        {
            // arrange
            StreamReaderMock mock = new StreamReaderMock();

            // act
            using (mock)
            {
                ScriptFile.Read(mock);
            }

        }


    }


    /// <summary>
    /// StreamWriterMock, used to simulate an IOException when wrote to.
    /// </summary>
    public class StreamWriterMock : StreamWriter
    {
        public StreamWriterMock() : base("C:\\Users\\Kevin Brewster\\Documents\\Test")
        {
        }

        public override void WriteLine(string? value)
        {
            throw new IOException();
        }
    }

    /// <summary>
    /// StreamReaderMock, used to simulate an IOException when read.
    /// </summary>
    public class StreamReaderMock : StreamReader
    {
        public StreamReaderMock() : base("C:\\Users\\Kevin Brewster\\Documents\\Test")
        {
        }

        public override string? ReadLine()
        {
            throw new IOException();
        }
    }


}