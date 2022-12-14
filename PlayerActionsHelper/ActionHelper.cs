using BepInEx;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HarmonyLib;
using InControl;

namespace PlayerActionsHelper
{
    // These are the mods required for our mod to work
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    internal class ActionHelper : BaseUnityPlugin
    {
        private const string ModId = "com.rounds.willuwontu.ActionHelper";
        private const string ModName = "Action Helper";
        public const string Version = "0.0.0"; // What version are we on (major.minor.patch)?

        public static ActionHelper instance { get; private set; }

        void Awake()
        {
            instance = this;

            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();


        }
        void Start()
        {

        }
    }

    public class PlayerActionManager
    {
        static Dictionary<string, ActionInfo> registeredActions = new Dictionary<string, ActionInfo>();

        public static ReadOnlyDictionary<string, ActionInfo> RegisteredActions => new ReadOnlyDictionary<string, ActionInfo>(registeredActions);

        public static void RegisterPlayerAction(string name, ActionInfo actionInfo)
        {
            if (registeredActions.ContainsKey(name))
            {
                UnityEngine.Debug.LogWarning($"An action called {name} was already registered, no new action is registered.");
                return;
            }

            registeredActions.Add(name, actionInfo);
        }
    }

    public class ActionInfo
    {
        public string name;
        public BindingSource keyboardlayout;
        public BindingSource controllerLayout;
    }
}
