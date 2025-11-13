using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoaderManager : MonoBehaviour
{
    [SerializeField] TMP_Text _progressText;
    
    [SerializeField] string _sceneNameToLoad;

    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(LoadAsyncScene(_sceneNameToLoad));
        }
    }

    IEnumerator LoadAsyncScene(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        //asyncLoad.allowSceneActivation = false;
        

        // Wait until the asynchronous scene fully loads
        while(!asyncLoad.isDone) {
            _progressText.text = asyncLoad.progress * 100f + "%";
            yield return new WaitForSeconds(1f);
        }
    }
}
