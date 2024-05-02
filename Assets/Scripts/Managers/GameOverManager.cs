﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public float restartDelay = 5f;
        Animator anim;
        float restartTimer;

        //LevelManager lm;
        //private UnityEvent listener;

        void Awake ()
        {
            //playerHealth = FindObjectOfType<PlayerHealth>();
            anim = GetComponent <Animator> ();
            //lm = FindObjectOfType<LevelManager>();
            //EventManager.StartListening("GameOver", ShowGameOver);
        }

        void OnDestroy()
        {
            //EventManager.StopListening("GameOver", ShowGameOver);
        }

        void Update()
        {
            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("GameOver");

                restartTimer += Time.deltaTime;

                if (restartTimer >= restartDelay )
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        //void ShowGameOver()
        //{
        //    anim.SetBool("GameOver", true);
        //}

        //private void ResetLevel()
        //{
        //    ScoreManager.score = 0;
        //    LevelManager lm = FindObjectOfType<LevelManager>();
        //    lm.LoadInitialLevel();
        //    anim.SetBool("GameOver", false);
        //    playerHealth.ResetPlayer();
        //}
    }
}