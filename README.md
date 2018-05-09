# Unity Exercise 

Clone this Github project and expand it by adding a new game to it.

The project is made in Unity 5.3

It implements a very simple game called React, which briefly displays a stimulus (rectangle) and the player has to respond as quickly as possible.

The codebase is structured so that more complex games can be built on top of it easily and its behavior can be controlled using an xml parameter file (which we call the session file).
See the 'Overview' section below for more details on the structure of the project.


Once you're familiar with the game and how the code is organized, you will be implementing one new game in this project.
In this game you should be able to:

- Specify in the session file whether the stimulus (rectangle) position per trial is random or predefined.
  - The predefined position should be defined inside the session file on a per trial basis. 
  - While the random position should be generated based off a defined range.
- Specify in the session file whether the stimulus should sometimes appear red, in which case the player should NOT respond in order to get a correct response.
- Save all the new game parameters (position, isRed, etc...) when creating a session log file at the end of a game.
- Log important events such as the result of each Trial to a trace file by utilizing the GUILog functionality that the project has, instead of Unity's Debug.Log


# Things to keep in mind

- Treat this exercise as a real world scenario where we ask you to add a new game to our existing project.
- The original React game should remain unchanged.
- The new code should maintain the formatting conventions of the original code.


# Project Overview

- **Stimulus** - An event that a player has to respond to.
- **Session** - A session refers to an entire playthrough of a game.
- **Trial** - A trial is when a player has to respond to a stimulus, which becomes marked as a success or failure depending on the player's response.
- **TrialResult** - A result contains data for how the player responded during a Trial.
- **Session File** - A session file contains all the Trials that will be played during a session, as well as any additional variables that allow us to control and customize the game.
- **Session Log** - An xml log file generated at the end of a session, contains all the attributes defined in the source Session file as well as all the Trial results that were generated during the game session.
- **Trace Log** - A text log file generated using GUILog for debugging and analytical purposes. GUILog requires a SaveLog() function to be called at the end of a session in order for the log to be saved.
- **GameController** - Tracks all the possible game options and selects a defined game to be played at the start of the application.
- **InputController** - Checks for player input and sends an event to the Active game that may be assigned.
- **GUILog** - A trace file logging solution, similar to Unity's Debug.Log, except this one creates a unique log file in the application's starting location.
- **GameBase** - The base class for all games.
- **GameData** - A base class, used for storing game specific data.
- **GameType** - Used to distinguish to which game a Session file belongs to.


# Submission
The new game rules are simple, a white or red rectangle stimulus will be briefly displayed and the player has to respond to it quickly. However, it is only treated as a success when responding to a white stimulus and ignoring a red one.

To play the new game, go to the GameController inpector and either check the Random Game bool to randomly play one of the games in the Games List or set the Which Game slider to 1.


The NewGame.xml session file contains the parameters for the game. Like React it has duration, guessTimeLimit, responseTimeLimit, and delay. It also has parameters for the whether the trial isRed, if its position is random or pre-set(Ex. "100 200"), and ranges for its position when selected to be random.

Trial.cs had to be modified to parse in from the session file if a trial was red and its position. Compared to ReactData, NewGameData parses and stores the X and Y ranges from the Session file in addition to having a function to split a string and parse it into an int. This was used for storing the position ranges and pre-set positions of the stimulus.

In the base script for NewGame, during the DisplayStimulus coroutine, each trial's position attribute is check for being set to "random" otherwise it calls the function that splits and parses strings SplitPosition() to get the pre-set position of the stimulus for the trial. Checking and setting the stimulus as red is also in DisplayStimulus(). The user responding to a red stimulus is simply checked by seeing if isRed for the trial was true and if the time was within the response time limit or passed it.

These were the biggest changes made, smaller changes/additions were made to other scripts to allow NewGame to function with the codebase. 
