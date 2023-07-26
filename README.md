# AutoChess Simplest Form

#bootcamp #csharp

## Overview

**Setup**

 - HP and Gold resources initiation
 - Shared Game Board
 - Access to common pool of units

**Recruitment Phase**

 - Players take turns to recruit from the common pool using gold
 - Units in the common pool have different costs

**Placement Phase**

- Player take turns placing the units/pieces on their side of the game board
- Position placement should be strategic and define the outcome of the battle

**Combat Phase**

- Automatic combat begins
- Deterministic outcome, by relying on the stats of the units on the board. No player intervention

**Resolution**

- Player HP would decrease based on the remaining units of the opponent
- Defeated units removed from the board, partially damaged healed

**Income and Upkeep**

- Both players receive flat amount of income in the form of gold for each round end
- *Idea:* Upkeep cost for units deployed on the board

**Repeat turns**

- Game continues with more powerful units in the pool (*also more expensive*)
- Added gold would be incrementally larger

**Winning Condition**

- When one Player reaches 0 HP
- Or when the game forced to stop before the first condition, the player with most victories accumulated

## Core Functionality

- Player HP and Gold concept
- Recruitment phase with limited time (*must multithread*)
- Placement phase with limited time
- Positioning system
- Auto combat *challenges:*
	- each piece role has its own stats
	- field randomizer to allow different piece attack each other
	- *advanced:* add logic feature to allow strategic positioning
- Turn keeping to track the number of turn passed
- Multiplier for gold increase in each turn

## Core Classes

- Player (*should player info separated from player instance itself?*)
- Board
- Piece
- Store
- Position
- Game Runner

## Questions

- Should PlayerInfo and the Player class separated?
- IPlayerInfo or let the PlayerInfo class implement multiple interfaces?
- Should exp and level a two different parameters?
  