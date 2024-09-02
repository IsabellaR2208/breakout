using UnityEngine;

namespace Breakout
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private BoxCollider2D boxCollider2D;
       
        private float screenBoundary;

        public void Start()
        {
            boxCollider2D.size = (transform as RectTransform).rect.size;
            // Calculate screen boundary based on the camera's orthographic size and aspect ratio
            float PaddleWidth = transform.localScale.x*2;
            screenBoundary = Camera.main.aspect * Camera.main.orthographicSize - PaddleWidth;
        }

        public  void Update()
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
                HandleKeyboardInput();
            #elif UNITY_IOS || UNITY_ANDROID
                HandleTouchInput();
            #endif
        }

        private void HandleKeyboardInput()
        {
            float input = Input.GetAxis("Horizontal");
            Vector3 position = transform.position;
            position.x += input * speed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, -screenBoundary, screenBoundary);
            transform.position = position;

        }

    private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                Vector3 position = transform.position;

                // Move the paddle only horizontally
                position.x = Mathf.Clamp(touchPosition.x, -screenBoundary, screenBoundary);
                transform.position = position;
            }
        }
    }
}