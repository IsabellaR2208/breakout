using UnityEngine;

namespace Breakout
{
    public class Ball : MonoBehaviour
    {
        private static readonly Vector3 BallOffestFromPaddle = new Vector3(0, 0.4f,0);
        
        [SerializeField] private Vector2 initialImpulse;
        [SerializeField] private Rigidbody2D rigidbody2D;
        
        private GameObject paddle;
        private bool isBallInPlay = false;

        public void Start(){

                paddle = GameObject.FindWithTag("Paddle");
                string message = (paddle != null) ? "Paddle found!" : "No GameObject with the tag 'Paddle' found.";
                Debug.Log(message);
        }
        
         public void  Update(){
            // Launching the ball 
            if (!isBallInPlay) {
                // Stick the ball to the paddle.
                transform.position = paddle.transform.position + BallOffestFromPaddle;
                // Lanuch the ball on fire.
                if (Input.GetButtonDown("Fire1")){
                    isBallInPlay = true;
                    rigidbody2D.AddForce(initialImpulse, ForceMode2D.Impulse);
                }
            }    
        }
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Brick"))
            {
                Destroy(collision.gameObject);
                //TODO Isabella 
                //GameController.Instance.OnBrickDestroyed();
            }
            else if (collision.gameObject.CompareTag("Boundary"))
            {
                //TODO ISabella
                 // GameController.Instance.OnBallLost();
            }
            // Special case hitting the player's paddle.
            else if (collision.gameObject.CompareTag("Paddle")) {
                OnHittingPaddle(collision);      
            }
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
 
    }
}