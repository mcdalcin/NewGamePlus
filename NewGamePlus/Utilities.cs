using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;

namespace NewGamePlus {
    public static class Utilities {
        public static string GetSteamPath() {
            using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Valve\Steam");
            return (string)reg.GetValue("InstallPath", "");
        }

        public static byte[] RandomBytes(int size) {
            byte[] output = new byte[size];
            new Random().NextBytes(output);
            return output;
        }

        /** Computes and returns the SteamId64 from the supplied SteamId3. See https://steamid.io for more info on Steam ids. */
        public static string SteamId3ToId64(string sid3) {
            return (ulong.Parse(sid3) + 76561197960265728UL).ToString();
        }

        /** Checks for a matching UUID for the Bethesda version. */
        public static bool CheckUUID(string s) {
            Regex re = new Regex("^[0-9a-f]{8}-?[0-9a-f]{4}-?[0-9a-f]{4}-?[0-9a-f]{4}-?[0-9a-f]{12}$", RegexOptions.IgnoreCase);
            return re.IsMatch(s);
        }
    }
}