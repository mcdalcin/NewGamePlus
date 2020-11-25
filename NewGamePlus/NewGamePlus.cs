using System;
using System.IO;

namespace NewGamePlus {
    class NewGamePlus {

        static void Main() {
            string savePath = SavePaths.GetSavePath();
            if (String.IsNullOrEmpty(savePath)) {
                Console.WriteLine("Unable to find save path. Please make sure that there is a save in slot 1.");
            }
            Console.WriteLine("Save path found: " + savePath + "\n\n");
            
            Console.Write("CAUTION: This will overwrite your save slot 1. Are you sure you want to continue? [y / n]: ");
            ConsoleKey response = Console.ReadKey(false).Key;
            if (response != ConsoleKey.Y) {
                Console.WriteLine("Aborted. Press any key to exit.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n\nWriting NewGame+ files...");
            EncryptAndWriteFiles(savePath);
            Console.WriteLine("\n\nFinished successfully. Press any key to exit.");
            Console.ReadKey();
        }

        private static void EncryptAndWriteFiles(string savePath) {
            String[] fileNames = { "game.details", "game_duration.dat" };
            foreach (string fileName in fileNames) {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var stream = assembly.GetManifestResourceStream("NewGamePlus." + fileName);
                byte[] fileData = new byte[stream.Length];
                stream.Read(fileData, 0, fileData.Length);
                
                byte[] encryptedFileData = Crypto.EncryptAndDigest(SavePaths.GetAAD(fileName), fileData);
                
                string filePath = Path.Combine(savePath, fileName);
                File.WriteAllBytes(filePath, encryptedFileData);
                Console.WriteLine("Successfully wrote to " + filePath);
            }
        }
    }
}
