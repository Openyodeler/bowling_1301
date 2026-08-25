using TMPro;
using UnityEngine;


public class ThreeRoundScore : MonoBehaviour
{
    [SerializeField] private TMP_Text First;
    [SerializeField] private TMP_Text Second;
    [SerializeField] private TMP_Text Three;
    [SerializeField] private TMP_Text Total;

    [SerializeField] private int hitted = 0;

    public int bonus = 0;
    public int bonusBowl = 0;

    public int score = 0;

    public void hit(int round, int hit)
    {
        if (hit == 10)
        {
            switch (round)
            {
                case 1:
                    First.text = "X";
                    bonus = 2;
                    break;

                case 2:
                    Second.text = "X";
                    break;

                case 3:
                    Three.text = "X";
                    break;
            }

            return;
        }

        switch (round)
        {
            case 1:
                First.text = $"{hit}";
                hitted += hit;
                break;

            case 2:
                Second.text = $"{hit}";
                hitted += hit;
                break;

            case 3:
                Three.text = $"{hit}";
                break;
        }

        // Spare after the second bowl
        if (round == 2 && hitted == 10)
        {
            Second.text = "/";
            bonus = 1;
        }
    }

    public void total(int i)
    {
        Total.text = $"{i}";
    }

    public void AddBonus()
    {
        if (bonusBowl > 0)
            bonusBowl--;
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public int GetScore()
    {
        return score;
    }

    public bool IsSpare()
    {
        return bonus == 1;
    }

    public bool IsStrike()
    {
        return bonus == 2;
    }
}