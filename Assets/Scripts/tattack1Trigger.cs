using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tattack1Trigger : MonoBehaviour {

	public int damage = 20;

	void OnTriggerEnter2D(Collider2D col){

		if (col.isTrigger != true && col.CompareTag("Enemy")){

			col.SendMessageUpwards("Damage", damage);
		}

	}

}
