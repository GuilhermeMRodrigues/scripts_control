using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseSelection : MonoBehaviour
{

    [SerializeField] private string character = "Character";
    [SerializeField] private string scene;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == character) {
            changeScene(scene);
        }
    }

    private void changeScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}