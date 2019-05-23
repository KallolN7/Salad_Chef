using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VegetableScript : MonoBehaviour
{
    public int vegId;
    public int orderCalculationID;
    private PlayerController player1;
    private Player2Controller player2;
    public GameObject Player1GoToChopButton1;
    public GameObject Player1GoToChopButton2;
    public GameObject Player1PickupAgainButton;
    public GameObject Player1PickAnotherVegButton;
    public GameObject Player2GoToChopButton1;
    public GameObject Player2GoToChopButton2;
    public GameObject Player2PickupAgainButton;
    public GameObject Player2PickAnotherVegButton;
    bool canpickup = false;
    bool player2pickup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape) && player1!= null && player1.destination == transform)
        {
            player1.ResetVegetableButtons();
            Debug.Log("reset");
        }

        if (Input.GetKeyDown(KeyCode.A) && canpickup)
        {
            player1.CloseVegButtons();

            Player1GoToChopButton1.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "S";
            Player1GoToChopButton1.transform.GetChild(1).GetComponent<Text>().text = "Go to Chopping Board";
            Player1GoToChopButton1.SetActive(true);

            //Player1GoToChopButton2.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "D";
            //Player1GoToChopButton2.transform.GetChild(1).GetComponent<Text>().text = "Go to Chopping Board2";
            //Player1GoToChopButton2.SetActive(true);

            Player1PickAnotherVegButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Esc";
            Player1PickAnotherVegButton.transform.GetChild(1).GetComponent<Text>().text = "Pick other Vegetable";
            Player1PickAnotherVegButton.SetActive(true);

            if (player1.vegetablesCarryingArray[0]==null || player1.vegetablesCarryingArray[1] == null)
            {
                Player1PickupAgainButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "A";
                Player1PickupAgainButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup Again";
                Player1PickupAgainButton.SetActive(true);
            }
            else 
                Player1PickupAgainButton.SetActive(false);

            player1.SelectVegetable(vegId, orderCalculationID);
        }

        //Player2 Zone
       if (Input.GetKeyDown(KeyCode.Backspace) && player2 != null && player2.destination == transform)
        {
            player2.ResetVegetableButtons();
            Debug.Log("reset");
        }

        if (Input.GetKeyDown(KeyCode.L) && player2pickup)
        {
            player2.SelectVegetable(vegId, orderCalculationID);

            player2.CloseVegButtons();

            //Player2GoToChopButton1.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ";";
            //Player2GoToChopButton1.transform.GetChild(1).GetComponent<Text>().text = "Go to Chopping Board1";
            //Player2GoToChopButton1.SetActive(true);

            Player2GoToChopButton2.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "'";
            Player2GoToChopButton2.transform.GetChild(1).GetComponent<Text>().text = "Go to Chopping Board";
            Player2GoToChopButton2.SetActive(true);

            Player2PickAnotherVegButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Backspace";
            Player2PickAnotherVegButton.transform.GetChild(1).GetComponent<Text>().text = "Pick other Vegetable";
            Player2PickAnotherVegButton.SetActive(true);

            if (player2.vegetablesCarryingArray[0] == null || player2.vegetablesCarryingArray[1] == null)
            {
                Player2PickupAgainButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "L";
                Player2PickupAgainButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup Again";
                Player2PickupAgainButton.SetActive(true);
            }
            else
                Player2PickupAgainButton.SetActive(false);
        }

        else
            return;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {         
            player1 = other.GetComponent<PlayerController>();
            if(player1.destination == transform)
            {
                canpickup = true;
                player1.CloseVegButtons();
                player1.Player1ActionButton.SetActive(true);
                player1.Player1Buttons[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Esc";
                player1.Player1Buttons[0].transform.GetChild(1).GetComponent<Text>().text = "Discard";
                player1.Player1Buttons[0].SetActive(true);
                Debug.Log("player entered zone " + gameObject.name);
            }
            
        }

        else if(other.tag == "Player2")
        {
            player2 = other.GetComponent<Player2Controller>();
            if (player2.destination == transform)
            {
                player2pickup = true;
                player2.CloseVegButtons();
                player2.Player1ActionButton.SetActive(true);
                player2.Player1Buttons[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "BackSpace";
                player2.Player1Buttons[0].transform.GetChild(1).GetComponent<Text>().text = "Discard";
                player2.Player1Buttons[0].SetActive(true);
                Debug.Log("player2 entered zone " + gameObject.name);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            canpickup = false;
            player1.CloseVegButtons();
            player1.Player1ActionButton.SetActive(false);
            player1 = null;
            Debug.Log("player left zone " + gameObject.name);
        }
        else if (other.tag == "Player2")
        {
            player2pickup = false;
            player2.CloseVegButtons();
            player2.Player1ActionButton.SetActive(false);
            player2 = null;
            Debug.Log("player2 left zone " + gameObject.name);
        }
    }

}
