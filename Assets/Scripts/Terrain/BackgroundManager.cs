using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] backgrounds; // Array to hold the different background prefabs
    public GameObject player; // Reference to the player GameObject
    public float speed = 1f; // Speed at which the background moves

    private GameObject currentBackground;
    private Vector3 startPosition;
    private float backgroundWidth;
    private int level;

    void Start()
    {
        // Get the level from ChoseBiome singleton
    }

    void Update()
    {
        level = ChoseBiome.Instance.level + 1;

        ChooseBackground(level);
        if (currentBackground != null)
        {
            startPosition = currentBackground.transform.position;
            backgroundWidth = currentBackground.GetComponent<SpriteRenderer>().bounds.size.x;
        }
        if (currentBackground != null && player != null)
        {
            FollowPlayer();
        }
    }

    void ChooseBackground(int level)
    {
        if (level < 1 || level > backgrounds.Length)
        {
            Debug.LogError("Invalid level. Make sure level is between 1 and " + backgrounds.Length);
            return;
        }

        if (currentBackground != null)
        {
            Destroy(currentBackground);
        }

        currentBackground = Instantiate(backgrounds[level - 1], Vector3.zero, Quaternion.identity);
        currentBackground.transform.position = new Vector3(startPosition.x, startPosition.y,100);
    
}

    void FollowPlayer()
    {
        if (currentBackground == null || player == null) return;

        // Calculate the new position based on player's position
        Vector3 playerPosition = player.transform.position;
        Vector3 newPosition = new Vector3(playerPosition.x * speed, currentBackground.transform.position.y, 100);

        // Move the background to follow the player
        currentBackground.transform.position = newPosition;

        // Ensure the background loops seamlessly
        if (backgroundWidth > 0)
        {
            float offset = Mathf.Repeat(playerPosition.x * speed, backgroundWidth);
            currentBackground.transform.position = startPosition + Vector3.left * offset;
            currentBackground.transform.position = new Vector3(currentBackground.transform.position.x, currentBackground.transform.position.y, 100);
        }
    }
}
