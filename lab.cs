using hasherLib;
using System;
using System.Collections.Generic;
namespace lab {
    class lab {
        static void Main(string[] args) {
            if (args.Length != 0) {
                ArgsParser parser = new ArgsParser(args);
                if (parser.filePaths.Count != 0) {
                    foreach (string path in parser.filePaths) {
                        Console.WriteLine($"{path}: {HasherFactory.getHasher(path, parser.mode).getHash():X}");
                    }
                }
            }

        }
    }
}


