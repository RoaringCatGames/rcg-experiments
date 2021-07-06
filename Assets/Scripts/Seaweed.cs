using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Seaweed: MonoBehaviour
    {
        [SerializeField] private GameObject ropePrefab;
        [SerializeField] public int hairCount = 100;
        [SerializeField] public float length = 10;
        
        [SerializeField] private float lengthMin = 0.1f;
        [SerializeField] private float lengthMax = 1.25f;
        [SerializeField] private float widthMin = 0.1f;
        [SerializeField] private float widthMax = 0.4f;
        
        private List<GameObject> _hairs = new List<GameObject>();
        private void Start()
        {

            var spaceBetween = length / hairCount;
            
            for (var i = 0; i < hairCount; i++)
            {
                var rootPosition = transform.position + (Vector3.right * spaceBetween * i);
                var len = Random.Range(lengthMin, lengthMax);
                var width = Random.Range(widthMin, widthMax);
                var hair = Instantiate(ropePrefab, rootPosition, Quaternion.identity);
                var rope = hair.GetComponent<Rope>();
                rope.shouldPinStartingPoint = true;
                rope.shouldTailFollowMouse = true;
                rope.ropeSegmentLength = len;
                rope.lineWidth = width;
                _hairs.Add(hair);
            }
        }
    }
}