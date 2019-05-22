using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ManagerScript : MonoBehaviour
{
    public PlayerController player1;
    public Text Player1ScoreText;
    public Text Player2ScoreText;
    public Text Player1TimeLeftText;
    public Text Player2TimeLeftText;
    public Image Player1TimeLeftImage;
    public Image Player2TimeLeftImage;
    public float player1CurrCountdownValue;
    public float player2CurrCountdownValue;
    public int Player1TimeLeft;
    public int Player2TimeLeft;
    public NavMeshAgent agent1;
    public NavMeshAgent agent2;
    public CustomerScript[] customers;
    public bool isGameOn;
    int customerLeft;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isGameOn)
        {
            isGameOn = true;
            StartCoroutine(Player1Countdown(Player1TimeLeft));
            StartCoroutine(Player2Countdown(Player2TimeLeft));

            for (int i = 0; i < customers.Length; i++)
            {
                customers[i].WaitForFood();
            }
        }


        //for (int i = 0; i < customers.Length; i++)
        //{
        //   if( customers[i].istimeLeft)
        //    {
        //        customerLeft++;
        //        if(customerLeft >= 5)
        //        {
        //            player1.CloseVegButtons();
        //            player1.Player1ActionButton.SetActive(false);
        //            isGameOn = false;
        //            //show highscore
        //            //option to restart
        //        }
        //    }
           
        //}

    }


    public IEnumerator Player1Countdown(float countdownValue)
    {
        player1CurrCountdownValue = countdownValue;
        while (player1CurrCountdownValue > 0)
        {
            (Player1TimeLeftImage.fillAmount) = (player1CurrCountdownValue / countdownValue);
            Debug.Log(Player1TimeLeftImage.fillAmount);
            yield return new WaitForSeconds(1);
            player1CurrCountdownValue--;
            
            if(player1CurrCountdownValue == 0)
            {
                agent1.speed = 0;
                Player1TimeLeftText.text = "Time's Up!";

            }
        }
      
    }


    public IEnumerator Player2Countdown(float countdownValue)
    {
        player2CurrCountdownValue = countdownValue;
        while (player2CurrCountdownValue > 0)
        {
            Player2TimeLeftImage.fillAmount = player2CurrCountdownValue / countdownValue;
            //Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1f);
            player2CurrCountdownValue--;
            
            if (player2CurrCountdownValue == 0)
            {
                agent2.speed = 0;
                Player2TimeLeftText.text = "Time's Up!";
            }
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1")
        {

        }
    }

}
