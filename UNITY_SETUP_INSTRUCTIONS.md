# Chess Game Unity Setup Instructions

## Current Status
✅ Scripts are created and ready (`Game.cs`, `Chessman.cs`, `MovePlate.cs`)
✅ Chess assets are available in `Assets/chessAssets/`
❌ Scene is empty - needs GameObjects and prefabs

## Step-by-Step Setup in Unity Editor

### 1. Open Unity Project
- Open the project in Unity Editor
- Go to `Assets/Scenes/SampleScene.unity`

### 2. Create GameController
1. **Right-click in Hierarchy** → Create Empty
2. **Rename** to "GameController"
3. **Add Component**: Game (script)
4. **Set Tag**:
   - Go to Inspector → Tag dropdown → "Add Tag..."
   - Add new tag: "GameController"
   - Set GameController object tag to "GameController"

### 3. Create Chess Board Background
1. **Right-click in Hierarchy** → 2D Object → Sprite
2. **Rename** to "ChessBoard"
3. **In Inspector**:
   - Sprite Renderer → Sprite: Select `Assets/chessAssets/board.png`
   - Set Position: (0, 0, 0)
   - Set Sprite Renderer → Order in Layer: -1

### 4. Configure Main Camera
1. **Select Main Camera** in hierarchy
2. **In Inspector**:
   - Set Position: (0, 0, -10)
   - Camera Component:
     - Projection: Orthographic
     - Size: 4
     - Clear Flags: Solid Color
     - Background: Black or dark color

### 5. Create ChessPiece Prefab
1. **Right-click in Hierarchy** → Create Empty
2. **Rename** to "ChessPiece"
3. **Add Components**:
   - Sprite Renderer
   - Box Collider 2D
   - Chessman (script)
4. **Configure Chessman Component**:
   - Assign ALL sprite fields:
     - black_king: `Assets/chessAssets/black_king.png`
     - black_queen: `Assets/chessAssets/black_queen.png`
     - black_rook: `Assets/chessAssets/black_rook.png`
     - black_bishop: `Assets/chessAssets/black_bishop.png`
     - black_knight: `Assets/chessAssets/black_knight.png`
     - black_pawn: `Assets/chessAssets/black_pawn.png`
     - white_king: `Assets/chessAssets/white_king.png`
     - white_queen: `Assets/chessAssets/white_queen.png`
     - white_rook: `Assets/chessAssets/white_rook.png`
     - white_bishop: `Assets/chessAssets/white_bishop.png`
     - white_knight: `Assets/chessAssets/white_knight.png`
     - white_pawn: `Assets/chessAssets/white_pawn.png`
5. **Create Prefab**: Drag ChessPiece to `Assets/Prefabs/` folder
6. **Delete** ChessPiece from hierarchy (keep prefab)

### 6. Create MovePlate Prefab
1. **Right-click in Hierarchy** → Create Empty
2. **Rename** to "MovePlate"
3. **Add Components**:
   - Sprite Renderer
   - Box Collider 2D
   - MovePlate (script)
4. **Configure**:
   - Sprite Renderer → Sprite: `Assets/chessAssets/MovePlate.png`
   - Set Tag: "MovePlate" (create tag if needed)
   - Sprite Renderer → Color: Semi-transparent green (0, 1, 0, 0.5)
5. **Create Prefab**: Drag MovePlate to `Assets/Prefabs/` folder
6. **Delete** MovePlate from hierarchy (keep prefab)

### 7. Connect Prefab References
1. **Select GameController** in hierarchy
2. **In Game component**: Drag `ChessPiece` prefab to "Chesspiece" field
3. **Open ChessPiece prefab** (double-click in Project)
4. **In Chessman component**: Drag `MovePlate` prefab to "Move Plate" field
5. **Save prefab** (Ctrl/Cmd + S)

### 8. Final Verification
- GameController should have Game script with ChessPiece prefab assigned
- ChessPiece prefab should have all sprites assigned and MovePlate reference
- MovePlate prefab should have MovePlate.png sprite and MovePlate tag
- Camera should be orthographic with size 4
- Chess board should be visible in scene view

### 9. Test the Game
1. **Press Play**
2. You should see:
   - Chess board in the scene
   - Chess pieces appear automatically in starting positions
   - Click pieces to see green/red move indicators
   - Functional chess gameplay

## Troubleshooting
- **No pieces appear**: Check GameController has ChessPiece prefab assigned
- **Can't click pieces**: Check pieces have Box Collider 2D
- **No move plates**: Check MovePlate prefab reference in ChessPiece
- **Wrong positioning**: Verify camera orthographic size is 4

## Expected Result
A fully functional chess game with:
- 32 pieces in starting positions
- Click-to-move gameplay
- Visual move indicators
- Turn-based play (white first)
- Win condition when king captured