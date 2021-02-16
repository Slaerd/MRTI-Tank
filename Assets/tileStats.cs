using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class tileStats : MonoBehaviour
{
    public GameObject associatedTile;
    string tileName;
    // Start is called before the first frame update
    void Start()
    {
        //retrive the name of the current gameobject
        tileName = associatedTile.name;
        Debug.Log("La tile qui a été mis sur ce script porte le nom: " + tileName);
    }

    // Update is called once per frame
    void Update()
    {
        //isTrackingMarker(associatedTile);
        //Debug.Log("L'image est tracked : " + isTrackingMarker(associatedTile));
    }

    //returns the bonus of vision depending on the tile you are on
    public int getTileBonus()
    {
        Debug.Log("in getTileBonus");
        if (tileName.Contains("mountain"))
            return 10;
        if (tileName.Contains("plain"))
            return 0;
        if (tileName.Contains("forrest"))
            return -10;
        //if no tile corresponds, send an "error"
        return -100;
    }

    public bool isTrackingMarker(GameObject imageTarget)
    {
        //var imageTarget = GameObject.Find(imageTargetName);
        var trackable = imageTarget.GetComponent<TrackableBehaviour>();
        var status = trackable.CurrentStatus;
        Debug.Log("Le status du tracking est : " + status);
        return status == TrackableBehaviour.Status.TRACKED;
    }
}
