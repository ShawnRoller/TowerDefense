﻿using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : Singleton<TowerManager> {

    public TowerBtn towerBtnPressed { get; set; }
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider.tag == "buildSite" && hit.collider.tag != "tower")
            {
                hit.collider.tag = "buildSiteFull";
                PlaceTower(hit);
            }
        }

        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
	}

    private void PlaceTower(RaycastHit2D hit) {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            spriteRenderer.enabled = false;
        }
    }

    public void SelectedTower(TowerBtn selectedTowerBtn) {
        towerBtnPressed = selectedTowerBtn;
        EnableDragSprite(towerBtnPressed.DragSprite);
    }

    private void FollowMouse() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private void EnableDragSprite(Sprite sprite) {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void DisableDragSprite()
    {
        spriteRenderer.enabled = false;
    }

}
