using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableScript : MonoBehaviour
{
    public int vegId;
    public PlayerController player;
    bool canpickup = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && canpickup)
        {
            player.SelectVegetable(vegId);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            canpickup = true;
            Debug.Log("player entered zone " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            canpickup = false;
            Debug.Log("player left zone " + gameObject.name);
        }
    }

}
