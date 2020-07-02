# KeypadSystem
A simple keypad system I made for Unity. I built the system for multiple types of usages.   
This system can be used pretty easily with these type of games:
- FPS type game.
- Mouse click type game   

![](http://bytevaultstudio.se/ShareX/ezgif.com-optimize.gif)

## The Inspector UI (RaycastSystem is for FPS only)

![](http://bytevaultstudio.se/ShareX/Unity_C6ntis32on.png)
![](http://bytevaultstudio.se/ShareX/Unity_Hf4axFABS3.png)

## How to setup the Keypad
You add the script Keypad to an object (Interactions are done with the collider component).
You then go through the options of the inspector UI.
### States
states will cover two bool states.
- Keycode Solved: This is the state if the code has been accepted or the door is open.
- Permanently Locked: This is the state if the code has been denied or the tries has exceeded max.
### Keypad Setup
This is where you set up the type of use, keycode etc
- Activate By Mouseclick: This is the option to set true if you are building a mouse click game. Simply using the OnMouseClickEvent.
- Keycode: The keycode for the door. It takes an Integer value.
- Auto Complete: This is the feature to set true if you want the keypad to automatically accept if the code is correct.
### Keypad Extra
Keypad Extra contains the features to limit the tries on the keypad.
- Limit Tries: This feature should be set to true if you want to limit the users inputs and lock the keypad.
- Tries Amount: This takes an Integer of how many tries the user can input before it locks itself.
### Methods to run
Methods to run is exactly what It sounds like. The setups for these are exactly like setting up the UI events for example, buttons.
- Access Granted: This method will be activated once the correct keycode has been entered. This method is also ends by invoking Return Control method.
- Access Denied: This method will be activated once the incorect keycode has been entered above the limited amount. This method is also ends by invoking Return Control method.
- Return Control: This method acts more like a free method. I, for example, used it to return control to the player once the keypad was closed. I modified the old FPSController in my demoscene and made two methods. One for disabling the controller and I needed the controller to be turned back on AFTER the keypad had been closed. 

## What code do I need to write?
