using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    /// <summary>
    /// Loads a scene based on string passed in
    /// </summary>
    /// <param name="scene"></param>
	public void LoadNewScene(string scene)
    {
        SceneManager.LoadScene(scene);        
    }
}
