using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank : MonoBehaviour
{
    public int health = 100;
    public int visionRange = 2;
    public int fireRange = 1;
    public int damage = 25;
    public int nbSmokes = 2;

    private bool bonusRangeActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void initTank(int hp, int vision, int fire, int dmg, int smokes)
    {
        this.health = hp;
        this.visionRange = vision;
        this.fireRange = fire;
        this.damage = dmg;
        this.nbSmokes = smokes;
    }

    public void receiveDamage(int dmg)
    {
        this.health = this.health - dmg;
    }

    public bool isDestroyed()
    {
        if (this.health <= 0) return true;
        return false;
    }

    public void fireSmoke()
    {
        if (nbSmokes > 0 && this.bonusRangeActivated) this.nbSmokes = this.nbSmokes - 1;
    }

    public void toggleBonusVision()
    {
        if (this.bonusRangeActivated)
        {
            this.bonusRangeActivated = false;
            this.visionRange = this.visionRange - 2;
        }
        this.bonusRangeActivated = true;
        this.visionRange = this.visionRange + 2;
    }

    public void tileBonus(int n)
    {
        this.visionRange = this.visionRange + n;
    }

    // GETTERS
    public int getHealth()
    {
        return this.health;
    }

    public int getVisionRange()
    {
        return this.visionRange;
    }
    
    public int getFireRange()
    {
        return this.fireRange;
    }

    public int getDamage()
    {
        return this.damage;
    }

    public int getNbSmokes()
    {
        return this.nbSmokes;
    }

    public bool getBonusRangeActivated()
    {
        return this.bonusRangeActivated;
    }
}
