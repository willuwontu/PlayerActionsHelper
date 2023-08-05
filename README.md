# Player Actions Helper

A mod designed to make creating player actions simpler.

Register an action with `PlayerActionManager.RegisterPlayerAction()` in your mod's awake function.

Access that action with `player.data.playerActions.GetPlayerAction()` in your mod.

<details>
<summary>Change log</summary>

----
### v0.0.1
- Fixed a small bug

----
### v0.0.0
- Initial Release

</details>

<details>
<summary>Properties</summary>

### RegisteredActions
```cs
ReadOnlyDictionary<string, ActionInfo> RegisteredActions { get; }
```
#### Description
A list of all actions registered so far.

</details>

<details>
<summary>Classes</summary>

### ActionInfo
```cs
class ActionInfo
```
#### Fields
- string name;
- BindingSource keyboardlayout;
- BindingSource controllerLayout;
 
#### Constructors
 
### ActionInfo()
```cs
ActionInfo ActionInfo(string name, BindingSource keyboardlayout = null, BindingSource controllerLayout = null)
```
#### Description
Creates a set of information for usage in binding player actions.

#### Parameters
- *string* `name` The name for the action.
- *BindingSource* `keyboardlayout` An optional parameter for setting the default keyboard layout.
- *BindingSource* `controllerLayout` An optional parameter for setting the default controller layout.

#### Example Usage
```CSHARP
PlayerActionManager.RegisterPlayerAction(new ActionInfo("ToggleFlight", new KeyBindingSource(Key.Key1), new DeviceBindingSource(InputControlType.Action3)));
```
</details>

<details>
<summary>Functions</summary>

### RegisterPlayerAction()
```cs
void RegisterPlayerAction(ActionInfo actionInfo)
```
#### Description
Registers an action to be automatically generated for players.

**NOTE:** You should only be registering actions in your mod's awake function. If you register them any later, they will not show in rebind controls.

#### Parameters
- *ActionInfo* `actionInfo` the details of the action to be registered.

#### Example Usage
```CSHARP
PlayerActionManager.RegisterPlayerAction(new ActionInfo("ToggleFlight", new KeyBindingSource(Key.Key1), new DeviceBindingSource(InputControlType.Action3)));
```
</details>

<details>
<summary>ExtensionMethods</summary>

### GetPlayerAction()
```cs
PlayerAction GetPlayerAction(this PlayerActions playerActions, string name)
```
#### Description
Returns a registered `PlayerAction` that has been created for the player. Returns null if the action doesn't exist.

#### Parameters
- *string* `name` the name of the registered action.

#### Example Usage
```CSHARP
player.data.playerActions.GetPlayerAction(name);
```

### ActionWasPressed()
```cs
bool ActionWasPressed(this PlayerActions playerActions, string name)
```
#### Description
Returns whether a registered action was pressed. Returns false if the action doesn't exist.

Note that this is an alternative to fetching the action and then checking the `PlayerAction::WasPressed` property.

#### Parameters
- *string* `name` the name of the registered action.

#### Example Usage
```CSHARP
player.data.playerActions.ActionWasPressed(name);
```

### ActionIsPressed()
```cs
bool ActionIsPressed(this PlayerActions playerActions, string name)
```
#### Description
Returns whether a registered action is pressed. Returns false if the action doesn't exist.

Note that this is an alternative to fetching the action and then checking the `PlayerAction::IsPressed` property.

#### Parameters
- *string* `name` the name of the registered action.

#### Example Usage
```CSHARP
player.data.playerActions.ActionIsPressed(name);
```

### ActionWasReleased()
```cs
bool ActionWasReleased(this PlayerActions playerActions, string name)
```
#### Description
Returns whether a registered action was released. Returns false if the action doesn't exist.

Note that this is an alternative to fetching the action and then checking the `PlayerAction::WasReleased` property.

#### Parameters
- *string* `name` the name of the registered action.

#### Example Usage
```CSHARP
player.data.playerActions.ActionWasReleased(name);
```
</details>