using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public GameObject ChessPiece;

    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    //current turn
    private string currentPlayer = "white";

    //Game Ending
    private bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[] { Create("Chess_Pieces_8", 0, 0), Create("Chess_Pieces_10", 1, 0),
            Create("Chess_Pieces_9", 2, 0), Create("Chess_Pieces_7", 3, 0), Create("Chess_Pieces_6", 4, 0),
            Create("Chess_Pieces_9", 5, 0), Create("Chess_Pieces_10", 6, 0), Create("Chess_Pieces_8", 7, 0),
            Create("Chess_Pieces_11", 0, 1), Create("Chess_Pieces_11", 1, 1), Create("Chess_Pieces_11", 2, 1),
            Create("Chess_Pieces_11", 3, 1), Create("Chess_Pieces_11", 4, 1), Create("Chess_Pieces_11", 5, 1),
            Create("Chess_Pieces_11", 6, 1), Create("Chess_Pieces_11", 7, 1) };
        playerBlack = new GameObject[] { Create("Chess_Pieces_2", 0, 7), Create("Chess_Pieces_4",1,7),
            Create("Chess_Pieces_3",2,7), Create("Chess_Pieces_1",3,7), Create("Chess_Pieces_0",4,7),
            Create("Chess_Pieces_3",5,7), Create("Chess_Pieces_4",6,7), Create("Chess_Pieces_2",7,7),
            Create("Chess_Pieces_5", 0, 6), Create("Chess_Pieces_5", 1, 6), Create("Chess_Pieces_5", 2, 6),
            Create("Chess_Pieces_5", 3, 6), Create("Chess_Pieces_5", 4, 6), Create("Chess_Pieces_5", 5, 6),
            Create("Chess_Pieces_5", 6, 6), Create("Chess_Pieces_5", 7, 6) };

        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(ChessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
        cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
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

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            //Using UnityEngine.SceneManagement is needed here
            SceneManager.LoadScene("Game"); //Restarts the game by loading the scene over again
        }
    }

    // public void Winner(string playerWinner)
    // {
    //     gameOver = true;

    //     //Using UnityEngine.UI is needed here
    //     GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
    //     GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

    //     GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    // }


}
