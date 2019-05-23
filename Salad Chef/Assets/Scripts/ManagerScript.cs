using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{


    //variables 
    public PlayerController player1;
    public Player2Controller player2;
    public Text Player1ScoreText;             //displaying player score
    public Text Player2ScoreText;             //displaying player score
    public Text Player1TimeLeftText;
    public Text Player2TimeLeftText;
    public Image Player1TimeLeftImage;               //displaying player time left
    public Image Player2TimeLeftImage;               //displaying player time left
    public float player1CurrCountdownValue;       //variable for current player time left
    public float player2CurrCountdownValue;        //variable for current player time left
    public int Player1TimeLeft;                                 //variable for current player time left
    public int Player2TimeLeft;                                  //variable for current player time left
    public NavMeshAgent agent1;                            //navMesh agent  in player
    public NavMeshAgent agent2;                            //navMesh agent  in player 
    public CustomerScript[] customers;                   //total customers
    public bool isGameOn;                                        //checking whether game is over or not
    int customerLeft;                                                 //No. of customers left  

    private int[] highscore = new int[10];              //storing the latest 10 highscore
    int[] oldHighScore = new int[10];                      //storing the 10 highscore from last round
    public Text[] highscoreTexts;                             //texts to show highscore
    public GameObject highscorePanel;               // highscore gameobject

    


    // Start is called before the first frame update
    void Start()
    {
        GetOldHighScore();
    }

    // Update is called once per frame
    void Update()
    {

        //if game is not on, then start it
        if(Input.GetKeyDown(KeyCode.Space) && !isGameOn)
        {
            isGameOn = true;
            player1.ResetVegetableButtons();
            player2.ResetVegetableButtons();
            player2.Player1ActionButton.SetActive(false);
            player1.Player1ActionButton.SetActive(false);
            StartCoroutine(Player1Countdown(Player1TimeLeft));
            StartCoroutine(Player2Countdown(Player2TimeLeft));

            for (int i = 0; i < customers.Length; i++)
            {
                customers[i].WaitForFood();
            }
        }

        //else if game is over, restart it
        else if(Input.GetKeyDown(KeyCode.Escape) && !isGameOn)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        //check to see how many customers left
        if(customers[0].istimeLeft == false && customers[1].istimeLeft == false && customers[2].istimeLeft == false && customers[3].istimeLeft ==false && customers[4].istimeLeft ==false )
        {
            player1.CloseVegButtons();
            player1.Player1ActionButton.SetActive(false);
            player2.CloseVegButtons();
            player2.Player1ActionButton.SetActive(false);
            player1.Player1ActionButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Esc";
            player1.Player1ActionButton.transform.GetChild(1).GetComponent<Text>().text = "Restart";
            player1.Player1ActionButton.SetActive(true);
            isGameOn = false;
            DisplayHighScore();
            highscorePanel.SetActive(true);

        }

    }

    //countdown timer for Player1
    public IEnumerator Player1Countdown(float countdownValue)
    {
        player1CurrCountdownValue = countdownValue;
        while (player1CurrCountdownValue > 0)
        {
            (Player1TimeLeftImage.fillAmount) = (player1CurrCountdownValue / countdownValue);
           // Debug.Log(Player1TimeLeftImage.fillAmount);
            yield return new WaitForSeconds(1);
            player1CurrCountdownValue--;
            
            if(player1CurrCountdownValue == 0)
            {
                agent1.speed = 0;
                Player1TimeLeftText.text = "Time's Up!";

            }
        }
      
    }

    //countdown timer for Player2
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

    //highscore display function
    public void DisplayHighScore()
    {
        if (Convert.ToInt32(Player1ScoreText.text) > Convert.ToInt32(Player2ScoreText.text))
        {
            CalculateHighScore(Convert.ToInt32(Player2ScoreText.text));
            CalculateHighScore(Convert.ToInt32(Player1ScoreText.text));
        }
 else
        {
            CalculateHighScore(Convert.ToInt32(Player1ScoreText.text));
            CalculateHighScore(Convert.ToInt32(Player2ScoreText.text));
        }

    }

    //highscore calculation for top 10
    public void CalculateHighScore(int score)
    {

        for (int i = 0; i < 10; i++)
        {
            if (score <= oldHighScore[i])
            {
                highscore[i] = oldHighScore[i];
            }

            else if (score > oldHighScore[i]) //((Convert.ToInt32(Player1ScoreText.text) > oldHighScore[i]) || Convert.ToInt32(Player2ScoreText.text) > oldHighScore[i])
            {
                highscore[i] = score;
            }
        }

        for (int j = 1; j < 10; j++)
        {
            if (score == highscore[j])
            {
                highscore[j] = oldHighScore[j - 1];
            }
            else if (score == highscore[0])
            {
                highscore[j] = oldHighScore[j - 1];
            }
        }

        for (int a = 1; a < 10; a++)
        {
            if (a + 1 >= 10)
            {
                if (highscore[a - 1] == oldHighScore[a - 1] && highscore[a] > score)
                    highscore[9] = score;
            }
            else if (a - 1 >= 0 && a + 1 < 10)
            {
                if (highscore[a - 1] == oldHighScore[a - 1] && highscore[a] > score && highscore[a + 1] < score)
                    highscore[a] = score;
            }

        }


        for (int k = 0; k < 10; k++)
        {
            highscoreTexts[k].text = highscore[k].ToString();
        }

        SetNewHighScore();
    }

    //get 10 highscore from previous rounds 
    public void GetOldHighScore()
    {
        oldHighScore[0] = PlayerPrefs.GetInt(" HighScore0", 0);
        oldHighScore[1] = PlayerPrefs.GetInt(" HighScore1", 0);
        oldHighScore[2] = PlayerPrefs.GetInt(" HighScore2", 0);
        oldHighScore[3] = PlayerPrefs.GetInt(" HighScore3", 0);
        oldHighScore[4] = PlayerPrefs.GetInt(" HighScore4", 0);
        oldHighScore[5] = PlayerPrefs.GetInt(" HighScore5", 0);
        oldHighScore[6] = PlayerPrefs.GetInt(" HighScore6", 0);
        oldHighScore[7] = PlayerPrefs.GetInt(" HighScore7", 0);
        oldHighScore[8] = PlayerPrefs.GetInt(" HighScore8", 0);
        oldHighScore[9] = PlayerPrefs.GetInt(" HighScore9", 0);
    }

    //set new highscore, if any, after latest round
    public void SetNewHighScore()
    {
        PlayerPrefs.SetInt(" HighScore0", highscore[0]);
        PlayerPrefs.GetInt(" HighScore1", highscore[1]);
         PlayerPrefs.GetInt(" HighScore2", highscore[2]);
        PlayerPrefs.GetInt(" HighScore3", highscore[3]);
        PlayerPrefs.GetInt(" HighScore4", highscore[4]);
        PlayerPrefs.GetInt(" HighScore5", highscore[5]);
        PlayerPrefs.GetInt(" HighScore6", highscore[6]);
        PlayerPrefs.GetInt(" HighScore7", highscore[7]);
        PlayerPrefs.GetInt(" HighScore8", highscore[8]);
        PlayerPrefs.GetInt(" HighScore9", highscore[9]);
    }

}
