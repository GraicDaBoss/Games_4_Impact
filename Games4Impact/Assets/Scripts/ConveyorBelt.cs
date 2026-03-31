using UnityEngine;
using System.Collections.Generic;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed, conveyorSpeed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private List<GameObject> onBelt = new();
    private Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        material.mainTextureOffset += new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < onBelt.Count; i++)
        {
            PlayerWalk player = onBelt[i].GetComponent<PlayerWalk>();
            if (player != null)
                player.ExternalVelocity = direction * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!onBelt.Contains(collision.gameObject))
            onBelt.Add(collision.gameObject);
    }

    void OnCollisionExit(Collision collision)
    {
        // Clear belt velocity when player leaves
        PlayerWalk player = collision.gameObject.GetComponent<PlayerWalk>();
        if (player != null)
            player.ExternalVelocity = Vector3.zero;

        onBelt.Remove(collision.gameObject);
    }
}