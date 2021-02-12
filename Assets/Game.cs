using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int gameMode = 0; //0 Player turn, 1 Enemy turn, 2 Attack
    // Start is called before the first frame update
    void Start()
    {
        //gameMode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerTurn()
    {
        gameMode = 0;
    }

    public void EnemyTurn()
    {
        gameMode = 1;
    }
    
    public void TankSelected()
    {
        gameMode = 2;
    }
}
