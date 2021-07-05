using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class HairyThing: MonoBehaviour
    {
        [SerializeField] private GameObject ropePrefab;
        [SerializeField] public int hairCount = 20;
        [SerializeField] public float maxAngle = 360f;
        
        [SerializeField] private float gravityXMin = 2.5f;
        [SerializeField] private float gravityXMax = 5f;
        [SerializeField] private float gravityYMin = 1f;
        [SerializeField] private float gravityYMax = 5f;
        
        [SerializeField] private float lengthMin = 0.1f;
        [SerializeField] private float lengthMax = 1.25f;
        
        [SerializeField] private float widthMin = 0.1f;
        [SerializeField] private float widthMax = 0.4f;
            
        private List<GameObject> _hairs = new List<GameObject>();

        private void Start()
        {
            var angleWidth = maxAngle / hairCount;

            for (var i = 0; i < hairCount; i++)
            {
                var xMag = Random.Range(gravityXMin, gravityXMax);
                var yMag = Random.Range(gravityYMin, gravityYMax);
                var len = Random.Range(lengthMin, lengthMax);
                var width = Random.Range(widthMin, widthMax);
                var targetDegrees = i * angleWidth;
                var hair = Instantiate(ropePrefab, transform);
                var rope = hair.GetComponent<Rope>();
                rope.gravity = DegreeToVector2(targetDegrees) * new Vector2(xMag, yMag);
                rope.ropeSegmentLength = len;
                rope.lineWidth = width;
                _hairs.Add(hair);
            }
        }
        
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
      
        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
    }
}