using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScoreScr : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    private int[] teamScore = new int[6];
    private int team;
    public static CanvasScoreScr instance;
    // Start is called before the first frame update
    void Start()
    {
        teamScore[0] = 0;
        teamScore[1] = 0;
        teamScore[2] = 0;
        teamScore[3] = 0;
        teamScore[4] = 0;
        teamScore[5] = 0;
        scoreText.text = "Team 1 Score: " + teamScore[0] + "\nTeam 2 Score: " + teamScore[1] + "\nTeam 3 Score: " + teamScore[2] + "\nTeam 4 Score: " + teamScore[3] + "\nTeam 5 Score: " + teamScore[4] + "\nTeam 6 Score: " + teamScore[5];
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GivePoints(int team)
    {
        teamScore[team]++;
        Debug.Log("Score:" + teamScore);
        scoreText.text = "Team 1 Score: " + teamScore[0] + "\nTeam 2 Score: " + teamScore[1] + "\nTeam 3 Score: " + teamScore[2] + "\nTeam 4 Score: " + teamScore[3] + "\nTeam 5 Score: " + teamScore[4] + "\nTeam 6 Score: " + teamScore[5];

    }
}
