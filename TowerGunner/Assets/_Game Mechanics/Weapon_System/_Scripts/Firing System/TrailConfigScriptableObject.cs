using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Trail Config", menuName = "ScriptableObjects/TrailConfig")]
public class TrailConfigScriptableObject : ScriptableObject
{
        public Material Material;
        public AnimationCurve WidthCurve;
        public float Duration = 0.5f;
        public float MinVertexDistance = 0.1f;
        public Gradient Color;

        public float MissDistance = 100f;
        public float SimulationSpeed = 100f;

        /*public object Clone()
        {
            TrailConfigScriptableObject config = CreateInstance<TrailConfigScriptableObject>();

            Utilities.CopyValues(this, config);

            return config;
        }*/
}