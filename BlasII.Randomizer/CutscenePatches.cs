﻿using HarmonyLib;
using Il2CppPlaymaker.UI;
using Il2CppTGK.Game.Cutscenes;
using Il2CppTGK.Game.Tutorial;

namespace BlasII.Randomizer
{
    [HarmonyPatch(typeof(ShowTutorial), nameof(ShowTutorial.OnEnter))]
    class Tutorial_Skip_Patch
    {
        public static bool Prefix(ShowTutorial __instance)
        {
            TutorialID tutorial = __instance.tutorial.Value.Cast<TutorialID>();
            Main.Randomizer.LogWarning("Skipping tutorial: " +  tutorial?.name);

            __instance.Finish();
            return false;
        }
    }

    [HarmonyPatch(typeof(PlayCutscene), nameof(PlayCutscene.OnEnter))]
    class Cutscene_Skip_Patch
    {
        public static bool Prefix(PlayCutscene __instance)
        {
            Main.Randomizer.LogWarning("Skipping cutscene: " + __instance.cutsceneId?.name);

            __instance.Finish();
            return false;
        }
    }

    [HarmonyPatch(typeof(ShowQuote), nameof(ShowQuote.OnEnter))]
    class Quote_Skip_Patch
    {
        public static bool Prefix(ShowQuote __instance)
        {
            Main.Randomizer.LogWarning("Skipping quote: " + __instance.Owner.name);

            __instance.Finish();
            return false;
        }
    }

    [HarmonyPatch(typeof(ShowMapDestinationTutorial), nameof(ShowMapDestinationTutorial.OnEnter))]
    class Map_Skip_Patch
    {
        public static bool Prefix(ShowMapDestinationTutorial __instance)
        {
            Main.Randomizer.LogWarning("Skipping map: " + __instance.Owner.name);

            __instance.Finish();
            return false;
        }
    }

    [HarmonyPatch(typeof(ShowSorrowsPopup), nameof(ShowSorrowsPopup.OnEnter))]
    class Sorrows_Skip_Patch
    {
        public static bool Prefix(ShowSorrowsPopup __instance)
        {
            Main.Randomizer.LogWarning("Skipping sorrows: " + __instance.Owner.name);

            __instance.Finish();
            return false;
        }
    }

    [HarmonyPatch(typeof(ShowDovePopup), nameof(ShowDovePopup.OnEnter))]
    class Dove_Skip_Patch
    {
        public static bool Prefix(ShowDovePopup __instance)
        {
            Main.Randomizer.LogWarning("Skipping dove: " + __instance.Owner.name);

            __instance.Finish();
            return false;
        }
    }

    [HarmonyPatch(typeof(ShowUnlockAbilityPopup), nameof(ShowUnlockAbilityPopup.OnEnter))]
    class Ability_Skip_Patch
    {
        public static bool Prefix(ShowUnlockAbilityPopup __instance)
        {
            Main.Randomizer.LogWarning("Skipping ability: " + __instance.Owner.name);

            __instance.Finish();
            return false;
        }
    }

    [HarmonyPatch(typeof(ShowWeaponPopup), nameof(ShowWeaponPopup.OnEnter))]
    class WeaponFind_Skip_Patch
    {
        public static bool Prefix(ShowWeaponPopup __instance)
        {
            Main.Randomizer.LogWarning("Skipping weapon find: " + __instance.Owner.name);

            __instance.Finish();
            return false;
        }
    }

    [HarmonyPatch(typeof(ShowWeaponTierPopup), nameof(ShowWeaponTierPopup.OnEnter))]
    class WeaponUpgrade_Skip_Patch
    {
        public static bool Prefix(ShowWeaponTierPopup __instance)
        {
            Main.Randomizer.LogWarning("Skipping weapon upgrade: " + __instance.Owner.name);

            __instance.Finish();
            return false;
        }
    }
}