using System.IO;
namespace hasherLib {
    internal class HasherSUM32 : IHasher {
        private string filePath;

        public HasherSUM32(string filePath) {
            this.filePath = filePath;
        }

        public ulong getHash() {
            int bufferSize = 4;
            uint num = 0;
            ulong crc = 0;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
            
                while ((bytesRead = fileStream.Read(buffer, 0, bufferSize)) > 4) {
                    for (int i = 3; i >= 0; --i) {
                        num |= ((uint)buffer[i])<<(i*8);
                    }
                    crc += num;
                }
            }
            return crc;
        }
    }
}