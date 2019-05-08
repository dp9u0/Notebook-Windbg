using System;

namespace CrashMe.Common {

    /// <summary>
    /// Crasher Base
    /// </summary>
    public abstract class CrasherBase : ICrasher {

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Crasher Name</param>
        /// <param name="command">Crasher Command</param>
        public CrasherBase(string name, string command) {
            _name = name;
            _command = command;
        }

        private readonly string _name;
        private readonly string _command;

        /// <summary>
        /// Crasher Name
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// Command
        /// </summary>
        public string Command => _command;

        /// <summary>
        /// Get Help Text
        /// </summary>
        public abstract string Help { get; }

        /// <summary>
        /// Run Crasher
        /// </summary>
        /// <param name="args">arguments</param>
        public void Run(RunArgs args = null) {
            RunCore(args);
        }

        /// <summary>
        /// Run Crasher Core
        /// </summary>
        /// <param name="args">arguments</param>
        protected abstract void RunCore(RunArgs args);
    }
}
