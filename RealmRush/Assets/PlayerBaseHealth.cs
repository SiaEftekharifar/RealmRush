using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseHealth : MonoBehaviour {

    [SerializeField] int baseHealth = 20;

    public void FriendlyBaseDamage() {
        print("Damaged!!");

        baseHealth--;
        if (baseHealth <1) {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

    }

}
