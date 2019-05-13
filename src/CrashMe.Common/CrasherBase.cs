namespace CrashMe.Common
{

    /// <summary>
    /// Crasher Base
    /// </summary>
    public abstract class CrasherBase : ICrasher
    {

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Crasher Name</param>
        /// <param name="command">Crasher Command</param>
        protected CrasherBase(string name, string command)
        {
            Name = name;
            Command = command;
        }

        /// <summary>
        /// Crasher Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Command
        /// </summary>
        public string Command { get; }

        /// <summary>
        /// Get Help Text
        /// </summary>
        public abstract string Help { get; }

        /// <summary>
        /// Run Crasher
        /// </summary>
        /// <param name="args">arguments</param>
        public void Run(RunArgs args = null)
        {
            RunCore(args);
        }

        /// <summary>
        /// Run Crasher Core
        /// </summary>
        /// <param name="args">arguments</param>
        protected abstract void RunCore(RunArgs args);

    }

}