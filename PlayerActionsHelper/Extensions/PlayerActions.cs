using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using InControl;

namespace PlayerActionsHelper.Extensions
{
    [Serializable]
    public class PlayerActionsAdditionalData
    {
        public Dictionary<string, PlayerAction> actions = new Dictionary<string, PlayerAction>();

        public PlayerActionsAdditionalData()
        {
            actions = new Dictionary<string, PlayerAction>();
        }
    }
    public static class PlayerActionsExtension
    {
        public static readonly ConditionalWeakTable<PlayerActions, PlayerActionsAdditionalData> data =
            new ConditionalWeakTable<PlayerActions, PlayerActionsAdditionalData>();

        internal static PlayerActionsAdditionalData GetAdditionalData(this PlayerActions playerActions)
        {
            return data.GetOrCreateValue(playerActions);
        }

        public static PlayerAction GetPlayerAction(this PlayerActions playerActions, string name)
        {
            if (playerActions.GetAdditionalData().actions.TryGetValue(name, out PlayerAction action))
            {
                return action;
            }
            UnityEngine.Debug.LogError($"Attempting to access a Player Action by the name of {name} that doesn't exist, returning null instead.");
            return null;
        }

        public static bool ActionWasPressed(this PlayerActions playerActions, string name)
        {
            var abilities = playerActions.GetAdditionalData().actions;
            if (abilities.ContainsKey(name))
            {
                return abilities[name].WasPressed;
            }
            UnityEngine.Debug.LogError($"Attempting to access a Player Action by the name of {name} that doesn't exist, returning false instead.");
            return false;
        }
        public static bool ActionIsPressed(this PlayerActions playerActions, string name)
        {
            var abilities = playerActions.GetAdditionalData().actions;
            if (abilities.ContainsKey(name))
            {
                return abilities[name].IsPressed;
            }
            UnityEngine.Debug.LogError($"Attempting to access a Player Action by the name of {name} that doesn't exist, returning false instead.");
            return false;
        }
        public static bool ActionWasReleased(this PlayerActions playerActions, string name)
        {
            var abilities = playerActions.GetAdditionalData().actions;
            if (abilities.ContainsKey(name))
            {
                return abilities[name].WasReleased;
            }
            UnityEngine.Debug.LogError($"Attempting to access a Player Action by the name of {name} that doesn't exist, returning false instead.");
            return false;
        }
    }
}
