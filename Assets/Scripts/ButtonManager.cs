using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void LoadNewScene(string scene)
    {
        SceneManager.LoadScene(scene);        
    }
}
