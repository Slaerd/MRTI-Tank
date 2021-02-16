using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ennemyTank;
    private int hp = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsDestroyed())
        {
            //faire ce que l'on veut quand la game est finie
            Destroy();
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

    public void takeDamage(int damage)
    {
        this.hp = this.hp - damage;
    }

    public void Destroy()
    {
        Object.Destroy(ennemyTank);
    }

}
