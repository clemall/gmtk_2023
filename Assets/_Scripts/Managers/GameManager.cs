using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public bool isPause = false;
        public bool isGameover = false;

        private int totalTrashToCollect = 5;
        public int trashCollected = 0;

        public GameObject GameOverPopup;
        public GameObject WinPopup;

        public GameObject fishermanMessage;
        
        public AudioSource fishermanSound;

        public TextMeshProUGUI trashToGetText;

        private static GameManager _instance;
        public static GameManager instance
        {
            get
            {
                if(_instance == null)
                    _instance = GameObject.FindObjectOfType<GameManager>();
                return _instance;
            }
        }

        private void Awake()
        {
            DOTween.Init();
        }

        public void Pause(){
            isPause = true;
            
            DOTween.Pause("toBePause");
        }

        public void UnPause(){
            isPause = false;
            
            DOTween.PlayAll();
        }


        public void GameOver(){
            isPause = true;
            isGameover = true;
            
            DOTween.Pause("toBePause");
            
            GameOverPopup.SetActive(true);
        }
        public void Win(){
            isPause = true;
            isGameover = true;

            DOTween.Pause("toBePause");

            WinPopup.SetActive(true);
        }
        
        public void addTrashToScore(string trashName, float x)
        {
            trashCollected++;

            trashToGetText.text = trashCollected.ToString() + "/" + totalTrashToCollect.ToString();
            
            Vector3 newPosition = fishermanMessage.transform.position;
            newPosition.x = x;
            fishermanMessage.transform.position = newPosition;
            
            fishermanMessage.SetActive(true);
            StopCoroutine("HideFishermanMessage");
            StartCoroutine("HideFishermanMessage");

            TextMeshProUGUI uiText = fishermanMessage.GetComponentInChildren<TextMeshProUGUI>();
            if (trashName == "trash1")
            {
                uiText.text = "AN EMPTY BOTTLE?!...AT LEAST GIVE ME A FULL FLASK";
            }
            else if (trashName == "trash2")
            {
                uiText.text = "WHAT SHOULD I DO WITH THIS, WEAR IT AS A NECKLACE?";
            }
            else if (trashName == "trash3")
            {
                uiText.text = "I DO NOT NEED A NEW TIRE, I HAVE ALREADY REPLACED MINE";
            }
            else if (trashName == "trash4")
            {
                uiText.text = "AT LEAST THOSE FISHES GOT COFFEE, I WOULD KILL FOR ONE";
            }
            else if (trashName == "trash5")
            {
                uiText.text = "SO DANGEROUS, SOMEONE COULD GET HURT";
            }
            
            fishermanSound.Play();

            if (trashCollected == totalTrashToCollect)
            {
                Win();
            }
        }
        
        private IEnumerator HideFishermanMessage()
        {
            while (true)
            {
                yield return new WaitForSeconds(4f);
                fishermanMessage.SetActive(false);

            }
        }
    }
}