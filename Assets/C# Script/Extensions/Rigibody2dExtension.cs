using Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace Extensions
{
    public static class Rigibody2dExtension
    {
        public static void SetVelocity(this Rigidbody2D rigidbody2D, Axis axis, float value)
        {
            var oldVelocity = rigidbody2D.velocity;
            var newVelocity = new Vector2(
                axis == Axis.X ? value : oldVelocity.x,
                axis == Axis.Y ? value : oldVelocity.y
                );
            rigidbody2D.velocity = newVelocity;
        }

        
    }
}
