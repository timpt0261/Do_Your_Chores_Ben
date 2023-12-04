using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Interaction_UI : MonoBehaviour
{
    private Camera _main;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private TextMeshProUGUI _promtText;

    private void Start()
    {
        _main = Camera.main;
        _uiPanel.SetActive(false);

    }

    private void LateUpdate()
    {
        var rotation = _main.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public bool IsDisplayed = false;
    public void SetUp(string promtText)
    {
        _promtText.text = promtText;
        _uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        _uiPanel.SetActive(false);
        IsDisplayed = false;
    }
}