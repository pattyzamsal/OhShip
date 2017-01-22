using UnityEngine;
using System.Collections;

/**
 * @author: Patricia Zambrano
 * @description: this class activates/deactives the buttons of vote and guess in runtime
 * @lastModifiedBy: Patricia Zambrano
 */
public class QuitApplication : MonoBehaviour {

    /**
	  * @description: this method quit application
	  */
    public void Quit() {
		//If we are running in the editor
		#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
		#endif

		UnityEngine.Application.Quit ();
	}
}
