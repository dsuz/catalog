using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンをロードするためのボタンを制御する
/// </summary>
public class LoadSceneButtonController : MonoBehaviour
{
    /// <summary>ロードするシーン名</summary>
    [SerializeField] string m_sceneName;
    /// <summary>ボタンのラベル</summary>
    [SerializeField] Text m_label = default;

    /// <summary>
    /// 初期化した時に呼ぶ。
    /// 指定されたシーン名を呼ぶように設定し、ラベルをシーン名にする
    /// </summary>
    /// <param name="sceneName"></param>
    public void Initialize(string sceneName)
    {
        m_sceneName = sceneName;
        m_label.text = m_sceneName;
    }

    /// <summary>
    /// 初期化した時に指定されたシーンをロードする
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene(m_sceneName);
    }
}
