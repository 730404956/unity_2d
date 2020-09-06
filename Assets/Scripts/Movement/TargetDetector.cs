/*
 * File: TargetDetector.cs
 * Project/package: Movement
 * File Created: Thursday, 26th March 2020 6:18:39 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 6th September 2020 1:56:27 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
[RequireComponent(typeof(Moveable))]
public class TargetDetector : MonoBehaviour
{
    [Serializable]
    public class OnGetTargetEvent : UnityEvent<Transform> { }
    [SerializeField]
    public OnGetTargetEvent onGetTarget;
    [SerializeField]
    protected bool auto_target = true;
    [SerializeField]
    protected float auto_target_distance = 3f;
    [SerializeField]
    protected float auto_target_angle = 30f;
    [SerializeField]
    protected int ray_cast_num = 10;
    private Moveable move_motor;
    [SerializeField]
    protected LayerMask detect_layerMask;
    public List<Collider2D> ignore_colliders;
    public string aim_tag;
    private void Start()
    {
        move_motor = GetComponent<Moveable>();
        if (auto_target)
        {
            this.enabled = true;
        }
        else
        {
            this.enabled = false;
        }
    }
    private void Update()
    {
        float angle_per_time = auto_target_angle / ray_cast_num;
        for (float i = -auto_target_angle; i < auto_target_angle; i += angle_per_time)
        {
            float x = move_motor.face_direction.x;
            float y = move_motor.face_direction.y;
            float cos_a = Mathf.Cos(i * Mathf.Deg2Rad);
            float sin_a = Mathf.Sin(i * Mathf.Deg2Rad);
            // x=X*cos(a)-Y*sin(a)
            // y=X*sin(a)+Y*cos(a)
            Vector2 direction = new Vector2(x * cos_a - y * sin_a, x * sin_a + y * cos_a);
            Debug.DrawLine(transform.position, transform.position + new Vector3(direction.x, direction.y, 0) * auto_target_distance, Color.blue);
            //check raycast hit 
            RaycastHit2D[] hits = Physics2D.RaycastAll(move_motor.position, direction, auto_target_distance, detect_layerMask);
            foreach (RaycastHit2D hit in hits)
            {
                //hit something
                if (hit.collider != null && !ignore_colliders.Contains(hit.collider)&&(aim_tag.Length<=0||hit.collider.gameObject.CompareTag(aim_tag)))
                {
                    onGetTarget?.Invoke(hit.collider.gameObject.transform);
                    this.enabled = false;
                    return;
                }
            }

        }
    }
    public void SetEnemyLayer(Equipment equipment,IEquipmentGear self) {
        detect_layerMask = LayerManager.GetOpenetLayer(self.GetActor().GetLayer());
        print("detect layer set to :" + detect_layerMask.ToString());
    }
}