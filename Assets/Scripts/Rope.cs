using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    [RequireComponent(typeof(LineRenderer))]
    public class Rope: MonoBehaviour
    {
        [SerializeField] public float ropeSegmentLength = 0.25f;
        [SerializeField] private int segmentCount = 35;
        [SerializeField] public float lineWidth = 0.1f;
        [SerializeField] public Vector2 gravity = new Vector2(0f, -1f);
        [SerializeField] public int constraintIterations = 50;

        private LineRenderer _lineRenderer;
        private readonly List<RopeSegment> _ropeSegments = new List<RopeSegment>();
        private Camera _camera;
        private bool _isCameraNotNull;

        private void Start()
        {
            _camera = Camera.main;
            _isCameraNotNull = _camera != null;
            _lineRenderer = GetComponent<LineRenderer>();
            var ropeStartPoint = GetMousePosition();
            


            foreach (var index in Enumerable.Range(0, segmentCount))
            {
                _ropeSegments.Add(new RopeSegment(ropeStartPoint + (Vector3.down * ropeSegmentLength * index)));   
            }
        }

        private void Update()
        {
            DrawRope();
        }

        private void FixedUpdate()
        {
            Simulate();
        }

        private Vector3 GetMousePosition()
        {
            return _isCameraNotNull ? _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) : Vector3.zero;
        }

        private void DrawRope()
        {
            var width = lineWidth;

            _lineRenderer.startWidth = width;
            _lineRenderer.endWidth = width;
            

            var ropePositions = _ropeSegments.Select((rs) => (Vector3)rs.currentPosition).ToArray();

            _lineRenderer.positionCount = ropePositions.Length;
            _lineRenderer.SetPositions(ropePositions);
        }

        private void Simulate()
        {
            for (var i = 0; i < segmentCount; i++)
            {
                var first = _ropeSegments[i];
                var velocity = first.currentPosition - first.previousPosition;
                first.previousPosition = first.currentPosition;
                first.currentPosition += velocity + gravity * Time.deltaTime;
                _ropeSegments[i] = first;
            }
            
            //Constraints

            foreach (var i in Enumerable.Range(0, constraintIterations))
            {
                ApplyConstraint();
            }
        }

        private void ApplyConstraint()
        {
            var firstSegment = _ropeSegments[0];
            firstSegment.currentPosition = GetMousePosition();
            _ropeSegments[0] = firstSegment;

            for (var i = 0; i < _ropeSegments.Count - 1; i++)
            {
                var lh = _ropeSegments[i];
                var rh = _ropeSegments[i + 1];
                float dist = (lh.currentPosition - rh.currentPosition).magnitude;
                float error = dist - ropeSegmentLength;
                var changeDir = (lh.currentPosition - rh.currentPosition).normalized;
                var changeAmount = changeDir * error;
                if (i != 0)
                {
                    lh.currentPosition -= changeAmount * 0.5f;
                    _ropeSegments[i] = lh;
                    rh.currentPosition += changeAmount * 0.5f;
                    _ropeSegments[i + 1] = rh;
                }
                else
                {
                    rh.currentPosition += changeAmount;
                    _ropeSegments[i + 1] = rh;
                }
            }
        }
    }
}