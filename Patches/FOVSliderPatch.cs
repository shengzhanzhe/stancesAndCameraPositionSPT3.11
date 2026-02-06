using EFT.UI;
using EFT.UI.Settings;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;

namespace CameraRotationMod.Patches
{
    /// <summary>
    /// Patch to extend the FOV slider range in GameSettingsTab.
    /// Re-binds the FOV NumberSlider with extended min/max values.
    /// Uses the publicized Assembly-CSharp.dll with GClass1053 (game settings class).
    /// </summary>
    public class FOVSliderPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameSettingsTab), nameof(GameSettingsTab.Show));
        }

        [PatchPostfix]
        private static void PatchPostfix(ref NumberSlider ____fov, GClass1053 ___gclass1053_0)
        {
            if (!Plugin._FOVExpandEnabled.Value)
                return;

            // Re-bind the FOV slider with extended range
            SettingsTab.BindNumberSliderToSetting(
                ____fov,
                ___gclass1053_0.FieldOfView,
                Plugin._FOVMinRange.Value,
                Plugin._FOVMaxRange.Value
            );
        }
    }
}
