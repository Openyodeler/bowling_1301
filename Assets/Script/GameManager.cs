using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pinLocation;
    [SerializeField] private Pin[] Pins;
    public static GameManager instance;
    [SerializeField] private Bowling ball;

    [SerializeField] private int Round = 1;
    [SerializeField] private int bowl = 1;
    [SerializeField] private int Roundscore;
    [SerializeField] private int hit;
    [SerializeField] private int TotalScore;

    [SerializeField] private TwoRoundScore[] twoRound;

    [SerializeField] private ThreeRoundScore threeRound;

    [SerializeField] private TMP_Text total;
    private int bonus;
    public bool ended = false;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SettingPin();
    }
    private void Update()
    {   
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            returnball();
            SettingPin();
        }

        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            returnball();
            StandingPin();
        }
    }

    private void SettingPin()
    {
        int i = 0;

        foreach (var pin in Pins)
        {
            pin.StopPin();
            pin.hasFallen = false;
            pin.transform.localPosition = pinLocation[i].transform.localPosition;
            pin.transform.localRotation = Quaternion.identity;

            pin.gameObject.SetActive(true);

            Rigidbody rb = pin.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.Sleep();

            i++;
        }
    }

    private void StandingPin()
    {
        int i = 0;

        foreach (var pin in Pins)
        {
            pin.StopPin();
            
            if (pin.hasFallen != true)
            {
                pin.transform.localPosition = pinLocation[i].transform.localPosition;
                pin.transform.localRotation = Quaternion.identity;

                pin.gameObject.SetActive(true);

                Rigidbody rb = pin.GetComponent<Rigidbody>();
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                rb.Sleep();
            }
            else
            {
                pin.gameObject.SetActive (false);
            }
            i++;
        }
    }

    private void returnball()
    {   
        ball.StopBall();
        ball.transform.position = new Vector3(0, 1.5f, -10);
        ball.gameObject.SetActive(true);
    }

    public void EndRound()
    {
        displayscore();
        returnball();

        // =========================
        // ROUND 10
        // =========================
        if (Round == 10)
        {
            EndLastRound();
            return;
        }

        // =========================
        // ROUND 1 - 9
        // =========================

        // Add current bowl to previous rounds
        // that still have bonus bowls
        for (int i = 0; i < Round - 1; i++)
        {
            if (twoRound[i].bonusBowl > 0)
            {
                twoRound[i].AddScore(Roundscore);
                twoRound[i].AddBonus();

                twoRound[i].total(twoRound[i].GetScore());
            }
        }

        // Add current bowl to current round
        twoRound[Round - 1].AddScore(Roundscore);
        twoRound[Round - 1].total(twoRound[Round - 1].GetScore());

        // Strike
        if (twoRound[Round - 1].bonus == 2)
        {
            twoRound[Round - 1].bonusBowl = 2;

            Roundscore = 0;
            bowl = 1;
            Round++;

            SettingPin();
            return;
        }

        bowl++;

        // End of normal frame / spare
        if (bowl > 2)
        {
            if (twoRound[Round - 1].bonus == 1)
            {
                twoRound[Round - 1].bonusBowl = 1;
            }

            Roundscore = 0;
            bowl = 1;
            Round++;

            SettingPin();
            return;
        }

        Roundscore = 0;

        if (Pins.Any(pin => pin.hasFallen))
        {
            StandingPin();
        }
    }


    public void addpoint(int i)
    {
        Roundscore += i;
        hit += 1;
    }

    public void displayscore()
    {
        if (Round <= 9)
        {
            twoRound[Round - 1].hit(bowl, Roundscore);
        }
        else if (Round == 10)
        {
            threeRound.hit(bowl, Roundscore);
        }
    }

    private void EndLastRound()
    {
        // Current 10th-frame bowl
        int currentHit = Roundscore;

        // Add the current bowl as bonus to previous rounds
        for (int i = 0; i < twoRound.Length; i++)
        {
            if (twoRound[i].bonusBowl > 0)
            {
                twoRound[i].AddScore(currentHit);
                twoRound[i].AddBonus();

                twoRound[i].total(twoRound[i].GetScore());
            }
        }

        // Add current bowl to 10th frame
        threeRound.AddScore(currentHit);
        threeRound.total(threeRound.GetScore());

        // First bowl
        if (bowl == 1)
        {
            bowl = 2;
            Roundscore = 0;

            if (Pins.Any(pin => !pin.hasFallen))
            {
                StandingPin();
            }
            else
            {
                SettingPin();
            }

            return;
        }

        // Second bowl
        if (bowl == 2)
        {
            // Strike or spare -> third bowl
            if (threeRound.bonus == 2 || threeRound.bonus == 1)
            {
                bowl = 3;
                Roundscore = 0;

                if (Pins.Any(pin => !pin.hasFallen))
                {
                    StandingPin();
                }
                else
                {
                    SettingPin();
                }

                return;
            }

            // No strike or spare -> game over
            FinishGame();
            return;
        }

        // Third bowl -> game over
        if (bowl == 3)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        ended = true;
        ShowallScore();
        return;
    }

    private void ShowallScore()
    {
        for (int i = 0; i < 10; i++) 
        {   
            if (i == 9)
            {
                TotalScore += threeRound.GetScore();
                threeRound.total(TotalScore);
                break;
            }
            TotalScore += twoRound[i].score;
            twoRound[i].total(TotalScore);
        }
        total.text = ($"{ TotalScore} ");
    }
}
