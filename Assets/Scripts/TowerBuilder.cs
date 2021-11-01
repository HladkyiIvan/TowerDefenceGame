using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuilder : MonoBehaviour
{
    private Color AllowColor = new Color(0, 255, 0, 0.7f);
    private Color BlockColor = new Color(255, 0, 0, 0.7f);

    private TowerData CurrentTowerData;
    private SpriteRenderer SpriteRenderer;


    private void Awake()
    {
        gameObject.SetActive(false);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Events.OnTowerSelected += TowerSelected;
    }

    private void OnDestroy()
    {
        Events.OnTowerSelected -= TowerSelected;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(
            Mathf.Round(mousePos.x + 0.5f) - 0.5f,
            Mathf.Round(mousePos.y + 0.5f) - 0.5f,
            0);

        transform.position = mousePos;

        bool free = IsFree(transform.position);
        free = free && ScenarioController.Money >= CurrentTowerData.Cost;

        if (free)
        {
            TintSprite(AllowColor);
        }
        else
        {
            TintSprite(BlockColor);
        }

        if (Input.GetMouseButton(0) && free)
        {
            Build();
        }
    }

    void TintSprite(Color col)
    {
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer rend in renderers)
        {
            rend.color = col;
        }
    }

    bool IsFree(Vector3 pos)
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(pos, 0.45f);

        foreach (Collider2D col in overlaps)
        {
            if (!col.isTrigger)
            {
                return false;
            } 
        }
        return true;
    }

    void Build()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Instantiate(CurrentTowerData.TowerPrefab, transform.position, Quaternion.identity, null);
        Events.PayForTower(-CurrentTowerData.Cost);
        gameObject.SetActive(false);
    }

    private void TowerSelected(TowerData data)
    {
        CurrentTowerData = data;
        SpriteRenderer.sprite = CurrentTowerData.Icon;
        gameObject.SetActive(true);
    }
}
