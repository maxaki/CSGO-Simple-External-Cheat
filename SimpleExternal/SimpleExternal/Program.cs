﻿using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace SimpleExternal
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread thread1 = new Thread(UpdateInfo);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Smurf Bot 0.1 \n For local development use only. Do not redistribute");
            Console.Title = "Smurf Bot";

            Process process = Process.GetProcessesByName("csgo")[0];
            Smurf.GlobalOffensive.Smurf.Attach(process);

            thread1.IsBackground = true;
            thread1.Priority = ThreadPriority.Highest;
            thread1.Start();

            while (true)
            {
                //TODO add more threads
                Smurf.GlobalOffensive.Smurf.Objects.Update();
                Smurf.GlobalOffensive.Smurf.ControlRecoil.Update();
                Smurf.GlobalOffensive.Smurf.TriggerBot.Update();
                Smurf.GlobalOffensive.Smurf.KeyUtils.Update();
                Smurf.GlobalOffensive.Smurf.BunnyJump.Update();
                Smurf.GlobalOffensive.Smurf.Settings.Update();
                Thread.Sleep(10);
            }
        }

        private static void UpdateInfo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("State: {0}\n\n", Smurf.GlobalOffensive.Smurf.Client.State);

                if (Smurf.GlobalOffensive.Smurf.Client.InGame && Smurf.GlobalOffensive.Smurf.LocalPlayer != null && Smurf.GlobalOffensive.Smurf.LocalPlayerWeapon != null &&Smurf.GlobalOffensive.Smurf.LocalPlayer.IsValid)
                {
                    var me = Smurf.GlobalOffensive.Smurf.LocalPlayer;
                    var myWeapon = Smurf.GlobalOffensive.Smurf.LocalPlayerWeapon;

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("ID:\t\t{0}", me.Id);
                    Console.WriteLine("Health:\t\t{0}", me.Health);
                    Console.WriteLine("Armor:\t\t{0}", me.Armor);
                    Console.WriteLine("Position:\t{0}", me.Position);
                    Console.WriteLine("Team:\t\t{0}", me.Team);
                    Console.WriteLine("Player Count:\t{0}", Smurf.GlobalOffensive.Smurf.Objects.Players.Count);
                    Console.WriteLine("Velocity: \t{0}", me.GetVelocity());
                    Console.WriteLine("Shots Fired: \t{0}", me.ShotsFired);
                    Console.WriteLine("VecPunch: \t{0}", me.VecPunch);
                    Console.WriteLine("Immune: \t{0}", me.GunGameImmune);
                    Console.WriteLine("--LocalPlayerWeapon--");
                    Console.WriteLine("Clip1: \t{0}",myWeapon.Clip1);
                    Console.WriteLine("--Misc--");
                    Console.WriteLine("Bomb Time Left: \t {0}");
                    Console.WriteLine("Aim on target:  {0}\t", Smurf.GlobalOffensive.Smurf.TriggerBot.AimOntarget);
                }
                Thread.Sleep(500);
            }
        }
    }
}
