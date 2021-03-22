using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    [SerializeField]
    GameObject healthEmpty;
    [SerializeField]
    GameObject healthHalf;
    [SerializeField]
    GameObject healthFull;
    [SerializeField]
    float spriteSpacing = 100f;

    [SerializeField]
    GameObject player;
    PlayerBase playerBase;

    private List<GameObject> livesImages = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
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
            healthObject.transform.parent = transform;
            
            livesImages.Add(healthObject);
        }
    }

    void ClearHearts() {
        foreach(GameObject livesImage in livesImages) {
            Destroy(livesImage);
        }
        livesImages = new List<GameObject>();
    }
}
