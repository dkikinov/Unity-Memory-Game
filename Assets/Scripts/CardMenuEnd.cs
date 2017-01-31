using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CardMenuEnd : MonoBehaviour {
	public GameObject card;
	string matName;
	public int cardNum;
	public bool playGame;
	public bool isClicked = false;
	public GameObject quad;

	void Start () {
		
	}
	void Update () {
		
		if (card != null) {
			if (isClicked) {
				isClicked = true;
				Vector3 targetAngles = quad.transform.eulerAngles + 180f * Vector3.up;
				card.transform.eulerAngles = Vector3.Lerp (card.transform.eulerAngles, targetAngles, 3f * Time.deltaTime);
			}

			if (!isClicked) {
				Vector3 targetAngles2 = quad.transform.eulerAngles + 0 * Vector3.up;
				card.transform.eulerAngles = Vector3.Lerp (card.transform.eulerAngles, targetAngles2, 3f * Time.deltaTime);
			}
		}
	}

	public void OnMouseOver(){
		isClicked = true;
	}
	public void OnMouseExit(){
		isClicked = false;
	}
	public void OnMouseDown(){
		if (playGame) {
			SceneManager.LoadScene ("Scene_0");
		} else if (!playGame) {
			Application.Quit ();
		}
	}
	public void SetActive(bool active){
		card.SetActive (active);
	}
}
