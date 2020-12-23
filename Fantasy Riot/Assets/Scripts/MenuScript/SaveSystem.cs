using UnityEngine;
using System.IO;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    private string PLAYER_KEY="MAURO";
    private FirebaseDatabase _database;
    public PlayerData LastPlayerData { get;private set;}
    public PlayerUpdatedEvent OnPlayerUpdated = new PlayerUpdatedEvent();
    private DatabaseReference _ref;

    public void DB()
    {
        _database = FirebaseDatabase.DefaultInstance;
        _ref = _database.GetReference(PLAYER_KEY);
        _ref.ValueChanged += HandleValueChanged;
    }
    public void ChangePLAYER_KEY(string a)
    {
        PLAYER_KEY = a;
    }
    private void OnDestory()
    {
        _ref.ValueChanged -= HandleValueChanged;
        _ref = null;
        _database = null;
    }
    private void HandleValueChanged(object sender, ValueChangedEventArgs e)
    {
        var json = e.Snapshot.GetRawJsonValue();
        if (!string.IsNullOrEmpty(json))
        {
            var playerData = JsonUtility.FromJson<PlayerData>(json);
            LastPlayerData = playerData;
            OnPlayerUpdated.Invoke(playerData);
        }
    }

    public void SavePlayer(PlayerData player,bool a)
    {
        Debug.Log("e 1");
        if (!player.Equals(LastPlayerData) || a)
        {
            Debug.Log("e 2");
            Debug.Log(PLAYER_KEY);
            _database.GetReference(PLAYER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(player));
        }
    }

    public async Task<PlayerData?> LoadPlayer()
    {
        var dataSnapshot = await _database.GetReference(PLAYER_KEY).GetValueAsync();
        if (!dataSnapshot.Exists)
        {
            return null;
        }
        
        return JsonUtility.FromJson<PlayerData>(dataSnapshot.GetRawJsonValue());
    }

    public async Task<bool> SaveExists()
    {
        var dataSnapshot = await _database.GetReference(PLAYER_KEY).GetValueAsync();
        return dataSnapshot.Exists;
    }
    public void EraseSave()
    {
        _database.GetReference(PLAYER_KEY).RemoveValueAsync();
    }

    [System.Serializable]
    public class PlayerUpdatedEvent : UnityEvent<PlayerData>
    {

    }
}
