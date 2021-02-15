using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    public GameObject pl;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool ret = isTrackingMarker(pl);
        Debug.Log("track status is = " + ret);
    }

    public bool isTrackingMarker(GameObject imageTargetName)
    {
        //var imageTarget = GameObject.Find(imageTargetName);
        var imageTarget = imageTargetName;
        var trackable = imageTarget.GetComponent<Vuforia.TrackableBehaviour>();
        var status = trackable.CurrentStatus;
        return status == Vuforia.TrackableBehaviour.Status.TRACKED;
    }

}
