using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 初期画面（ボタンを表示し、クリックすると各シーンをロードする）を制御するコンポーネント
/// </summary>
public class LoaderManager : MonoBehaviour
{
    /// <summary>シーンをロードするボタンをこのオブジェクトの子にする</summary>
    [SerializeField] LayoutGroup m_buttonRoot = default;
    /// <summary>シーンをロードするボタンのプレハブ</summary>
    [SerializeField] LoadSceneButtonController m_buttonPrefab = default;
    /// <summary>DontDestroyOnLoad に設定する GameObject</summary>
    [SerializeField] GameObject[] m_dontDestroyOnLoads = default;

    void Start()
    {
        Initialize();

        foreach(var o in m_dontDestroyOnLoads)
        {
            DontDestroyOnLoad(o);
        }
    }

    /// <summary>
    /// 初期化する
    /// </summary>
    void Initialize()
    {
        // Scenes In Build に追加された全てのシーン名を取得する
        var sceneNames = CollectAllScenesInBuild();

        // Build Index が 1 以上にシーンに対してボタンを構成する
        for (int i = 1; i < sceneNames.Length; i++)
        {
            var go = Instantiate(m_buttonPrefab, m_buttonRoot.transform);
            go.Initialize(sceneNames[i]);
        }
    }

    /// <summary>
    /// Scenes In Build に追加されている全てのシーン名を取得する
    /// </summary>
    /// <returns>シーン名が入った配列</returns>
    string[] CollectAllScenesInBuild()
    {
        int i = 0;
        List<string> result = new List<string>();

        while (true)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);

            if (scenePath.Length < 1)
            {
                break;
            }

            Debug.Log(scenePath);
            result.Add(GetSceneNameByPath(scenePath));
            i++;
        }

        return result.ToArray();
    }

    /// <summary>
    /// シーンのパスからシーン名のみを取り出す
    /// </summary>
    /// <param name="scenePath">シーンのパス</param>
    /// <returns>シーン名</returns>
    string GetSceneNameByPath(string scenePath)
    {
        string[] array = scenePath.Split('/');
        string sceneName = array[array.Length - 1].Replace(".unity", "");
        return sceneName;
    }
}
