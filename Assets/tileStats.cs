﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileStats : MonoBehaviour
{
    public GameObject associatedTile;
    string tileName;
    // Start is called before the first frame update
    void Start()
    {
        //retrive the name of the current gameobject
        tileName = associatedTile.name; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //returns the bonus of vision depending on the tile you are on
    public int getTileBonus()
    {
        if (tileName.Contains("mountain"))
            return 1;
        if (tileName.Contains("plain"))
            return 0;
        if (tileName.Contains("forrest"))
            return -1;
        //if no tile corresponds, send an "error"
        return -100;
    }
}
