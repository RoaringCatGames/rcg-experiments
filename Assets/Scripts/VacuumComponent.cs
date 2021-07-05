using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterController))]
    public class VacuumComponent : MonoBehaviour
    {
        private CharacterController _control;

        private void Awake()
        {
            _control = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var kb = Keyboard.current;
            if (kb == null) return;

            if (kb.leftArrowKey.isPressed)
            {
                transform.position += Vector3.left;
                //_control.SimpleMove(Vector3.left * 100f);
            }
        }
    }
}