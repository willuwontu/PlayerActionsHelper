using HarmonyLib;
using System;
using System.Reflection;
using InControl;
using PlayerActionsHelper.Extensions;


namespace PlayerActionsHelper.Patches
{
    [HarmonyPatch(typeof(PlayerActions))]
    class PlayerActions_Patch
    {
        [HarmonyPatch(typeof(PlayerActions))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { })]
        [HarmonyPostfix]
        private static void CreateAction(PlayerActions __instance)
        {
            foreach (string key in PlayerActionManager.RegisteredActions.Keys)
            {
                __instance.GetAdditionalData().actions[key] = (PlayerAction)typeof(PlayerActions).InvokeMember("CreatePlayerAction", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic, null, __instance, new object[] { key });
            }

        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerActions), "CreateWithControllerBindings")]
        private static void SetControllerBinding(ref PlayerActions __result)
        {
            foreach (string key in PlayerActionManager.RegisteredActions.Keys)
            {
                BindingSource layout = PlayerActionManager.RegisteredActions[key].controllerLayout;
                if (layout != null)
                {
                    __result.GetAdditionalData().actions[key].AddDefaultBinding(layout);
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerActions), "CreateWithKeyboardBindings")]
        private static void SetKeyboardBinding(ref PlayerActions __result)
        {
            foreach (string key in PlayerActionManager.RegisteredActions.Keys)
            {
                BindingSource layout = PlayerActionManager.RegisteredActions[key].keyboardlayout;
                if (layout != null)
                {
                    __result.GetAdditionalData().actions[key].AddDefaultBinding(layout);
                }
            }
        }

    }
}