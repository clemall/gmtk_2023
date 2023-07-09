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
            print("Game is paused");
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
            
            Vector3 newPosition = fishermanMessage.transform.position;
            newPosition.x = x;
            fishermanMessage.transform.position = newPosition;
            
            fishermanMessage.SetActive(true);
            StopCoroutine("HideFishermanMessage");
            StartCoroutine("HideFishermanMessage");

            TextMeshProUGUI uiText = fishermanMessage.GetComponentInChildren<TextMeshProUGUI>();
            if (trashName == "trash1")
            {
                uiText.text = "A BOTTLE?! AND IT IS EMPTY...I MISS MY ALCOHOL";
            }
            else if (trashName == "trash2")
            {
                uiText.text = "WHAT SHOULD I DO WITH THIS, USE IT AS A NECKLESS?";
            }
            else if (trashName == "trash3")
            {
                uiText.text = "I DO NOT NEED A NEW TIRE, I ALREADY REPLACED MINE";
            }
            else if (trashName == "trash4")
            {
                uiText.text = "AT LEAST THOSE FISH GOT COFFEE, I WOULD KILL FOR ONE";
            }
            else if (trashName == "trash5")
            {
                uiText.text = "THIS IS DANGEROUS, ONE CAN EASILY SUFFOCATE IN ONE OF THESE";
            }

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