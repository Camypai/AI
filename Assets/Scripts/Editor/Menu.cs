using UnityEditor;

namespace Ig.Editor
{
    public class Menu
    {
        [MenuItem("IG/Расставить аптечки")]
        private static void MenuOption()
        {
            EditorWindow.GetWindow(typeof(CreateKitWindow), false, "IG");
        }
    }
}