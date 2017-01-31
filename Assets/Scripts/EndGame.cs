using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGame : MonoBehaviour {
	GameObject goGet;
	Text score;
	Text time;
	Text result;
	string timeS;
	int scoreVal;
	bool win;
	bool once;
	public AudioClip winClip;
	public AudioClip loseClip;
	// Use this for initialization
	void Start () {
		goGet = GameObject.Find ("Score");
		score = goGet.GetComponent<Text> ();
		timeS = PlayerPrefs.GetString ("Time");
		goGet = GameObject.Find ("Time");
		time = goGet.GetComponent<Text> ();
		scoreVal = PlayerPrefs.GetInt ("Score");
		goGet = GameObject.Find ("Title");
		result = goGet.GetComponent<Text> ();
		once = true;
		if (scoreVal == 0) {
			win = false;
		} else {
			win = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		time.text = "Final " +  timeS;
		score.text = "Final Score: " + scoreVal;
		if (win && once) {
			result.text = "You win!";
			once = false;
			if (winClip != null) {
				AudioSource.PlayClipAtPoint (winClip, Vector3.zero);
			}
		} else if (!win && once) {
			result.text = "You Lose!";
			once = false;
			if (loseClip != null) {
				AudioSource.PlayClipAtPoint (loseClip, Vector3.zero);
			}
		}
	}
}
