using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Main : MonoBehaviour {
	public AudioClip cardflip;
	public AudioClip match;
	public AudioClip nomatch;
	GameObject cardGetter;
	Card card1;
	Card card2;
	Card card3;
	Card card4;
	Card card5;
	Card card6;
	Card card7;
	Card card8;
	Card card9;
	Card card10;
	Card card11;
	Card card12;
	Card card13;
	Card card14;
	Card card15;
	Card card16;
	Card card17;
	Card card18;
	Card[] cardArray;
	int clicked = 0;
	Card firstCard;
	Card secondCard;
	int num;
	int num2;
	int scoreVal;
	int pairsMatched;
	bool twocards = false;
	bool removed = false;
	bool check = false;
	Text score;
	Text time;
	string timeS;
	private static System.Random rng = new System.Random();

	// Use this for initialization
	void Start () {
		scoreVal = 1000;
		pairsMatched = 0;
		cardGetter = GameObject.Find("Card0");
		card1 = cardGetter.AddComponent <Card>() as Card;
		card1.setCard (GameObject.Find("Card0"), 1);
		cardGetter = GameObject.Find ("Card1");
		card2 = cardGetter.AddComponent <Card> () as Card;
		card2.setCard (GameObject.Find("Card1"), 1);
		cardGetter = GameObject.Find ("Card2");
		card3 = cardGetter.AddComponent <Card> () as Card;
		card3.setCard (GameObject.Find("Card2"), 2);
		cardGetter = GameObject.Find ("Card3");
		card4 = cardGetter.AddComponent <Card> () as Card;
		card4.setCard (GameObject.Find("Card3"), 2);
		cardGetter = GameObject.Find ("Card4");
		card5 = cardGetter.AddComponent <Card> () as Card;
		card5.setCard (GameObject.Find("Card4"), 3);
		cardGetter = GameObject.Find ("Card5");
		card6 = cardGetter.AddComponent <Card> () as Card;
		card6.setCard (GameObject.Find("Card5"), 3);
		cardGetter = GameObject.Find ("Card6");
		card7 = cardGetter.AddComponent <Card> () as Card;
		card7.setCard (GameObject.Find("Card6"), 4);
		cardGetter = GameObject.Find ("Card7");
		card8 = cardGetter.AddComponent <Card> () as Card;
		card8.setCard (GameObject.Find("Card7"), 4);
		cardGetter = GameObject.Find ("Card8");
		card9 = cardGetter.AddComponent <Card> () as Card;
		card9.setCard (GameObject.Find("Card8"), 5);
		cardGetter = GameObject.Find("Card9");
		card10 = cardGetter.AddComponent <Card>() as Card;
		card10.setCard (GameObject.Find("Card9"), 5);
		cardGetter = GameObject.Find ("Card10");
		card11 = cardGetter.AddComponent <Card> () as Card;
		card11.setCard (GameObject.Find("Card10"), 6);
		cardGetter = GameObject.Find ("Card11");
		card12 = cardGetter.AddComponent <Card> () as Card;
		card12.setCard (GameObject.Find("Card11"), 6);
		cardGetter = GameObject.Find ("Card12");
		card13 = cardGetter.AddComponent <Card> () as Card;
		card13.setCard (GameObject.Find("Card12"), 7);
		cardGetter = GameObject.Find ("Card13");
		card14 = cardGetter.AddComponent <Card> () as Card;
		card14.setCard (GameObject.Find("Card13"), 7);
		cardGetter = GameObject.Find ("Card14");
		card15 = cardGetter.AddComponent <Card> () as Card;
		card15.setCard (GameObject.Find("Card14"), 8);
		cardGetter = GameObject.Find ("Card15");
		card16 = cardGetter.AddComponent <Card> () as Card;
		card16.setCard (GameObject.Find("Card15"), 8);
		cardGetter = GameObject.Find ("Card16");
		card17 = cardGetter.AddComponent <Card> () as Card;
		card17.setCard (GameObject.Find("Card16"), 9);
		cardGetter = GameObject.Find ("Card17");
		card18 = cardGetter.AddComponent <Card> () as Card;
		card18.setCard (GameObject.Find("Card17"), 9);
		cardGetter = GameObject.Find ("Score");
		score = cardGetter.GetComponent<Text> ();
		cardGetter = GameObject.Find ("Time");
		time = cardGetter.GetComponent<Text> ();
		cardArray = new Card[18];
		cardArray [0] = card1;
		cardArray [1] = card2;
		cardArray [2] = card3;
		cardArray [3] = card4;
		cardArray [4] = card5;
		cardArray [5] = card6;
		cardArray [6] = card7;
		cardArray [7] = card8;
		cardArray [8] = card9;
		cardArray [9] = card10;
		cardArray [10] = card11;
		cardArray [11] = card12;
		cardArray [12] = card13;
		cardArray [13] = card14;
		cardArray [14] = card15;
		cardArray [15] = card16;
		cardArray [16] = card17;
		cardArray [17] = card18;
		Shuffle (cardArray);
		
	}
	
	// Update is called once per frame
	void Update () {
		timeS = "Time: " + string.Format ("{0:00}:{1:00}", Time.timeSinceLevelLoad / 60, Time.timeSinceLevelLoad % 60);
		PlayerPrefs.SetString ("Time", timeS);

		time.text = timeS;
		score.text = "Score: " + scoreVal;
		if (clicked < 1) {
			checkFirst ();
		} else if (clicked < 2) {

			checkSecond ();
			twocards = true;
		}
		if (clicked == 2 && twocards) {
			twocards = false;
			deActivateAll();
			Invoke("checkPair", 2f);
		}
		if (removed && check) {
			AudioSource.PlayClipAtPoint (match, Vector3.zero);
			removed = false;
			check = false;
			pairsMatched++;
		} else if (!removed && check) {
			scoreVal -= 40;
			AudioSource.PlayClipAtPoint (nomatch, Vector3.zero);
			check = false;
		}
		PlayerPrefs.SetInt ("Score", scoreVal);
		if (pairsMatched == 9 || scoreVal == 0) {
			SceneManager.LoadScene ("End");
		}
	}
	public static void Shuffle(Card[] list){
		int n = 18;
		while( n > 1){
			n--;
			int k = rng.Next(n);
			Texture temp = list [k].GetComponentInChildren<Renderer> ().material.GetTexture ("_MainTex");
			Texture temp2 = list [n].GetComponentInChildren<Renderer>().material.GetTexture("_MainTex");
			int x = list[k].cardNum;
			int y = list [n].cardNum;
			list [k].swap (temp2, y);
			list [n].swap (temp, x);
		}
	}



	void checkPair(){
		if (firstCard.cardNum == secondCard.cardNum  && firstCard != null && secondCard != null) {
			firstCard.isClicked = false;
			secondCard.isClicked = false;
			remove ();
		} else {
			firstCard.isClicked = false;
			secondCard.isClicked = false;
		}
		reActivateAll ();
		check = true;
	}
	void deActivateAll(){
		card1.CanBeClicked = false;
		card2.CanBeClicked = false;
		card3.CanBeClicked = false;
		card4.CanBeClicked = false;
		card5.CanBeClicked = false;
		card6.CanBeClicked = false;
		card7.CanBeClicked = false;
		card8.CanBeClicked = false;
		card9.CanBeClicked = false;
		card10.CanBeClicked = false;
		card11.CanBeClicked = false;
		card12.CanBeClicked = false;
		card13.CanBeClicked = false;
		card14.CanBeClicked = false;
		card15.CanBeClicked = false;
		card16.CanBeClicked = false;
		card17.CanBeClicked = false;
		card18.CanBeClicked = false;
	}
	void reActivateAll(){
		num = 0;
		num2 = 0;
		clicked = 0;
		twocards = false;
		card1.CanBeClicked = true;
		card2.CanBeClicked = true;
		card3.CanBeClicked = true;
		card4.CanBeClicked = true;
		card5.CanBeClicked = true;
		card6.CanBeClicked = true;
		card7.CanBeClicked = true;
		card8.CanBeClicked = true;
		card9.CanBeClicked = true;
		card10.CanBeClicked = true;
		card11.CanBeClicked = true;
		card12.CanBeClicked = true;
		card13.CanBeClicked = true;
		card14.CanBeClicked = true;
		card15.CanBeClicked = true;
		card16.CanBeClicked = true;
		card17.CanBeClicked = true;
		card18.CanBeClicked = true;
		card1.isClicked = false;
		card2.isClicked = false;
		card3.isClicked = false;
		card4.isClicked = false;
		card5.isClicked = false;
		card6.isClicked = false;
		card7.isClicked = false;
		card8.isClicked = false;
		card9.isClicked = false;
		card10.isClicked = false;
		card11.isClicked = false;
		card12.isClicked = false;
		card13.isClicked = false;
		card14.isClicked = false;
		card15.isClicked = false;
		card16.isClicked = false;
		card17.isClicked = false;
		card18.isClicked = false;
	}
	void remove(){
		removed = true;
		switch(num){
		case 1:
			card1.SetActive (false);
			break;
		case 2:
			card2.SetActive (false);
			break;
		case 3: 
			card3.SetActive (false);
			break;
		case 4:
			card4.SetActive (false);
			break;
		case 5: 
			card5.SetActive (false);
			break;
		case 6:
			card6.SetActive (false);
			break;
		case 7:
			card7.SetActive (false);
			break;
		case 8: 
			card8.SetActive (false);
			break;
		case 9:
			card9.SetActive (false);
			break;
		case 10:
			card10.SetActive (false);
			break;
		case 11:
			card11.SetActive (false);
			break;
		case 12:
			card12.SetActive (false);
			break;
		case 13:
			card13.SetActive (false);
			break;
		case 14:
			card14.SetActive (false);
			break; 
		case 15:
			card15.SetActive (false);
			break;
		case 16:
			card16.SetActive (false);
			break;
		case 17:
			card17.SetActive (false);
			break;
		case 18:
			card18.SetActive (false);
			break;
		}
		switch(num2){
		case 1:
			card1.SetActive (false);
			break;
		case 2:
			card2.SetActive (false);
			break;
		case 3: 
			card3.SetActive (false);
			break;
		case 4:
			card4.SetActive (false);
			break;
		case 5: 
			card5.SetActive (false);
			break;
		case 6:
			card6.SetActive (false);
			break;
		case 7:
			card7.SetActive (false);
			break;
		case 8: 
			card8.SetActive (false);
			break;
		case 9:
			card9.SetActive (false);
			break;
		case 10:
			card10.SetActive (false);
			break;
		case 11:
			card11.SetActive (false);
			break;
		case 12:
			card12.SetActive (false);
			break;
		case 13:
			card13.SetActive (false);
			break;
		case 14:
			card14.SetActive (false);
			break; 
		case 15:
			card15.SetActive (false);
			break;
		case 16:
			card16.SetActive (false);
			break;
		case 17:
			card17.SetActive (false);
			break;
		case 18:
			card18.SetActive (false);
			break;
		}
	}
	void checkFirst(){
		if (card1.isClicked) {
			clicked++;
			firstCard = card1;
			num = 1;
		} else if (card2.isClicked) {
			clicked++;
			firstCard = card2;
			num = 2;
		} else if (card3.isClicked) {
			clicked++;
			firstCard = card3;
			num = 3;
		} else if (card4.isClicked) {
			clicked++;
			firstCard = card4;
			num = 4;
		} else if (card5.isClicked) {
			clicked++;
			firstCard = card5;
			num = 5;
		} else if (card6.isClicked) {
			clicked++;
			firstCard = card6;
			num = 6;
		} else if (card7.isClicked) {
			clicked++;
			firstCard = card7;
			num = 7;
		} else if (card8.isClicked) {
			clicked++;
			firstCard = card8;
			num = 8;
		} else if (card9.isClicked) {
			clicked++;
			firstCard = card9;
			num = 9;
		} else if (card10.isClicked) {
			clicked++;
			firstCard = card10;
			num = 10;
		} else if (card11.isClicked) {
			clicked++;
			firstCard = card11;
			num = 11;
		} else if (card12.isClicked) {
			clicked++;
			firstCard = card12;
			num = 12;
		} else if (card13.isClicked) {
			clicked++;
			firstCard = card13;
			num = 13;
		} else if (card14.isClicked) {
			clicked++;
			firstCard = card14;
			num = 14;
		} else if (card15.isClicked) {
			clicked++;
			firstCard = card15;
			num = 15;
		} else if (card16.isClicked) {
			clicked++;
			firstCard = card16;
			num = 16;
		} else if (card17.isClicked) {
			clicked++;
			firstCard = card17;
			num = 17;
		} else if (card18.isClicked) {
			clicked++;
			firstCard = card18;
			num = 18;
		}
		if (num != 0) {
			AudioSource.PlayClipAtPoint (cardflip, Vector3.zero); 
		}
	}
	void checkSecond(){
		if (card1.isClicked && num != 1) {
			clicked++;
			secondCard = card1;
			num2 = 1;
		}
		else if (card2.isClicked && num != 2) {
			clicked++;
			secondCard = card2;
			num2 = 2;
		}
		else if (card3.isClicked && num != 3) {
			clicked++;
			secondCard = card3;
			num2 = 3;
		}
		else if (card4.isClicked && num != 4) {
			clicked++;
			secondCard = card4;
			num2 = 4;
		}else if (card5.isClicked && num != 5) {
			clicked++;
			secondCard = card5;
			num2 = 5;
		}else if (card6.isClicked && num != 6) {
			clicked++;
			secondCard = card6;
			num2 = 6;
		}else if (card7.isClicked && num != 7) {
			clicked++;
			secondCard = card7;
			num2 = 7;
		}else if (card8.isClicked && num != 8) {
			clicked++;
			secondCard = card8;
			num2 = 8;
		}else if (card9.isClicked && num != 9) {
			clicked++;
			secondCard = card9;
			num2 = 9;
		}else if (card10.isClicked && num != 10) {
			clicked++;
			secondCard = card10;
			num2 = 10;
		}
		else if (card11.isClicked && num != 11) {
			clicked++;
			secondCard = card11;
			num2 = 11;
		}
		else if (card12.isClicked && num != 12) {
			clicked++;
			secondCard = card12;
			num2 = 12;
		}
		else if (card13.isClicked && num != 13) {
			clicked++;
			secondCard = card13;
			num2 = 13;
		}else if (card14.isClicked && num != 14) {
			clicked++;
			secondCard = card14;
			num2 = 14;
		}else if (card15.isClicked && num != 15) {
			clicked++;
			secondCard = card15;
			num2 = 15;
		}else if (card16.isClicked && num != 16) {
			clicked++;
			secondCard = card16;
			num2 = 16;
		}else if (card17.isClicked && num != 17) {
			clicked++;
			secondCard = card17;
			num2 = 17;
		}else if (card18.isClicked && num != 18) {
			clicked++;
			secondCard = card18;
			num2 = 18;
		}
		if (num2 != 0) {
			AudioSource.PlayClipAtPoint (cardflip, Vector3.zero); 
		}
	}
}
