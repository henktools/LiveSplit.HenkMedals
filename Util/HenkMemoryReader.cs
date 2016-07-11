using LiveSplit.ComponentUtil;
using LiveSplit.Model;
using LiveSplit.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.HenkMedals.Util
{
    class HenkMemoryReader
    {
        // So, uhhh, yeah. Henk doesn't seem to have a reliable way of getting
        // the current level object. Instead, just assume a given order of
        // levels and use the earliest one that is not a 4 (or 1 for bonuses/
        // challenges) as the current level.
        static RegularLevel[] LevelList = {
            new RegularLevel("Hello World",            new TimeSpan(0, 0, 0, 18, 840), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x748, 0x28c)),
            new RegularLevel("Buttslide Basics",       new TimeSpan(0, 0, 0, 19, 500), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x748, 0x1c4)),
            new RegularLevel("Loop of Destiny",        new TimeSpan(0, 0, 0, 25, 730), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x748,  0xfc)),
            new RegularLevel("Back 2 Back",            new TimeSpan(0, 0, 0, 24, 640), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x748,  0x34)),
            new RegularLevel("Hot Feet",               new TimeSpan(0, 0, 0, 27, 200), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8, 0x73c)),
            new RegularLevel("Betsy",                  new TimeSpan(0, 0, 0, 20, 840), 1, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8, 0x674)),
            new RegularLevel("Swan Henk",              new TimeSpan(0, 0, 0, 32, 000), 1, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8, 0x5ac)),
            // Throttle Wide Open
            new RegularLevel("The Classic",            new TimeSpan(0, 0, 0, 25, 770), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8, 0x4e4)),
            new RegularLevel("Sweet Flow",             new TimeSpan(0, 0, 0, 21, 230), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8, 0x41c)),
            new RegularLevel("Multipath",              new TimeSpan(0, 0, 0, 23, 270), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8, 0x354)),
            new RegularLevel("Pro Skater",             new TimeSpan(0, 0, 0, 23, 200), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8, 0x28c)),
            new RegularLevel("Wall Tricks",            new TimeSpan(0, 0, 0, 23, 860), 4, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8, 0x1c4)),
            new RegularLevel("Boxing Betsy",           new TimeSpan(0, 0, 0, 25, 210), 1, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8,  0xfc)),
            new RegularLevel("Corporate Betsy",        new TimeSpan(0, 0, 0, 27, 000), 1, new DeepPointer("ActionHenk.exe", 0x00A363D8, 0x770,  0x68, 0x3c8,  0x34)),
            // The Fever
            new RegularLevel("Tornado",                new TimeSpan(0, 0, 0, 24, 770), 4, new DeepPointer("ActionHenk.exe", 0x00A364A4, 0x48c,  0x68, 0x1fc, 0x4f4)),
            new RegularLevel("Spaghetti",              new TimeSpan(0, 0, 0, 31, 400), 4, new DeepPointer("ActionHenk.exe", 0x00A364A4, 0x48c,  0x68, 0x1fc, 0x42c)),
            new RegularLevel("Boing",                  new TimeSpan(0, 0, 0, 23, 190), 4, new DeepPointer("ActionHenk.exe", 0x00A364A4, 0x48c,  0x68, 0x1fc, 0x364)),
            new RegularLevel("Easy Peasy",             new TimeSpan(0, 0, 0, 23, 190), 4, new DeepPointer("ActionHenk.exe", 0x00A364A4, 0x48c,  0x68, 0x1fc, 0x29c)),
            new RegularLevel("Party Hardy",            new TimeSpan(0, 0, 0, 29, 840), 4, new DeepPointer("ActionHenk.exe", 0x00A363E0, 0x76c,  0x70,  0xb8, 0x1c4)),
            new RegularLevel("Neil",                   new TimeSpan(0, 0, 0, 31, 650), 1, new DeepPointer("ActionHenk.exe", 0x00A363E0, 0x76c,  0x70,  0xb8,  0xfc)),
            new RegularLevel("Gabber Betsy",           new TimeSpan(0, 0, 0, 23, 000), 1, new DeepPointer("ActionHenk.exe", 0x00A363E0, 0x76c,  0x70,  0xb8,  0x34)),
            // Hold on Tight
            new RegularLevel("Getting Hooked",         new TimeSpan(0, 0, 0, 14, 350), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720, 0x73c)),
            new RegularLevel("Smooth Swinging",        new TimeSpan(0, 0, 0, 18, 120), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720, 0x674)),
            new RegularLevel("Halfway Hook",           new TimeSpan(0, 0, 0, 19, 060), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720, 0x5ac)),
            new RegularLevel("The Drop",               new TimeSpan(0, 0, 0, 17, 800), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720, 0x4e4)),
            new RegularLevel("Close Call",             new TimeSpan(0, 0, 0, 22, 810), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720, 0x41c)),
            new RegularLevel("Hook Betsy",             new TimeSpan(0, 0, 0, 29, 340), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720, 0x354)),
            new RegularLevel("IT Neil",                new TimeSpan(0, 0, 0, 23, 000), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720, 0x28c)),
            // Spooky Times
            new RegularLevel("Gotta Ghost Fast",       new TimeSpan(0, 0, 0, 21, 980), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720, 0x1c4)),
            new RegularLevel("Wicked Waves",           new TimeSpan(0, 0, 0, 22, 270), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720,  0xfc)),
            new RegularLevel("Cursed Curves",          new TimeSpan(0, 0, 0, 22, 400), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1fc, 0x720,  0x34)),
            new RegularLevel("Deadly Drops",           new TimeSpan(0, 0, 0, 21, 010), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1d4, 0x67c, 0x41c)),
            new RegularLevel("Tricks & Treats",        new TimeSpan(0, 0, 0, 23, 650), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1d4, 0x67c, 0x354)),
            new RegularLevel("Kentony",                new TimeSpan(0, 0, 0, 32, 420), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1d4, 0x67c, 0x28c)),
            new RegularLevel("Zombie Neil",            new TimeSpan(0, 0, 0, 24, 000), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1d4, 0x67c, 0x1c4)),
            // No Diving
            new RegularLevel("Rise 'n Shine",          new TimeSpan(0, 0, 0, 27, 850), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1d4, 0x67c,  0xfc)),
            new RegularLevel("Pipe 'n Slide",          new TimeSpan(0, 0, 0, 25, 000), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1d4, 0x67c,  0x34)),
            new RegularLevel("Quick Tricks",           new TimeSpan(0, 0, 0, 11, 230), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x1d4, 0x720,  0x34)),
            new RegularLevel("Wave Rider",             new TimeSpan(0, 0, 0, 29, 700), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x184, 0x15c, 0x41c)),
            new RegularLevel("Down The Tube",          new TimeSpan(0, 0, 0, 28, 480), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x184, 0x15c, 0x354)),
            new RegularLevel("Lifeguard Betsy",        new TimeSpan(0, 0, 0, 31, 580), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x184, 0x15c, 0x28c)),
            new RegularLevel("Summer Henk",            new TimeSpan(0, 0, 0, 25, 000), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x54c, 0x184, 0x15c, 0x1c4)),
            // Feet in the Sand
            new RegularLevel("Deep Dive",              new TimeSpan(0, 0, 0, 27, 540), 4, new DeepPointer("mono.dll",       0x001F20ac, 0x54c, 0x184, 0x15c,  0xfc)),
            new RegularLevel("The Plunger",            new TimeSpan(0, 0, 0, 20, 690), 4, new DeepPointer("mono.dll",       0x001F20ac, 0x54c, 0x184, 0x15c,  0x34)),
            new RegularLevel("The Big Climb",          new TimeSpan(0, 0, 0, 27, 410), 4, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x184, 0x348, 0x1c4)),
            new RegularLevel("Sand Snails",            new TimeSpan(0, 0, 0, 26, 520), 4, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x184, 0x348,  0xfc)),
            new RegularLevel("Leap of Faith",          new TimeSpan(0, 0, 0, 25, 620), 4, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x184, 0x348,  0x34)),
            new RegularLevel("Cedar",                  new TimeSpan(0, 0, 0, 31, 840), 1, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8, 0x73c)),
            new RegularLevel("Rastafro",               new TimeSpan(0, 0, 0, 29, 000), 1, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8, 0x674)),
            // King of the Jungle
            new RegularLevel("Full Swing Ahead",       new TimeSpan(0, 0, 0, 21, 190), 4, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8, 0x5ac)),
            new RegularLevel("Hook Maze",              new TimeSpan(0, 0, 0, 18, 050), 4, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8, 0x4e4)),
            new RegularLevel("Throwback",              new TimeSpan(0, 0, 0, 18, 000), 4, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8, 0x41c)),
            new RegularLevel("Flappy Swing",           new TimeSpan(0, 0, 0, 21, 810), 4, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8, 0x354)),
            new RegularLevel("Right Round Baby",       new TimeSpan(0, 0, 0, 25, 050), 4, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8, 0x28c)),
            new RegularLevel("Jungle Cedar",           new TimeSpan(0, 0, 0, 34, 060), 1, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8, 0x1c4)),
            new RegularLevel("Tribal Cedar",           new TimeSpan(0, 0, 0, 27, 000), 1, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8,  0xfc)),
            // Night Crisis
            new RegularLevel("Sick Burn",              new TimeSpan(0, 0, 0, 25, 940), 4, new DeepPointer("mono.dll",       0x001F20AC, 0x54c, 0x134,  0xb8,  0x34)),
            new RegularLevel("Full Stop",              new TimeSpan(0, 0, 0, 20, 500), 4, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154,  0x8c, 0x534,  0x34)),
            new RegularLevel("The Wall",               new TimeSpan(0, 0, 0, 31, 650), 4, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c, 0x73c)),
            new RegularLevel("Spinebreaker",           new TimeSpan(0, 0, 0, 27, 960), 4, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c, 0x674)),
            new RegularLevel("Pinball",                new TimeSpan(0, 0, 0, 29, 990), 4, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c, 0x5ac)),
            new RegularLevel("Kentinator",             new TimeSpan(0, 0, 0, 49, 280), 1, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c, 0x4e4)),
            new RegularLevel("Afronaut",               new TimeSpan(0, 0, 0, 29, 000), 1, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c, 0x41c)),
            // Back to the City
            new RegularLevel("Transition Kings",       new TimeSpan(0, 0, 0, 30, 600), 4, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c, 0x354)),
            new RegularLevel("Hardcore Hooks",         new TimeSpan(0, 0, 0, 38, 200), 4, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c, 0x28c)),
            new RegularLevel("Hi-speed Hysteria",      new TimeSpan(0, 0, 0, 32, 740), 4, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c, 0x1c4)),
            new RegularLevel("The Ultimate Test",      new TimeSpan(0, 0, 0, 38, 260), 4, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c,  0xfc)),
            new RegularLevel("The Way of the Ninja",   new TimeSpan(0, 0, 0, 45, 290), 4, new DeepPointer("ActionHenk.exe", 0x00A2FBB0, 0x154, 0x644, 0x67c,  0x34)),
            new RegularLevel("90s Henk",               new TimeSpan(0, 0, 1, 11, 150), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8, 0x73c)),
            new RegularLevel("Action Hank",            new TimeSpan(0, 0, 3, 10, 000), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8, 0x674)),
            // All I Want for Henkmas
            new RegularLevel("The Highway",            new TimeSpan(0, 0, 0, 18, 660), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8, 0x5ac)),
            new RegularLevel("Back and Forth",         new TimeSpan(0, 0, 0, 25, 140), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8, 0x4e4)),
            new RegularLevel("Slidey Slidey",          new TimeSpan(0, 0, 0, 24, 960), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8, 0x41c)),
            new RegularLevel("Bumper Jumper",          new TimeSpan(0, 0, 0, 24, 550), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8, 0x354)),
            new RegularLevel("The Zigzag",             new TimeSpan(0, 0, 0, 22, 260), 4, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8, 0x28c)),
            new RegularLevel("Santa Henk",             new TimeSpan(0, 0, 0, 24, 450), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8, 0x1c4)),
            new RegularLevel("Henkdolph",              new TimeSpan(0, 0, 0, 30, 000), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8,  0xfc)),
            // Credits
            new RegularLevel("Credits",                new TimeSpan(0, 0, 1, 01, 000), 1, new DeepPointer("mono.dll",       0x001f20ac, 0x2ac, 0x524, 0x2c8,  0x34))
        };

        private static Process _game = null;

        private static void TryConnect()
        {
            _game = Process.GetProcessesByName("ActionHenk").FirstOrDefault(x => !x.HasExited);
            Log.Info(String.Format("Found game: {0}", _game.ProcessName));
        }

        // The current level object is the first one that is not completed.
        public static RegularLevel CurrentLevelObject()
        {
            TryConnect();
            return LevelList.First(level => !level.IsCompleted(_game));
        }

        public static TimeSpan? CurrentRainbowTime()
        {
            return CurrentLevelObject().RainbowTime;
        }

        public static String CurrentLevelName()
        {
            return CurrentLevelObject().LevelName;
        }
    }
}