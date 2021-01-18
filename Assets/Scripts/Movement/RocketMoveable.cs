/*
 * File: RocketMoveable.cs
 * Project/package: Movement
 * File Created: Friday, 13th March 2020 6:22:21 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 17th January 2021 3:13:01 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 * 26-03-2020 6pm	LXR	Remove target detecting function, make it an independent class
 */


using UnityEngine;

using System.Collections;
namespace Acetering
{
    public class RocketMoveable : Moveable, RecycleObjectInitiator
    {
        [SerializeField]
        protected bool enable_speed_transform = true;
        [SerializeField]
        [Range(1, 200)]
        protected float angle_liner_transform_rate = 2;
        [SerializeField]
        [Range(1, 900)]
        protected float angle_speed_decrease_rate = 60;
        [SerializeField]
        protected Transform target;
        //override to chase target
        protected override float Move(float speed_scale = 1)
        {
            if (target != null)
            {
                //get target position
                Vector2 target_position = target.position;
                //face to the position
                Face(target_position - position);
                Debug.DrawLine(transform.position, new Vector3(target_position.x, target_position.y, 0), Color.red);
                Debug.DrawLine(transform.position, transform.position + new Vector3(m_face_direction.x, m_face_direction.y, 0), Color.green);

            }
            if (enable_speed_transform && m_rotate_speed > Values.float_angle_zero)
            {
                //calculate angle decrease in this frame
                float angle_decrease = Mathf.Min(m_rotate_speed, angle_speed_decrease_rate * Time.deltaTime);
                //calculate speed increase in this frame
                float speed_transform = angle_decrease * angle_liner_transform_rate / 100;
                this.m_move_speed += speed_transform;
                this.m_rotate_speed = Mathf.Max(m_rotate_speed - angle_decrease, 0);
            }
            //move forward to close the position
            return base.Move();
        }
        public void SetTarget(Transform target)
        {
            if (target.GetComponent<BaseController>() != null)
            {
                this.target = target;
                print(gameObject + " get target " + target.gameObject + " !!!");
            }
        }
        public void OnObjectInit()
        {

        }
        public void OnObjectCreate(IRecycleObjectFactory factory)
        {

        }
        public void OnObjectDestroy()
        {
            target = null;
        }
        public void OnParticleSystemStopped()
        {
        }
    }
}
