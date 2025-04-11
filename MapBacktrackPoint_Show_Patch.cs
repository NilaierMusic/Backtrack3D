using HarmonyLib;
using UnityEngine;

namespace VisualBacktrack
{
    /// This patch makes sure that when a MapBacktrackPoint is “shown” (its
    /// 2D sprite animation is triggered), the corresponding 3D sphere is positioned correctly
    /// in the world and its animation is started.
    [HarmonyPatch(typeof(MapBacktrackPoint), "Show")]
    public static class MapBacktrackPoint_Show_Patch
    {
        public static void Postfix(MapBacktrackPoint __instance, bool _sameLayer)
        {
            var visual = __instance.GetComponent<BacktrackPoint3DVisual>();
            if (visual != null)
            {
                // Convert the map’s coordinate to the actual 3D world position.
                // MapBacktrackPoint.transform.position is computed via:
                //   mapPos = worldPos * Map.Instance.Scale + Map.Instance.OverLayerParent.position
                // So reverse the calculation:
                if (Map.Instance != null)
                {
                    Vector3 mapPos = __instance.transform.position;
                    Vector3 worldPos = (mapPos - Map.Instance.OverLayerParent.position) / Map.Instance.Scale;
                    // Here we use 0 for ground level.
                    worldPos = new Vector3(worldPos.x, 0f, worldPos.z);
                    visual.Set3DPosition(worldPos);
                }
                visual.TriggerAnimation();
            }
        }
    }
}