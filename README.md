# UnityBinarySaveLoadData
Basic Binary Save Load Data in Unity


Example:

Save

SaveObject(EnumSaveLoadData.GameData1, yourObject)

Load

var loadedObject = LoadObject<yourObjectType>(EnumSaveLoadData.GameData1)
