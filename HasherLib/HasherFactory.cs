namespace hasherLib {
    class HasherFactory {
        private HasherFactory() {}
        
        public static IHasher getHasher(string filePath, HasherType type) {
            IHasher hasher;
            switch (type) {
                case HasherType.CRC32:
                    hasher = new HasherCRC32(filePath);
                    break;
                case HasherType.SUM32:
                    hasher = new HasherSUM32(filePath);
                    break;
                default:
                    hasher = new HasherSUM32(filePath);
                    break;
            }
            return hasher;
        }
    }
}