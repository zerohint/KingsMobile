using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    public GameObject popupPrefab;
    private GameObject currentPopup;
    public Transform targetTransform;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPopup(string message, Action onConfirm, Action onCancel = null)
    {
        if (currentPopup != null) Destroy(currentPopup);

        currentPopup = Instantiate(popupPrefab, targetTransform);
        currentPopup.transform.SetAsLastSibling();

        RectTransform popupRect = currentPopup.GetComponent<RectTransform>();
        popupRect.anchoredPosition = Vector2.zero;

        TMP_Text messageText = currentPopup.transform.Find("MessageText").GetComponent<TMP_Text>();
        Button confirmButton = currentPopup.transform.Find("ConfirmButton").GetComponent<Button>();
        Button cancelButton = currentPopup.transform.Find("CancelButton").GetComponent<Button>();
        Button closeButton = currentPopup.transform.Find("CloseButton").GetComponent<Button>();

        messageText.text = message;

        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() => {
            onConfirm?.Invoke();
            ClosePopup();
        });

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() => {
            onCancel?.Invoke();
            ClosePopup();
        });

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(ClosePopup);
    }

    public void ClosePopup()
    {
        if (currentPopup != null) Destroy(currentPopup);
    }
}
