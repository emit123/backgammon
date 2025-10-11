# 🎲 Backgammon Android Game – Full Project Design Document

## 1. Overview

- **Platform:** Android
- **Framework:** .NET with Visual Studio (Xamarin.Android)
- **Game:** Playable Backgammon (local 2-player, optional AI)
- **Goal:** Not overly advanced, but fully functional with correct rules and clean UI.

## 2. Project Structure

```
BackgammonApp/
│
├── Activities/
│   ├── MainActivity.cs
│   ├── GameActivity.cs
│   ├── SettingsActivity.cs
│
├── Models/
│   ├── Board.cs
│   ├── Point.cs
│   ├── Checker.cs
│   ├── Player.cs
│   ├── Dice.cs
│   ├── Move.cs
│   ├── GameManager.cs
│   ├── Rules.cs
│
├── Helpers/
│   ├── GraphicsHelper.cs
│   ├── SaveManager.cs
│   ├── AIManager.cs (optional)
│
├── Resources/
│   ├── Layout/
│   │   ├── main_menu.xml
│   │   ├── game_screen.xml
│   │   ├── settings.xml
│   │   ├── board_view.xml
│   │   └── checker_view.xml
│   ├── Drawable/ (board, checker, dice images)
│   ├── Values/ (strings.xml, colors.xml, styles.xml)
```

## 3. Classes & Members

### 🟤 `Point.cs`

Represents a triangle.

- `int Index` → Point number (0–23).
- `PlayerColor? Owner` → Who owns checkers.
- `int Count` → How many checkers.
- `AddChecker(color)`
- `RemoveChecker()`
- `IsBlockedFor(color)` → Opponent has ≥2 checkers.
- `IsBlot()` → Exactly 1 checker.

### 🟤 `Board.cs`

Represents the game state.

- `Point[] Points` → 24 points.
- `Dictionary<PlayerColor,int> BarCounts` → Captured checkers.
- `Dictionary<PlayerColor,int> BornOffCounts` → Borne off checkers.
- `InitializeStartingPosition()`
- `MoveChecker(from, to, mover)` → Handles hits.
- `PlaceOnBar(color)`
- `BearOffChecker(color)`
- `CloneDeep()`

### 🟤 `Checker.cs`

Represents a piece.

- `PlayerColor Owner`

*(Optional – can use only `Point.Owner + Count` if simplifying).*

### 🟤 `Player.cs`

Represents a player.

- `string Name`
- `PlayerColor Color` (White/Black)
- `bool IsAI`
- `int Score` (optional)
- `int Direction` (+1 or -1)
- `int HomeStartIndex, HomeEndIndex`

### 🟤 `Dice.cs`

Handles dice.

- `int[] Values`
- `bool IsDouble`
- `List<int> AvailableMoves`
- `Roll()`
- `Consume(dieValue)`
- `Reset()`

### 🟤 `Move.cs`

Represents one move.

- `int FromIndex`
- `int ToIndex`
- `int DieUsed`
- `PlayerColor Player`
- `bool IsHit`
- `bool IsBearingOff`

### 🟤 `Rules.cs`

Game rules logic.

- `GetLegalMoves(board, player, dice)`
- `GetAllMoveSequences(board, player, dice)`
- `IsMoveLegal(board, player, move)`
- `HasLegalMove(board, player, dice)`

### 🟤 `GameManager.cs`

Main game controller.

- `Board Board`
- `Player[] Players`
- `int CurrentPlayerIndex`
- `Dice Dice`
- `Rules Rules`
- `Stack<List<Move>> TurnHistory`
- Events: `OnBoardChanged`, `OnDiceRolled`, `OnTurnChanged`, `OnGameOver`

Methods:

- `StartNewGame()`
- `RollDiceForCurrentPlayer()`
- `ApplyMoveSequence(seq)`
- `EndTurn()`
- `UndoLastTurn()`
- `SaveGame(path)` / `LoadGame(path)`

### 🟤 `AIManager.cs` (optional)

Simple AI.

- `GetGreedySequence(board, player, dice)`

Heuristics: hit blots, advance, bear off if possible.

### 🟤 `SaveManager.cs`

Persistence.

- `SaveBoard(board, filename)`
- `LoadBoard(filename)`
- `SaveSettings(settings)`
- `LoadSettings()`

## 4. XML Layouts

- `main_menu.xml`
- `game_screen.xml`
- `settings.xml`
- `board_view.xml`
- `checker_view.xml`

Each layout defines the UI for its corresponding screen or component, combining static imagery with dynamic elements (e.g., ImageViews for checkers).

## 5. Screens & Flow

### Main Menu

- Buttons → Start Game / Settings / Exit

### Game Screen

- Board background
- Checkers drawn dynamically
- Dice images
- Text: current turn
- Buttons: Roll Dice, End Turn, Undo

### Settings

- Options: sound on/off, change theme

## 6. Game Flow

1. Launch → `MainActivity` (menu).
2. Start Game → `GameActivity`.
3. Player rolls dice → `Dice.Roll()`.
4. `Rules` generates valid moves → highlights on UI.
5. Player taps checker + destination → `GameManager.ApplyMoveSequence()`.
6. Board updates → next move.
7. End turn → switch player.
8. Repeat until one player borne off all 15 checkers.
9. Show winner.

## 7. Checker Placement Details

### 7.1 Logical Placement (Game State)

All piece positions are stored in the `Board` class, which owns 24 `Point` objects. Each `Point` tracks:

```csharp
public PlayerColor? Owner;  // White or Black
public int Count;           // How many checkers on that point
```

The starting layout follows the real backgammon setup:

- **White:** 2 on point 0, 5 on point 11, 3 on point 16, 5 on point 18
- **Black:** 2 on point 23, 5 on point 12, 3 on point 7, 5 on point 5

`Board.InitializeStartingPosition()` seeds these values. When a move happens, `Board.MoveChecker(from, to, player)` updates this data — removing a checker from one point and adding it to another (handling hits and bar moves as needed).

### 7.2 Visual Placement (GUI Rendering)

The board image is static (`@drawable/backgammon_board`), and all checkers are drawn on top of it dynamically using coordinates. Each of the 24 board triangles has a fixed `(X, Y)` coordinate pair on the image stored in an array such as:

```csharp
public static readonly PointF[] PointPositions;
```

On every board update the app:

1. Clears existing checker views.
2. Loops through all 24 points.
3. For each checker on a point:
   - Creates an `ImageView` (white or black checker).
   - Sets its position using `SetX()` and `SetY()` derived from `PointPositions[i]`.
   - Offsets each stacked checker vertically (for example, `+ j * 40`) to visualize the stack.

### 7.3 Selecting and Moving a Checker

Player interactions follow a tap-to-select flow:

1. Player taps a checker → the app reads its `Tag` (which point it belongs to).
2. Legal destinations are calculated by `Rules.GetLegalMoves()`.
3. The UI highlights valid destination points.
4. Player taps a highlight → `GameManager.ApplyMoveSequence(from, to)` executes the move.
5. Board logic updates → `RenderBoard()` redraws checkers in their new spots.

### 7.4 Scaling for Different Devices

To support varying screen sizes, `PointPositions` can be stored as relative percentages (`0–1` range) instead of absolute pixels:

```csharp
float x = boardWidth * RelativePositions[i].X;
float y = boardHeight * RelativePositions[i].Y;
```

This keeps the checker layout responsive across phones and tablets.

### 7.5 Summary

| Layer        | Purpose                                 | Implementation                                   |
|--------------|-----------------------------------------|--------------------------------------------------|
| Logic        | Keeps the checker counts and ownership  | `Board`, `Point`, `GameManager`                  |
| Visual       | Draws checkers on top of the board image| `RenderBoard()` in `GameActivity`                |
| Interaction  | Lets player select and move checkers    | Tap events + `Rules.GetLegalMoves()`             |

**In short:** logical positions are stored in `Board.Points`, visual positions come from mapped coordinates, and every move redraws checkers according to the updated board state.
