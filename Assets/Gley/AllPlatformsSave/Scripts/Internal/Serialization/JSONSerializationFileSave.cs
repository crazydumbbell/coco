﻿namespace Gley.AllPlatformsSave.Internal
{
#if JSONSerializationFileSave
    using UnityEngine.Events;
    using System;

#if GLEY_NEWTONSOFT_JSON
    using Newtonsoft.Json;
#else
    using UnityEngine;
#endif

#if UNITY_WINRT && !UNITY_EDITOR
using UnityEngine.Windows;
#else
    using System.IO;

#endif


    public class JSONSerializationFileSave : ISaveClass
    {
        string saveStatus;
        string loadStatus;
        string fileCreatedMessage = "File does not exists";
#if UNITY_EDITOR
       
#if GLEY_NEWTONSOFT_JSON
        private Formatting formatting = Formatting.Indented;
#else
        bool prettyPrint = true;
#endif
#else      
#if GLEY_NEWTONSOFT_JSON
        Formatting formatting = Formatting.None;
#else
        bool prettyPrint = false;
#endif
#endif

        public void SaveString<T>(T dataToSave, UnityAction<SaveResult, string> CompleteMethod, bool encrypted) where T : class, new()
        {
            saveStatus = String.Empty;
            byte[] bytes = null;
#if GLEY_NEWTONSOFT_JSON
            string formattedString = JsonConvert.SerializeObject(dataToSave, formatting);
#else
            string formattedString = JsonUtility.ToJson(dataToSave, prettyPrint);
#endif
            if (encrypted)
            {
                try
                {
                    bytes = BinarySerializationUtility.GetBytes(formattedString);
                }
                catch (Exception e)
                {
                    saveStatus += "Serialization Error: " + e.Message;
                }

                try
                {
                    BinarySerializationUtility.EncryptData(ref bytes);
                }
                catch (Exception e)
                {
                    saveStatus += "Encryption Error: " + e.Message;
                }
            }
            if (saveStatus == String.Empty)
            {
                formattedString = ConvertToString(bytes);
                CompleteMethod(SaveResult.Success, formattedString);
            }
            else
            {
                CompleteMethod(SaveResult.Error, saveStatus);
            }
        }

        public void Save<T>(T dataToSave, string path, UnityAction<SaveResult, string> CompleteMethod, bool encrypted) where T : class, new()
        {
            byte[] bytes = null;
            saveStatus = String.Empty;
            try
            {
#if GLEY_NEWTONSOFT_JSON
                bytes = BinarySerializationUtility.GetBytes(JsonConvert.SerializeObject(dataToSave, formatting));
#else
                bytes = BinarySerializationUtility.GetBytes(JsonUtility.ToJson(dataToSave, prettyPrint));
#endif
            }
            catch (Exception e)
            {
                saveStatus += "Serialization Error: " + e.Message;
            }

            if (encrypted)
            {
                try
                {
                    BinarySerializationUtility.EncryptData(ref bytes);
                }
                catch (Exception e)
                {
                    saveStatus += "Encryption Error: " + e.Message;
                }
            }

            try
            {
                File.WriteAllBytes(path, bytes);
            }
            catch (Exception e)
            {
                saveStatus += "File Write Error: " + e.Message;
            }
            if (CompleteMethod != null)
            {
                if (saveStatus == String.Empty)
                {
                    CompleteMethod(SaveResult.Success, saveStatus);
                }
                else
                {
                    CompleteMethod(SaveResult.Error, saveStatus);
                }
            }
        }
        public void LoadString<T>(string stringToLoad, UnityAction<T, SaveResult, string> LoadCompleteMethod, bool encrypted) where T : class, new()
        {
            T deserializedData = new T();
            loadStatus = String.Empty;
            if (encrypted)
            {
                byte[] bytes = ConvertToBytes(stringToLoad);
                BinarySerializationUtility.DecryptData(ref bytes);
#if GLEY_NEWTONSOFT_JSON
                deserializedData = JsonConvert.DeserializeObject<T>(BinarySerializationUtility.GetString(bytes));
#else
                deserializedData = JsonUtility.FromJson<T>(BinarySerializationUtility.GetString(bytes));
#endif
            }
            else
            {
                try
                {
#if GLEY_NEWTONSOFT_JSON
                    deserializedData = JsonConvert.DeserializeObject<T>(stringToLoad);
#else
                    deserializedData = JsonUtility.FromJson<T>(stringToLoad);
#endif
                }
                catch (Exception e)
                {
                    loadStatus += "Deserialization Error: " + e.Message;
                }
            }


            if (loadStatus == String.Empty)
            {
                LoadCompleteMethod(deserializedData, SaveResult.Success, loadStatus);
                return;
            }
            LoadCompleteMethod(null, SaveResult.Error, loadStatus);
        }

        public void Load<T>(string path, UnityAction<T, SaveResult, string> LoadCompleteMethod, bool encrypted) where T : class, new()
        {
            loadStatus = String.Empty;
            byte[] bytes = ReadFromFile<T>(path);
            if (bytes != null)
            {
                if (encrypted)
                {
                    try
                    {
                        BinarySerializationUtility.DecryptData(ref bytes);
                    }
                    catch (Exception e)
                    {
                        loadStatus += "Decryption Error: " + e.Message;
                    }
                }
                T deserializedData = new T();
                try
                {
#if GLEY_NEWTONSOFT_JSON
                    deserializedData = JsonConvert.DeserializeObject<T>(BinarySerializationUtility.GetString(bytes));
#else
                    deserializedData = JsonUtility.FromJson<T>(BinarySerializationUtility.GetString(bytes));
#endif
                }
                catch (Exception e)
                {
                    loadStatus += "Deserialization Error: " + e.Message;
                }

                if (loadStatus == String.Empty)
                {
                    LoadCompleteMethod(deserializedData, SaveResult.Success, loadStatus);
                    return;
                }
            }
            LoadCompleteMethod(null, SaveResult.Error, loadStatus);
        }

        public void ClearFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }


        public void ClearAllData(string path)
        {
            if (Directory.Exists(path))
            {
#if !UNITY_WINRT
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
#else
            Directory.Delete(path);
            Directory.CreateDirectory(path);          
#endif
            }
        }


        byte[] ReadFromFile<T>(string path) where T : class, new()
        {
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            loadStatus = fileCreatedMessage + " " + path;
            return null;
        }


        string ConvertToString(byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes);
        }


        byte[] ConvertToBytes(string content)
        {
            return System.Convert.FromBase64String(content);
        }
    }
#endif
}
