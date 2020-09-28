using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public Actor main_actor;
    public BackpackUI backPackUI;
    public UITextFactory uITextFactory;
    private void Start() {
        instance = this;
        backPackUI.SetUp(main_actor);
    }
}