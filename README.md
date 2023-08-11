# AutoChessSharp Library 🤼

👋 Welcome to AutoChessSharp library repository! This library provides simple API set to develop and interact with various aspects of the _auto chess game_ through the ease of C# language.

## Game Features 🍎

- Support for two players
- Lightweight console application
- Piece randomizer for store stock
- Clash randomizer to simulate auto chess war
- Provided rarity and arche type properties for pieces

## Repository Navigation 🔎

```txt
src
├── AutoChessSharp.Core
│   ├── bin
│   │   └── Release
│   │       └── net7.0
│   │           └── AutoChessSharp.Core.exe 
│   └── Database
│       └── PiecesToPlay.json    
└── AutoChessSharp.PieceFactory
```

The library consists mainly of the **core library** csharp project and **piece factory** csharp project

The core project provides the core library functionality and the piece factory used to generate the `PieceToPlay.json`

- [Navigate to the released binary](./src/AutoChessSharp.Core/bin/Release/net7.0)
- [Navigate to the json database](./src/AutoChessSharp.Core/Database)
- [Navigate to the core project](./src/AutoChessSharp.Core/)
- [Navigate to the piece factory](./src/AutoChessSharp.PieceFactory/)
- [Navigate to the Xunit test project (_ongoing_)](./src/AutoChessSharp.XTest/)

## Roadmap 🗺️

- Implement positioning mechanics into consideration _future_
- Implement attack and HP relation with clash outcome _future_
- Logger _on going with NLog_ 🕙
- Unit test _on going with Xunit_ 🕙
- Docs _on going with docfx_ 🕙

## TODOs ✅

- branch out a new dev branch to reconfig store fields to implement interfaces
- resolve dependency issues caused by the store fields

## Known Issues ⚠️

- Clash algorithm currently favors player with more pieces on the field
- Player can not enter the clash with zero piece on the field
- Buying piece from the store also means positioning them on the field
- No position evaluator, currently every piece involved in a clash positioned at coordinate (0,0)
- Multiple player name can be set as the value evaluated is the instance of the player, not its name field
- Development for beyond 2 players support might cumbersome
- Multiple log folders currently exist, this might lead to confusion when the program excuted multiple times from different release folder
