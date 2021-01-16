using UnityEngine;
using System.IO;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;
using UnityEngine.Events;

/*this script Save the data in the DB*/
public class SaveSystem : MonoBehaviour
{
    private string PLAYER_KEY="User"; //used for find player inf in the db
    private FirebaseDatabase _database;
    public PlayerData LastPlayerData { get;private set;}
    public PlayerScore LastPlayerScore {get;private set;}
    public PlayerUpdatedEvent OnPlayerUpdated = new PlayerUpdatedEvent();
    public PlayerUpdatedEvent2 OnPlayerUpdated2 = new PlayerUpdatedEvent2();
    private DatabaseReference _ref;
    private DatabaseReference _ref2;

    //Prefab
    [Header("Other")]
    public PlayerStatus arc;
    public PlayerStatus fig;
    public PlayerStatus mag;
    public PlayerStatus lan;
    public GemsManager gemm;

    public void DB()//initialize the DB if needed
    {
        PLAYER_KEY = PlayerPrefs.GetString("ActualUser");
        _database = FirebaseDatabase.DefaultInstance;
        _ref = _database.GetReference("users").Child(PLAYER_KEY);
        _ref.ValueChanged += HandleValueChanged;
        _ref2 = _database.GetReference("score").Child(PLAYER_KEY);
        _ref2.ValueChanged += HandleValueChanged2;
    }
    /*public void ChangePLAYER_KEY(string a)//change the playerKey
    {
        PLAYER_KEY = a;
    }*/
    private void OnDestory()//destro player in case of deleting account
    {
        _ref.ValueChanged -= HandleValueChanged;
        _ref = null;
        _database = null;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs e)
    {
        var json = e.Snapshot.GetRawJsonValue();
        Debug.Log("json:" + json);
        if (!string.IsNullOrEmpty(json))
        {
            var playerData = JsonUtility.FromJson<PlayerData>(json);
            LastPlayerData = playerData;
            OnPlayerUpdated.Invoke(playerData);
            if (!PlayerPrefs.HasKey("FirstLogin"))
            {
                PlayerPrefs.SetInt("FirstLogin", 1);
                SetPrefab();
            }
            PlayerPrefs.SetInt("CurrentGems", LastPlayerData.Gemms);
            gemm.SetGems();
        }
    }
    private void SetPrefab()
    {
        Debug.Log(LastPlayerData.lvlA);
        arc.playerLevel = LastPlayerData.lvlA;
        Debug.Log(arc.playerLevel);
        arc.currentHp = LastPlayerData.HPA;
        arc.maxHp = LastPlayerData.HPA;
        arc.attack = LastPlayerData.atkA;

        fig.playerLevel = LastPlayerData.lvlF;
        fig.maxHp = LastPlayerData.HPF;
        fig.currentHp = LastPlayerData.HPF;
        fig.attack = LastPlayerData.atkF;

        mag.playerLevel = LastPlayerData.lvlM;
        mag.maxHp = LastPlayerData.HPM;
        mag.currentHp = LastPlayerData.HPM;
        mag.attack = LastPlayerData.atkM;

        lan.playerLevel = LastPlayerData.lvlL;
        lan.maxHp = LastPlayerData.HPA;
        lan.currentHp = LastPlayerData.HPL;
        lan.attack = LastPlayerData.atkL;
        gemm.curGems =LastPlayerData.Gemms;
        PlayerPrefs.SetInt("CurrentGems", gemm.curGems);
        PlayerPrefs.SetInt("FirstLogin", 0);
    }



    private void HandleValueChanged2(object sender, ValueChangedEventArgs e)
    {
        var json = e.Snapshot.GetRawJsonValue();
        Debug.Log("json:" + json);
        if (!string.IsNullOrEmpty(json))
        {
            var playerData = JsonUtility.FromJson<PlayerScore>(json);
            LastPlayerScore = playerData;
            OnPlayerUpdated2.Invoke(playerData);
        }
    }

    public void SavePlayer(PlayerData player,bool a)//save player data in DB
    {
        Debug.Log("in save player");
        if (!player.Equals(LastPlayerData) || a)
        {
            Debug.Log("saving player");
            Debug.Log(JsonUtility.ToJson(player));
            _database.RootReference.Child("users").Child(PLAYER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(player));
        }
    }
    public void SaveScore(PlayerScore player, bool a)//save player data in DB
    {
        Debug.Log(player.UserScore);
        Debug.Log(LastPlayerScore.UserScore);
        if ((player.UserScore > LastPlayerScore.UserScore) || a)
        {
            Debug.Log("saving player");
            Debug.Log(JsonUtility.ToJson(player));
            _database.RootReference.Child("score").Child(PLAYER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(player));
        }
    }

    public async Task<PlayerData?> LoadPlayer()//load player data in DB
    {
        var dataSnapshot = await _database.RootReference.Child("users").Child(PLAYER_KEY).GetValueAsync();
        if (!dataSnapshot.Exists)
        {
            return null;
        }
        
        return JsonUtility.FromJson<PlayerData>(dataSnapshot.GetRawJsonValue());
    }

    public async Task<PlayerData?> LoadPlayerScore()//load player data in DB
    {
        var dataSnapshot = await _database.RootReference.Child("score").Child(PLAYER_KEY).GetValueAsync();
        Debug.Log(LastPlayerScore.UserScore);
        if (!dataSnapshot.Exists)
        {
            return null;
        }

        return JsonUtility.FromJson<PlayerData>(dataSnapshot.GetRawJsonValue());
    }

    public async Task<bool> SaveExists()//check if the save exists
    {
        var dataSnapshot = await _database.RootReference.Child("users").Child(PLAYER_KEY).GetValueAsync();
        return dataSnapshot.Exists;
    }
    public void EraseSave()
    {
        _database.RootReference.Child("users").Child(PLAYER_KEY).RemoveValueAsync();
    }

    [System.Serializable]
    public class PlayerUpdatedEvent : UnityEvent<PlayerData> { }
    [System.Serializable]
    public class PlayerUpdatedEvent2 : UnityEvent<PlayerScore> { }

}
