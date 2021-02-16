using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject ennemyTank;
    private int hp = 100;
    public UnityEvent onDestruction;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsDestroyed())
        {
            //faire ce que l'on veut quand la game est finie
            ennemyTank.SetActive(false);
        }
    }

    public bool IsDestroyed()
    {
        if (hp <= 0)
        {
            
            return true;
        }

        return false;
    }

    public int GetHealth()
    {
        return this.hp;
    }

    public void receiveDamage(int damage)
    {
        this.hp = this.hp - damage;
        if(hp <= 0)
        {
            onDestruction.Invoke();
        }
    }


}
