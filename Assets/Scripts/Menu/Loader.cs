using UnityEngine.SceneManagement;

// NEW
public class Loader
{
    public static void Load(EScenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}
