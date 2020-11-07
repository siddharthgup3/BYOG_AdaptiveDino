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
        [OdinSerialize] private ShakeCamera _shakeCamera;
        [OdinSerialize] private SpriteRenderer _spriteRenderer;
        [OdinSerialize] private GameObject mainSpawner;
        
        [ShowInInspector,ReadOnly] private Spawner _bottomSpawner;
        [ShowInInspector,ReadOnly] private Spawner _topSpawner;
        [ShowInInspector, ReadOnly] private bool gameRunning;
        


        private IEnumerator unlearning;
        private Transform mainCamera;

        public bool GameRunning
        {
            get => gameRunning;
            set
            {
                gameRunning = value;
                if (value == false)
                {
                    this.StopAllCoroutines();
                    Time.timeScale = 0f;
                    unlearningText.enabled = false;
                }

                PauseGame();
            }
        }


        private void Start()
        {
            mainCamera = _shakeCamera.gameObject.transform;

            var temp = mainSpawner.GetComponents<Spawner>();
            _bottomSpawner = temp[0];
            _topSpawner = temp[1];
            
            
            gameRunning = true;
            gameplayModes["Gravity"].Value = true;
            gameplayModes["Jump_Player"].Value = true;
            gameplayModes["Jump_Obs"].Value = false;
            gameplayModes["Spawn_L"].Value = false;
            gameplayModes["Spawn_R"].Value = true;
            Time.timeScale = 1f;
        }

        private void Update()
        {
            if (!GameRunning && Input.GetKeyDown(spaceKey))
            {
                Debug.Log($"Reset");
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

        public void SwitchMode()
        {
            var roll = Random.Range(1, 2);
            if (unlearning != null)
            {
                StopCoroutine(unlearning);
                unlearningText.gameObject.SetActive(false);
            }

            float lastTimeScale = Time.timeScale;

            unlearning = UnlearningTime();
            StartCoroutine(unlearning);
            switch (roll)
            {
                case 1:
                    gameplayModes["Spawn_L"].Value = !gameplayModes["Spawn_L"].Value;
                    gameplayModes["Spawn_R"].Value = !gameplayModes["Spawn_R"].Value;
                    FlipSprite(true, lastTimeScale);
                    break;
                case 2:
                    gameplayModes["Jump_Player"].Value = !gameplayModes["Jump_Player"].Value;
                    gameplayModes["Jump_Obs"].Value = !gameplayModes["Jump_Obs"].Value;
                    break;
                case 3:
                    FlipSprite(false, lastTimeScale);
                    gameplayModes["Gravity"].Value = !gameplayModes["Gravity"].Value;
                    break;
                case 4:
                    StartCoroutine(StartFlipCamera(mainCamera.eulerAngles.z + 90f));
                    break;
            }
        }

        #region FlipSides

        private void FlipSprite(bool horizontal, float lastTimeScale)
        {
            if (horizontal)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
                StartCoroutine(Bleh(lastTimeScale));
            }
            else
            {
                _spriteRenderer.flipY = !_spriteRenderer.flipY;
            }
        }

        private IEnumerator Bleh(float timer)
        {
            yield return new WaitForSeconds(timer / 2f);
            var temp = _spriteRenderer.flipX ? _bottomSpawner.rightSpawnedObstacles : _bottomSpawner.leftSpawnedObstacles;

            foreach (var spawnedObstacle in temp)
            {
                spawnedObstacle.moveSpeed = spawnedObstacle.moveSpeed == _bottomSpawner.RMoveSpeed
                    ? _bottomSpawner.LMoveSpeed
                    : _bottomSpawner.RMoveSpeed;
            }
            
            var otherTemp = _spriteRenderer.flipX ? _topSpawner.rightSpawnedObstacles : _topSpawner.leftSpawnedObstacles;
            foreach (var spawnedObstacle in otherTemp)
            {
                spawnedObstacle.moveSpeed = spawnedObstacle.moveSpeed == _bottomSpawner.RMoveSpeed
                    ? _bottomSpawner.LMoveSpeed
                    : _bottomSpawner.RMoveSpeed;
            }
        }

        #endregion


        #region FlipCamera

        private IEnumerator StartFlipCamera(float targetZ)
        {
            float progress = 0f;
            float startingZ = mainCamera.eulerAngles.z;
            while (true)
            {
                progress += Time.unscaledDeltaTime;
                if (progress >= 1)
                    break;

                var bla = Mathf.Lerp(startingZ, targetZ, progress);
                mainCamera.eulerAngles = new Vector3(0, 0, bla);
                yield return null;
            }

            yield return null;
        }

        #endregion
        
        
        #region Adaptive Coroutines

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

            _shakeCamera.TriggerShake(0.01f);

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

            while (Time.timeScale < timeScaleToGoBackTo && gameRunning)
            {
                Time.timeScale += 0.1f;
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }

        #endregion
    }
}