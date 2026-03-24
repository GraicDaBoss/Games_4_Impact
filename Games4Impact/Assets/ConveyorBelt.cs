using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Movement")]
    public Vector3 beltDirection = Vector3.right;
    public float beltSpeed = 2f;

    [Header("Scrolling Texture")]
    public Renderer beltRenderer;
    public string textureMaterialProperty = "_MainTex";

    private MaterialPropertyBlock propBlock;

    void Start()
    {
        propBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        // Scroll texture
        beltRenderer.GetPropertyBlock(propBlock);
        Vector2 offset = propBlock.GetVector(textureMaterialProperty);
        offset += new Vector2(beltDirection.normalized.x, beltDirection.normalized.z) * beltSpeed * Time.deltaTime;
        propBlock.SetVector(textureMaterialProperty + "_ST", new Vector4(1, 1, offset.x, offset.y));
        beltRenderer.SetPropertyBlock(propBlock);
    }

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        rb.linearVelocity = new Vector3(
            beltDirection.normalized.x * beltSpeed,
            rb.linearVelocity.y,
            beltDirection.normalized.z * beltSpeed
        );
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;
        rb.linearVelocity = Vector3.zero;
    }
}
