using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VisualBacktrack
{
    [BepInPlugin(VisualBacktrackPlugin.PluginGuid, VisualBacktrackPlugin.PluginName, VisualBacktrackPlugin.PluginVersion)]
    public class VisualBacktrackPlugin : BaseUnityPlugin
    {
        public const string PluginGuid = "com.nilaier.visualbacktrack";
        public const string PluginName = "VisualBacktrackPlugin";
        public const string PluginVersion = "1.0.0";

        private void Awake()
        {
            // Initialize configuration settings from a dedicated config file.
            VisualBacktrackConfig.Initialize(Config);

            var harmony = new Harmony(PluginGuid);
            harmony.PatchAll();
            Logger.LogInfo($"{PluginName} loaded.");
        }

        /// Once MapBacktrack.Start() spawns its points, add a BacktrackPoint3DVisual
        /// component to each MapBacktrackPoint so that each one gets its own 3D sphere.
        [HarmonyPatch(typeof(MapBacktrack), "Start")]
        public static class MapBacktrack_Start_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(MapBacktrack __instance)
            {
                foreach (var point in __instance.GetComponentsInChildren<MapBacktrackPoint>(true))
                {
                    if (point.gameObject.GetComponent<BacktrackPoint3DVisual>() == null)
                    {
                        point.gameObject.AddComponent<BacktrackPoint3DVisual>();
                    }
                }
            }
        }
    }
}