using UnityEngine;

namespace Game.Scripts
{
    public class RopeEffect : MonoBehaviour
    {
        protected bool OnRope;
        
        #region Rope Collision

        private void FixedUpdate()
        {
            OnRope = false;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.contactCount > 0)
            {
                OnRope = collision.GetContact(0).normal == Vector3.down;
            }
        }

        #endregion
    }
}