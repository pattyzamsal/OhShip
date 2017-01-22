using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChargeLevels : MonoBehaviour {

    public void SelectLevel(string level) {
        SceneManager.LoadScene(level);
    }
}
