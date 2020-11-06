using System;
using System.Collections;
using System.Collections.Generic;
using MyArchitecture;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace BreakYourOwnGame
{
    public class GameEnforcer : SerializedMonoBehaviour
    {
        [OdinSerialize] private KeyCode spaceKey = KeyCode.Space;

        [FoldoutGroup("TMP references")] [OdinSerialize]
        private TextMeshProUGUI resetText;

        [FoldoutGroup("TMP references")] [OdinSerialize]
        private TextMeshProUGUI unlearningText;

        [OdinSerialize] private Dictionary<string, BooleanVariable> gameplayModes;

        [ShowInInspector, ReadOnly] private bool gameRunning;

        [SerializeField] private TextMeshProUGUI debugText;

        private IEnumerator unlearning;
        public bool GameRunning
        {
            get => gameRunning;
            set
            {
                gameRunning = value;
                if (value == false)
                {
                    this.StopAllCoroutines();
                    unlearningText.enabled = false;
                }

                PauseGame();
            }
        }

        private void Start()
        {
            gameRunning = true;
            gameplayModes["Gravity"].Value = true;
            gameplayModes["Jump_Player"].Value = true;
            gameplayModes["Jump_Obs"].Value = false;
            gameplayModes["Spawn_L"].Value = false;
            gameplayModes["Spawn_R"].Value = true;
            Time.timeScale = 1f;
            UpdateText();
        }

        private void Update()
        {
            if (!GameRunning && Input.GetKeyDown(spaceKey))
            {
                Time.timeScale = 1f;
                resetText.enabled = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                gameRunning = true;
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchMode();
            }
        }

        public  void SwitchMode()
        {
            var roll = Random.Range(1, 4);
            if (unlearning != null)
            {
                StopCoroutine(unlearning);
                unlearningText.gameObject.SetActive(false);
            }

            unlearning = UnlearningTime();
            StartCoroutine(unlearning);
            switch (roll)
            {
                case 1:
                    gameplayModes["Spawn_L"].Value = !gameplayModes["Spawn_L"].Value;
                    gameplayModes["Spawn_R"].Value = !gameplayModes["Spawn_R"].Value;
                    FlipSprite.FlipX();
                    break;
                case 2:
                    gameplayModes["Jump_Player"].Value = !gameplayModes["Jump_Player"].Value;
                    gameplayModes["Jump_Obs"].Value = !gameplayModes["Jump_Obs"].Value;
                    break;
                case 3:
                    FlipSprite.FlipY();
                    gameplayModes["Gravity"].Value = !gameplayModes["Gravity"].Value;
                    break;
            }

            UpdateText();
        }

        private void UpdateText()
        {
            bool rightSpawn = gameplayModes["Spawn_R"].Value;
            bool gravity = gameplayModes["Gravity"].Value;
            bool jump = gameplayModes["Jump_Player"].Value;

            debugText.text = $"Right Spawn = {rightSpawn} \n InverseGravity = {gravity} \n PlayerJump = {jump}";
        }

        private void PauseGame()
        {
            gameRunning = false;
            resetText.enabled = true;
            Time.timeScale = 0;
        }

        private IEnumerator UnlearningTime()
        {
            var timeScaleToGoBackTo = Time.timeScale;

            Time.timeScale = 0f;

            float temp = 1;
            unlearningText.gameObject.SetActive(true);
            unlearningText.enabled = true;
            while (temp > 0)
            {
                unlearningText.text = $"{temp:F2}";
                temp -= Time.unscaledDeltaTime;
                yield return null;
            }

            unlearningText.gameObject.SetActive(false);

            while (Time.timeScale < timeScaleToGoBackTo)
            {
                Time.timeScale += 0.1f;
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }
    }
}