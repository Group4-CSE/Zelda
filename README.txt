# Zelda - Group 4 CSE 3902
We Decided to start the player with infinite of all items.
Upon Pulling you may have to rebuild the solution to avoid build errors.

Controls:
W/Up Arrow- Move Link Up
A/Left Arrow- Move Link Left
S/Down Arrow- Move Link Down
D/Right Arrow- Move Link Right
Z/N- Link Attacks
X/M- Link uses currently selected item
I- Open Item Selection Screen
P- Pause Game
E- Link Takes Damage
1- Link uses bow
2- Link uses Bomb
3- Link uses Link uses Boomerang
Q- Quit
R- Reset program to initial state

In Item Selection Menu:
A/Left Arrow- select Previous Item
D/Right Arrow- Select Next item
C- Close Item Selection Menu

Pause Screen
C- Close Pause Screen


Known Bugs: 
-Pressing a fourth key, while three are already held down results in the fourth key not being logged as a keypress.
-Spamming some player actions causes the sprite to lag behind. (specifically spamming attack can cause the sprite to overload until you change direction)
-Some Enemies do not behave exactly as they will in the final product (specifically Wallmasters have slightly different behavior, And Traps player detection is still imperfect)
- Game states(Win, Lose, Item select, Playing, Pause) are in, but the transitions into these states are not yet fully implemented.

-Some of the conditional information is still not fully finalized. sometimes player can pick up invisible items that should be unavailable for the moment, and doors have yet been properly programmed to open when blocks move or if a bomb is placed near a hidden cave door
- Code to enter and leave the underground room is not yet implemented


Our team used Trello for project management, Photoshop/GIMP for spritesheet creation, and Discord for communication.
Here are the share links for the trello and the github, to document our workflow:
https://trello.com/invite/b/e9PPVWKM/d9c854c4f14d79490c3d390d754b1470/cse3902-project
https://github.com/Group4-CSE/Zelda
