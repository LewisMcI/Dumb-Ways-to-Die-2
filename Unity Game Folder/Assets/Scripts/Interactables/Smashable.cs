using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashable : MonoBehaviour
{
    #region fields
    [SerializeField]
    private GameObject[] smashedPieces;
    [SerializeField]
    private float destroyTime = 2.5f;

    private Rigidbody rb;
    private BoxCollider bc;
    private bool broken = false;
    #endregion
    #region methods
    private void Awake()
    {
        // Try catch incase user has already setup a rigid body for other reasons.
        try
        {
            rb = GetComponent<Rigidbody>();
        }
        catch
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        // Sets up BoxCollider Reference
        bc = GetComponent<BoxCollider>();
    }
    private void FixedUpdate()
    {
        // If object is not broken and has velocity
        if (!broken && rb.velocity.magnitude >= 1)
        {
            CanBreak();
        }
    }

    void CanBreak()
    {
        // If the BoxCollider isn't a trigger and the object is not currently being held.
        if (!bc.isTrigger && !GetComponent<Interactable>().interacting)
        {
            bc.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If OnTriggerEnter was called by this box collider.
        if (bc.isTrigger == true)
        {
            // Object is now broken
            broken = true;
            // Destroy unnecessary components.
            Destroy(bc); Destroy(rb);
            // For each piece in model
            foreach (var piece in smashedPieces)
            {
                // Creates a new RigidBody for each component
                Rigidbody newRb;
                try
                {
                    newRb = piece.AddComponent<Rigidbody>();
                }
                catch
                {
                    newRb = piece.GetComponent<Rigidbody>();
                }
                // Adds explosive force to separate objects.
                newRb.AddExplosionForce(10, Vector3.down, 10);
                // Audio
                AudioManager.Instance.PlayAudio("Bottle Smash");
            }
            // Destroys bottle and it's pieces after time.
            StartCoroutine(DestroyAfterSeconds(destroyTime));
        }
    }

    IEnumerator DestroyAfterSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
    #endregion
}
