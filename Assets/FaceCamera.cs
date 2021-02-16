using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private Camera arCam;
    [SerializeField] private GameObject myself;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myself.transform.rotation = Quaternion.LookRotation(arCam.transform.position, Vector3.up);
    }
}
