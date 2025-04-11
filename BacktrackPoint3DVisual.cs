using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace VisualBacktrack
{
    /// This component is attached to each MapBacktrackPoint.
    /// It creates and manages the 3D sphere that mimics the animation of the point’s sprite,
    /// but is shown in the actual 3D world.
    public class BacktrackPoint3DVisual : MonoBehaviour
    {
        private GameObject sphereObject;
        private MeshRenderer sphereRenderer;
        private Coroutine animationCoroutine;

        // Final size multiplier – adjust as needed.
        public float targetScale = 0.35f;

        private void Awake()
        {
            // Create the sphere in the 3D world.
            sphereObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphereObject.name = "BacktrackSphere3D";

            // Detach from the MapBacktrackPoint so it lives in world space.
            sphereObject.transform.SetParent(null, worldPositionStays: true);
            // Start with zero scale.
            sphereObject.transform.localScale = Vector3.zero;
            Destroy(sphereObject.GetComponent<Collider>());

            sphereRenderer = sphereObject.GetComponent<MeshRenderer>();
            if (sphereRenderer != null)
            {
                // Create a new material using the Standard shader.
                var mat = new Material(Shader.Find("Standard"));

                // Apply color and opacity from config
                Color configuredColor = VisualBacktrackConfig.BacktrackPointColor.Value;
                configuredColor.a = VisualBacktrackConfig.Opacity.Value;
                mat.color = configuredColor;

                // If opacity is less than 1, set the shader to Transparent mode.
                if (configuredColor.a < 1f)
                {
                    mat.SetFloat("_Mode", 3);
                    mat.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;
                }

                sphereRenderer.material = mat;

                // Use the config value to determine whether the spheres cast shadows.
                sphereRenderer.shadowCastingMode = VisualBacktrackConfig.CastShadows.Value ?
                    ShadowCastingMode.On : ShadowCastingMode.Off;
            }
        }

        /// Sets the 3D sphere’s position in the world.
        /// The given position is expected to be in world coordinates.
        /// An offset is applied so the sphere sits above ground.
        public void Set3DPosition(Vector3 worldPos)
        {
            if (sphereObject != null)
            {
                sphereObject.transform.position = worldPos + new Vector3(0f, targetScale / 2f, 0f);
            }
        }

        /// Triggers the scale animation for the 3D sphere.
        public void TriggerAnimation()
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            animationCoroutine = StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            // Determine whether to sync with the MapBacktrackPoint animation or use config settings.
            MapBacktrackPoint mapPoint = GetComponent<MapBacktrackPoint>();

            bool syncAnimation = VisualBacktrackConfig.SyncAnimation.Value && mapPoint != null;
            AnimationCurve curve = syncAnimation ? mapPoint.curve : AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            float speed = syncAnimation ? mapPoint.speed : VisualBacktrackConfig.AnimationSpeed.Value;

            float lerp = 0f;
            while (lerp < 1f)
            {
                lerp += Time.deltaTime * speed;
                float scaleValue = curve.Evaluate(lerp);
                sphereObject.transform.localScale = Vector3.one * scaleValue * targetScale;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}