using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextBox : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public float charPerSecond = 20f;

    public static TextBox Instance { get; private set; }

    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowText(string text)
    {
        textBox.text = "";
        typingCoroutine = StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textBox.maxVisibleCharacters = 0;
        textBox.text = text;

        for (int i = 0; i < text.Length; i++)
        {
            textBox.maxVisibleCharacters++;
            yield return new WaitForSeconds(1f / charPerSecond);
        }
    }
}
