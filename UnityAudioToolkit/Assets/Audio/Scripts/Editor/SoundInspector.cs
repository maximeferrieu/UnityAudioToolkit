﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;

namespace AudioToolkit
{
    [CustomEditor(typeof(Sound))]
    public class SoundInspector : Editor
    {
        Sound Target
        {
            get
            {
                return (Sound)target;
            }
        }

        public override void OnInspectorGUI()
        {
            Header();

            if (ShowProperties)
            {
                Settings();
            }
        }

        bool ShowProperties = false;

        bool ShowClips = false;

        void Header()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField(Target.Name);

            if (!ShowProperties)
            {
                if (GUILayout.Button("Settings", GUILayout.Width(75f)))
                {
                    ShowProperties = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide", GUILayout.Width(75f)))
                {
                    ShowProperties = false;
                }
            }
            

            GUILayout.EndHorizontal();
        }

        void Settings()
        {
            GUILayout.Space(5f);
            EditorGUILayout.LabelField("PARAMETERS", EditorStyles.boldLabel);

            Target.PlaybackMode = (SoundPlaybackMode)EditorGUILayout.EnumPopup("Playback Mode", Target.PlaybackMode);

            Target.AttenuationPrefab = (GameObject)EditorGUILayout.ObjectField("Attenuation Prefab", Target.AttenuationPrefab, typeof(GameObject), false);

            Target.Output = (AudioMixerGroup)EditorGUILayout.ObjectField("Output", Target.Output, typeof(AudioMixerGroup), false);

            Target.MaxDistance = EditorGUILayout.FloatField("Max Distance", Target.MaxDistance);
            if (Target.MaxDistance < 1f)
                Target.MaxDistance = 1f;

            Target.MaxDelay = EditorGUILayout.FloatField("Max Delay", Target.MaxDelay);
            if (Target.MaxDelay < Target.MinDelay)
                Target.MaxDelay = Target.MinDelay;

            Target.MinDelay = EditorGUILayout.FloatField("Min Delay", Target.MinDelay);
            if (Target.MinDelay < 0f)
                Target.MinDelay = 0f;
            if (Target.MinDelay > Target.MaxDelay)
                Target.MinDelay = Target.MaxDelay;

            Target.MaxVolume = EditorGUILayout.FloatField("Max Volume", Target.MaxVolume);
            if (Target.MaxVolume > 1f)
                Target.MaxVolume = 1f;
            if (Target.MaxVolume < Target.MinVolume)
                Target.MaxVolume = Target.MinVolume;

            Target.MinVolume = EditorGUILayout.FloatField("Min Volume", Target.MinVolume);
            if (Target.MinVolume < 0f)
                Target.MinVolume = 0f;
            if (Target.MinVolume > Target.MaxVolume)
                Target.MinVolume = Target.MaxVolume;

            Target.MaxPitch = EditorGUILayout.FloatField("Max Pitch", Target.MaxPitch);
            if (Target.MaxPitch > 3f)
                Target.MaxPitch = 3f;
            if (Target.MaxPitch < Target.MinPitch)
                Target.MaxPitch = Target.MinPitch;

            Target.MinPitch = EditorGUILayout.FloatField("Min pitch", Target.MinPitch);
            if (Target.MinPitch < 0.1f)
                Target.MinPitch = 0.1f;
            if (Target.MinPitch > Target.MaxPitch)
                Target.MinPitch = Target.MaxPitch;

            if (Target.PlaybackMode == SoundPlaybackMode.Loop)
            {
                LoopSettings();
            }

            if (Target.PlaybackMode == SoundPlaybackMode.Granular)
            {
                GranularSettings();
            }

            GUILayout.Space(10f);
            
            ClipsHeader();
        }

        void LoopSettings()
        {
            Target.RandomStartPosition = EditorGUILayout.Toggle("Random Start Position", Target.RandomStartPosition);
        }

        void GranularSettings()
        {
            GUILayout.Space(5f);
            EditorGUILayout.LabelField("-----Granular Parameters-----");

            Target.MaxSpawnTime = EditorGUILayout.FloatField("Max Spawn Time", Target.MaxSpawnTime);
            if (Target.MaxSpawnTime < Target.MinSpawnTime)
                Target.MaxSpawnTime = Target.MinSpawnTime;

            Target.MinSpawnTime = EditorGUILayout.FloatField("Min Spawn Time", Target.MinSpawnTime);
            if (Target.MinSpawnTime < 0f)
                Target.MinSpawnTime = 0f;
            if (Target.MinSpawnTime > Target.MaxSpawnTime)
                Target.MinSpawnTime = Target.MaxSpawnTime;

            Target.OverridePositionToListener = EditorGUILayout.Toggle("Spawn On Listener", Target.RandomStartPosition);

            Target.GranularSpawnPositionRandomness = EditorGUILayout.Slider("Position Randomness", Target.GranularSpawnPositionRandomness, 0f, 1f);
        }

        void ClipsHeader()
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("AUDIO CLIPS", EditorStyles.boldLabel);

            if (!ShowClips)
            {
                if (GUILayout.Button("Show", GUILayout.Width(75f)))
                {
                    ShowClips = true;                  
                }
            }
            else
            {
                if (GUILayout.Button("Hide", GUILayout.Width(75f)))
                {
                    ShowClips = false;
                }
            }
            GUILayout.EndHorizontal();

            if (ShowClips)
            {
                ClipsMasterButtons();
                ClipsList();
            }
            GUILayout.EndVertical();
        }

        void ClipsMasterButtons()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Empty"))
            {
                Target.Clips.Add(null);
            }

            if (GUILayout.Button("Add Selection"))
            {
                Object[] _clipSelection = Selection.objects;

                foreach (Object _clipObject in _clipSelection)
                {
                    if (_clipObject.GetType() == typeof(AudioClip))
                    {
                        Target.Clips.Add((AudioClip)_clipObject);
                    }
                }
            }

            if (GUILayout.Button("Clear"))
            {
                Target.Clips.Clear();
            }

            GUILayout.EndHorizontal();
        }

        void ClipsList()
        {           
            for (int i = 0; i < Target.Clips.Count; i++)
            {
                GUILayout.BeginHorizontal();

                Target.Clips[i] = (AudioClip)EditorGUILayout.ObjectField(Target.Clips[i], typeof(AudioClip), false);

                if (GUILayout.Button("Del", GUILayout.Width(50f)))
                {
                    Target.Clips.Remove(Target.Clips[i]);
                }

                GUILayout.EndHorizontal();
            }
        }
    }
}

