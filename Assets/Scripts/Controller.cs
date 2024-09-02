using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

namespace Breakout
{
    public class Controller : MonoBehaviour
    {
        [Header("Unity objects")]
        [SerializeField] private Brick BrickPrefab;
        [SerializeField] private Ball Ball;
        [SerializeField] private Paddle Paddle;
        [SerializeField] private RectTransform BrickContainer;
        [SerializeField] private GameObject GameOver;
        [SerializeField] private TextMeshProUGUI ScoreText;
        [SerializeField] private GameObject Win;
        [SerializeField] private TextMeshProUGUI WinScoreText;
        [SerializeField] private GameObject StartGameText;

        [SerializeField] private Timer gameTimer;  // Assign the Timer script in the Inspector

        private int score = 0;
        private Brick[] bricks;
        private int destructibleBricksCount =0;
   
        bool isGameStart = false;

        void Start() {
            // Subscribe to the events
            Brick.OnBrickDestroyed += HandleBrickDestroyed;
            Ball.OnBallLost += HandleGameOver;
            gameTimer.OnTimerEnd += HandleGameOver;  
            InitBricks();
        }

         public void  Update()
         {
            // Launching the ball 
            if (!isGameStart) {
                if (Input.anyKeyDown){
                    isGameStart = true;
                    Ball.ShootBall();
                    RestController();
                    StartGameText.SetActive(false); 
                    gameTimer.StartTimer();

                }
            }    
        }
        void OnDestroy()
        {
            // Unsubscribe from the event to avoid memory leaks           
             Brick.OnBrickDestroyed -= HandleBrickDestroyed;      
             gameTimer.OnTimerEnd -= HandleGameOver;            
             Ball.OnBallLost -= HandleGameOver;

        }

        private void InitBricks()
        {
            // Find all blocks, and count non-destructible blocks.
            bricks =  BrickContainer.GetComponentsInChildren<Brick>();
            foreach (Brick brick in bricks) {
                if (brick.IsDestructible()) {
                    destructibleBricksCount++;
                }
            }
        } 
        private void HandleBrickDestroyed(int blockScore){
            
            score += blockScore;
            ScoreText.text = WinScoreText.text = score.ToString();
            destructibleBricksCount--;

            if (destructibleBricksCount <= 0){
                Win.SetActive(true);
                OnResetLevel();
            }
        }

        private void HandleGameOver()
        {
            GameOver.SetActive(true);
            OnResetLevel();
        }

        private void RestController()
        {
            Win.SetActive(false);
            GameOver.SetActive(false);
        }

        private void OnResetLevel()
        {
            StartGameText.SetActive(true);
            isGameStart = false;
            gameTimer.StopTimer();
             Paddle.ResetPaddle(); 
            Ball.ResetBall();
           
            score = 0;
         
            foreach (Brick brick in bricks) {
                brick.ResetBrick();    
            }    
           
           
        }
    }
}