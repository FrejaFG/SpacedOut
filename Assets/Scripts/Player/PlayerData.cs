using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private string statsFilePath;
    [SerializeField]
    private string optionsFilePath;

    // Current player stats and options data, which are automatically initialized to default values if they are null.
    [SerializeField]
    PlayerStats stats = new PlayerStats();
    [SerializeField]
    PlayerOptions options = new PlayerOptions();

    bool loaded = false;

    /// <summary>
    /// This method is called when the game object this script is attached to is initialized.
    /// It loads player data from JSON files stored in the application's persistent data path.
    /// </summary>
    void Awake(){
        // Prevent the game object from being destroyed when a new scene is loaded.
        DontDestroyOnLoad(this.gameObject);

        // Set the file paths for the player stats and options JSON files.
        statsFilePath = Application.persistentDataPath + "/playerStats.json";
        optionsFilePath = Application.persistentDataPath + "/playerOptions.json";

        // If the game object has not been loaded before, load the player stats and options data from the JSON files.
        if (!loaded)
        {
            stats = LoadStats();
            options = LoadOptions();
            loaded = true;
        }

        gameObject.GetComponent<PlayerData>().stats = stats;
    }

    private float SaveTimer = 15f;

    void Update()
    {
        if (!loaded)
        {
            if(SaveTimer >= 0)  SaveTimer -= Time.deltaTime;
            else
            {
                SaveStats(stats);
                SaveTimer = 15f;
            }
        }
    }
    /// <summary>
    /// Save the player stats data to a JSON file at the file path specified in statsFilePath.
    /// </summary>
    /// <param name="stats">The PlayerStats data to be saved.</param>
    public void SaveStats(PlayerStats stats)
    {
        // Convert the stats data to a JSON string.
        string json = JsonUtility.ToJson(stats);
        // Write the JSON string to the stats JSON file.
        File.WriteAllText(statsFilePath, json);
        // Set the game object's stats variable to the saved stats data.
        this.stats = stats;
    }

    /// <summary>
    /// Save the player options data to a JSON file at the file path specified in optionsFilePath.
    /// </summary>
    /// <param name="options">The PlayerOptions data to be saved.</param>
    public void SaveOptions(PlayerOptions options)
    {
        string json = JsonUtility.ToJson(options);
        File.WriteAllText(optionsFilePath, json);
        this.options = options;
    }

    /// <summary>
    /// Load player stats data from the JSON file at the file path specified in statsFilePath.
    /// If the file does not exist, create a new file with default player stats data.
    /// </summary>
    /// <returns>The PlayerStats data loaded from the JSON file.</returns>
    public PlayerStats LoadStats()
    {
        // If the stats JSON file exists, load the data from the file.
        if (File.Exists(statsFilePath))
        {
            string json = File.ReadAllText(statsFilePath);
            PlayerStats stats = JsonUtility.FromJson<PlayerStats>(json);
            return stats;
        }
        // If the stats JSON file does not exist, create a new file with default player stats data and load it.
        else
        {
            PlayerStats ps = new PlayerStats();
            SaveStats(ps);
            return ps;
        }
    }

    /// <summary>
    /// Load player options data from the JSON file at the file path specified in optionsFilePath.
    /// If the file does not exist, create a new file with default player options data.
    /// </summary>
    /// <returns>The PlayerOptions data loaded from the JSON file.</returns>
    public PlayerOptions LoadOptions()
    {
        if (File.Exists(optionsFilePath))
        {
            string json = File.ReadAllText(optionsFilePath);
            PlayerOptions options = JsonUtility.FromJson<PlayerOptions>(json);

            return options;
        }
        else
        {
            PlayerOptions opt = new PlayerOptions();
            SaveOptions(opt);
            return opt;
        }
    }

    /// <summary>
    /// Reduce the player's current health by the specified amount of damage.
    /// If the player's current health becomes zero or less, call the OnDeath method.
    /// </summary>
    /// <param name="damage">The amount of damage to be dealt to the player.</param>
    public void OnHit(double damage)
    {
        // If the player's current health is greater than zero and the damage dealt does not kill the player,
        // reduce the player's current health by the damage amount.
        if (stats.CHealth > 0 && (stats.CHealth - damage) > 0)
        {
            stats.CHealth -= damage;
        }
        // If the player's current health becomes zero or less, set the current health to zero and call the OnDeath method.
        else
        {
            stats.CHealth = 0;
            OnDeath();
        }
    }

    public void OnDeath()
    {
        stats.Deaths++;

        //TODO Change how the On Death Event should be handled
    }

    public void Hit(/* TODO Enemy */)
    {

    }

    /// <summary>
    /// Increase the current health of the player character by the specified amount, up to the maximum health value.
    /// </summary>
    /// <param name="amount">The amount to increase the player character's health by.</param>
    public void Heal(double amount)
    {
        if ((stats.CHealth + amount) > stats.MHealth)
            stats.CHealth = stats.MHealth;
        else
            stats.CHealth += amount;
    }

    public PlayerOptions GetOptions() { return options; }
    public PlayerStats GetStats() { return stats; }
}
