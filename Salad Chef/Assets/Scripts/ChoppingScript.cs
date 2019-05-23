using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoppingScript : MonoBehaviour
{
    private PlayerController player1;                         //player1 scripts reference
    private Player2Controller player2;                       //player2 scripts reference
    bool canDrop;                                                         //whether player can drop a vegetable to chop or not
    bool choppingDone;                                             //whether chopping is done or not
    bool canPickupFromSideTable;                          //whether player can pickup from side table or not
    float currCountdownValue;                                 //current time left for chopping to finish
    public GameObject saladBowl;                          // salad bowl object to instantiate once chopping is done 
    public GameObject spawnPos;                           // salad bowl spawing position
    public GameObject sidePlatePos;                       // side plate position

    public GameObject Player1ChopButton;                  //display button for chopping
    public GameObject Player1ChopAgainButton;          //display button for chopping another vegetable in hand
    public GameObject Player1SidePlateButton;            //display button for dropping at side plate
    public GameObject Player1TrashButton;                  //display button for trash
    public GameObject Player1ServeButton;                   //display button for serving customer
    public GameObject Player2ChopButton;
    public GameObject Player2ChopAgainButton;
    public GameObject Player2SidePlateButton;
    public GameObject Player2TrashButton;
    public GameObject Player2ServeButton;

    private GameObject PlayerVeg1, PlayerVeg2;                         //reference to the vegetables carried by player
    private GameObject Player2Veg1, Player2Veg2;                       //reference to the vegetables carried by player

    private GameObject spawnedBowl;                            //spawned salad bowl
    private string combination;                                          //combination of salad made
    private int combinationOrderID;                                 // used for combination calculation when matching with customer order
    private int sidePlateorderID;                                        //side plate vegetable id
    GameObject sidePlateVeg;                                            //vegetable kept on side plate 
    GameObject ChoppingVeg;                                          // vegetable being chopped
    public Image timerImage;                                              // image to show time left to finish chopping

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        //drop a vegetable to chop
        if (Input.GetKeyDown(KeyCode.Space) && canDrop)
        {
            Chop();
            canDrop = false;
        }

        //pickup salad bowl after chopping
        else if (Input.GetKeyDown(KeyCode.Space) && choppingDone)
        {
            PickUpSaladBowl();
            choppingDone = false;
        }
        //chop another vegetable in hand
        else if (Input.GetKeyDown(KeyCode.LeftAlt) && choppingDone)
        {
            Chop();
            canDrop = false;
        }
        //drop a vegetable at side plate
        else if (Input.GetKeyDown(KeyCode.LeftControl) && canDrop && !canPickupFromSideTable)
        {
            DropAtSidePlate();
        }
        //pickup vegetable from side plate
        else if (Input.GetKeyDown(KeyCode.LeftControl) && canPickupFromSideTable)
        {
            PickUpFromSidePlate();
        }
        //trash salad bowl
        else if (Input.GetKeyDown(KeyCode.LeftShift) && choppingDone)
        {
            Trash();

        }
        //serve salad to customer
        else if (Input.GetKeyDown(KeyCode.Tab) && choppingDone)
        {
            player1.OpenServeButtons();
        }


        //Player2 Actions
        if (Input.GetKeyDown(KeyCode.Return) && canDrop)
        {
            Player2Chop();
            canDrop = false;
        }

        else if (Input.GetKeyDown(KeyCode.Return) && choppingDone)
        {
            Player2PickUpSaladBowl();
            choppingDone = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightAlt) && choppingDone)
        {
            Player2Chop();
            canDrop = false;
        }

        else if (Input.GetKeyDown(KeyCode.RightControl) && canDrop && !canPickupFromSideTable)
        {
            Player2DropAtSidePlate();
        }
        else if (Input.GetKeyDown(KeyCode.RightControl) && canPickupFromSideTable)
        {
            Player2PickUpFromSidePlate();
        }

        else if (Input.GetKeyDown(KeyCode.RightShift) && choppingDone)
        {
            Player2Trash();

        }
        else if (Input.GetKeyDown(KeyCode.Backslash) && choppingDone)
        {
            player2.OpenServeButtons();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            canDrop = true;
            Debug.Log("player entered zone " + gameObject.name);
            player1 = other.GetComponent<PlayerController>();

            if(player1.vegetablesCarryingArray!= null && player1.destination == transform)
            {
                player1.CloseVegButtons();                                                                   //close buttons to show all vegetables
                player1.Player1ActionButton.SetActive(false);


                //assigning texts and activating all buttons to display all options a player has.

                Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Space";
                Player1ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Chop";
                Player1ChopButton.SetActive(true);

                if (other.GetComponent<PlayerController>().vegetablesCarryingArray[1] != null)
                {
                    PlayerVeg2 = other.GetComponent<PlayerController>().vegetablesCarryingArray[1];

                    Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                    Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Keep at Side Plate";
                    Player1SidePlateButton.SetActive(true);
                    Debug.Log(PlayerVeg2.name);
                }
                else if(other.GetComponent<PlayerController>().vegetablesCarryingArray[0] != null)
                {
                    PlayerVeg1 = other.GetComponent<PlayerController>().vegetablesCarryingArray[0];
                    Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                    Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Keep at Side Plate";
                    Player1SidePlateButton.SetActive(true);
                    Debug.Log(PlayerVeg1.name);
                }
            }

        }


        //Player2 Actions
        if (other.tag == "Player2")
        {
            canDrop = true;
            Debug.Log("player2 entered zone " + gameObject.name);
            player2 = other.GetComponent<Player2Controller>();

            if (player2.vegetablesCarryingArray != null && player2.destination == transform)
            {
                player2.CloseVegButtons();
                player2.Player1ActionButton.SetActive(false);

                Player2ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Enter";
                Player2ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Chop";
                Player2ChopButton.SetActive(true);

                if (other.GetComponent<Player2Controller>().vegetablesCarryingArray[1] != null)
                {
                    Player2Veg2 = other.GetComponent<Player2Controller>().vegetablesCarryingArray[1];

                    Player2SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "RCtrl";
                    Player2SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Keep at Side Plate";
                    Player2SidePlateButton.SetActive(true);
                    Debug.Log(Player2Veg2.name);
                }
                else if (other.GetComponent<Player2Controller>().vegetablesCarryingArray[0] != null)
                {
                    Player2Veg1 = other.GetComponent<Player2Controller>().vegetablesCarryingArray[0];
                    Player2SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "RCtrl";
                    Player2SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Keep at Side Plate";
                    Player2SidePlateButton.SetActive(true);
                    Debug.Log(Player2Veg1.name);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //removing player reference on its exit
        if (other.tag == "Player1")
        {
            canDrop = false;
           
            player1.CloseVegButtons();
            Player1ChopButton.SetActive(false);
            player1 = null;
            Debug.Log("player left zone " + gameObject.name);
        }

        //Player2 Zone
       else if (other.tag == "Player2")
        {
            canDrop = false;

            player2.CloseVegButtons();
            Player2ChopButton.SetActive(false);
            player2 = null;
            Debug.Log("player2 left zone " + gameObject.name);
        }
    }

    public void Chop()
    {
        if (player1.vegetablesCarryingArray != null)
        {
            if (player1.vegetablesCarryingArray[0] != null)                                                        //checking if player's first hand is empty or not
            {
                player1.isChopping = true;
                ChoppingVeg = player1.vegetablesCarryingArray[0];
                combination = combination + ChoppingVeg.name;                                                        //assigning name to newly made salad
                combinationOrderID = combinationOrderID + player1.orderID[0];                             //calculationg combination id
                Destroy(player1.vegetablesCarryingArray[0]);
                StartCoroutine(Countdown(5));
                player1.vegetablesCarryingArray[0] = null;
            }
            else if (player1.vegetablesCarryingArray[1] != null)                                         //checking if player's second hand is empty or not
            {
                player1.isChopping = true;
                ChoppingVeg = player1.vegetablesCarryingArray[1];
                combination = combination + ChoppingVeg.name;                                         //assigning name to newly made salad
                combinationOrderID = combinationOrderID + player1.orderID[1];             //calculationg combination id
                Destroy(player1.vegetablesCarryingArray[1]);
                StartCoroutine(Countdown(5));
                player1.vegetablesCarryingArray[1] = null;
            }
        }          
            else
                return;
    }


    //chopping countdown
    public IEnumerator Countdown(float countdownValue)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            timerImage.fillAmount = currCountdownValue / countdownValue;
            currCountdownValue--;
        }
        timerImage.fillAmount = 1;
        Debug.Log(combination);
        Debug.Log(combinationOrderID);
        Destroy(spawnedBowl);
       spawnedBowl = Instantiate(saladBowl, spawnPos.transform.position, spawnPos.transform.rotation);                    //instantiating new salad bowl on chopping completion
        spawnedBowl.name = combination;
        choppingDone = true;
        player1.isChopping = false;

        //displaying all options to player once chopping is done
        Player1ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Space";
        Player1ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup Bowl";
        Player1ChopButton.SetActive(true);

        Player1ChopAgainButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LAlt";
        Player1ChopAgainButton.transform.GetChild(1).GetComponent<Text>().text = "Chop Again";
        Player1ChopAgainButton.SetActive(true);

        Player1TrashButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LShift";
        Player1TrashButton.transform.GetChild(1).GetComponent<Text>().text = "Trash";
        Player1TrashButton.SetActive(true);

        Player1ServeButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Esc";
        Player1ServeButton.transform.GetChild(1).GetComponent<Text>().text = "Serve";
        Player1ServeButton.SetActive(true);

        if(sidePlateVeg == null)
        {
            Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCntrl";
            Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Drop at side plate";
            Player1SidePlateButton.SetActive(true);
        }
        else
        {
            Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCntrl";
            Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "pickup from side plate";
            Player1SidePlateButton.SetActive(true);
        }    
    }

    //function to pickup salad bowl
    public void PickUpSaladBowl()
    {
        player1.PickUpSaladBowl(spawnedBowl);
        //player1.saladBowl = spawnedBowl;
        player1.saladCombination = combination;
        player1.saladCombinationID = combinationOrderID;
        player1.canServe = true;
        saladBowl = null;
        combination = null;
    }

    // dropping at side plate function
    public void DropAtSidePlate()
    {
        if (sidePlateVeg == null)
        {
            if (player1.vegetablesCarryingArray[0] != null)
            {
                //sidePlateVeg = player1.vegetablesCarryingArray[0];
                //sidePlateVeg.name = player1.vegetablesCarryingArray[0].name;

                for ( int i=0; i< player1.vegetablePrefabs.Length; i++)
                {
                    if(player1.vegetablePrefabs[i].name == player1.vegetablesCarryingArray[0].name)
                    {
                        sidePlateVeg = Instantiate(player1.vegetablePrefabs[i], sidePlatePos.transform.position, sidePlatePos.transform.rotation);
                        sidePlateVeg.name = player1.vegetablePrefabs[i].name;
                        Destroy(player1.vegetablesCarryingArray[0]);
                    }
                }
                canPickupFromSideTable = true;
                player1.vegetablesCarryingArray[0] = null;
                sidePlateorderID = player1.orderID[0];
                player1.orderID[0] = 0;
                Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup from Side Plate";
                Player1SidePlateButton.SetActive(true);
                Debug.Log("sidePlateVeg plate veg: " + sidePlateVeg);
            }
            else if (player1.vegetablesCarryingArray[1] != null)
            {
                //sidePlateVeg = player1.vegetablesCarryingArray[1];
                //sidePlateVeg.name = player1.vegetablesCarryingArray[1].name;

                for (int i = 0; i < player1.vegetablePrefabs.Length; i++)
                {
                    if (player1.vegetablePrefabs[i].name == player1.vegetablesCarryingArray[1].name)
                    {
                        sidePlateVeg = Instantiate(player1.vegetablePrefabs[i], sidePlatePos.transform.position, sidePlatePos.transform.rotation);
                        sidePlateVeg.name = player1.vegetablePrefabs[i].name;
                        Destroy(player1.vegetablesCarryingArray[1]);
                    }
                }
                canPickupFromSideTable = true;
                player1.vegetablesCarryingArray[1] = null;
                sidePlateorderID = player1.orderID[1];
                player1.orderID[1] = 0;
                Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup from Side Plate";
                Player1SidePlateButton.SetActive(true);
                Debug.Log("sidePlateVeg plate veg: " + sidePlateVeg);
            }
        }
        else
            return;
    }

    //function to pickup vegetable from side plate
    public void PickUpFromSidePlate()
    {
        if(sidePlateVeg != null)
        {
            Debug.Log(sidePlateVeg.name);
            if (player1.vegetablesCarryingArray[0] == null)
            {
                 for (int i = 0; i < player1.vegetablePrefabs.Length; i++)
                {
                    if (player1.vegetablePrefabs[i].name == sidePlateVeg.name)
                    {
                       GameObject obj = Instantiate(player1.vegetablePrefabs[i], player1.bowlHoldingPos.transform.position, player1.bowlHoldingPos.transform.rotation);
                        obj.transform.SetParent(player1.transform);
                        obj.name = player1.vegetablePrefabs[i].name;
                        player1.vegetablesCarryingArray[0] = obj;
                        player1.orderID[0] = sidePlateorderID;
                        sidePlateorderID = 0;
                    }
                }
                canPickupFromSideTable = false;
                Destroy(sidePlateVeg);
                Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Drop at Side Plate";
                Player1SidePlateButton.SetActive(true);
                Debug.Log("picked up side plate veg: " + sidePlateVeg.name);
            }
            else if (player1.vegetablesCarryingArray[1] == null)
            {
                for (int i = 0; i < player1.vegetablePrefabs.Length; i++)
                {
                    if (player1.vegetablePrefabs[i].name == sidePlateVeg.name)
                    {
                        GameObject obj = Instantiate(player1.vegetablePrefabs[i], player1.bowlHoldingPos.transform.position, player1.bowlHoldingPos.transform.rotation);
                        obj.transform.SetParent(player1.transform);
                        obj.name = player1.vegetablePrefabs[i].name;
                        player1.vegetablesCarryingArray[1] = obj;
                        player1.orderID[1] = sidePlateorderID;
                        sidePlateorderID = 0;
                    }
                }
                canPickupFromSideTable = false;
                Destroy(sidePlateVeg);
                Player1SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                Player1SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Drop at Side Plate";
                Player1SidePlateButton.SetActive(true);
                Debug.Log("picked up side plate veg: " + sidePlateVeg.name);
            }
        }
    }

    //function to trash salad bowl
    public void Trash()
    {
        //destination = VegetableStops[11].transform;
        //Vector3 target = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        //agent.SetDestination(target);
        Destroy(spawnedBowl);
        player1.canServe = false;
    }




    //Player2 Functions

    public void Player2Chop()
    {
        if (player2.vegetablesCarryingArray != null)
        {
            if (player2.vegetablesCarryingArray[0] != null)
            {
                player2.isChopping = true;
                ChoppingVeg = player2.vegetablesCarryingArray[0];
                combination = combination + ChoppingVeg.name;
                combinationOrderID = combinationOrderID + player2.orderID[0];
                Destroy(player2.vegetablesCarryingArray[0]);
                StartCoroutine(Player2Countdown(5));
                player2.vegetablesCarryingArray[0] = null;
            }
            else if (player2.vegetablesCarryingArray[1] != null)
            {
                player2.isChopping = true;
                ChoppingVeg = player2.vegetablesCarryingArray[1];
                combination = combination + ChoppingVeg.name;
                combinationOrderID = combinationOrderID + player2.orderID[1];
                Destroy(player2.vegetablesCarryingArray[1]);
                StartCoroutine(Player2Countdown(5));
                player2.vegetablesCarryingArray[1] = null;
            }
        }
        else
            return;
        //combine vegetable if any previous chopped vegetables present
    }


    public IEnumerator Player2Countdown(float countdownValue)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            timerImage.fillAmount = currCountdownValue / countdownValue;
            currCountdownValue--;
        }
        timerImage.fillAmount = 1;
        Debug.Log(combination);
        Debug.Log(combinationOrderID);
        Destroy(spawnedBowl);
        spawnedBowl = Instantiate(saladBowl, spawnPos.transform.position, spawnPos.transform.rotation);
        spawnedBowl.name = combination;
        choppingDone = true;
        player2.isChopping = false;

        Player2ChopButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Enter";
        Player2ChopButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup Bowl";
        Player2ChopButton.SetActive(true);

        Player2ChopAgainButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "RAlt";
        Player2ChopAgainButton.transform.GetChild(1).GetComponent<Text>().text = "Chop Again";
        Player2ChopAgainButton.SetActive(true);

        Player2TrashButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "RShift";
        Player2TrashButton.transform.GetChild(1).GetComponent<Text>().text = "Trash";
        Player2TrashButton.SetActive(true);

        Player2ServeButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Backspace";
        Player2ServeButton.transform.GetChild(1).GetComponent<Text>().text = "Serve";
        Player2ServeButton.SetActive(true);

        if (sidePlateVeg == null)
        {
            Player2SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "RCntrl";
            Player2SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Drop at side plate";
            Player2SidePlateButton.SetActive(true);
        }
        else
        {
            Player2SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "RCntrl";
            Player2SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "pickup from side plate";
            Player2SidePlateButton.SetActive(true);
        }
    }

    public void Player2PickUpSaladBowl()
    {
        player2.PickUpSaladBowl(spawnedBowl);
        //player1.saladBowl = spawnedBowl;
        player2.saladCombination = combination;
        player2.saladCombinationID = combinationOrderID;
        player2.canServe = true;
        saladBowl = null;
        combination = null;
    }


    public void Player2DropAtSidePlate()
    {
        if (sidePlateVeg == null)
        {
            if (player2.vegetablesCarryingArray[0] != null)
            {
                //sidePlateVeg = player1.vegetablesCarryingArray[0];
                //sidePlateVeg.name = player1.vegetablesCarryingArray[0].name;

                for (int i = 0; i < player2.vegetablePrefabs.Length; i++)
                {
                    if (player2.vegetablePrefabs[i].name == player2.vegetablesCarryingArray[0].name)
                    {
                        sidePlateVeg = Instantiate(player2.vegetablePrefabs[i], sidePlatePos.transform.position, sidePlatePos.transform.rotation);
                        sidePlateVeg.name = player2.vegetablePrefabs[i].name;
                        Destroy(player2.vegetablesCarryingArray[0]);
                    }
                }
                canPickupFromSideTable = true;
                player2.vegetablesCarryingArray[0] = null;
                sidePlateorderID = player2.orderID[0];
                player2.orderID[0] = 0;
                Player2SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "RCtrl";
                Player2SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup from Side Plate";
                Player2SidePlateButton.SetActive(true);
                Debug.Log("sidePlateVeg plate veg: " + sidePlateVeg);
            }
            else if (player2.vegetablesCarryingArray[1] != null)
            {
                //sidePlateVeg = player1.vegetablesCarryingArray[1];
                //sidePlateVeg.name = player1.vegetablesCarryingArray[1].name;

                for (int i = 0; i < player2.vegetablePrefabs.Length; i++)
                {
                    if (player2.vegetablePrefabs[i].name == player2.vegetablesCarryingArray[1].name)
                    {
                        sidePlateVeg = Instantiate(player2.vegetablePrefabs[i], sidePlatePos.transform.position, sidePlatePos.transform.rotation);
                        sidePlateVeg.name = player2.vegetablePrefabs[i].name;
                        Destroy(player2.vegetablesCarryingArray[1]);
                    }
                }
                canPickupFromSideTable = true;
                player2.vegetablesCarryingArray[1] = null;
                sidePlateorderID = player1.orderID[1];
                player2.orderID[1] = 0;
                Player2SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "RCtrl";
                Player2SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Pickup from Side Plate";
                Player2SidePlateButton.SetActive(true);
                Debug.Log("sidePlateVeg plate veg: " + sidePlateVeg);
            }
        }
        else
            return;
    }

    public void Player2PickUpFromSidePlate()
    {
        if (sidePlateVeg != null)
        {
            Debug.Log(sidePlateVeg.name);
            if (player2.vegetablesCarryingArray[0] == null)
            {
                for (int i = 0; i < player2.vegetablePrefabs.Length; i++)
                {
                    if (player2.vegetablePrefabs[i].name == sidePlateVeg.name)
                    {
                        GameObject obj = Instantiate(player2.vegetablePrefabs[i], player2.bowlHoldingPos.transform.position, player2.bowlHoldingPos.transform.rotation);
                        obj.transform.SetParent(player2.transform);
                        obj.name = player2.vegetablePrefabs[i].name;
                        player2.vegetablesCarryingArray[0] = obj;
                        player2.orderID[0] = sidePlateorderID;
                        sidePlateorderID = 0;
                    }
                }
                canPickupFromSideTable = false;
                Destroy(sidePlateVeg);
                Player2SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "LCtrl";
                Player2SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Drop at Side Plate";
                Player2SidePlateButton.SetActive(true);
                Debug.Log("picked up side plate veg: " + sidePlateVeg.name);
            }
            else if (player2.vegetablesCarryingArray[1] == null)
            {
                for (int i = 0; i < player2.vegetablePrefabs.Length; i++)
                {
                    if (player2.vegetablePrefabs[i].name == sidePlateVeg.name)
                    {
                        GameObject obj = Instantiate(player2.vegetablePrefabs[i], player2.bowlHoldingPos.transform.position, player2.bowlHoldingPos.transform.rotation);
                        obj.transform.SetParent(player2.transform);
                        obj.name = player2.vegetablePrefabs[i].name;
                        player2.vegetablesCarryingArray[1] = obj;
                        player2.orderID[1] = sidePlateorderID;
                        sidePlateorderID = 0;
                    }
                }
                canPickupFromSideTable = false;
                Destroy(sidePlateVeg);
                Player2SidePlateButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "RCtrl";
                Player2SidePlateButton.transform.GetChild(1).GetComponent<Text>().text = "Drop at Side Plate";
                Player2SidePlateButton.SetActive(true);
                Debug.Log("picked up side plate veg: " + sidePlateVeg.name);
            }
        }
    }

    public void Player2Trash()
    {
        //destination = VegetableStops[11].transform;
        //Vector3 target = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        //agent.SetDestination(target);
        Destroy(spawnedBowl);
        player2.canServe = false;
    }

}
