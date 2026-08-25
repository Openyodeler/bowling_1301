using UnityEngine;
using TMPro;
using System.Xml.Schema;

public class TwoRoundScore : MonoBehaviour
{   
    [SerializeField] private TMP_Text First;
    [SerializeField] private TMP_Text Second;

    [SerializeField] private TMP_Text Total;
    [SerializeField] private int hitted = 0;

    public int bonus = 0;
    public int bonusBowl;

    public int score;

    public void hit(int round, int hit)
    {
        if (hit >= 10)
        {
            Stike();
            return;
        }

        switch (round)
        {
            case 1:
                First.text = ($"{hit}");
                hitted += hit;
                break;
            case 2:
                Second.text = ($"{hit}");
                hitted += hit;
                break;
        }
        if (hitted == 10)
            Spare();
    }

    public void Spare()
    {
        Second.text = ($"/");
        bonus = 1;
        bonusBowl = 1;
    }

    public void Stike()
    {
        Second.text = ($"X");
        bonus = 2;
        bonusBowl = 2;
    }

    public void total(int i)
    {
        Total.text = $"{i}";
    }

    public void AddBonus()
    {
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
}
