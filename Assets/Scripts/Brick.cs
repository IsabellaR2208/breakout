using UnityEngine;

namespace Breakout
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private Animator Animator;

        [Header("Brick parameters")]
        [SerializeField] private int hitsToDestroy = 1;
        [SerializeField] private bool isDestructible = true;
        [SerializeField] private int score = 10;

        // Handle collisions with the brick.
        private void OnCollisionEnter2D(Collision2D collision) {
            // Only care if the brick is destructible.
            if (isDestructible) {
                // Register the hit, by decrementing our counter.
                hitsToDestroy--;

                // If it's been hit enough times destroy it and register score.
                if (hitsToDestroy == 0) {
                //  gameManager.BlockDestroyed(score);
                    gameObject.SetActive(false);
                }
            }
            
        }

        public bool IsDestructible()
        {
            return isDestructible;
        }
    }
}