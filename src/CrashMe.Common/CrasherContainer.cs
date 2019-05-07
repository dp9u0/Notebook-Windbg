using System.Reflection;

namespace CrashMe.Common {

    /// <summary>
    ///  Crasher Container
    /// </summary>
    public class CrasherContainer {

        /// <summary>
        /// Static Single Instance Pattern
        /// </summary>
        static CrasherContainer() {
            Instance = new CrasherContainer();
        }

        /// <summary>
        /// Private Make Sure None Can Create A Instance
        /// </summary>
        private CrasherContainer() {}

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static CrasherContainer Instance {
            get;
            private set;
        }

        /// <summary>
        /// Load Default Crasher
        /// </summary>
        public void LoadDefault() {
            LoadFromAssembly(Assembly.GetExecutingAssembly());
        }


        /// <summary>
        /// Load Crasher From Assembly
        /// </summary>
        /// <param name="assembly"></param>
        public void LoadFromAssembly(Assembly assembly) {

        }

        /// <summary>
        /// Add Crasher
        /// </summary>
        /// <param name="crasher">crasher</param>
        public void Add(ICrasher crasher) {

        }

        /// <summary>
        /// Pick A Craher by input and Run it
        /// </summary>
        /// <param name="input">input command line and arguments</param>
        public void Run(string input) {
        }
    }
}
