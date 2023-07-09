using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfter : MonoBehaviour
{
	public float delay = 15f;
    void Start()
    {
        StartCoroutine("Hide");
    }

    IEnumerator Hide (){
		while(true)
		{

			yield return  new WaitForSeconds(delay);

			gameObject.SetActive(false);
		}
	}
}
