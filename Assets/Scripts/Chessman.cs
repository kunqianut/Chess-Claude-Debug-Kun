using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    // References
    public GameObject controller;
    public GameObject movePlate;

    // Positions
    private int xBoard = -1;
    private int yBoard = -1;

    // Variable for keeping track of the player
    private string player;

    // References to all the sprites that the chesspiece can be
    public Sprite black_king, black_queen, black_rook, black_bishop, black_knight, black_pawn;
    public Sprite white_king, white_queen, white_rook, white_bishop, white_knight, white_pawn;

    public void Activate()
    {
        Debug.Log($"Activating chessman: {this.name} at board position ({xBoard}, {yBoard})");

        // Take the instantiated location and adjust transform
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (controller == null)
        {
            Debug.LogError($"CRITICAL: GameController not found for {this.name}! Make sure GameObject has 'GameController' tag.");
            return;
        }

        // Adjust position
        SetCoords();

        // Choose correct sprite based on piece name
        switch (this.name)
        {
            case "black_king":
                if (black_king == null)
                {
                    Debug.LogError($"CRITICAL: black_king sprite is not assigned for {this.name}!");
                }
                this.GetComponent<SpriteRenderer>().sprite = black_king;
                player = "black";
                break;
            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black_queen;
                player = "black";
                break;
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook;
                player = "black";
                break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop;
                player = "black";
                break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knight;
                player = "black";
                break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn;
                player = "black";
                break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king;
                player = "white";
                break;
            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = white_queen;
                player = "white";
                break;
            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook;
                player = "white";
                break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop;
                player = "white";
                break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knight;
                player = "white";
                break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn;
                player = "white";
                break;
        }

        // Log sprite assignment result
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError($"CRITICAL: No SpriteRenderer component found on {this.name}!");
        }
        else if (sr.sprite == null)
        {
            Debug.LogError($"CRITICAL: Sprite not set for {this.name} - piece will be invisible!");
        }
        else
        {
            Debug.Log($"Successfully set sprite for {this.name}: {sr.sprite.name}");
        }

        Debug.Log($"Piece {this.name} activated as {player} player at world position: {this.transform.position}");
    }

    public void SetCoords()
    {
        Debug.Log($"[MOVEMENT] SetCoords for {this.name}: converting board position ({xBoard}, {yBoard}) to world position");

        Vector3 oldPosition = this.transform.position;

        // Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        // Adjust by unity values
        x *= 0.66f;
        y *= 0.66f;

        // Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        // Set actual unity position
        Vector3 worldPos = new Vector3(x, y, -1.0f);
        this.transform.position = worldPos;

        Debug.Log($"[MOVEMENT] {this.name} moved from {oldPosition} to {worldPos} (board: {xBoard}, {yBoard})");
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        Debug.Log($"[CHESSMAN] OnMouseUp called on {this.name} at position ({xBoard}, {yBoard})");

        if (controller == null)
        {
            Debug.LogError($"[CHESSMAN] Controller is null for {this.name}!");
            return;
        }

        Game gameScript = controller.GetComponent<Game>();
        if (gameScript == null)
        {
            Debug.LogError($"[CHESSMAN] Game component not found on controller for {this.name}!");
            return;
        }

        bool isGameOver = gameScript.IsGameOver();
        string currentPlayer = gameScript.GetCurrentPlayer();

        Debug.Log($"[CHESSMAN] Game state - IsGameOver: {isGameOver}, CurrentPlayer: {currentPlayer}, ThisPlayer: {player}");

        if (!isGameOver && currentPlayer == player)
        {
            Debug.Log($"[CHESSMAN] Valid turn for {this.name} - showing move options");

            // Remove all moveplates relating to previously selected piece
            DestroyMovePlates();

            // Create new move plates
            InitiateMovePlates();
        }
        else
        {
            if (isGameOver)
                Debug.Log($"[CHESSMAN] Cannot move {this.name} - game is over");
            else if (currentPlayer != player)
                Debug.Log($"[CHESSMAN] Cannot move {this.name} - not {player}'s turn (current: {currentPlayer})");
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        Debug.Log($"[MOVEPLATE] Destroying {movePlates.Length} move plates for {this.name}");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        Debug.Log($"[MOVEPLATE] Creating move plates for {this.name} ({player})");

        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && 
                sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && 
                sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        Debug.Log($"[MOVEPLATE] Spawning normal move plate for {this.name} at board position ({matrixX}, {matrixY})");

        // Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        // Adjust by unity values
        x *= 0.66f;
        y *= 0.66f;

        // Add constants
        x += -2.3f;
        y += -2.3f;

        // Set actual unity position
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);

        Debug.Log($"[MOVEPLATE] Move plate created at world position ({x}, {y})");
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        Debug.Log($"[MOVEPLATE] Spawning ATTACK move plate for {this.name} at board position ({matrixX}, {matrixY})");

        // Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        // Adjust by unity values
        x *= 0.66f;
        y *= 0.66f;

        // Add constants
        x += -2.3f;
        y += -2.3f;

        // Set actual unity position
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);

        Debug.Log($"[MOVEPLATE] Attack move plate created at world position ({x}, {y})");
    }
}