using Managers;
using UnityEngine;

namespace Global
{
    public class ManagerLocator : MonoBehaviour
    {
        //[SerializeField] private 
        private IManagerLocator<BaseManager> locator;

        private void Awake()
        {
            locator = new ManagerLocator<BaseManager>();

            //locator.Register(analytics);
            //then
            //var analytics = locator.Get<AnalyticsService>();
            //Debug.Log($"AnalyticsService version: {analytics.Version}");
        }

        public BaseManager Get<T>() where T : BaseManager
        {
            return locator.Get<T>();
        }

        public void Register<T>(T manager) where T : BaseManager
        {
            locator.Register(manager);
        }
    }
}