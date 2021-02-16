using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    public int health = 100;
    public int visionRange = 2;
    public int defaultDamage = 25;
    public int fireRange = 1;
    public int damage = 25;
    public int nbSmokes = 2;
    [SerializeField] private Material tankTextureBasic;
    [SerializeField] private Material tankTextureSelected;
    [SerializeField] private Material tankTextureTargeted;
    [SerializeField] private Text HP;



    //public GameObject currentTile;
    private bool bonusRangeActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        RaycastController.onTankDrag += Attack;
        tankTextureBasic = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("La visionRange du tank est de: " + this.visionRange);
        
    }

    private void OnDestroy()
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

    //receive dmg to health
    public void receiveDamage(int dmg)
    {
        this.health -= dmg;
        HP.text = this.health.ToString();
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

        }
        this.bonusRangeActivated = true;
        this.visionRange = this.visionRange + 2;

    }

    //retrievs the tileBonus from the tile the tank is located on
    public void applyTileBonus(GameObject tile)
    {
        //reset the vision range so it doesnt stack infinitely
        Debug.Log(tile.name);
        int modifier = tile.GetComponent<tileStats>().getTileBonus();
        if (this.defaultDamage + modifier != this.damage)
        {
            this.damage = this.defaultDamage + modifier;
        }
        
        
    }
    private static void Attack(GameObject attacker, GameObject defender)
    {
        if (!GameObject.ReferenceEquals(attacker, defender))
            defender.GetComponent<Tank>().receiveDamage(attacker.GetComponent<Tank>().getDamage());
    }

    public void Effect(Tank target)
    {
        target.receiveDamage(-50);
        target.Target();
    }

    public void Select()
    {
        gameObject.GetComponent<MeshRenderer>().material = tankTextureSelected;
    }

    public void Unselect()
    {
        gameObject.GetComponent<MeshRenderer>().material = tankTextureBasic;
    }

    public void Target()
    {
        gameObject.GetComponent<MeshRenderer>().material = tankTextureTargeted;
    }

    public void Untarget()
    {
        gameObject.GetComponent<MeshRenderer>().material = tankTextureBasic;
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