using FirebaseAdmin;

namespace HappyVacation.Services.Firebase
{
    public class FirebaseCloudMessaging
    {
        private static FirebaseApp? _instance;
        public static FirebaseApp Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FirebaseApp.Create();
                }
                return _instance;
            }
            private set { }
        }
        private FirebaseCloudMessaging() { }
    }
}
