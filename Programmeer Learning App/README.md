# Programmeer Learning App

## Learning using Exercises
In order to practice Exercises, you need to click on the LEARN button at the top. Once you've done so, a windows file explorer menu will open. It accepts text documents as input and we have prepared a couple for you just in case.

Those example exercises can be found under './Example Exercises'.
The same goes for Example Programs, which you can try to import using the Import button and you can reload your own program by first exporting, and then importing them.

## Building Exercises
You can easily make your own exercises by following the structure in the premade exercises. These can be of any size, but you must keep them consistent, so each row must have the same number of characters as the others.
But specifically for PathFindingExercise (currently the only one), here are all the tokens which are accepted:
- s : The startposition of the player (no regard for facingDir), can be placed multiple times but will only keep 1. Or if none are places, it will pick the top-left most corner.
- x : The endposition, the goal of the player. Can be placed multiple times but will only pick one. The must be at least 1 in order for the Exercise to load.
- o : The open spaces. The player may walk here and there can be as many of these as the Exercise designer wants.
- + or 'Space' : A Blockade, a square in which the player is not allowed to move. There can be as many of them as the designer wants, even to the point that the Exercise becomes impossible to solve.

## Building Programs
The best way to make programs is with our app, but it is possible to build programs using a text file. But keep in mind, the FileReader is very fragile.