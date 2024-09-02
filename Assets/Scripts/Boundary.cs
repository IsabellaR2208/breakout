using UnityEngine;

namespace Breakout
{
    public class Boundary : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boxCollider2D;

        public void Start(){  
            boxCollider2D.size = (transform as RectTransform).rect.size;
            boxCollider2D.offset = (boxCollider2D.size/2) *boxCollider2D.offset;
        }                 
    }
}
