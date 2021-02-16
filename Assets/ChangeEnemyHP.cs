using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEnemyHP : MonoBehaviour
{

    public GameObject enemyTank;
    Text txt;
    public int enemyHP = 100;
    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        txt.text = "" + enemyTank.GetComponent<Enemy>().GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "" + enemyTank.GetComponent<Enemy>().GetHealth();
    }
}
