using UnityEngine;
using System;

namespace Breakout
{
    public class Ball : MonoBehaviour
    {
        private static readonly Vector3 BallOffestFromPaddle = new Vector3(0, 0.5f,0);
        [SerializeField] private Vector2 initialImpulse;
        [SerializeField] private Rigidbody2D rigidbody2D; 
        private GameObject paddle;
        private Vector3 initialPos;

        public static event Action OnBallLost;

        public void Start(){
            paddle = GameObject.FindWithTag("Paddle");
            initialPos = paddle.transform.position;
        }
        
         public void  ShootBall(){
            rigidbody2D.isKinematic = false;
            // Stick the ball to the paddle.
            transform.position = paddle.transform.position + BallOffestFromPaddle;
            rigidbody2D.AddForce(initialImpulse, ForceMode2D.Impulse);
            
        }
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("BottomBoundary"))
            {
                OnHittingBottomBoundary();
          
            }
            // Special case hitting the player's paddle.
            else if (collision.gameObject.CompareTag("Paddle")) {
                OnHittingPaddle(collision);      
            }
        }

        private void OnHittingBottomBoundary()
        {
            OnBallLost?.Invoke();

        }
        private void OnHittingPaddle(Collision2D collision)
        {
            // Get the difference in x to see if we hit on the left or right.
            // Normalising with the halfWidth gets +1 on the right and -1 on the left.
            float halfWidth = collision.collider.bounds.size.x;
            float x = (transform.position.x - collision.transform.position.x) / halfWidth;

            // Create the new direction (normalised).
            Vector2 direction = new(4 * x, 1);
            direction = direction.normalized;

            // The current speed is the magnitude of the velocity.
            float currentSpeed = rigidbody2D.velocity.magnitude;

            // Set the new ball velocity.
            rigidbody2D.velocity = direction * currentSpeed;
        }


         public void ResetBall()
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0f;
            rigidbody2D.Sleep();
            rigidbody2D.isKinematic = true;
            transform.position = paddle.transform.position + BallOffestFromPaddle;
        }
    }
}