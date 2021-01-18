/*
 * File: IMoveable.cs
 * Project/package: Movement
 * File Created: Saturday, 16th January 2021 3:28:41 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 17th January 2021 11:13:11 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
namespace Acetering
{
    public interface IMoveable
    {
        void Move(Vector2 direction);
        void Face(Vector2 direction);
        Vector2 GetFaceDirection();
    }
    public interface IBattleMoveable : IMoveable
    {
        void Jump();
        void Rush(float force, bool forward = true);
    }
}