using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class nextScene : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
