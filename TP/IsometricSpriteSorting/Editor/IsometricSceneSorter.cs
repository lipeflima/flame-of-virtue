using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class IsometricSceneSorter : EditorWindow
{
    [SerializeField]
    private int layerIndex = 0;

    [SerializeField]
    private bool sortOnPlay = false;

    [MenuItem("Window/Isometric Sprite Sorting", false, 10000)]
    static void ShowWindow()
    {
        GetWindow<IsometricSceneSorter>();
    }

    void OnEnable()
    {
        this.titleContent.text = "Isometric Sort";
        this.minSize = new Vector2(100, 60);
        this.maxSize = new Vector2(4000, 60);
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (sortOnPlay && state == PlayModeStateChange.ExitingEditMode)
        {
            SortSceneRenderers();
        }
    }

    void OnGUI()
    {
        using (new EditorGUILayout.VerticalScope())
        {
            if (SortingLayer.layers.Length > 0)
            {
                string[] sortingLayerNames = new string[SortingLayer.layers.Length];
                for (int i = 0; i < sortingLayerNames.Length; i++)
                {
                    sortingLayerNames[i] = SortingLayer.layers[i].name;
                }
                layerIndex = EditorGUILayout.Popup("Layer", layerIndex, sortingLayerNames);
            }

            if (GUILayout.Button("Sort Renderers"))
            {
                SortSceneRenderers();
            }

            sortOnPlay = GUILayout.Toggle(sortOnPlay, "Automatically Sort on Play");

            GUILayout.Space(5);
            if (GUILayout.Button("More info at: <color=blue><b>www.codeartist.mx</b></color>", GetLinkStyle()))
            {
                Application.OpenURL("https://codeartist.mx/tutorials/isometric-sorting/");
            }
        }
    }

    private void SortSceneRenderers()
    {
        if (layerIndex >= SortingLayer.layers.Length)
        {
            Debug.LogError("Invalid sorting layer selected.");
            return;
        }

        SortingLayer selectedLayer = SortingLayer.layers[layerIndex];
        int sortedCount = 0;

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            float y = go.transform.position.y;
            int sortingOrder = (int)(y * IsometricSorter.PrecisionValue);

            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            if (sr != null && sr.sortingLayerID == selectedLayer.id)
            {
                sr.sortingOrder = sortingOrder;
                EnsureIsometricSorter(go);
                sortedCount++;
            }

            TilemapRenderer tr = go.GetComponent<TilemapRenderer>();
            if (tr != null && tr.sortingLayerID == selectedLayer.id)
            {
                tr.sortingOrder = sortingOrder;
                EnsureIsometricSorter(go);
                sortedCount++;
            }
        }

        Debug.Log($"<color=green><b>{sortedCount}</b></color> objects sorted and updated in layer <b>{selectedLayer.name}</b>");
    }

    void EnsureIsometricSorter(GameObject go)
    {
        if (go.GetComponent<IsometricSorter>() == null)
        {
            go.AddComponent<IsometricSorter>();
        }
    }

    private GUIStyle GetLinkStyle()
    {
        GUIStyle s = new GUIStyle(GUI.skin.label);
        s.richText = true;
        s.alignment = TextAnchor.MiddleCenter;
        return s;
    }
}
