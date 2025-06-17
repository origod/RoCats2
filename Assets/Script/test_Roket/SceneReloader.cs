using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 現在のアクティブなシーンを取得
            Scene currentScene = SceneManager.GetActiveScene();
            // シーンを再読み込み
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
