using UnityEngine;
namespace Acetering{

public class GridMoveable : MonoBehaviour, IMoveable
{
    public static Vector2 UP = new Vector2(0, GridSystem.GRID_SIZE);
    public static Vector2 Down = new Vector2(0, -GridSystem.GRID_SIZE);
    public static Vector2 LEFT = new Vector2(-GridSystem.GRID_SIZE, 0);
    public static Vector2 RIGHT = new Vector2(GridSystem.GRID_SIZE, 0);
    protected Animator anim;
    protected Vector2 face_direction;
    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void Move(Vector2 direction)
    {
        Vector2 start = transform.position;
        Vector2 to = start+Normalize(direction);
        Face(direction);
        transform.position = Vector2.Lerp(start, to, 1);
    }
    public void Face(Vector2 direction)
    {
        Vector2 to = Normalize(direction);
        anim.SetFloat("move_x", to.x);
        anim.SetFloat("move_y", to.y);
        face_direction = to;
    }
    protected Vector2 Normalize(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                return RIGHT;
            else
                return LEFT;
        }
        else
        {
            if (direction.y > 0)
                return UP;
            else
                return Down;
        }
    }
    public Vector2 GetFaceDirection()
    {
        return face_direction;
    }
}}