using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
//Attach to the image tracker
public class ChildObjectsActivator : MonoBehaviour, ITrackableEventHandler
{
    bool visibleTile;//the variable we will use to determine if the tile is visible or not
    private TrackableBehaviour trackableBehaviour;

    void Start()
    {
        Debug.Log("aled");
        visibleTile = true;//set all the tiles as currently seen at the beginning of the game
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (trackableBehaviour)
            trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    //getter for the visibleTile variable
    public bool getVisibleTile()
    {
        return this.visibleTile;
    }


    
    public void OnTrackableStateChanged(
      TrackableBehaviour.Status previousStatus,
      TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            OnTrackingFound();
        else
            onTrackingLost();
    }
    private void OnTrackingFound()
    {
        visibleTile = true; //in case we have sight of the tile again
        if (transform.childCount > 0)
            SetChildrenActive(true);
    }
    private void onTrackingLost()
    {
        visibleTile = false; //when we lose sight of the tile
        if (transform.childCount > 0)
            SetChildrenActive(false);
    }
    private void SetChildrenActive(bool activeState)
    {
        for (int i = 0; i <= transform.childCount; i++)
            transform.GetChild(i++).gameObject.SetActive(activeState);
    }
        
}