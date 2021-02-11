using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map1 : MonoBehaviour
{
    public GameObject[] tokens = new GameObject[12];

    int[,] map = new int[3, 4];
    GameObject[,] tokenMap = new GameObject[3, 4];
    int[] oldPos = new int[2];
    // Start is called before the first frame update
    void Start()
    {
        map[0, 0] = 0;
        map[0, 1] = 2;
        map[0, 2] = 0;
        map[1, 0] = 0;
        map[1, 1] = 0;
        map[1, 2] = 0;
        map[2, 0] = 0;
        map[2, 1] = 0;
        map[2, 2] = 0;
        map[3, 0] = 0;
        map[3, 1] = 0;
        map[3, 2] = 0;


        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                tokenMap[i, j] = tokens[i + j];
            }
        }
        oldPos[0] = 1;
        oldPos[1] = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Checks if the move performed */
    public bool correctMove(GameObject tank)
    {
        int[] move = getCoords(tank);
        if(oldPos[0] != move[0] && oldPos[1] != move[1]) return false;
        if (oldPos[0] - move[0] > 1 || oldPos[0] - move[0] < -1) return false;
        if (oldPos[1] - move[1] > 1 || oldPos[1] - move[1] < -1) return false;
        return true;   
    }

    public bool isInRange(GameObject tank, int[] fire)
    {
        int[] tankPos = getCoords(tank);
       
        if (tankPos[0] != fire[0] && tankPos[1] != fire[1]) return false;
        if (tankPos[0] - fire[0] > 2 || tankPos[0] - fire[0] < -2) return false;
        if (tankPos[1] - fire[1] > 2 || tankPos[1] - fire[1] < -2) return false;
        return true;
    }

    /* Used to change the "map" grid when placing the tank at the begining of the game */
    public void placeTank(GameObject tank)
    {
        int[] pos = getCoords(tank);
        map[pos[0], pos[1]] = 1;
        map[oldPos[0], oldPos[1]] = 0;
        oldPos = pos;
    }

    public int[] getCoords(GameObject tank)
    {
        int[] res = new int[2];
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                bool visible = tokenMap[i, j].GetComponent<ChildObjectsActivator>().getVisibleTile();
                if (visible)
                {
                    res[0] = i;
                    res[1] = j;
                    Debug.Log("Pos i: " + i + " Pos j: ");
                    break;
                }
                
            }
        }
        return res;
    }

   
}
