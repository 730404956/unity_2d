using UnityEngine;
namespace Acetering
{
    public class BattleMoveable : MonoBehaviour, IBattleMoveable
    {
        public bool face_left = false;
        [Range(0.5f, 8f)]
        public float speed = 2;
        [Range(0.3f, 2f)]
        public float jump_force = 1;
        public static float JUMP_RATE = 4000;
        protected BattlerAnimationManager anim;
        protected Rigidbody2D rg2d;
        protected Vector2 face_direction;
        protected void Awake()
        {
            rg2d = GetComponent<Rigidbody2D>();
            anim = transform.Find("model").GetComponent<BattlerAnimationManager>();
            face_direction = face_left ? Vector2.left : Vector2.right;
        }
        public void Move(Vector2 direction)
        {
            Face(direction);
            anim.GetAnimator().SetFloat("speed", Mathf.Abs(direction.x * speed));
            rg2d.position += face_direction * speed * Time.deltaTime;
        }
        public void Face(Vector2 direction)
        {
            if (IsDirectionReverse(direction))
            {
                face_direction.x *= -1;
                anim.GetAnimator().transform.localScale = new Vector3(-anim.GetAnimator().transform.localScale.x, 1, 1);
            }

        }
        protected bool IsDirectionReverse(Vector2 direction)
        {
            return direction.x * face_direction.x < 0;
        }
        public void Jump()
        {
            rg2d.AddForce(Vector2.up * JUMP_RATE * jump_force);
        }
        public void Rush(float force, bool forward)
        {
            if (forward)
            {
                rg2d.AddForce(face_direction * force);
            }
            else
            {
                rg2d.AddForce(-face_direction * force);
            }
        }
        public Vector2 GetFaceDirection()
        {
            return face_direction;
        }
    }
}