using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public int CustomerID;
    public string CustomerOrder;
    private bool canPlaceBowl;
    private PlayerController player1;
    private PlayerController player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && canPlaceBowl)
        {
            player1.ServeCustomer(CustomerID);

            if (CustomerOrder == player1.saladCombination)
                Debug.Log("Happy");
            else
                Debug.Log("Angry");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            canPlaceBowl = true;
            player1 = other.GetComponent<PlayerController>();
            Debug.Log("player entered zone " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            canPlaceBowl = false;
            Debug.Log("player left zone " + gameObject.name);
        }
    }
}
