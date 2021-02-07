using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_token : MonoBehaviour
{
    string tankName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if(Physics.Raycast(ray, out Hit))
            {
                tankName = Hit.transform.name;
                //do what you want to happen when you click on the tank on the phone
            }
        }
    }
}
