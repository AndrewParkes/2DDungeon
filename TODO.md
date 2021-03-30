# 2DDungeon TODO:

Rework tile lookup/caching
* Try something like a map (C# Dictionary) to store previously looked up tiles
* First check for tiles in map if doesnt exist lookup from resources

Disable rooms that are not active (Character is not in the room)
* When transitioning in and out of room deactivate rooms outside of view activate current room

Add animation to the held weapon
* If not being used weapon should move with the character (up and down with idle animation for example)

Add attack sprite and animation
* There is a skull placeholder. Replace this with a swipe or projectile.
* Add a attack animation as there is no indication of attacking other than the placeholder

Animate enemies
* Currently they only have a single sprite

Player Jumping
* Jump while holding space. Allow for small/ large jumps based on the length jump is held