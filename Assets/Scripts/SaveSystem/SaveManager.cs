using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
#if UNITY_EDITOR
using UnityEditor.SearchService;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour 
{
    //声明全局单例
    public static SaveManager instance;

    //获取怪物，角色管理,刷怪，经验
    public MousterManager mous;
    public CharacterBase player;
    public ExpManager exp;
    public NormalShoot shoot;
    public UIinfo ui;
    //存储路径
    public string savePath;
    public string saveFolder;
    //读档标记
    public bool dudang;
    

    public void GetRef()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<CharacterBase>();
            shoot = playerObj.GetComponent<NormalShoot>();
        }
        GameObject mousObj = GameObject.FindWithTag("MousterManager");
        if (mousObj != null)
        {
            mous = mousObj.GetComponent<MousterManager>();
        }
        GameObject expObj = GameObject.FindWithTag("ExpController");
        if (expObj != null)
        {
            exp = expObj.GetComponent<ExpManager>();
        }
        GameObject uiObj = GameObject.FindWithTag("UIData");
        if (uiObj != null)
        {
            ui = uiObj.GetComponent<UIinfo>();
        }
    }

    //新建存档文件夹
    private void Awake()
    {
        //创建全局单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        //创建存档文件夹
        string root = Application.dataPath;
        root = root.Replace("/Assets", "");
        saveFolder = Path.Combine(root, "SaveData");
        savePath = Path.Combine(saveFolder, "save.json");
        if (!Directory.Exists(saveFolder))
        { 
            Directory.CreateDirectory(saveFolder);
        }
        
    }


    //存档
    public void SaveGame()
    { 
        GameSaveData gameData = new GameSaveData();
        gameData.playerData = player.ExportData();
        gameData.MousterData = mous.CollectData();
        gameData.MousM = mous.SaveMous();
        gameData.ExpSaveData = exp.SaveExp();
        gameData.ShootData = shoot.SaveShoot();
        gameData.UISaveData = ui.SaveData();
        string josn = JsonUtility.ToJson(gameData,true);
        File.WriteAllText(savePath, josn);
    }
    //存档检测
    public bool Saved()
    { 
        return File.Exists(savePath);
    }

    //读档
    public void LoadGame()
    {
        if (Saved())
        {
            GameSaveData saveData = JsonUtility.FromJson<GameSaveData>(File.ReadAllText(savePath));
            player.ImportData(saveData.playerData);
            mous.ImportData(saveData.MousterData);
            mous.LoadMous(saveData.MousM);
            exp.LoadExp(saveData.ExpSaveData);
            shoot.LoadShoot(saveData.ShootData);
            ui.LoadData(saveData.UISaveData);
            File.Delete(savePath);
        }
    }
    //读档开关
    public void StartLoad()
    {
        dudang = true;
        SceneManager.LoadScene("MainScene");
    }
}
