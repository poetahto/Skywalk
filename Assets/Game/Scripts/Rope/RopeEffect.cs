using System;
using UnityEngine;

namespace Game.Scripts
{
    public class RopeEffect : MonoBehaviour
    {
        protected bool OnRope;
        private bool _wasOnRope;

        protected virtual void OnRopeEnter()
        {
            // do nothing
        }

        protected virtual void OnRopeExit()
        {
            // do nothing
        }

        #region Rope Collision

            private void FixedUpdate()
            {
                OnRope = false;
            }

            private void Update()
            {
                if (_wasOnRope && !OnRope)
                    OnRopeExit();
                
                else if (!_wasOnRope && OnRope)
                    OnRopeEnter();
                
                _wasOnRope = OnRope;
            }

            private void OnCollisionStay(Collision collision)
            {
                if (collision.contactCount > 0)
                {
                    // OnRope = collision.GetContact(0).normal == Vector3.down;
                    float angle = Vector3.Angle(collision.GetContact(0).normal, Vector3.down);
                    OnRope = angle < 90;
                }
            }

        #endregion
    }
}