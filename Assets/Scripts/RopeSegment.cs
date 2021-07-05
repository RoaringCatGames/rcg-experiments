using UnityEngine;

namespace DefaultNamespace
{
    public struct RopeSegment
    {
        public Vector2 currentPosition;
        public Vector2 previousPosition;

        public RopeSegment(Vector2 position)
        {
            this.currentPosition = position;
            this.previousPosition = position;
        }
    }
}