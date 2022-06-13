using JetBrains.Annotations;
using UnityEngine;

namespace poetools.Editor
{
    // Provides tools for quick debugging by displaying important information to the screen.
    public static class VisualiseExtensions
    {
        private const float DefaultDuration = 1;

        [PublicAPI]
        public static void Visualize(this Vector3 vector, Vector3 position, Color color, float duration = DefaultDuration)
        {
            Debug.DrawRay(position, vector, color, duration);
        }

        [PublicAPI]
        public static void Visualize(this Collision collision, Color color, float duration = DefaultDuration)
        {
            foreach (var contact in collision.contacts)
                contact.Visualize(color, duration);
        }

        [PublicAPI]
        public static void Visualize(this ContactPoint point, Color color, float duration = DefaultDuration)
        {
            point.normal.Visualize(point.point, color);
        }
    }
}
