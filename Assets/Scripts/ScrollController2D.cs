using UnityEngine;
using System.Linq;

/// <summary>
/// 無限スクロールを制御するコンポーネント
/// 適当なオブジェクトにアタッチして使う（スクロールするオブジェクトそのものにアタッチしてはいけない）。
/// スクロール速度は実行中に変更可能だが、スクロール方向は実行中に変更できない。
/// </summary>
public class ScrollController2D : MonoBehaviour
{
    /// <summary>スクロールさせる Sprite</summary>
    [SerializeField] SpriteRenderer m_sprite = default;
    /// <summary>スクロールさせる方向</summary>
    [SerializeField] ScrollDirection m_scrollDirection = default;
    /// <summary>スクロール速度</summary>
    [SerializeField] float m_scrollSpeed = 1f;
    /// <summary>スクロールさせるオブジェクト（２つ）</summary>
    SpriteRenderer[] m_scrollObjects = default;
    /// <summary>スクロールさせるオブジェクトの初期位置</summary>
    Vector2 m_initialPosition = default;
    /// <summary>スクロールさせる方向のベクトル</summary>
    Vector2 m_scrollVector = default;
    /// <summary>スクロールさせるオブジェクトが戻る位置</summary>
    Vector2 m_switchBackPosition = default;
    

    void Start()
    {
        m_initialPosition = m_sprite.transform.position;
        Initialize();
    }

    void Update()
    {
        Scroll();
    }

    void Scroll()
    {
        // 背景画像をスクロールする
        m_scrollObjects.ToList().ForEach(bg => bg.transform.Translate(m_scrollVector * m_scrollSpeed * Time.deltaTime));

        // 限度までスクロールさせたら、戻す
        switch (m_scrollDirection)
        {
            case ScrollDirection.Up:
                m_scrollObjects.ToList().ForEach(bg =>
                {
                    if (bg.transform.position.y > m_initialPosition.y + bg.bounds.size.y)
                    {
                        bg.transform.position = m_switchBackPosition;
                    }
                });
                break;
            case ScrollDirection.Down:
                m_scrollObjects.ToList().ForEach(bg =>
                {
                    if (bg.transform.position.y < m_initialPosition.y - bg.bounds.size.y)
                    {
                        bg.transform.position = m_switchBackPosition;
                    }
                });
                break;
            case ScrollDirection.Right:
                m_scrollObjects.ToList().ForEach(bg =>
                {
                    if (bg.transform.position.x > m_initialPosition.x + bg.bounds.size.x)
                    {
                        bg.transform.position = m_switchBackPosition;
                    }
                });
                break;
            case ScrollDirection.Left:
                m_scrollObjects.ToList().ForEach(bg =>
                {
                    if (bg.transform.position.x < m_initialPosition.x - bg.bounds.size.x)
                    {
                        bg.transform.position = m_switchBackPosition;
                    }
                });
                break;
        }
    }

    /// <summary>
    /// 初期化する
    /// </summary>
    void Initialize()
    {
        // スクロールさせるオブジェクトを複製する
        var clone = Instantiate(m_sprite, this.transform);
        Vector2 offset = default;

        // スクロール方向により各種変数を計算する
        switch (m_scrollDirection)
        {
            case ScrollDirection.Up:
                offset = Vector2.down * m_sprite.bounds.size.y;
                m_scrollVector = Vector2.up;
                m_switchBackPosition = m_initialPosition + Vector2.down * m_sprite.bounds.size.y;
                break;
            case ScrollDirection.Down:
                offset = Vector2.down * m_sprite.bounds.size.y;
                m_scrollVector = Vector2.down;
                m_switchBackPosition = m_initialPosition + Vector2.up * m_sprite.bounds.size.y;
                break;
            case ScrollDirection.Right:
                offset = Vector2.right * m_sprite.bounds.size.x;
                m_scrollVector = Vector2.right;
                m_switchBackPosition = m_initialPosition + Vector2.left * m_sprite.bounds.size.x;
                break;
            case ScrollDirection.Left:
                offset = Vector2.right * m_sprite.bounds.size.x;
                m_scrollVector = Vector2.left;
                m_switchBackPosition = m_initialPosition + Vector2.right * m_sprite.bounds.size.x;
                break;
        }

        // 複製したオブジェクトをずらす
        clone.transform.Translate(offset);
        // 複製したオブジェクトと元のオブジェクトを配列にまとめる
        m_scrollObjects = new SpriteRenderer[] { m_sprite, clone };
    }


    enum ScrollDirection
    {
        Up,
        Down,
        Right,
        Left,
    }
}
