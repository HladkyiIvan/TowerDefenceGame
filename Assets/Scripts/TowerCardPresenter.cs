using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerCardPresenter : MonoBehaviour
{
    public TowerData TowerData;

    public TextMeshProUGUI CostText;
    public TextMeshProUGUI HotkeyText;
    public Image IconImage;

    private Button button;

    public void Update()
    {
        if (Input.GetKeyDown(HotkeyText.text))
        {
            Events.SelectTower(TowerData);
        }
    }

    public void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(Pressed);
        }

        if (TowerData != null)
        {
            CostText.text = TowerData.Cost.ToString();
            HotkeyText.text = TowerData.Hotkey;
            IconImage.sprite = TowerData.Icon;
        }
    }

    public void Pressed()
    {
        Events.SelectTower(TowerData);
    }
}
