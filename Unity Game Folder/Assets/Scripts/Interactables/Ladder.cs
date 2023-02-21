using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Ladder : Interactable
{
    #region methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "treehouse(FixedFinal)")
        {
            TreehouseSnap(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "treehouse(FixedFinal)")
        {
            TreehouseSnap(other.gameObject);
        }
    }

    private void TreehouseSnap(GameObject treehouse)
    {
        // Drop ladder
        InteractionSystem.Instance.DropObject();
        // Disable
        Destroy(GetComponent<Rigidbody>());
        GetComponent<Interactable>().enabled = false;
        treehouse.GetComponent<Collider>().enabled = false;
        // Snap to treehouse
        transform.parent = treehouse.transform;
        transform.localPosition = new Vector3(-2.33f, 1.05f, -3.71f);
        transform.localRotation = Quaternion.Euler(0.85f, 90, -80.952f);
        transform.localScale = new Vector3(2.249109f, 1.932703f, 1.946055f);
        // Make climable
        GetComponent<Interactable>().Type = InteractableType.Other;
        GetComponent<Interactable>().Text = "Climb";
    }

    public override void Action()
    {
        PlayerController.Instance.transform.position = new Vector3(12.38533f, 5.190731f, 10.819f);
    }
    #endregion
}
