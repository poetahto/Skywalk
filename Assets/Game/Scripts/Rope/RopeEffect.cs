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
                if (ElapsedTime > 0.25f)
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

            private float _lastCheckTime = 0;
            private float ElapsedTime => Time.time - _lastCheckTime;
            
            private void OnCollisionStay(Collision collision)
            {
                if (collision.contactCount > 0 && ElapsedTime > 0.25f)
                {
                    // OnRope = collision.GetContact(0).normal == Vector3.down;
                    // float angle = Vector3.Angle(collision.GetContact(0).normal, Vector3.down);
                    // OnRope = angle < 90;
                    OnRope = true;
                    _lastCheckTime = Time.time;
                }
            }

        #endregion
    }
}