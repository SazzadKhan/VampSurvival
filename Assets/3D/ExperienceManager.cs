using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
 public static ExperienceManager instance;

    public float currentExperience = 0;
    public int currentLevel = 1;
    public float experienceToNextLevel = 10;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddExperience(float amount)
    {
        currentExperience += amount;
        if (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;
        currentExperience -= experienceToNextLevel;
        experienceToNextLevel *= 1.2f; // Increase exp needed for next level
        
        // Implement level up bonuses here (e.g., heal player, increase stats)
        Debug.Log("Level up! Current level: " + currentLevel);
    }
}
