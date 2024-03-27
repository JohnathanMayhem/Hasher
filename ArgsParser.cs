using System.IO;
using System;
using CommandLine;
namespace hasherLib {
    class Options
    {
        private string[] _files = new string[0];
        [Option('f', "files", Required = true, HelpText = "File paths")]
        public IEnumerable<string> Files {
            get { return _files; }
            set { _files = value.ToArray(); }
        }
        public string[] FilesArrays {
            get { return _files; }
        }

        [Option('m', "mode", Required = true, HelpText = "Type of hash (CRC32 or SUM32)")]
        public string HashType { get; set; }
    }

    class ArgsParser {

        public List<string> filePaths;
        public HasherType mode;

        public ArgsParser(string[] args) {
            filePaths = new List <string> ();
            parse(args);
        }

        private void parse(string[] args) {
            try {
                Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    if (o.Files != null)
                    {
                        foreach (string file in o.Files)
                        {
                            if (File.Exists(file)) {
                                filePaths.Add(file);
                            } 
                            else {
                                Console.WriteLine($"File {file} does not exist");
                            }
                        }
                    }

                    if (o.HashType != null)
                    {
                        switch (o.HashType.ToLower())
                        {
                            case "crc32":
                                mode = HasherType.CRC32;
                                break;
                            case "sum32":
                                mode = HasherType.SUM32;
                                break;
                            default:
                                Console.WriteLine("Incorrect mode. Use: CRC32 or SUM32.");
                                break;
                        }
                    }
                });
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}