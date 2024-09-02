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
      

        private int score = 0;
        private Brick[] bricks;
        private int destructibleBricksCount =0;
   
        bool isGameStart = false;

        void Start() {
            Brick.OnBrickDestroyed += HandleBrickDestroyed;
            Ball.OnBallLost += HandleGameOver;
            InitBricks();
        }

         public void  Update(){
                    
            // Launching the ball 
            if (!isGameStart) {
                if (Input.anyKeyDown){
                    isGameStart = true;
                    OnResetLevel();
                    Ball.ShootBall();
                }
            }    
        }
        void OnDestroy()
        {
            // Unsubscribe from the OnBrickDestroyed event to avoid memory leaks
            Brick.OnBrickDestroyed -= HandleBrickDestroyed;
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
                isGameStart = false;        
            }
        }

        private void HandleGameOver()
        {
            GameOver.SetActive(true);
            isGameStart = false;
        }
        private void OnResetLevel()
        {
            score = 0;
            Win.SetActive(false);
            GameOver.SetActive(false);
            foreach (Brick brick in bricks) {
                brick.ResetBrick();    
            }    
            Ball.RestBall();
            Paddle.RestPaddle(); 
        }
    }
}