using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    private string player;
    private int xBoard = -1;
    private int yBoard = -1;

    public Sprite Chess_Pieces_0, Chess_Pieces_1, Chess_Pieces_2, Chess_Pieces_3, Chess_Pieces_4, Chess_Pieces_5;
    public Sprite Chess_Pieces_6, Chess_Pieces_7, Chess_Pieces_8, Chess_Pieces_9, Chess_Pieces_10, Chess_Pieces_11;


    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");


        //Take the instantiated location and adjust transform
        SetCoords();

        //Choose correct sprite based on piece's name
        switch (this.name)
        {
            case "Chess_Pieces_0": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_0; player = "black"; break;
            case "Chess_Pieces_1": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_1; player = "black"; break;
            case "Chess_Pieces_2": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_2; player = "black"; break;
            case "Chess_Pieces_3": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_3; player = "black"; break;
            case "Chess_Pieces_4": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_4; player = "black"; break;
            case "Chess_Pieces_5": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_5; player = "black"; break;
            case "Chess_Pieces_6": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_6; player = "white"; break;
            case "Chess_Pieces_7": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_7; player = "white"; break;
            case "Chess_Pieces_8": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_8; player = "white"; break;
            case "Chess_Pieces_9": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_9; player = "white"; break;
            case "Chess_Pieces_10": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_10; player = "white"; break;
            case "Chess_Pieces_11": this.GetComponent<SpriteRenderer>().sprite = Chess_Pieces_11; player = "white"; break;
        }
    }

    public void SetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
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
        //Remove all moveplates relating to previously selected piece
        DestroyMovePlates();

        //Create new MovePlates
        InitiateMovePlates();

    }

    public void DestroyMovePlates()
    {
        //Destroy old MovePlates
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "Chess_Pieces_1":
            case "Chess_Pieces_7":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "Chess_Pieces_4":
            case "Chess_Pieces_10":
                LMovePlate();
                break;
            case "Chess_Pieces_3":
            case "Chess_Pieces_9":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "Chess_Pieces_0":
            case "Chess_Pieces_6":
                SurroundMovePlate();
                break;
            case "Chess_Pieces_2":
            case "Chess_Pieces_8":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "Chess_Pieces_5":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "Chess_Pieces_11":
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
        PointMovePlate(xBoard - 1, yBoard + 0);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 0);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
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

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}


