using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace The_Great_Fleece.Game.Scripts
{
    [CreateAssetMenu(menuName = "Camera transform settings", fileName = "CameraTransformSettings")]
    public class CameraTransformSettings : ScriptableObject
    {
        //[SerializeField] public Dictionary<Collider, Transform> cameraTransforms;
        [SerializeField] private List<TriggerTransformPair> cameraTransforms;

        public Transform GetCameraTransformForTrigger(GameObject triggerObject)
        {
            var cameraTransform = cameraTransforms.FirstOrDefault(c => 
                c.TriggerObject.name == triggerObject.name).CameraTransorm;
            return cameraTransform;
        }
    }
    [Serializable]
    public struct TriggerTransformPair
    {
        public GameObject TriggerObject;
        public Transform CameraTransorm;
    }
}
