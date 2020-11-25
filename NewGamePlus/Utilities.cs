using Microsoft.Win32;
using System;

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

        public static string Id3ToId64(string sid3) {
            return (ulong.Parse(sid3) + 76561197960265728UL).ToString();
        }
    }
}