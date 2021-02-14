using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank : MonoBehaviour
{
    public int health = 100;
    public int visionRange = 2;
    public int defaultVisionRange = 2;
    public int fireRange = 1;
    public int damage = 25;
    public int nbSmokes = 2;

    public GameObject currentTile;
    private bool bonusRangeActivated = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //apl cette ligne en lui donant le gameobject (la tile) sur laquelle tu es en train de tester
        applyTileBonus(currentTile);
        Debug.Log("La visionRange du tank est de: " + this.visionRange);
    }
    public void initTank(int hp, int vision, int fire, int dmg, int smokes)
    {
        this.health = hp;
        this.visionRange = vision;
        this.fireRange = fire;
        this.damage = dmg;
        this.nbSmokes = smokes;
    }

    //receive dmg to health
    public void receiveDamage(int dmg)
    {
        this.health = this.health - dmg;
    }

    //check if game is over
    public bool isDestroyed()
    {
        if (this.health <= 0) return true;
        return false;
    }

    //fires a smoke only if you have enough ammo AND are not in the bonusRange mode
    public void fireSmoke()
    {
        if (nbSmokes > 0 && this.bonusRangeActivated) this.nbSmokes = this.nbSmokes - 1;
    }

    //toggle the bonus vision range
    public void toggleBonusVision()
    {
        if (this.bonusRangeActivated)
        {
            this.bonusRangeActivated = false;
            this.visionRange = this.visionRange - 2;
            this.defaultVisionRange = this.defaultVisionRange - 2;
        }
        this.bonusRangeActivated = true;
        this.visionRange = this.visionRange + 2;
        this.defaultVisionRange = this.defaultVisionRange + 2;
    }

    //retrievs the tileBonus from the tile the tank is located on
    public void applyTileBonus(GameObject tile)
    {
        //reset the vision range so it doesnt stack infinitely
        this.visionRange = this.defaultVisionRange;
        this.visionRange = this.visionRange + tile.GetComponent<tileStats>().getTileBonus();
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

    public void SwitchMaterial(Material m)
    {
        gameObject.GetComponent<MeshRenderer>().material = m;
    }
}
