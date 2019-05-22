using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CustomerScript : MonoBehaviour
{
    public bool istimeLeft = true;
    public int CustomerID;
    public string CustomerOrder;
    public int customerOrderID;
    private bool canPlaceBowl;
    public GameObject serveButton;
    public GameObject player2ServeButton;
    private PlayerController player1;
    private Player2Controller player2;
    private float currCountdownValue;
    public Text remarkText;
    public Text orderText;
    public GameObject customer;
    public Image timeBar;
    public int WaitingTime;
    public ManagerScript manager;
    private float timePast;
    Coroutine co;
    public GameObject[] bonus;
    public Transform[] bonusSpawnPos;
    int randomBonus;
    int randomArea;
    bool bonusEarned;


    // Start is called before the first frame update
    void Start()
    {
        orderText.text = CustomerOrder;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && canPlaceBowl)
        {
            player1.ServeCustomer(CustomerID);
            player1.canServe = false;
            canPlaceBowl = false;
            float n = (timePast / WaitingTime) * 100;

            if (customerOrderID == player1.saladCombinationID && n >= 30.0f)
            {
                remarkText.text = "Excellent!";
                bonusEarned = true;
                SpawnRandomBonus();
                serveButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "`";
                serveButton.transform.GetChild(1).GetComponent<Text>().text = "Take Bonus";
                serveButton.SetActive(true);
            }
            else if (customerOrderID == player1.saladCombinationID)
            {
                remarkText.text = "Good";
                player1.player1Points++;
                manager.Player1ScoreText.text = "Player1 Score: " + player1.player1Points.ToString();
                gameObject.SetActive(false);
                player1.CloseVegButtons();
                player1.ResetVegetableButtons();
                player1.Player1ActionButton.SetActive(false);
            }

            else
            {
                remarkText.text = "I am angry!!";
                player1.player1Points--;
                manager.Player1ScoreText.text = "Player1 Score: " + player1.player1Points.ToString();
                StopCoroutine(co);
                timeBar.color = Color.red;
                StartCoroutine(WaitingCountdown(timePast, 0.5f));
                player1.CloseVegButtons();
                player1.ResetVegetableButtons();
                player1.Player1ActionButton.SetActive(false);
                //reduce time
            }
        }

        else if (Input.GetKeyDown(KeyCode.BackQuote) && bonusEarned)
        {
            Vector3 target = new Vector3(bonusSpawnPos[randomArea].position.x, bonusSpawnPos[randomArea].position.y, bonusSpawnPos[randomArea].position.z);
            player1.GetComponent<NavMeshAgent>().SetDestination(target);
            bonusEarned = false;
        }

        //Player2 Actions
        else if (Input.GetKeyDown(KeyCode.Backslash) && canPlaceBowl)
        {
            player2.ServeCustomer(CustomerID);
            player2.canServe = false;
            canPlaceBowl = false;
            float n = (timePast / WaitingTime) * 100;

            if (customerOrderID == player2.saladCombinationID && n >= 30.0f)
            {
                remarkText.text = "Excellent!";
                bonusEarned = true;
                SpawnRandomBonus();
                serveButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "=";
                serveButton.transform.GetChild(1).GetComponent<Text>().text = "Take Bonus";
                serveButton.SetActive(true);
            }
            else if (customerOrderID == player2.saladCombinationID)
            {
                remarkText.text = "Good";
                player2.player1Points++;
                manager.Player2ScoreText.text =  player2.player1Points.ToString() + " :Player2 Score";
                gameObject.SetActive(false);
                player2.CloseVegButtons();
                player2.ResetVegetableButtons();
                player2.Player1ActionButton.SetActive(false);
            }

            else
            {
                remarkText.text = "I am angry!!";
                player2.player1Points--;
                manager.Player2ScoreText.text = player2.player1Points.ToString() + " :Player2 Score";
                StopCoroutine(co);
                timeBar.color = Color.red;
                StartCoroutine(WaitingCountdown(timePast, 0.5f));
                player2.CloseVegButtons();
                player2.ResetVegetableButtons();
                player2.Player1ActionButton.SetActive(false);
                //reduce time
            }

        }

        else if (Input.GetKeyDown(KeyCode.Equals) && bonusEarned)
        {
            Vector3 target = new Vector3(bonusSpawnPos[randomArea].position.x, bonusSpawnPos[randomArea].position.y, bonusSpawnPos[randomArea].position.z);
            player2.GetComponent<NavMeshAgent>().SetDestination(target);
            bonusEarned = false;
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

        //Player2 Zone
       else if (other.tag == "Player2")
        {
            canPlaceBowl = true;
            player2 = other.GetComponent<Player2Controller>();
            if (player2.destination == transform)
            {
                player2ServeButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Backslash" ;
                player2ServeButton.transform.GetChild(1).GetComponent<Text>().text = "Serve";
                player2ServeButton.SetActive(true);
                Debug.Log("player2 entered zone " + gameObject.name);
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
        else if (other.tag == "Player2")
        {
            canPlaceBowl = false;
            player2.CloseVegButtons();
            player2.Player1ActionButton.SetActive(false);
            player2 = null;
            Debug.Log("player2 left zone " + gameObject.name);
        }
    }

    public void WaitForFood()
    {
        co = StartCoroutine(WaitingCountdown(WaitingTime, 1));
    }


    public IEnumerator WaitingCountdown(float countdownValue, float timeRate)
    {
        orderText.text = CustomerOrder;
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            //Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(timeRate);
            currCountdownValue--;
            timePast = currCountdownValue;
            timeBar.fillAmount = (currCountdownValue / countdownValue);

            if (currCountdownValue == 0)
            {
                remarkText.text = "I am leaving!";
                yield return new WaitForSeconds(1.5f);
                istimeLeft = false;
                customer.SetActive(false);
                gameObject.SetActive(false);
            }
        }

    }

    public void SpawnRandomBonus()
    {
        randomBonus = Random.Range(0, 3);
        randomArea = Random.Range(0, bonusSpawnPos.Length);

        Instantiate(bonus[randomBonus], bonusSpawnPos[randomArea].position, bonusSpawnPos[randomArea].rotation);
    }


}
