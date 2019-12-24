namespace Infra.SaveLoad
{
    public interface ISaveLoadDataManager
    {
        void SaveObject<T>(EnumSaveLoadData key, T value);
        T LoadObject<T>(EnumSaveLoadData key);
    }
}