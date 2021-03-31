using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private GameObject healthEmpty;
    [SerializeField] private GameObject healthHalf;
    [SerializeField] private GameObject healthFull;
    [SerializeField] private float spriteSpacing = 100f;

    [SerializeField] private GameObject player;
    private PlayerBase playerBase;

    private List<GameObject> livesImages = new List<GameObject>();

    private void Start()
    {
        playerBase = player.GetComponent<PlayerBase>();
        UpdateHearts(playerBase.GetHealthMax(), playerBase.GetHealth());
    }

    public void UpdateHearts(float healthMax, float health) {
        ClearHearts();
        for(float i = 0; i < healthMax / 20 / 2; i++) {
            GameObject healthType;
            if((i + 1) * 2 * 20 <= health ) {
                healthType = healthFull;
            } else if ((i + 1) * 2 * 20 - 20 <= health) {
                healthType = healthHalf;
            } else {
                healthType = healthEmpty;
            }
            GameObject healthObject = (GameObject)Instantiate(healthType, new Vector3(0, 0, 0), Quaternion.identity);
            RectTransform rect = healthObject.transform.GetChild(0).GetComponent<RectTransform>();
            rect.transform.position = new Vector3(rect.transform.position.x + spriteSpacing * i, rect.transform.position.y, rect.transform.position.z);
            healthObject.transform.SetParent(transform);
            
            livesImages.Add(healthObject);
        }
    }

    private void ClearHearts() {
        foreach(GameObject livesImage in livesImages) {
            Destroy(livesImage);
        }
        livesImages = new List<GameObject>();
    }
}
