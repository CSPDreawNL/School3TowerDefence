using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.UniversalAssets {
    public class Support : MonoBehaviour {
#if UNITY_EDITOR
        public static void DrawRay(Vector3 position, Vector3 direction, Color color) {
            // draw a ray with a solid disc at the end of it
            if (true) {
                Debug.DrawRay(position, direction, color);
                UnityEditor.Handles.color = color;
                UnityEditor.Handles.DrawSolidDisc(position + direction, Vector3.up, 0.25f);
            }
        }

        public static void DrawLabel(Vector3 position, string label, Color color) {
            // draw a label at a certain position in the provided color
            UnityEditor.Handles.BeginGUI();
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.Label(position, label);
            UnityEditor.Handles.EndGUI();
        }

        static public void DrawWireDisc(Vector3 position, float radius, Color color) {
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawWireDisc(position, Vector3.up, radius);
        }

        static public void DrawSolidDisc(Vector3 position, float radius, Color color) {
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawSolidDisc(position, Vector3.up, radius);
        }
#endif
    }
}
