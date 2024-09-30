using UnityEditor;
using UnityEngine;

namespace EmptyAtZeroCreator
{
    public class AdjustMass
    {
        [MenuItem("PlateUp!/Utils/Increase Mass +10")]
        public static void IncreaseMass10()
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject == null) return;
            
            foreach (Rigidbody rigidbody in selectedObject.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.mass += 10;
            }
        }
        
        [MenuItem("PlateUp!/Utils/Increase Mass +20")]
        public static void IncreaseMass20()
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject == null) return;
            
            foreach (Rigidbody rigidbody in selectedObject.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.mass += 20;
            }
        }
        
        [MenuItem("PlateUp!/Utils/Increase Mass +100")]
        public static void IncreaseMass100()
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject == null) return;
            
            foreach (Rigidbody rigidbody in selectedObject.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.mass += 100;
            }
        }
        
        
        [MenuItem("PlateUp!/Utils/Decrease Mass -10")]
        public static void DecreaseMass10()
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject == null) return;
            
            foreach (Rigidbody rigidbody in selectedObject.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.mass -= 10;
            }
        }
        
        [MenuItem("PlateUp!/Utils/Decrease Mass -20")]
        public static void DecreaseMass20()
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject == null) return;
            
            foreach (Rigidbody rigidbody in selectedObject.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.mass -= 20;
            }
        }
        
        [MenuItem("PlateUp!/Utils/Decrease Mass -100")]
        public static void DecreaseMass100()
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject == null) return;
            
            foreach (Rigidbody rigidbody in selectedObject.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.mass -= 100;
            }
        }
        
        [MenuItem("PlateUp!/Utils/Enable Projection")]
        public static void EnableProjection()
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject == null) return;
            
            foreach (CharacterJoint joint in selectedObject.GetComponentsInChildren<CharacterJoint>())
            {
                joint.enableProjection = true;
            }
        }
        
        [MenuItem("PlateUp!/Utils/Disable Projection")]
        public static void DisableProjection()
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject == null) return;
            
            foreach (CharacterJoint joint in selectedObject.GetComponentsInChildren<CharacterJoint>())
            {
                joint.enableProjection = false;
            }
        }
    }
}