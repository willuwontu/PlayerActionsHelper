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
                try
                {
                    //__instance.GetAdditionalData().actions[key] = new PlayerAction(key, __instance);
                    __instance.GetAdditionalData().actions[key] = (PlayerAction)typeof(PlayerActions).InvokeMember("CreatePlayerAction", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic, null, __instance, new object[] { key });
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError($"Error thrown when attempting to create player action '{key}'.");
                    UnityEngine.Debug.LogException(e);
                    __instance.GetAdditionalData().actions[key] = __instance.GetPlayerActionByName(key);
                }
            }

        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerActions), "CreateWithControllerBindings")]
        private static void SetControllerBinding(ref PlayerActions __result)
        {
            foreach (string key in PlayerActionManager.RegisteredActions.Keys)
            {
                try
                {
                    BindingSource layout = PlayerActionManager.RegisteredActions[key].controllerLayout;
                    if (layout != null)
                    {
                        __result.GetAdditionalData().actions[key].AddDefaultBinding(layout);
                    }
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError($"Error thrown when attempting set default binding for player action '{key}'.");
                    UnityEngine.Debug.LogException(e);
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerActions), "CreateWithKeyboardBindings")]
        private static void SetKeyboardBinding(ref PlayerActions __result)
        {
            foreach (string key in PlayerActionManager.RegisteredActions.Keys)
            {
                try
                {
                    BindingSource layout = PlayerActionManager.RegisteredActions[key].keyboardlayout;
                    if (layout != null)
                    {
                        __result.GetAdditionalData().actions[key].AddDefaultBinding(layout);
                    }
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError($"Error thrown when attempting set default binding for player action '{key}'.");
                    UnityEngine.Debug.LogException(e);
                }
            }
        }

    }
}