using System;

using System.IO;

namespace NewGamePlus {
    public static class SavePaths {
        private const string SteamGameID = "782330";
        private static readonly string SteamId64;
        private static readonly string SteamSavePath;
        private static readonly string BnetSavePath;

        static SavePaths() {
            string steamSavePath = Path.Combine(Utilities.GetSteamPath(), "userdata");
            foreach (var steamId3 in Directory.GetDirectories(steamSavePath, "*.*", SearchOption.TopDirectoryOnly)) {
                string tempPath = Path.Combine(steamId3, SteamGameID, "remote", "GAME-AUTOSAVE0");
                if (Directory.Exists(tempPath)) {
                    SteamSavePath = tempPath;
                    SteamId64 = Utilities.SteamId3ToId64(Path.GetFileNameWithoutExtension(steamId3));
                    break;
                }
            }

            // TODO(mcdalcin): Get it to work with Bethesda.
            string bnetSavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games", "id Software", "DOOMEternal", "base", "savegame", "GAME-AUTOSAVE0");
            BnetSavePath = Directory.Exists(bnetSavePath) ? bnetSavePath : "";
        }

        public static string GetSavePath() {
            return IsPlatformSteam() ? SteamSavePath : BnetSavePath;
        }

        public static string GetAAD(string filename) {
            if (IsPlatformSteam()) {
                return SteamId64 + "MANCUBUS" + filename;
            } else {
                return "BETHESDA -- CURRENTLY UNSUPPORTED.";
            }
        }

        private static bool IsPlatformSteam() {
            return !String.IsNullOrEmpty(SteamSavePath);
        }

    }
}