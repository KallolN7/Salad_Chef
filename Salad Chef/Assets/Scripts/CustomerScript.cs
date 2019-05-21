using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerScript : MonoBehaviour
{
    public int CustomerID;
    public string CustomerOrder;
    private bool canPlaceBowl;
    public GameObject serveButton;
    private PlayerController player1;
    private PlayerController player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && canPlaceBowl)
        {
            player1.ServeCustomer(CustomerID);
            player1.canServe = false;
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
            if(player1.destination == transform)
            {
                serveButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Tab";
                serveButton.transform.GetChild(1).GetComponent<Text>().text = "Serve";
                serveButton.SetActive(true);
                Debug.Log("player entered zone " + gameObject.name);
            }
          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            canPlaceBowl = false;
            player1.CloseVegButtons();
            player1.Player1ActionButton.SetActive(false);
            player1 = null;
            Debug.Log("player left zone " + gameObject.name);
        }
    }
}
