using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Pass : MonoBehaviour
{
    [SerializeField] private Text roundNumber;
    [SerializeField] private GameObject blue;
    public static UnityAction<bool> switchTurn;
    private bool toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextRound()
    {
        toggle = !toggle;
        roundNumber.text = (int.Parse(roundNumber.text) + 1).ToString();
        blue.SetActive(toggle);
        switchTurn.Invoke(toggle);
    }
}
