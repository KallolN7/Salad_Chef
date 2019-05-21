using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VegetableScript : MonoBehaviour
{
    public int vegId;
    public PlayerController player;
    public GameObject Player1GoToChopButton1;
    public GameObject Player1GoToChopButton2;
    public GameObject Player1PickupAgainButton;
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
          
            Player1GoToChopButton1.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "S";
            Player1GoToChopButton1.transform.GetChild(1).GetComponent<Text>().text = "Go to Chopping Board1";
            Player1GoToChopButton1.SetActive(true);

            Player1GoToChopButton2.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "D";
            Player1GoToChopButton2.transform.GetChild(1).GetComponent<Text>().text = "Go to Chopping Board2";
            Player1GoToChopButton2.SetActive(true);

            if (canpickup)
            {
                Player1PickupAgainButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
                Player1PickupAgainButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup Again";
                Player1PickupAgainButton.SetActive(true);
            }
            else
                Player1PickupAgainButton.SetActive(false);

            player.SelectVegetable(vegId);
            return;
        }
        else
            return;
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
