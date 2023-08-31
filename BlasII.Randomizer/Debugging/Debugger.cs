﻿using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.StatsSystem.Data;
using Il2CppTGK.Game.Managers;
using Il2CppTGK.Game.PlayerSpawn;
using UnityEngine;

namespace BlasII.Randomizer.Debugging;

public class Debugger
{
    public void ProcessInput()
    {
        // Restore health
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (Main.Randomizer.Data.TryGetRangeStat("Health", out RangeStatID health))
            {
                Main.Randomizer.Log("Restoring health");
                Main.Randomizer.PlayerStats.SetCurrentToMax(health);
            }
        }
        // Restore fervour
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            if (Main.Randomizer.Data.TryGetRangeStat("Fervour", out RangeStatID fervour))
            {
                Main.Randomizer.Log("Restoring fervour");
                Main.Randomizer.PlayerStats.SetCurrentToMax(fervour);
            }
        }
        // Add tears
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            if (Main.Randomizer.Data.TryGetValueStat("Tears", out ValueStatID tears))
            {
                Main.Randomizer.Log("Adding 5000 tears");
                Main.Randomizer.PlayerStats.AddToCurrentValue(tears, 5000);
            }
        }
        // Add marks
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            if (Main.Randomizer.Data.TryGetValueStat("Orbs", out ValueStatID orbs))
            {
                Main.Randomizer.Log("Adding 1 mark");
                Main.Randomizer.PlayerStats.AddToCurrentValue(orbs, 1);
            }
        }
        // Add all items
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            // Items
            foreach (var bead in Main.Randomizer.Data.GetAllRosaryBeads())
                Main.Randomizer.PlayerInventory.AddRosaryBeadAsync(bead, 0, 0, false);
            foreach (var prayer in Main.Randomizer.Data.GetAllPrayers())
                Main.Randomizer.PlayerInventory.AddPrayerAsync(prayer, 0, 0, false);
            foreach (var figurine in Main.Randomizer.Data.GetAllFigurines())
                Main.Randomizer.PlayerInventory.AddFigureAsync(figurine, 0, 0, false);

            // Abilities
            foreach (var ability in Main.Randomizer.Data.GetAllAbilities())
                CoreCache.AbilitiesUnlockManager.SetAbility(ability, true);

            // Weapons
            foreach (var weapon in Main.Randomizer.Data.GetAllWeapons())
                CoreCache.EquipmentManager.Unlock(weapon);
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            //foreach (Room room in CoreCache.Room.rooms.Values)
            //{
            //    Main.Randomizer.LogWarning("Room: " + room.Name);
            //    if (room.Name == "Z0505")
            //    {

            //    }
            //        CoreCache.PlayerSpawn.TeleportPlayer()
            //}

            SceneEntryID entry = new SceneEntryID();
            entry.scene = "Z0505";
            entry.entryId = 0;
            CoreCache.PlayerSpawn.TeleportPlayer(entry, false, null);
        }
    }
}