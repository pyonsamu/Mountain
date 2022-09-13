using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public bool loadActive = true;

    //プレイヤー情報
    [SerializeField]
    private Transform player;

    //時間情報
    [SerializeField]
    private Timer timer;

    //ワールド情報
    [SerializeField]
    private Explode destructible1;
    [SerializeField]
    private Explode destructible2;

    private float counter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (!loadActive)
        {
            return;
        }
        //Debug.Log("load");
        if (PlayerPrefs.GetFloat("Map2PlayerPosX") == 0 && PlayerPrefs.GetFloat("Map2PlayerPosY") == 0)
        {
            return;
        }
        player.position = new Vector3(PlayerPrefs.GetFloat("Map2PlayerPosX"), PlayerPrefs.GetFloat("Map2PlayerPosY"),0.0f);
        player.eulerAngles = new Vector3(0.0f,0.0f, PlayerPrefs.GetFloat("Map2PlayerRotZ"));

        player.GetComponent<Rigidbody>().velocity = new Vector3(PlayerPrefs.GetFloat("Map2PlayerVelX"), PlayerPrefs.GetFloat("Map2PlayerVelY"), 0.0f);
        player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, PlayerPrefs.GetFloat("Map2PlayerAngvelZ"));

        timer.timer = PlayerPrefs.GetFloat("Map2Time");
        timer.isActive = PlayerPrefs.GetInt("Map2IsTimerActive") ==1;

        destructible1.destroy = PlayerPrefs.GetInt("Map2Destructible1") == 1;
        destructible2.destroy = PlayerPrefs.GetInt("Map2Destructible2") == 1;

    }

    private void Update()
    {
        counter += Time.deltaTime;
        if(counter > 1)
        {
            counter = 0;
            SaveData();
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat("Map2PlayerPosX", player.position.x);
        PlayerPrefs.SetFloat("Map2PlayerPosY", player.position.y);
        PlayerPrefs.SetFloat("Map2PlayerRotZ", player.eulerAngles.z);

        PlayerPrefs.SetFloat("Map2PlayerVelX", player.GetComponent<Rigidbody>().velocity.x);
        PlayerPrefs.SetFloat("Map2PlayerVelY", player.GetComponent<Rigidbody>().velocity.y);
        PlayerPrefs.SetFloat("Map2PlayerAngvelZ", player.GetComponent<Rigidbody>().angularVelocity.z);

        PlayerPrefs.SetFloat("Map2Time", timer.timer);
        PlayerPrefs.SetInt("Map2IsTimerActive", timer.isActive ? 1 : 0);

        PlayerPrefs.SetInt("Map2Destructible1", destructible1.destroy ? 1 : 0);
        PlayerPrefs.SetInt("Map2Destructible2", destructible2.destroy ? 1 : 0);
    }

    private void OnApplicationQuit()
    {
        //Debug.Log("save");
        
    }
}
