using poetools.Runtime.Systems;
using UnityEditor;
using UnityEngine;

namespace poetools.Editor
{
    [CustomEditor(typeof(RotationSystem))]
    public class RotationSystemEditor : UnityEditor.Editor
    {
        private SerializedProperty _pitchTransform;
        private SerializedProperty _yawTransform;
        private SerializedProperty _rollTransform;
        
        private void OnEnable()
        {
            _pitchTransform = serializedObject.FindProperty("pitchTransform");
            _yawTransform = serializedObject.FindProperty("yawTransform");
            _rollTransform = serializedObject.FindProperty("rollTransform");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(20f);
            
            if (GUILayout.Button("Generate Rotation Transforms"))
                GenerateRotationTransforms();
        }

        private void GenerateRotationTransforms()
        {
            serializedObject.Update();

            Undo.IncrementCurrentGroup();
            Undo.SetCurrentGroupName("Generate Rotation Transforms");
                
            Transform parentTransform = ((RotationSystem) target).transform;

            var pitchObject = new GameObject("Pitch");
            var pitchTransform = pitchObject.transform;
            Undo.RegisterCreatedObjectUndo(pitchObject, "");

            var yawObject = new GameObject("Yaw");
            var yawTransform = yawObject.transform;
            Undo.RegisterCreatedObjectUndo(yawObject, "");

            var rollObject = new GameObject("Roll");
            var rollTransform = rollObject.transform;
            Undo.RegisterCreatedObjectUndo(rollObject, "");
                
            Undo.SetTransformParent(yawTransform, parentTransform, false, "");
            Undo.SetTransformParent(pitchTransform, yawTransform, false, "");
            Undo.SetTransformParent(rollTransform, pitchTransform, false, "");

            _pitchTransform.objectReferenceValue = pitchTransform;
            _yawTransform.objectReferenceValue = yawTransform;
            _rollTransform.objectReferenceValue = rollTransform;

            serializedObject.ApplyModifiedProperties();
            Undo.CollapseUndoOperations(Undo.GetCurrentGroup());
        }
    }
}