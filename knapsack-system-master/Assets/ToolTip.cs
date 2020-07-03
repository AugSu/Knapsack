using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToolTip : MonoBehaviour
{
    private Text tooltipText;
    private Text contenText;

    private float targetAlpha;
    private CanvasGroup canvasGroup;

    public float smoothingSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        targetAlpha = 0f;
        tooltipText = GetComponent<Text>();
        contenText = transform.Find("Content").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothingSpeed * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }

    }

    /// <summary>
    /// 信息板显示
    /// </summary>
    /// <param name="text"></param>
    public void Show(string text)
    {
        tooltipText.text = text;
        contenText.text = text;
        targetAlpha = 1f;
    }

    /// <summary>
    /// 信息板隐藏
    /// </summary>
    public void Hide()
    {
        targetAlpha = 0f;
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
