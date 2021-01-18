using UnityEngine;
namespace Acetering
{
    using System.Collections.Generic;
    namespace Acetering
    {
        public class AIController : BaseController
        {
            [SerializeField]
            protected float search_enemy_distance = 50f;
            [SerializeField]
            protected float forget_enemy_distance = 150f;

            public List<Transform> targets;
            protected TargetDetector detector;
            public Vector2 hung_up_position;

            public float battle_mind = 0f;

            protected override void Init()
            {
                base.Init();
                detector = GetComponent<TargetDetector>();
                targets = new List<Transform>();
            }
            protected void DoSomething()
            {
                float r = Random.Range(0f, 100f);
                r += battle_mind;
                if (r > 150)
                {
                    Attack();
                }
                else if (r > 100)
                {
                    Approach();
                }
                else if (r > 70)
                {
                    SearchEnemy();
                }
                else if (r > 30)
                {
                    HungUp();
                }
                else
                {
                    Idle();
                }
            }

            protected override void Update()
            {
                base.Update();
                targets.RemoveAll((enemy) => { return enemy == null || Vector2.Distance(transform.position, enemy.position) > forget_enemy_distance; });
                if (targets.Count > 0)
                {
                    // battle_mind++;
                }
                else
                {
                    battle_mind = 0;
                }
                DoSomething();

            }
            protected void SearchEnemy()
            {
                if (targets.Count > 0)
                {
                    Approach();
                }
                else
                {
                    detector.enabled = true;
                }
            }
            protected void Attack()
            {
                if (targets.Count > 0)
                {
                    Transform enemy = targets[Random.Range(0, targets.Count)];
                    Weapon current_weapon = gear.GetWeapon();
                    if (current_weapon != null)
                    {
                        Gun gun = current_weapon.GetComponent<Gun>();
                        if (gun != null)
                            gun.AimDirection(enemy.position);
                    }
                    gear.GetWeapon().Use();
                    Approach();
                }
            }
            protected void Approach()
            {
                if (targets.Count > 0)
                {
                    Transform enemy = targets[Random.Range(0, targets.Count)];
                    moveable.Move(enemy.position);
                }
            }
            protected void HungUp()
            {
                if (targets.Count > 0)
                {
                    Approach();
                }
                else
                {
                    if (hung_up_position == null)
                    {
                        Vector2 vector2 = new Vector2(transform.position.x, transform.position.y) + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                        RaycastHit2D raycast = Physics2D.Raycast(transform.position, vector2, 100f, LayerMask.GetMask("Environment"));
                        hung_up_position = raycast.centroid;
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, hung_up_position, Color.red);
                        moveable.Move(hung_up_position);
                        if (Vector2.Distance(transform.position, hung_up_position) < 1f)
                        {
                            Vector2 vector2 = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                            RaycastHit2D raycast = Physics2D.Raycast(transform.position, vector2, 100f, LayerMask.GetMask("Environment"));
                            hung_up_position = raycast.centroid;
                        }
                    }

                }
            }
            protected void Idle() { }
            public void GetEnemy(Transform enemy)
            {
                detector.enabled = false;
                battle_mind += 100;
                if (!targets.Contains(enemy))
                {
                    targets.Add(enemy);
                }
            }
        }
    }
}