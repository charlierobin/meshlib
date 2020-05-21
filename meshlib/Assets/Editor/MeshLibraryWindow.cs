using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class MeshLibraryWindow : EditorWindow
{
    private List<string> sources;
    private List<string> files;
    private List<string> paths;

    private List<string> acceptable;

    private bool needsRefresh = true;

    private Vector2 scrollPos;

    public string test;

    [MenuItem("Assets/From Mesh Library… #%M", false, 20)]

    static void Init()
    {
        MeshLibraryWindow window = (MeshLibraryWindow)EditorWindow.GetWindow(typeof(MeshLibraryWindow));
        window.titleContent = new GUIContent("Mesh Library", "Import assets from your folder of commonly needed meshes");
        window.Show();
    }

    private void refresh()
    {
        this.files = new List<string>();
        this.paths = new List<string>();

        try
        {
            foreach (string folder in this.sources)
            {
                var files = Directory.EnumerateFiles(folder);

                foreach (string file in files)
                {
                    if (!this.acceptable.Contains(Path.GetExtension(file))) continue;

                    string fileName = file.Substring(folder.Length + 1);

                    if (fileName.StartsWith(".")) continue;

                    this.files.Add(fileName);
                    this.paths.Add(folder);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void OnGUI()
    {
        if (this.needsRefresh)
        {
            this.needsRefresh = false;
            this.sources = new List<string>();

            this.sources.Add("/Volumes/external/work/_unity object lib");
            this.sources.Add("/Users/charlie/Documents/_primitves");

            this.acceptable = new List<string>();

            this.acceptable.Add(".obj");
            this.acceptable.Add(".fbx");

            this.refresh();
        }

        this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos);

        int index = 0;

        foreach (string file in this.files)
        {
            if (GUILayout.Button(file))
            {
                var relativePath = "Assets/" + file;

                if (File.Exists(relativePath)) continue;

                string sourcePath = this.paths[index] + "/" + file;

                var assetsPath = Application.dataPath + "/" + file;

                File.Copy(sourcePath, assetsPath);

                AssetDatabase.ImportAsset(relativePath);
            }

            index++;
        }

        EditorGUILayout.EndScrollView();
    }
}
