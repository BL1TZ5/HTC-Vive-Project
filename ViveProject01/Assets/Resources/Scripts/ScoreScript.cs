using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour
{

    public UnityEngine.UI.Text scoreText;

    public delegate void OnScoreChangedEvent(float score);
    public event OnScoreChangedEvent OnScoreChanged;

	// Use this for initialization
	void Start ()
    {
        OnScoreChanged += ScoreScript_OnScoreChangedEvent;
	}

    private void ScoreScript_OnScoreChangedEvent(float score)
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    private float score;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            if (OnScoreChanged != null)
                OnScoreChanged(score);

            scoreText.text = score.ToString();
        }
    }
}
