﻿using System;
using System.Diagnostics;
using System.Threading;
using Smurf.GlobalOffensive.SDK;

namespace Smurf.GlobalOffensive
{
    internal static class Program
    {
        #region Methods
        private static void Main()
        {
            Thread thread1 = new Thread(PrintInfo);
            Thread thread2 = new Thread(UpdateBhop);
            Thread thread3 = new Thread(UpdateRcs);
            Thread thread4 = new Thread(UpdateSettings);
            Thread thread5 = new Thread(UpdateKeyUtils);
            Thread thread6 = new Thread(UpdateAutoPistol);
            Thread thread7 = new Thread(UpdateAimAssist);

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"> Waiting for {Core.GameTitle} to start up...");
            while ((Core.HWnd = WinAPI.FindWindowByCaption(Core.HWnd, Core.GameTitle)) == IntPtr.Zero)
                Thread.Sleep(250);

            Console.Clear();

            Process[] process = Process.GetProcessesByName("csgo");
            Core.Attach(process[0]);

            StartThreads(thread1, thread2, thread3, thread4, thread5, thread6, thread7);

            while (true)
            {
                Core.Objects.Update();
                Core.TriggerBot.Update();
                Core.SoundEsp.Update();
                Core.Radar.Update();
                Thread.Sleep(1);
            }
        }

        private static void UpdateAimAssist()
        {
            while (true)
            {
                Core.AimAssist.Update();
                Thread.Sleep(2);
            }
        }

        private static void StartThreads(params Thread[] threads)
        {
            foreach (var thread in threads)
            {
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
        }
        private static void PrintInfo()
        {
#if DEBUG
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"State: {Core.Client.State}\n\n");
                Console.WriteLine();
                Console.WriteLine("Config Path: {0}", Settings.Path);
                Console.WriteLine();


                if (Core.Client.InGame && Core.LocalPlayer != null && Core.LocalPlayerWeapon != null && Core.LocalPlayer.IsValid && Core.LocalPlayer.IsAlive)
                {
                    var me = Core.LocalPlayer;
                    var myWeapon = Core.LocalPlayerWeapon;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("--- Aim Assist ---");
                    if (Core.AimAssist.AimTarget != null)
                    {
                        Console.WriteLine($"Target ID: \t {Core.AimAssist.AimTarget.Id}");
                        Console.WriteLine($"Target Distance: \t {Core.AimAssist.AimTarget.DistanceMeters}");
                    }
                    Console.WriteLine("--- Aim Assist ---");
                    Console.WriteLine($"Players: \t{Core.Objects.Players.Count}");
                    Console.WriteLine($"ID:\t\t{me.Id}");
                    Console.WriteLine($"Health:\t\t{me.Health}");
                    Console.WriteLine($"Armor:\t\t{me.Armor}");
                    Console.WriteLine($"VecOrigin:\t{me.Position}");
                    Console.WriteLine($"Team:\t\t{me.Team}");
                    Console.WriteLine($"Player Count:\t{Core.Objects.Players.Count}");
                    Console.WriteLine($"Velocity: \t{me.Velocity}");
                    Console.WriteLine($"Shots Fired: \t{me.ShotsFired}");
                    Console.WriteLine($"VecPunch: \t{me.VecPunch}");
                    Console.WriteLine($"Immune: \t{me.GunGameImmune}");
                    Console.WriteLine($"Active Weapon: \t{myWeapon.WeaponName}");
                    Console.WriteLine($"Active Weapon ID: \t{myWeapon.ItemDefinitionIndex}");
                    Console.WriteLine($"Clip1: \t\t{myWeapon.Clip1}");
                    Console.WriteLine($"Flags: \t\t{me.Flags}");
                    Console.WriteLine($"Flash: \t\t{me.FlashMaxAlpha}");
                    Console.WriteLine($"Weapon Group: \t{myWeapon.WeaponType}");
                    Console.WriteLine($"Zoom Level: \t{myWeapon.ZoomLevel}");
                    Console.WriteLine($"Recoil Control Yaw: \t{Core.ControlRecoil.RandomYaw}");
                    Console.WriteLine($"Recoil Control Pitch: \t{Core.ControlRecoil.RandomPitch}");
                    Console.WriteLine($"Trigger Delay First: \t{Core.TriggerBot.TriggerDelayFirstRandomize}");
                    Console.WriteLine($"Trigger Delay Shots1: \t{Core.TriggerBot.TriggerDelayShotsRandomize}");
                }

                Thread.Sleep(500);
            }
#endif

        }
        private static void UpdateBhop()
        {
            while (true)
            {
                Core.BunnyJump.Update();
                Thread.Sleep(5);
            }
        }
        private static void UpdateRcs()
        {
            while (true)
            {
                Core.ControlRecoil.Update();
                Thread.Sleep(1);
            }
        }
        private static void UpdateSettings()
        {
            while (true)
            {
                Core.Settings.Update();
                Thread.Sleep(10);
            }
        }
        private static void UpdateKeyUtils()
        {
            while (true)
            {
                Core.KeyUtils.Update();
                Core.NoFlash.Update();
                Thread.Sleep(10);
            }
        }
        private static void UpdateAutoPistol()
        {
            while (true)
            {
                Core.AutoPistol.Update();
                Thread.Sleep(1);
            }
        }
        #endregion
    }
}
