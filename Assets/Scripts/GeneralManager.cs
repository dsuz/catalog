using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 全てのシーンに共通した処理はここに追加する
/// </summary>
public class GeneralManager : MonoBehaviour
{
    void Awake()
    {
        // 重複を避けるために、自分と同じコンポーネントがいたら自分自身を破棄する
        var components = GameObject.FindObjectsOfType(this.GetType());
        
        if (components.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        // ESC で初期シーンに戻る
        if (Input.GetButtonUp("Cancel"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
