using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testWeaponColliderProxy : MonoBehaviour
{
    public testPlayerController playerController;

    public void EnableWeaponCollider()
    {
        playerController.EnableWeaponCollider();
        Debug.Log("boxcolider bekapcsolva");
    }

    public void DisableWeaponCollider()
    {
        playerController.DisableWeaponCollider();
        Debug.Log("boxcolider kikapcsolva");
    }
}
