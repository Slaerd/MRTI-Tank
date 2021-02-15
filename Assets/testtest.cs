using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtest : MonoBehaviour
{
    public GameObject tile;
    bool res;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        test();
    }
    void test()
    {
        Debug.Log(tile.GetComponent<ChildObjectsActivator>().getVisibleTile());
      // Debug.Log(res);
    }
}
