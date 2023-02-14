using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : Interactable
{
    #region methods
    public override void Action()
    {
        AudioManager.Instance.PlayAudio("Eat");
        GameObject.Find("Eating").GetComponent<ParticleSystem>().Play();
        CanInteract = false;
        transform.GetComponent<Renderer>().enabled = false;

        GameManager.Instance.taskManager.UpdateTaskCompletion("Make Jam Toast");
        // Play grab animation
        Animator anim = PlayerController.Instance.transform.GetChild(0).GetComponent<Animator>();
        if (!anim.GetBool("Notepad"))
            anim.SetTrigger("Grab");
    }
    #endregion
}
