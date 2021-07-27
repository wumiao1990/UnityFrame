
/// <summary>
/// AB实用函数集，主要是路径拼接
/// </summary>
public class ABPathUtilities
{
    public static string GetUIPath(string fileName)
    {
        return $"Assets/ABRes/PlayGround/UIPrefabs/{fileName}.prefab";
    }
    
    public static string GetScenePath(string fileName)
    {
        return $"Assets/ABRes/PlayGround/Scenes/{fileName}.unity";
    }
    
    public static string GetTexturePath(string fileName)
    {
        return $"Assets/ABRes/PlayGround/UI/{fileName}.png";
    }
    
    public static string GetEffectPath(string fileName)
    {
        return $"Assets/ABRes/PlayGround/Effect/{fileName}.prefab";
    }
}
