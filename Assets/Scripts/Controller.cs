using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace Breakout
{
    public class Controller : MonoBehaviour
    {
        [Header("Unity objects")]
        [SerializeField] private Brick BrickPrefab;
        [SerializeField] private RectTransform BrickContainer;
        [SerializeField] private  Text scoreText;
        [SerializeField] private GameObject winText;

        private int score = 0;
        private Brick[] bricks;
        private int totalBricks =0 ;
        private int bricksRemaining =0;
   

        private void Start() {

            // Find all blocks, and count non-destructible blocks.
            bricks = FindObjectsOfType<Brick>();
            foreach (Brick brick in bricks) {
                if (brick.IsDestructible()) {
                    totalBricks++;
                }
            }
            // Start the game with all blocks remaining.
            bricksRemaining = totalBricks;
        }

      // Public so it can be called from the Blocks.
        public void BlockDestroyed(int blockScore) {
            // Register score.
            score += blockScore;
            scoreText.text = score.ToString();

            // Update the remaining blocks.
            bricksRemaining--;

            // Check for game completion.
            if (bricksRemaining == 0) {
                // Put the ball back on the paddle.
                ResetGame();
                // Show winning text
                winText.SetActive(true);
            }
        }

        private void ResetGame()
        {
            
        }
    }
}