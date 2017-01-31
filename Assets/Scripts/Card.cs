using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {
	GameObject card;
	string matName;
	public int cardNum;
	public bool CanBeClicked;
	public bool isClicked = false;
	public GameObject quad;
	// Use this for initialization
	void Start () {
		quad = GameObject.Find("Quad");
		CanBeClicked = true;
	}
	
	// Update is called once per frame
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
		
	public void OnMouseDown(){
		if (CanBeClicked) {
			isClicked = true;
		}
	}
	public void SetActive(bool active){
		card.SetActive (active);
	}

	public bool getClicked(){
		return isClicked;
	}

	public void setCard(GameObject go, int num){
		card = go;
		cardNum = num;
	}
	public void swap(Texture input, int num){
		card.GetComponentInChildren<Renderer>().material.SetTexture ("_MainTex", input);
		this.cardNum = num;
	}
}
