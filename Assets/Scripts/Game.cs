using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // Chess piece prefabs
    public GameObject chesspiece;

    // Positions for pieces
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    // Current player
    private string currentPlayer = "white";

    // Game over
    private bool gameOver = false;

    void Awake()
    {
        Debug.Log("[AWAKE] Game script is loading...");
        Debug.Log("[TEST] Basic logging test - if you see this, Debug.Log works!");
    }

    void Start()
    {
        Debug.Log("=== GAME START() METHOD CALLED ===");
        Debug.Log("=== CHESS GAME INITIALIZATION STARTED ===");
        Debug.Log($"Game Start Time: {Time.time} seconds");
        Debug.Log("Creating chess board positions array [8,8]");

        // Check critical references before proceeding
        if (chesspiece == null)
        {
            Debug.LogError("CRITICAL: chesspiece prefab is not assigned! Cannot create pieces!");
            return;
        }
        else
        {
            Debug.Log($"Chesspiece prefab found: {chesspiece.name}");
        }

        Debug.Log("Creating WHITE pieces...");
        playerWhite = new GameObject[]
        {
            Create("white_rook", 0, 0), Create("white_knight", 1, 0), Create("white_bishop", 2, 0),
            Create("white_queen", 3, 0), Create("white_king", 4, 0), Create("white_bishop", 5, 0),
            Create("white_knight", 6, 0), Create("white_rook", 7, 0), Create("white_pawn", 0, 1),
            Create("white_pawn", 1, 1), Create("white_pawn", 2, 1), Create("white_pawn", 3, 1),
            Create("white_pawn", 4, 1), Create("white_pawn", 5, 1), Create("white_pawn", 6, 1),
            Create("white_pawn", 7, 1)
        };
        Debug.Log($"Created {playerWhite.Length} white pieces");

        Debug.Log("Creating BLACK pieces...");
        playerBlack = new GameObject[]
        {
            Create("black_rook", 0, 7), Create("black_knight", 1, 7), Create("black_bishop", 2, 7),
            Create("black_queen", 3, 7), Create("black_king", 4, 7), Create("black_bishop", 5, 7),
            Create("black_knight", 6, 7), Create("black_rook", 7, 7), Create("black_pawn", 0, 6),
            Create("black_pawn", 1, 6), Create("black_pawn", 2, 6), Create("black_pawn", 3, 6),
            Create("black_pawn", 4, 6), Create("black_pawn", 5, 6), Create("black_pawn", 6, 6),
            Create("black_pawn", 7, 6)
        };
        Debug.Log($"Created {playerBlack.Length} black pieces");

        Debug.Log("Setting piece positions on board...");
        // Set all pieces positions on the board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }

        Debug.Log($"Current player: {currentPlayer}");
        Debug.Log("=== CHESS GAME INITIALIZATION COMPLETED ===");
    }

    public GameObject Create(string name, int x, int y)
    {
        Debug.Log($"Creating piece: {name} at board position ({x}, {y})");

        if (chesspiece == null)
        {
            Debug.LogError($"FAILED to create {name}: chesspiece prefab is null!");
            return null;
        }

        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);

        if (obj == null)
        {
            Debug.LogError($"FAILED to instantiate {name}: Instantiate returned null!");
            return null;
        }

        Chessman cm = obj.GetComponent<Chessman>();
        if (cm == null)
        {
            Debug.LogError($"FAILED to get Chessman component from {name}!");
            return obj;
        }

        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        Debug.Log($"Successfully created {name} at world position: {obj.transform.position}");
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("FAILED to set position: GameObject is null!");
            return;
        }

        Chessman cm = obj.GetComponent<Chessman>();
        if (cm == null)
        {
            Debug.LogError($"FAILED to set position: No Chessman component on {obj.name}!");
            return;
        }

        int x = cm.GetXBoard();
        int y = cm.GetYBoard();

        Debug.Log($"Setting board position [{x}, {y}] = {cm.name}");
        positions[x, y] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    void Update()
    {
        // Mouse interactions are handled by OnMouseUp events in Chessman and MovePlate scripts
        // No input handling needed here to avoid Input System conflicts
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        
        GameObject winnerUI = GameObject.FindGameObjectWithTag("WinnerText");
        if (winnerUI != null)
        {
            winnerUI.GetComponent<UnityEngine.UI.Text>().enabled = true;
            winnerUI.GetComponent<UnityEngine.UI.Text>().text = playerWinner + " is the winner";
        }
    }
}