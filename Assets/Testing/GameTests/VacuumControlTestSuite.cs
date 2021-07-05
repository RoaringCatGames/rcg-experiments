using NUnit.Framework;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;


namespace Testing.GameTests
{
    public class VacuumControlTestSuite: InputTestFixture
    {
        private Keyboard _keyboard;
        private VacuumComponent _vacuum;

        public override void Setup()
        {
            base.Setup();
            _keyboard = InputSystem.AddDevice<Keyboard>();
            var go = new GameObject();
            go.AddComponent<CharacterController>();
            _vacuum = go.AddComponent<VacuumComponent>();

        }

        public override void TearDown()
        {
            base.TearDown();
            InputSystem.RemoveDevice(_keyboard);
        }

        [Test]
        public void VacuumCanBeAdded()
        {
            // Given - A Vacuum component added to a GameObject
            // When - We try to Reference the Game Object
            // Then - It is not Unity Null
            UnityEngine.Assertions.Assert.IsNotNull(_vacuum);
        }
        
        [UnityTest]
        public IEnumerator VacuumShouldMoveLeftWhenLeftArrowPressed()
        {
            // Given - A Vacuum Object at a known position
            var transform = _vacuum.transform;
            transform.position = Vector3.zero;
            var initialXPosition = transform.position.x;
            
            // When - The left arrow key is pressed
            Press(_keyboard.leftArrowKey);
            yield return new WaitForSeconds(0.1f);
            
            // Then - The Vacuum should move to it's left
            Assert.Less(_vacuum.transform.position.x, initialXPosition, "Vacuum should have moved Left relative to Vector3.zero when left arrow key is pressed");
        }
    }
}