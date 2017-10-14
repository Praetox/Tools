   -== TABLE OF CONTENTS ==-

1. What?
   Quick summary of what GRebind does.

2. Why?
   Why would you need GRebind?

3. How does it work?
   A quick peek under the hood.

4. How do I use it?
   Guide lol

5. What is...
   ...that button with the wierd caption?

6. Final notes
   Well yeah, self explanatory rite?


   -== THE MASSIVE WALL OF TEXT ==-



>> 1. What?

GRebind can change your keyboard keys so that one acts as another,
but not necessarily the other way around. Also, it's pronounced
gree-bind as in gree(ting) and bind(ing).

You will need .NET Framework version 2.0 or later to use this
software. There's a download link on my website (www.praetox.com).



>> 2. Why?

Have you ever played a great game, but the default keys just plain
sucks? While keymappings are changeable in most games, some force
you to use a horrible setup. Let's do something about that.



>> 3. How does it work?

Basically it acts as a proxy between your keyboard and the
application you wish to remap. Whenever you press a key, it will
check if the key has been remapped. If so, it will interrupt the
keystroke and replace it with the correct key.

As this is exactly how many keyloggers work, this application will
(or should) be detected as a dangerous trojan. Needless to say,
these are all false alarms. Just add GRebind to your safelist.



>> 4. How do I use it?

If it's the first time you use it, you'll have to do things a bit
differently. First, start the program/game you wish to remap. When
it has launched fully, start GRebind. Find your application in the
"Target application" dropdown, select it, and start adding remaps.

Adding remaps is done by pressing the ADD button (lower middle),
followed by the key you wish to add a remap to, and finally the
keystroke that should be sent whenever that key is pressed. You
may remove remaps by selecting the remap(s) you wish to remove,
followed by pressing the REM button.

Once you've added all the remaps you wish to use, press [Save]
(lower left). You will be prompted with a name for the profile.
Once you enter it and hit enter, the configuration will be saved
for future use.

Now that you've created a remap profile, setting up GRebind
consists of 2 (or 3) easy steps. First off, launch GRebind.
GRebind will automatically load the last used profile upon launch.
If you're going to use a mapping for another program, simply
select the correct profile in the "Predefined profile" dropdown.
Now you may launch the game/app/whatever.



>> 5. What is...

    Target application
The game or program that GRebind should affect. The dropdown menu
contains all the software that GRebind could see when it was
started. If you wish to remap a program that you can't see in the
list, try to restart GRebind (not the program you wish to remap).


    Predefined profile
Stored configurations for GRebind. These profiles contain every
single changeable thing in GRebind, and makes future use simpler
and more efficient. Simply select the config you wish to use in
the dropdown menu (pressing [Load] is usually not necessary) and
enjoy. To store (or replace/update) a profile, hit [Save].


    Mapped keys
The keys that will be changed. If it says "E >> G", it means that
the target application will see "G" whenever you press "E". If you
press G, however, the application will see the original keypress.
In other words, you will have to add two remaps to switch two
buttons (one for E->G, and one for G->E).


    ADD and REM
The buttons you press to add and remove remaps.


    Forcefully inject keystrokes
If this is unchecked, GRebind will simply emulate regular
keystrokes when remapping keys. Some applications don't like this.
If you find that GRebind just stops the keys from working instead
of rebinding them, try checking this box.


    Rapid fire mode (all keys)
A little addon for "shoot 'em up" style games, amongst others. If
this box is checked, all keys that are remapped will be repeatedly
repressed while they are down. The boxes below define how quick
various actions should be performed, and are specified in
milliseconds. From left to right, the boxes are:

Down-time: How long the key should be held down in each sequence
Up-time: How long the key should be kept up between each sequence
Down-fluct: Variation in the down-time (+/- ms)
Up-fluct: Variation in the up-time (+/- ms)

Fluct? Well, some games try to avoid rapid fire hacks. A popular
method of doing so, is by checking if the button is repressed at
a steady interval. By adding a bit of variation here, it will
seem like a regular person is hammering the button manually.


    Show charcode list
If you don't have a certain key on your keyboard, then binding
if to another key will be difficult (as you don't have it in the
first place, hurrr). This is where the charcode list comes in
handy. Simply find the charcodes you need - namely for the assign
and emulate keys - and doubleclick the mapped keys list.



>> 6. Final notes

I added a premade profile for Doukutsu Monogatari (or Cave Story,
for those of you who don't speak moon). This game is simply
awesome in every single way, except for the key mapping (which
can't be changed). In fact, this game is the very reason why I
made GRebind in the first place. As this was the very first
profile created, it would be a shame not to include it.

If you want to give it a go, you control Quote (that's you) by
using WASD. Jump using K, attack with O, change weapons with ;
and P, open the menu by pressing K, and finally you open the
map by hitting L. Note that you may only jump and open the menu
in the very beginning, you get the gun and map later on.

Final final note. If you've never played Doukutsu before, I'll
have to track you down and force you. Really though, that game
is simply made of win. You're missing out.