using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public bool isPause = false;
        public bool isGameover = false;

        private int totalTrashToCollect = 3;
        public int trashCollected = 0;

        public GameObject GameOverPopup;

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
            // isPause = true;
            // isGameover = true;
            //
            // DOTween.Pause("toBePause");
            //
            // GameOverPopup.SetActive(true);
        }
        public void win(){
            isPause = true;
            isGameover = true;

            DOTween.Pause("toBePause");

            GameOverPopup.SetActive(true);
        }
        
        public void addTrashToScore()
        {
            trashCollected++;

            if (trashCollected == totalTrashToCollect)
            {
                win();
            }
        }
    }
}