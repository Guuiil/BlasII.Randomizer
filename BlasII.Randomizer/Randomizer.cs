﻿using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Persistence;
using BlasII.ModdingAPI.Storage;
using BlasII.Randomizer.Items;
using BlasII.Randomizer.Settings;
using Il2Cpp;
using Il2CppTGK.Game;
using Il2CppTGK.Game.Components.Interactables;
using UnityEngine;

namespace BlasII.Randomizer
{
    public class Randomizer : BlasIIMod, IPersistentMod
    {
        public Randomizer() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

        public DataStorage Data { get; } = new();

        public ItemHandler ItemHandler { get; } = new();
        public SettingsHandler SettingsHandler { get; } = new();

        public RandomizerSettings CurrentSettings { get; set; } = RandomizerSettings.DefaultSettings;

        protected override void OnInitialize()
        {
            Data.Initialize();
        }

        protected override void OnUpdate()
        {
            SettingsHandler.Update();

            if (!LoadStatus.GameSceneLoaded)
                return;

            if (Input.GetKeyDown(KeyCode.Keypad6))
                SettingsHandler.DisplaySettings(CurrentSettings);

            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                if (StatStorage.TryGetModifiableStat("BasePhysicalattack", out var stat))
                    StatStorage.PlayerStats.AddBonus(stat, "test", 100, 0);
            }
        }

        protected override void OnSceneLoaded(string sceneName)
        {
            if (sceneName == "Z1506")
                LoadWeaponSelectRoom();
            else if (sceneName == "Z2501")
                LoadChapelRoom();
            else if (GetQuestBool("ST00", "DREAM_RETURN"))
                LoadBossKeyRoom(sceneName);
        }

        protected override void OnSceneUnloaded(string sceneName)
        {

        }

        public void NewGame()
        {
            Log($"Shuffling items with seed {CurrentSettings.seed}");
            ItemHandler.FakeShuffle(CurrentSettings.seed, CurrentSettings);
            AllowPrieDieuWarp();
        }

        public SaveData SaveGame()
        {
            Log($"Saved file with {ItemHandler.CollectedLocations.Count} collected locations");
            return new RandomizerSaveData()
            {
                mappedItems = ItemHandler.MappedItems,
                collectedLocations = ItemHandler.CollectedLocations,
                collectedItems = ItemHandler.CollectedItems,
                settings = CurrentSettings,
            };
        }

        public void LoadGame(SaveData data)
        {
            RandomizerSaveData randomizerData = data as RandomizerSaveData;
            ItemHandler.MappedItems = randomizerData.mappedItems;
            ItemHandler.CollectedLocations = randomizerData.collectedLocations;
            ItemHandler.CollectedItems = randomizerData.collectedItems;

            CurrentSettings = randomizerData.settings;
            Log($"Loaded file with {randomizerData.collectedLocations.Count} collected locations");
        }

        public void ResetGame()
        {
            ItemHandler.MappedItems.Clear();
            ItemHandler.CollectedLocations.Clear();
            ItemHandler.CollectedItems.Clear();
        }

        private void AllowPrieDieuWarp()
        {
            foreach (var upgrade in CoreCache.PrieDieuManager.config.upgrades)
            {
                if (upgrade.name == "TeleportToAnotherPrieuDieuUpgrade")
                    CoreCache.PrieDieuManager.Upgrade(upgrade);
            }
        }

        // Special rooms

        private void LoadWeaponSelectRoom()
        {
            Log("Loading weapon room");

            foreach (var statue in Object.FindObjectsOfType<QuestVarTrigger>())
            {
                int weapon;
                if (statue.name.EndsWith("CENSER"))
                    weapon = 0;
                else if (statue.name.EndsWith("ROSARY"))
                    weapon = 1;
                else if (statue.name.EndsWith("RAPIER"))
                    weapon = 2;
                else
                    continue;

                if (weapon != CurrentSettings.RealStartingWeapon)
                {
                    int[] disabledAnimations = new int[] { -1322956020, -786038676, -394840968 };

                    Object.Destroy(statue.GetComponent<BoxCollider2D>());
                    Object.Destroy(statue.GetComponent<PlayMakerFSM>());
                    statue.transform.Find("sprite").GetComponent<Animator>().Play(disabledAnimations[weapon]);
                }
            }
        }

        private void LoadChapelRoom()
        {
            foreach (var fsm in Object.FindObjectsOfType<PlayMakerFSM>())
            {
                if (fsm.name == "NPC13_ST09_PILGRIM")
                {
                    // Remove this npc so that he cant give you a key
                    Object.Destroy(fsm.gameObject);
                }
            }
        }

        private void LoadBossKeyRoom(string sceneName)
        {
            string locationId = $"{sceneName}.key";
            Main.Randomizer.LogError("Randomizer.LoadBossKeyRoom - " + locationId);

            if (ItemHandler.IsRandomLocation(locationId))
                ItemHandler.GiveItemAtLocation(locationId);
        }

        // Quests

        public string GetQuestName(int questId, int varId)
        {
            var quest = CoreCache.Quest.GetQuestData(questId, string.Empty);
            var variable = quest.GetVariable(varId);

            return $"{quest.Name}.{variable.id}";
        }

        public bool GetQuestBool(string questId, string varId)
        {
            var input = CoreCache.Quest.GetInputQuestVar(questId, varId);
            return CoreCache.Quest.GetQuestVarBoolValue(input.questID, input.varID);
        }

        public int GetQuestInt(string questId, string varId)
        {
            var input = CoreCache.Quest.GetInputQuestVar(questId, varId);
            return CoreCache.Quest.GetQuestVarIntValue(input.questID, input.varID);
        }

        public void SetQuestValue<T>(string questId, string varId, T value)
        {
            var input = CoreCache.Quest.GetInputQuestVar(questId, varId);
            CoreCache.Quest.SetQuestVarValue(input.questID, input.varID, value);
        }
    }
}
