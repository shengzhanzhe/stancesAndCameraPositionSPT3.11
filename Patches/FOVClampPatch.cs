using SPT.Reflection.Patching;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CameraRotationMod.Patches
{
    /// <summary>
    /// Patch to override the FOV value clamping in GClass1053.Class1718.method_0.
    /// This allows FOV values outside the default 50-75 range to be saved/applied.
    /// </summary>
    public class FOVClampPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GClass1053.Class1718), nameof(GClass1053.Class1718.method_0));
        }

        [PatchPostfix]
        public static void PatchPostfix(int x, ref int __result)
        {
            if (!Plugin._FOVExpandEnabled.Value)
                return;

            __result = Mathf.Clamp(x, Plugin._FOVMinRange.Value, Plugin._FOVMaxRange.Value);
        }
    }
}
