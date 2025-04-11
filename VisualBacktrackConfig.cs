using BepInEx.Configuration;
using UnityEngine;

namespace VisualBacktrack
{
    public static class VisualBacktrackConfig
    {
        // These config entries will be automatically written to the plugin's .cfg file.
        public static ConfigEntry<bool> CastShadows;
        public static ConfigEntry<Color> BacktrackPointColor;
        public static ConfigEntry<bool> SyncAnimation;
        public static ConfigEntry<float> AnimationSpeed;
        public static ConfigEntry<float> Opacity;

        public static void Initialize(ConfigFile config)
        {
            // We use a dedicated section ("3D Visuals") for clarity.
            CastShadows = config.Bind("3D Visuals", "CastShadows", true,
                "Should the 3D Backtrack points cast shadows?");

            // Note: Color can be configured as rgba (the string format is "R,G,B,A").
            BacktrackPointColor = config.Bind("3D Visuals", "BacktrackPointColor", Color.white,
                "Color of the 3D Backtrack points");

            SyncAnimation = config.Bind("3D Visuals", "SyncAnimation", true,
                "Sync animation with the map's 2D backtrack points?");

            AnimationSpeed = config.Bind("3D Visuals", "AnimationSpeed", 1.0f,
                "Animation speed for 3D backtrack points if not in sync");

            Opacity = config.Bind("3D Visuals", "Opacity", 0.75f,
                "Opacity (transparency) of the 3D backtrack points");
        }
    }
}