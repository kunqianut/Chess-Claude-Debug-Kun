using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    // Board position, not world position
    int matrixX;
    int matrixY;

    // false = movement, true = attack
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            // Change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        Debug.Log($"[MOVEPLATE] MovePlate clicked at board position ({matrixX}, {matrixY}), Attack: {attack}");

        controller = GameObject.FindGameObjectWithTag("GameController");

        if (controller == null)
        {
            Debug.LogError("[MOVEPLATE] GameController not found!");
            return;
        }

        if (reference == null)
        {
            Debug.LogError("[MOVEPLATE] Reference piece is null!");
            return;
        }

        Chessman movingPiece = reference.GetComponent<Chessman>();
        if (movingPiece == null)
        {
            Debug.LogError("[MOVEPLATE] Moving piece has no Chessman component!");
            return;
        }

        int fromX = movingPiece.GetXBoard();
        int fromY = movingPiece.GetYBoard();

        Debug.Log($"[MOVEMENT] Moving {movingPiece.name} from ({fromX}, {fromY}) to ({matrixX}, {matrixY})");

        if (attack)
        {
            GameObject targetPiece = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            if (targetPiece != null)
            {
                Debug.Log($"[COMBAT] {movingPiece.name} attacking {targetPiece.name} at ({matrixX}, {matrixY})");

                if (targetPiece.name == "white_king")
                {
                    Debug.Log("[GAME] BLACK WINS! White king captured!");
                    controller.GetComponent<Game>().Winner("black");
                }
                if (targetPiece.name == "black_king")
                {
                    Debug.Log("[GAME] WHITE WINS! Black king captured!");
                    controller.GetComponent<Game>().Winner("white");
                }

                Destroy(targetPiece);
            }
            else
            {
                Debug.LogWarning($"[COMBAT] Attack move but no target piece found at ({matrixX}, {matrixY})");
            }
        }

        // Clear old position
        controller.GetComponent<Game>().SetPositionEmpty(fromX, fromY);

        // Set new position
        movingPiece.SetXBoard(matrixX);
        movingPiece.SetYBoard(matrixY);
        movingPiece.SetCoords(); // This will trigger movement logging

        // Update board
        controller.GetComponent<Game>().SetPosition(reference);

        // Switch turns
        string previousPlayer = controller.GetComponent<Game>().GetCurrentPlayer();
        controller.GetComponent<Game>().NextTurn();
        string newPlayer = controller.GetComponent<Game>().GetCurrentPlayer();

        Debug.Log($"[GAME] Turn changed from {previousPlayer} to {newPlayer}");

        // Clean up move plates
        movingPiece.DestroyMovePlates();

        Debug.Log($"[MOVEMENT] Move complete: {movingPiece.name} now at ({matrixX}, {matrixY})");
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}