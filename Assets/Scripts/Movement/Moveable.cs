/*
 * File: Moveable.cs
 * Project/package: Movement
 * File Created: Thursday, 12th March 2020 6:28:59 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Friday, 16th October 2020 2:20:20 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 * 28-03-2020 12am	LXR	Change rewritten methods name to make them the same ; Now force moving time depends on force_moving_rate instead of const 0.01f;
 * 26-03-2020 9pm	LXR	Remove force_timer, now time of forced moving can be specified by param or default to force/100 
 * 26-03-2020 9pm	LXR	Change method call logic, now method Move and Face is protected, and all public move method will check moveable or not.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Moveable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("influence on the abort time when hitted by others")]
    protected float force_moving_rate = 0.002f;
    public float speed { get { return m_move_speed; } set { m_move_speed = Mathf.Max(value, 0); } }
    public Vector2 position { get { return rigidbody2D.position; } }
    public Vector2 face_direction { get { return m_face_direction; } }
    [SerializeField]
    protected float m_move_speed;// move speed per second for x and y direction
    [SerializeField]
    protected float m_rotate_speed;// rotate speed(degree per second)
    [Tooltip("disable model rotation, but still change face direction")]
    public bool disable_rotation = true;// actor will disable rotate
    protected Vector2 m_face_direction;// current face direction
    protected bool force_moving = false;
    protected float force_moving_timer = 0f;
    new protected Rigidbody2D rigidbody2D;


    protected virtual void Start()
    {
        print("in Moveable start");
        rigidbody2D = GetComponent<Rigidbody2D>();
        m_face_direction = transform.right;
    }


    protected virtual void FixedUpdate()
    {
        if (force_moving)
        {
            force_moving_timer -= Time.fixedDeltaTime;
            if (force_moving_timer < Values.float_zero)
            {
                force_moving = false;
                rigidbody2D.velocity = Vector2.zero;
            }
        }
    }

    public virtual Moveable Copy(Moveable m) {
        this.speed = m.speed;
        this.force_moving_rate = m.force_moving_rate;
        this.disable_rotation = m.disable_rotation;
        this.m_face_direction = m.face_direction;
        this.m_rotate_speed = m.m_rotate_speed;
        return this;
    }
    protected bool IsMoveable()
    {
        return !force_moving;
    }


    public virtual Vector2 FaceTowards(Vector2 t_direction)
    {
        if (IsMoveable())
        {
            return Face(t_direction);
        }
        else
        {
            return m_face_direction;
        }
    }


    /// <summary>
    /// immediately face to direction
    /// </summary>
    /// <param name="t_direction"></param>
    /// <param name="ignore_condition">if true,will move no matter how is its physical status</param>
    public virtual void FaceToDirectionImmediately(Vector2 t_direction, bool ignore_condition = true)
    {
        if (ignore_condition || IsMoveable())
        {
            Vector2 direction = t_direction.normalized;
            if (direction.x == 0 && direction.y == 0)
            {
                return;
            }
            transform.right = direction;
            m_face_direction = direction;
        }
    }


    /// <summary>
    /// move one step with specified direction;
    /// </summary>
    /// <param name="t_direction"></param>
    public Vector2 MoveTowards(Vector2 t_direction)
    {
        if (IsMoveable())
        {
            //change face direction
            Face(t_direction);
            //move
            Move(t_direction.magnitude);
        }
        return m_face_direction;
    }
    public Vector2 MoveTo(Vector2 position)
    {
        if (IsMoveable())
        {
            //change face direction
            Face(position - (Vector2)transform.position);
            //move
            Move();
        }
        return m_face_direction;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3)(face_direction));
    }
    /// <summary>
    /// add force to make it move, default abort time is relative to force magnitude
    /// </summary>
    /// <param name="force">include direction and magnitude</param>
    public void AddForce(Vector2 force, float abort_time = -1f)
    {
        if (force_moving_rate < Values.float_zero)
        {
            return;
        }
        force_moving = true;
        if (abort_time < 0)
        {

            force_moving_timer = force.magnitude * force_moving_rate;
        }
        else
        {
            force_moving_timer = abort_time;
        }
        rigidbody2D.AddForce(force);
    }
    /// <summary>
    /// move one step with with face direction
    /// </summary>
    public virtual float MoveForward(float speed_scale = 1f)
    {
        if (IsMoveable())
        {
            return Move(speed_scale);
        }
        else
        {
            return 0;
        }
    }
    protected virtual float Move(float speed_scale = 1f)
    {

        //get current position
        Vector2 current_position = rigidbody2D.transform.position;
        float distance = m_move_speed * speed_scale * Time.deltaTime;
        //calculate position in next frame 
        Vector2 destination = current_position + m_face_direction * distance;
        //change position
        rigidbody2D.MovePosition(destination);
        return distance;
    }

    protected virtual Vector2 Face(Vector2 t_direction)
    {
        Vector2 direction = t_direction.normalized;
        if (force_moving)
        {
            return m_face_direction;
        }
        if (direction.x == 0 && direction.y == 0)
        {
            return m_face_direction;
        }
        if (disable_rotation)
        {
            m_face_direction = direction;
            return m_face_direction;
        }
        else if (m_rotate_speed <= Values.float_angle_zero)
        {
            return m_face_direction;
        }
        // 计算当前方向与目标方向的角度差
        float angle = Vector2.Angle(transform.right, direction);
        // 180度的情况
        if (angle > 180 - Values.float_angle_zero)
        {
            transform.Rotate(0, 0, -Random.Range(Values.float_angle_zero, 1));
        }
        // 根据最大旋转速度，计算转向目标共计需要的时间
        float needTime = angle / m_rotate_speed;
        // 如果角度很小，就直接对准目标
        if (needTime <= Values.float_zero)
        {
            transform.right = direction;
        }
        else
        {
            // 当前帧间隔时间除以需要的时间，获取本次应该旋转的比例。
            transform.right = Vector3.Slerp(transform.right, direction, Time.deltaTime / needTime).normalized;
        }
        m_face_direction = transform.right;
        return m_face_direction;
    }
    //*****************************************rewrite methods*************************************************/
    public Vector2 MoveTowards(float x, float y)
    {
        return MoveTowards(new Vector2(x, y));
    }


    public Vector2 FaceTowards(float x, float y)
    {
        return FaceTowards(new Vector2(x, y));
    }
}
