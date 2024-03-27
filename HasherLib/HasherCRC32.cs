using System.IO;
namespace hasherLib {
    internal class HasherCRC32 : IHasher {

        private string filePath;

        public HasherCRC32(string filePath) {
            this.filePath = filePath;
        }

        public ulong getHash() {
            ulong[] crc_table = new ulong[256];
            ulong crc = 0;

            for (ulong i = 0; i < 256; i++) {
                crc = i;
                for (int j = 0; j < 8; j++) {
                    crc = ((crc & 1) == 1) ? (crc >> 1) ^ 0xEDB88320UL : crc >> 1;
                }
                crc_table[i] = crc;
            }

            int bufferSize = 1024;

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                int i = 0;

                while ((bytesRead = fileStream.Read(buffer, 0, bufferSize)) > 0) {
                    while ((bytesRead--) != 0) {
                        crc = crc_table[(crc ^ buffer[++i]) & 0xFF] ^ (crc >> 8);
                    }
                }
            }
            return crc ^ 0xFFFFFFFFUL;
        }
    }
}