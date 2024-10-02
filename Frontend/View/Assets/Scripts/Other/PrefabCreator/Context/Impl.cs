namespace PrefabCreator.Context
{
    public class PrefabCreatorFactory
    {
        public static PrefabCreatorApi CreateApi()
        {
            return new PrefabCreatorApiLogic();
        }
    } 
}